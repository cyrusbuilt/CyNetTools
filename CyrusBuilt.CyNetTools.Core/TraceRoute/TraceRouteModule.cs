using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace CyrusBuilt.CyNetTools.Core.TraceRoute
{
    /// <summary>
    /// A threaded wrapper for the Microsoft tracert utility.
    /// </summary>
    public class TraceRouteModule : ExternalToolBase
    {
        #region Constants
        /// <summary>
        /// The default hop count (30).
        /// </summary>
        public const Int32 DEFAULT_HOP_COUNT = 30;
        #endregion

        #region Fields
        private Process _traceProc = null;
        private ProcessStartInfo _startInfo = null;
        private Thread _traceMonitor = null;
        private Boolean _noResolve = false;
        private Boolean _roundTrip = false;
        private Int32 _maxHops = 0;
        private Int32 _timeout = 0;
        private String _hostList = String.Empty;
        private String _target = String.Empty;
        private IPAddress _srcAddress = IPAddress.None;
        private IPVersion _ipVer = IPVersion.IPv4;
        private FileInfo _exec = null;
        private Regex _reReply = null;
        private static readonly Object _flagLock = new Object();
        #endregion

        #region Events
        /// <summary>
        /// Occurs when trace progress is made.
        /// </summary>
        public event TraceProgressEventHandler Progress;
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.TraceRoute.TraceRouteModule</b>
        /// class. This is the default constructor.
        /// </summary>
        public TraceRouteModule()
            : base() {
                var execPath = Path.Combine(Environment.SystemDirectory, "tracert.exe");
                this._exec = new FileInfo(execPath);
                this._reReply = new Regex(".+\\d.+ms.+\\d.+ms.+\\d.+ms.+",
                                            RegexOptions.IgnoreCase |
                                            RegexOptions.IgnorePatternWhitespace |
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
                if (this._traceProc != null) {
                    try {
                        if (!this._traceProc.HasExited) {
                            this._traceProc.Kill();
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
                        // Do NOT throw exceptions from dispose.
                    }
                    finally {
                        this._traceProc.Close();
                        this._traceProc.Dispose();
                        this._traceProc = null;
                    }
                }
            }

            if (this._traceMonitor != null) {
                try {
                    if (this._traceMonitor.IsAlive) {
                        this._traceMonitor.Abort();
                    }
                }
                catch (ThreadAbortException) {
                }
                this._traceMonitor = null;
            }

            this._startInfo = null;
            this._reReply = null;
            this._exec = null;
            base._isRunning = false;
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
        ~TraceRouteModule() {
            this.Dispose(false);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets whether or not to disable performing name resolution on the
        /// target and each node in between. Default is false.
        /// </summary>
        public Boolean DoNotResolveNames {
            get { return this._noResolve; }
            set { this._noResolve = value; }
        }

        /// <summary>
        /// Gets or sets the max number of hops to search for target. Default
        /// is zero (ignore).
        /// </summary>
        public Int32 MaxHops {
            get { return this._maxHops; }
            set {
                if (value < 0) {
                    value = DEFAULT_HOP_COUNT;
                }
                this._maxHops = value;
            }
        }

        /// <summary>
        /// Gets or sets the timeout in milliseconds to wait for each reply.
        /// Default is zero (ignore).
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
        /// Gets or sets the IP protocol version to use (default is <see cref="IPVersion.IPv4"/>).
        /// </summary>
        public IPVersion IPProtocolVersion {
            get { return this._ipVer; }
            set { this._ipVer = value; }
        }

        /// <summary>
        /// Gets or sets the space-delimited list of hosts source the route along.
        /// Default is an empty string (disabled). Ignored unless <see cref="IPProtocolVersion"/>
        /// is <see cref="IPVersion.IPv4"/>.
        /// </summary>
        public String HostList {
            get { return this._hostList; }
            set {
                if (value == null) {
                    value = String.Empty;
                }
                this._hostList = value;
            }
        }

        /// <summary>
        /// Gets or sets whether or not to trace round-trip path. Ignored unless
        /// <see cref="IPProtocolVersion"/> is <see cref="IPVersion.IPv6"/>.
        /// Default is false;
        /// </summary>
        public Boolean RoundTrip {
            get { return this._roundTrip; }
            set { this._roundTrip = value; }
        }

        /// <summary>
        /// Gets or sets the source address to use. Default is <see cref="IPAddress.None"/>.
        /// Ignored unless <see cref="IPProtocolVersion"/> is <see cref="IPVersion.IPv6"/>.
        /// </summary>
        public IPAddress SourceAddress {
            get { return this._srcAddress; }
            set { this._srcAddress = value; }
        }

        /// <summary>
        /// Gets or sets the target hostname or IP address to trace.
        /// </summary>
        public String TargetHost {
            get { return this._target; }
            set {
                if (value == null) {
                    value = String.Empty;
                }
                this._target = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Raises the <see cref="Progress"/> event.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        protected virtual void OnProgress(TraceProgressEventArgs e) {
            if (this.Progress != null) {
                this.Progress(this, e);
            }
        }

        /// <summary>
        /// Performs any neccessary preflight work prior to tracert execution.
        /// This method is intended to validate parameters and input and throw
        /// the appropriate exception if any invalid parameter values are
        /// detected or if the instance instance is in an invalid state.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Target host not specified.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The tracert executable could not be found.
        /// </exception>
        protected override void Preflight() {
            base._exitCode = 0;
            base._wasCancelled = false;
            base._isRunning = false;

            // **** Perform sanity checks ****

            // Name resolution
            var args = String.Empty;
            if (this._noResolve) {
                args += " -d";
            }

            // Maximum hop count.
            if (this._maxHops > 0) {
                args += " -h " + this._maxHops.ToString();
            }

            // Timeout milliseconds.
            if (this._timeout > 0) {
                args += " -w " + this._timeout.ToString();
            }

            if (this._ipVer == IPVersion.IPv4) {
                // Loose host list.
                if (!String.IsNullOrEmpty(this._hostList)) {
                    if ((this._hostList.Contains(",")) ||
                        (this._hostList.Contains("\\")) ||
                        (this._hostList.Contains("/"))) {
                        throw new InvalidOperationException("TraceRouteModule.HostList can only be delimited by spaces.");
                    }

                    String[] hosts = this._hostList.Split(' ');
                    if (hosts.Length > 0) {
                        // List is valid. We reconstruct clean here (eliminate
                        // extra spaces, if any).
                        var newHostList = String.Empty;
                        foreach (var h in hosts) {
                            newHostList += h + " ";
                        }

                        // Strip trailing space if present (likely).
                        if (newHostList.EndsWith(" ")) {
                            newHostList = newHostList.TrimEnd(' ');
                        }

                        this._hostList = newHostList;
                        Array.Clear(hosts, 0, hosts.Length);
                        if (!String.IsNullOrEmpty(this._hostList)) {
                            args += " -j " + this._hostList;
                        }
                    }
                }
            }
            else if (this._ipVer == IPVersion.IPv6) {
                // Round-trip.
                if (this._roundTrip) {
                    args += " -R";
                }

                // Source address.
                if (this._srcAddress != IPAddress.None) {
                    args += " -S " + this._srcAddress.ToString();
                }
            }

            // Combine arguments with target if specified. BETTER BE!
            if (String.IsNullOrEmpty(this._target)) {
                throw new InvalidOperationException("Target host not specified.");
            }
            else {
                args += " " + this._target;
            }

            // Validate exec path.
            this._startInfo = new ProcessStartInfo();
            if (this._exec.Exists) {
                this._startInfo.FileName = this._exec.FullName;
                this._startInfo.WorkingDirectory = this._exec.DirectoryName;
            }
            else {
                throw new FileNotFoundException("Tracert executable not found.", this._exec.FullName);
            }

            // Destroy previous process object (if present).
            if (this._traceProc != null) {
                this._traceProc.Close();
                this._traceProc.Dispose();
            }

            // Setup remaining start parameters.
            this._startInfo.UseShellExecute = false;
            this._startInfo.RedirectStandardError = false;
            this._startInfo.RedirectStandardInput = false;
            this._startInfo.RedirectStandardOutput = true;
            this._startInfo.CreateNoWindow = true;
            this._startInfo.Arguments = args;

            // Setup the process.
            this._traceProc = new Process();
            this._traceProc.StartInfo = this._startInfo;
        }

        /// <summary>
        /// Monitors the process and reads it's output. This method should only
        /// execute in the reader thread.
        /// </summary>
        protected override void ProcessReader() {
            var errMsg = String.Empty;
            try {
                Match mReply = null;
                var hop = 0;
                var count = this._maxHops;
                if (count <= 0) {
                    count = DEFAULT_HOP_COUNT;
                }

                // Start the process and raise the running event.
                this._traceProc.Start();
                base.OnProcessStarted(new ProcessStartedEventArgs(this._traceProc.Id));

                // Read the output until the process terminates.
                var output = this._traceProc.StandardOutput.ReadLine();
                while ((!base.WasCancelled) && (output != null)) {
                    // Notify output listeners.
                    base.OnOutputReceived(new ProcessOutputEventArgs(output));

                    // See if the output message is a tracert reply.
                    mReply = this._reReply.Match(output);
                    if (mReply.Success) {
                        hop++;
                        this.OnProgress(new TraceProgressEventArgs(hop, count));
                    }

                    // This is blocking, which is why we use a thread.
                    output = this._traceProc.StandardOutput.ReadLine();
                }

                // Wait for the process to finish up, then get the exit code.
                this._traceProc.WaitForExit();
                base._exitCode = this._traceProc.ExitCode;

                // If somehow the total count is greater than the last hop
                // number, then set the count equal to the last hop number,
                // set the completed percentage to 100, and raise the progress
                // event.  This should prevent an exception if the numbers are
                // wrong.
                if (count > hop) {
                    count = hop;
                    this.OnProgress(new TraceProgressEventArgs(hop, count));
                }

                // If the complete percentage is 100% or the process exit was 0,
                // the process completed.
                if ((((hop * 100) / count) == 100) || (base.ExitCode == 0)) {
                    base.OnProcessFinished(new ProccessDoneEventArgs(base.ExitCode));
                }
                else {
                    errMsg = "Trace process failed. Exit code: " + base.ExitCode.ToString();
                    base.OnOutputReceived(new ProcessOutputEventArgs(errMsg));
                    base.OnProcessCancelled(ProcessCancelledEventArgs.Empty);
                    lock (_flagLock) {
                        base._wasCancelled = true;
                    }
                }
            }
            catch (InvalidOperationException ex) {
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
                base.OnOutputReceived(new ProcessOutputEventArgs(errMsg));
                base.OnProcessCancelled(new ProcessCancelledEventArgs(ex));
                lock (_flagLock) {
                    base._wasCancelled = true;
                }
            }
            finally {
                // Destroy the process and update the state flag.
                this._traceProc.Close();
                lock (_flagLock) {
                    base._isRunning = false;
                }
            }
        }

        /// <summary>
        /// Launches the net process on a separate thread.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Target host not specified.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The tracert executable could not be found.
        /// </exception>
        public override void Start() {
            if (base._isRunning) {
                return;
            }

            this.Preflight();
            this._traceMonitor = new Thread(new ThreadStart(this.ProcessReader));
            this._traceMonitor.IsBackground = true;
            this._traceMonitor.Name = "tracertMonitor";
            this._traceMonitor.Start();
            base.Start();
        }

        /// <summary>
        /// Cancels the current tracert if it is running. This will attempt to
        /// gracefully terminate the process first, and then force-kill the
        /// process if graceful termination fails.
        /// </summary>
        /// <exception cref="Win32Exception">
        /// Unable to terminate process.
        /// </exception>
        public override void Cancel() {
            if ((!base.WasCancelled) || (base.IsRunning)) {
                if (this._traceProc != null) {
                    try {
                        // Try to do this gracefully first.
                        base.Cancel();
                        Thread.Sleep(1000);

                        // Force terminate.
                        if (!this._traceProc.HasExited) {
                            this._traceProc.Kill();
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
                        this._traceProc.Close();
                    }
                }
            }
        }
        #endregion
    }
}
