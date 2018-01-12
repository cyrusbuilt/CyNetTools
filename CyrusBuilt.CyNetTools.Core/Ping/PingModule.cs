using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace CyrusBuilt.CyNetTools.Core.Ping
{
    /// <summary>
    /// Provides a threaded wrapper for the Microsoft Ping utility.
    /// </summary>
    public class PingModule : ExternalToolBase
    {
        #region Parameter Constants
        /// <summary>
        /// The maximum send buffer size (65527 bytes).
        /// </summary>
        public const Int32 MAX_BUFFER_SIZE = 65527;

        /// <summary>
        /// The maximum number of hops to record timestamp for (4).
        /// </summary>
        public const Int32 MAX_TIMESTAMP_HOPS = 4;

        /// <summary>
        /// The maximum number of hops to record route for (9).
        /// </summary>
        public const Int32 MAX_ROUTE_HOPS = 9;

        /// <summary>
        /// The maximum number of hosts to source list from (9).
        /// </summary>
        public const Int32 MAX_SRCLIST_HOSTS = 9;

        /// <summary>
        /// The default echo request count (4).
        /// </summary>
        public const Int32 DEFAULT_REQ_COUNT = 4;
        #endregion

        #region Fields
        private Process _pingProc = null;
        private ProcessStartInfo _pingProcStartInfo = null;
        private Thread _pingReader = null;
        private Boolean _resolveNames = true;
        private Boolean _noFrag = false;
        private Boolean _continuous = false;
        private Boolean _testRevRoute = false;
        private Int32 _count = DEFAULT_REQ_COUNT;
        private Int32 _buffSize = 0;
        private Int32 _ttl = 0;
        private Int32 _hopCount = 0;
        private Int32 _timeStampCount = 0;
        private Int32 _timeout = 0;
        private String _hostList = String.Empty;
        private String _targetHost = String.Empty;
        private IPVersion _ipVer = IPVersion.IPv4;
        private RouteMode _sourceRouteMode = RouteMode.None;
        private IPAddress _srcAddress = IPAddress.None;
        private FileInfo _exec = null;
        private Regex _reReply = null;
        private Regex _reFailed = null;
        private static readonly Object _flagLock = new Object();
        #endregion

        #region Events
        /// <summary>
        /// Occurs when progress is made.
        /// </summary>
        public event PingProgressEventHandler Progress;
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.Ping.PingModule</b>
        /// class. This is the default constructor.
        /// </summary>
        public PingModule()
            : base() {
                var execPath = Path.Combine(Environment.SystemDirectory, "ping.exe");
                this._exec = new FileInfo(execPath);
                this._reReply = new Regex("\\AReply\\s",
                                            RegexOptions.IgnoreCase |
                                            RegexOptions.CultureInvariant |
                                            RegexOptions.IgnorePatternWhitespace |
                                            RegexOptions.Compiled);
                this._reFailed = new Regex("\\ARequest timed\\s",
                                            RegexOptions.IgnoreCase |
                                            RegexOptions.CultureInvariant |
                                            RegexOptions.Compiled);
        }

        /// <summary>
        /// Releases all resources used by this component.
        /// </summary>
        /// <param name="disposing">
        /// Set true if disposing all managed resources.
        /// </param>
        protected void Dispose(Boolean disposing) {
            if (disposing) {
                if (this._pingProc != null) {
                    try {
                        this._pingProc.Kill();
                    }
                    catch (InvalidOperationException) {
                        // This exception will be throw by Kill() if
                        // the process has already been terminated or if there
                        // is no process currently associated with the object.
                        // There is really nothing to do with this exception,
                        // since it is expected to occur, so we'll just ignore it
                        // and dispose the objects in the finally block as normal.
                    }
                    catch (Win32Exception) {
                        // Do NOT throw exceptions from dispose.
                    }
                    finally {
                        this._pingProc.Close();
                        this._pingProc.Dispose();
                    }
                }
            }

            if (this._pingReader != null) {
                base.Cancel();
                Thread.Sleep(50);
                if (this._pingReader.IsAlive) {
                    try {
                        this._pingReader.Abort();
                    }
                    catch (ThreadAbortException) {
                        Thread.ResetAbort();
                    }
                }
                this._pingReader = null;
            }

            this._pingProcStartInfo = null;
            base._isRunning = false;
            this._exec = null;
            this._reFailed = null;
            this._reReply = null;
            base.Dispose();
        }

        /// <summary>
        /// Releases all resources used by this component.
        /// </summary>
        public override void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~PingModule() {
            this.Dispose(false);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets whether or not to resolve addresses to hostnames
        /// (default is true).
        /// </summary>
        public Boolean ResolveHostNames {
            get { return this._resolveNames; }
            set { this._resolveNames = value; }
        }

        /// <summary>
        /// Gets or sets whether or not to set the fragment the no fragmentation
        /// flag in the packet (IPv4 only). Default is false.
        /// </summary>
        public Boolean DoNotFragment {
            get { return this._noFrag; }
            set { this._noFrag = value; }
        }

        /// <summary>
        /// Gets or sets whether or not to ping continuously until <see cref="PingModule.Cancel()"/>
        /// is called. Default is false. If enabled, progress will always be
        /// reported as 100%.
        /// </summary>
        public Boolean ContinuousPing {
            get { return this._continuous; }
            set { this._continuous = value; }
        }

        /// <summary>
        /// Gets or sets whether or not to use the routing header to test the
        /// reverse route also (IPv6) only. Default is false.
        /// </summary>
        public Boolean TestReversRoute {
            get { return this._testRevRoute; }
            set { this._testRevRoute = value; }
        }

        /// <summary>
        /// Gets or sets the number of echo requests to send. Default is 4.
        /// Setting this value to less than or equal to zero will result in
        /// the default count value being used instead.
        /// </summary>
        public Int32 RequestCount {
            get { return this._count; }
            set {
                if (value <= 0) {
                    value = DEFAULT_REQ_COUNT;
                }
                this._count = value;
            }
        }

        /// <summary>
        /// Gets or sets the send buffer size. Cannot be greater than <see cref="MAX_BUFFER_SIZE"/>.
        /// Setting this value to less than or equal zero will result in the
        /// internal default size being used. Default is zero.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Buffer size cannot be greater than <see cref="MAX_BUFFER_SIZE"/>.
        /// </exception>
        public Int32 BufferSize {
            get { return this._buffSize; }
            set {
                if (value > MAX_BUFFER_SIZE) {
                    throw new ArgumentException("Buffer size cannot be greater than " + MAX_BUFFER_SIZE.ToString() + ".",
                                                "PingModule.BufferSize");
                }

                if (value < 0) {
                    value = 0;
                }
                this._buffSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the time-to-live in packet. If this value is set to
        /// less than or equal to zero, then the internal default will be used
        /// instead. Default is zero.
        /// </summary>
        public Int32 TimeToLive {
            get { return this._ttl; }
            set {
                if (value < 0) {
                    value = 0;
                }
                this._ttl = value;
            }
        }

        /// <summary>
        /// Gets or sets the record hop count. Records the route for the specified
        /// number of hops (IPv4 only). If set to less than or equal to zero,
        /// then the internal default value will be used. Cannot be greater than
        /// <see cref="MAX_ROUTE_HOPS"/>. Default is zero.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Value cannot be greater than <see cref="MAX_ROUTE_HOPS"/>.
        /// </exception>
        public Int32 RecordHopCount {
            get { return this._hopCount; }
            set {
                if (value > MAX_ROUTE_HOPS) {
                    throw new ArgumentException("Record hop count cannot be greater than " + MAX_ROUTE_HOPS.ToString() + ".",
                                                "PingModule.RecordHopCount");
                }

                if (value < 0) {
                    value = 0;
                }
                this._hopCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the hop count to record the timestamp for (IPv4) only.
        /// If this value is set to less than or equal to zero, then this option
        /// will be disabled (default). Cannot be greater than <see cref="MAX_TIMESTAMP_HOPS"/>.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Value cannot be greater than <see cref="MAX_TIMESTAMP_HOPS"/>.
        /// </exception>
        public Int32 TimestampCount {
            get { return this._timeStampCount; }
            set {
                if (value > MAX_TIMESTAMP_HOPS) {
                    throw new ArgumentException("Timestamp hop count cannot be greater than " + MAX_TIMESTAMP_HOPS.ToString() + ".",
                                                "PingModule.TimestampCount");
                }

                if (value < 0) {
                    value = 0;
                }
                this._timeStampCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the timeout in milliseconds to wait for each reply. If
        /// set to less than or equal to zero (default), then the internal
        /// default value will be used instead.
        /// </summary>
        public Int32 Timeout {
            get { return this._timeout; }
            set {
                if (value < 0) {
                    value = 0;
                }
                this._timeout = value;
            }
        }

        /// <summary>
        /// Gets or sets the host-list to use with <see cref="SourceRouteMode"/>
        /// (IPv4 only). Default is an empty string (disabled).
        /// </summary>
        public String SourceRouteList {
            get { return this._hostList; }
            set { this._hostList = value; }
        }

        /// <summary>
        /// Gets or sets the source route mode. This is used with <see cref="PingModule.SourceRouteList"/>
        /// to either use a strict or loose route along the specified host-list
        /// (IPv4 only). Default is <see cref="RouteMode.None"/> (disabled).
        /// </summary>
        public RouteMode SourceRouteMode {
            get { return this._sourceRouteMode; }
            set { this._sourceRouteMode = value; }
        }

        /// <summary>
        /// Gets or sets the IP protocol version to use (default is <see cref="IPVersion.IPv4"/>).
        /// </summary>
        public IPVersion IPProtocolVersion {
            get { return this._ipVer; }
            set { this._ipVer = value; }
        }

        /// <summary>
        /// Gets or sets the source address to use. This can be used to spoof
        /// the sender address. If set null (default), then the IP address of
        /// the default active network interface will be used.
        /// </summary>
        public IPAddress SourceAddress {
            get { return this._srcAddress; }
            set { this._srcAddress = value; }
        }

        /// <summary>
        /// Gets a <see cref="PingErrorCodes"/> enum member corresponding to
        /// the process exit code returned by <see cref="ExternalToolBase.ExitCode"/>.
        /// This value will always be <see cref="PingErrorCodes.NoError"/>
        /// until the process is terminated.
        /// </summary>
        public PingErrorCodes ExitStatus {
            get { return (PingErrorCodes)base._exitCode; }
        }

        /// <summary>
        /// Gets or sets the target host to ping.
        /// </summary>
        public String TargetHost {
            get { return this._targetHost; }
            set { this._targetHost = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Raises the <see cref="Progress"/> event.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        protected void OnProgress(PingProgressEventArgs e) {
            if (this.Progress != null) {
                this.Progress(this, e);
            }
        }

        /// <summary>
        /// Performs any neccessary preflight work prior to ping execution.
        /// This method is intended to validate parameters and input and throw
        /// the appropriate exception if any invalid parameter values are
        /// detected or if the instance instance is in an invalid state.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Illegal delimiters in HostList - or - too many hosts in HostList
        /// - or - No target specified.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The ping executable could not be found.
        /// </exception>
        protected override void Preflight() {
            var args = String.Empty;
            base._exitCode = 0;
            base._wasCancelled = false;
            base._isRunning = false;

            // **** Perform sanity checks ****

            // Name resolution.
            if (this._resolveNames) {
                args += " -a";
            }

            // Echo count.
            if (this._count <= 0) {
                this._count = DEFAULT_REQ_COUNT;
            }

            if ((this._count > 0) && (!this._continuous)) {
                args += " -n " + this._count.ToString();
            }
            else {
                args += " -t";
            }

            // Buffer size.
            if ((this._buffSize > 0) && (this._buffSize <= MAX_BUFFER_SIZE)) {
                args += " -l " + this._buffSize.ToString();
            }

            // No packet fragmentation.
            if ((this._ipVer == IPVersion.IPv4) && (this._noFrag)) {
                args += " -f";
            }

            // TTL.
            if (this._ttl > 0) {
                args += " -i " + this._ttl.ToString();
            }

            // Record route hop count.
            if ((this._ipVer == IPVersion.IPv4) && (this._hopCount > 0) && (this._hopCount <= MAX_ROUTE_HOPS)) {
                args += " -r " + this._hopCount.ToString();
            }

            // Timestamp hop count.
            if ((this._ipVer == IPVersion.IPv4) && 
                (this._timeStampCount > 0) &&
                (this._timeStampCount <= MAX_TIMESTAMP_HOPS)) {
                    args += " -s " + this._timeStampCount.ToString();
            }

            // Host list (strict or loose).
            if ((this._ipVer == IPVersion.IPv4) && (!String.IsNullOrEmpty(this._hostList))) {
                if ((this._hostList.Contains(",")) ||
                    (this._hostList.Contains("\\")) ||
                    (this._hostList.Contains("/"))) {
                        throw new InvalidOperationException("PingModule.HostList can only be delimited by spaces.");
                }

                String[] arrHosts = this._hostList.Split(' ');
                if ((arrHosts.Length > 0) && (arrHosts.Length <= MAX_SRCLIST_HOSTS)) {
                    // String is valid. Cleanly reconstruct it.
                    this._hostList = String.Empty;
                    foreach (var host in arrHosts) {
                        this._hostList += host + " ";
                    }

                    if (this._hostList.EndsWith(" ")) {
                        this._hostList = this._hostList.Substring(0, this._hostList.Length - 1);
                    }
                }
                else {
                    throw new InvalidOperationException("PingModule.HostList cannont contain more than " +
                                                        MAX_SRCLIST_HOSTS.ToString());
                }

                Array.Clear(arrHosts, 0, arrHosts.Length);
                if (this._sourceRouteMode == RouteMode.Loose) {
                    args += " -j " + (Char)34 + this._hostList + (Char)34;
                }
                else if (this._sourceRouteMode == RouteMode.Strict) {
                    args += " -k " + (Char)34 + this._hostList + (Char)34;
                }
            }

            // Echo timeout.
            if (this._timeout > 0) {
                args += " -w " + this._timeout.ToString();
            }

            // Validate exec path.
            this._pingProcStartInfo = new ProcessStartInfo();
            if (this._exec.Exists) {
                this._pingProcStartInfo.FileName = this._exec.FullName;
                this._pingProcStartInfo.WorkingDirectory = this._exec.DirectoryName;
            }
            else {
                throw new FileNotFoundException("Ping executable not found.", this._exec.FullName);
            }

            // Combine arguments with target if specified. BETTER BE!
            if (String.IsNullOrEmpty(this._targetHost)) {
                throw new InvalidOperationException("Target host not specified.");
            }
            else {
                args += " " + this._targetHost;
            }

            // Destroy previous process object (if present).
            if (this._pingProc != null) {
                this._pingProc.Close();
                this._pingProc.Dispose();
            }

            // Setup remaining start parameters.
            this._pingProcStartInfo.UseShellExecute = false;
            this._pingProcStartInfo.RedirectStandardError = false;
            this._pingProcStartInfo.RedirectStandardOutput = true;
            this._pingProcStartInfo.CreateNoWindow = true;
            this._pingProcStartInfo.Arguments = args;

            // Setup the process.
            this._pingProc = new Process();
            this._pingProc.StartInfo = this._pingProcStartInfo;
        }

        /// <summary>
        /// Monitors the process and reads it's output. This method should only
        /// execute in the reader thread.
        /// </summary>
        protected override void ProcessReader() {
            ProcessOutputEventArgs outArgs = null;
            PingProgressEventArgs progress = null;
            var output = String.Empty;
            var errMsg = String.Empty;
            var i = 1;
            var count = this._count;
            var succeeded = 0;
            var failed = 0;
            Match mReply = null;
            Match mFailed = null;

            try {
                // Start the process and raise the running event.
                this._pingProc.Start();
                base.OnProcessStarted(new ProcessStartedEventArgs(this._pingProc.Id));

                // Read the output until the process terminates.
                output = this._pingProc.StandardOutput.ReadLine();
                while ((output != null) && (!base.WasCancelled)) {
                    // Notify output listeners.
                    outArgs = new ProcessOutputEventArgs(output);
                    base.OnOutputReceived(outArgs);

                    // See if the output message is a ping reply.
                    mReply = this._reReply.Match(output);
                    mFailed = this._reFailed.Match(output);
                    if ((mReply.Success) || (mFailed.Success)) {
                        // If we're pinging continuously, the request count and
                        // current request are always equal.
                        if (this._continuous) {
                            count = i;
                        }

                        // Compute responses received and lost.
                        if (mReply.Success) {
                            succeeded++;
                        }

                        if ((mFailed.Success) || (output.Contains("unreachable"))) {
                            failed++;
                        }

                        // Raise progress event.
                        progress = new PingProgressEventArgs(i, succeeded, failed, count);
                        this.OnProgress(progress);
                        i++;
                    }

                    // This loop is blocking, which is why we are using a thread.
                    output = this._pingProc.StandardOutput.ReadLine();
                }

                // Wait for the process to finish up, then get the exit code.
                this._pingProc.WaitForExit();
                base._exitCode = this._pingProc.ExitCode;

                // If the progress hits 100% or the process terminated normally,
                // the notify listeners that we're done. Otherwise, notify output
                // and cancellation listeners.
                if ((base._exitCode == 0) || ((progress != null) && (progress.PercentComplete == 100))) {
                    base.OnProcessFinished(new ProccessDoneEventArgs(base._exitCode));
                }
                else {
                    errMsg = "A ping error occured: " + PingUtils.GetPingErrorMessage(this.ExitStatus) +
                             " Error code: " + base._exitCode.ToString();
                    base.OnOutputReceived(new ProcessOutputEventArgs(errMsg));
                    base.OnProcessCancelled(ProcessCancelledEventArgs.Empty);
                    lock (_flagLock) {
                        base._wasCancelled = true;
                    }
                }
            }
            catch (InvalidOperationException) {
                // This will occur if the Cancel() or Dispose() methods are called,
                // since this is expected to occur under these conditions, then just
                // raise the ProcessCancelled event.
                base.OnProcessCancelled(ProcessCancelledEventArgs.Empty);
                lock (_flagLock) {
                    base._wasCancelled = true;
                }
            }
            catch (Exception ex) {
                errMsg = "An error occurred while reading process output: " + ex.ToString();
                outArgs = new ProcessOutputEventArgs(errMsg);
                base.OnOutputReceived(outArgs);
                base.OnProcessCancelled(new ProcessCancelledEventArgs(ex));
                lock (_flagLock) {
                    base._wasCancelled = true;
                }
            }
            finally {
                // Destroy the process and update the state flag.
                if (this._pingProc != null) {
                    this._pingProc.Close();
                    this._pingProc.Dispose();
                }

                lock (_flagLock) {
                    base._isRunning = false;
                }
            }
        }

        /// <summary>
        /// Launches the ping process on a separate thread.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Illegal delimiters in HostList - or - too many hosts in HostList
        /// - or - No target specified.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The ping executable could not be found.
        /// </exception>
        public override void Start() {
            if (base.IsRunning) {
                return;
            }

            this.Preflight();
            this._pingReader = new Thread(new ThreadStart(this.ProcessReader));
            this._pingReader.IsBackground = true;
            this._pingReader.Name = "pingReader";
            this._pingReader.Start();
            base.Start();
        }

        /// <summary>
        /// Cancels the current ping if it is running. This will attempt to
        /// gracefully terminate the process first, and then force-kill the
        /// process if graceful termination fails.
        /// </summary>
        /// <exception cref="Win32Exception">
        /// Unable to terminate process.
        /// </exception>
        public override void Cancel() {
            if ((!base.WasCancelled) || (base.IsRunning)) {
                if (this._pingProc != null) {
                    try {
                        // Try to do this gracefully first.
                        base.Cancel();
                        Thread.Sleep(1000);

                        // Force terminate.
                        if (!this._pingProc.HasExited) {
                            this._pingProc.Kill();
                        }
                    }
                    catch (InvalidOperationException) {
                        // This exception will be throw by Kill() if
                        // the process has already been terminated or if there
                        // is no process currently associated with the object.
                        // There is really nothing to do with this exception,
                        // since it is expected to occur, so we'll just ignore it
                        // and dispose the objects in the finally block as normal.
                    }
                    catch (Win32Exception) {
                        // Process could not be terminated.  Rethrow the exception.
                        throw;
                    }
                    finally {
                        this._pingProc.Close();
                    }
                }
            }
        }
        #endregion
    }
}
