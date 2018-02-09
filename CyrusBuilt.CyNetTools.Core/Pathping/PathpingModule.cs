using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace CyrusBuilt.CyNetTools.Core.Pathping
{
    /// <summary>
    /// Provides a threaded wrapper for the Microsoft Pathping utility.
    /// </summary>
    public class PathpingModule : ExternalToolBase
    {
        #region Constants
        /// <summary>
        /// The default hop count (30).
        /// </summary>
        public const Int32 DEFAULT_HOP_COUNT = 30;
        #endregion

        #region Fields
        private Process _pathpingProc = null;
        private Thread _procReader = null;
        private FileInfo _exec = null;
        private Boolean _resolveNames = true;
        private IPVersion _ipVer = IPVersion.IPv4;
        private Int32 _maxHops = 0;
        private Int32 _timeout = 0;
        private Int32 _wait = 0;
        private Int32 _queriesPerHop = 0;
        private String _hostList = String.Empty;
        private String _targetHost = String.Empty;
        private IPAddress _srcAddress = IPAddress.None;
        private static readonly Object _flagLock = new Object();
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.TraceRoute.PathpingModule</b>
        /// class. This is the default constructor.
        /// </summary>
        public PathpingModule() : base() {
            var execPath = Path.Combine(Environment.SystemDirectory, "pathping.exe");
            this._exec = new FileInfo(execPath);
        }

        /// <summary>
        /// Releases all resources used by this component.
        /// </summary>
        /// <param name="disposing">
        /// Set true if disposing all managed resources.
        /// </param>
        protected void Dispose(bool disposing) {
            if (disposing) {
                if (this._pathpingProc != null) {
                    try {
                        this._pathpingProc.Kill();
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
                        this._pathpingProc.Dispose();
                        this._pathpingProc = null;
                    }
                }
            }

            if (this._procReader != null) {
                base.Cancel();
                Thread.Sleep(50);
                if (this._procReader.IsAlive) {
                    try {
                        this._procReader.Abort();
                    }
                    catch (ThreadAbortException) {
                        Thread.ResetAbort();
                    }
                }

                this._procReader = null;
            }

            this._isRunning = false;
            this._exec = null;
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
        ~PathpingModule() {
            this.Dispose(false);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets whether or not to performing name resolution on the
        /// target and each node in between. Default is false.
        /// </summary>
        public Boolean ResolveHostNames {
            get { return this._resolveNames; }
            set { this._resolveNames = value; }
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
        /// Default is an empty string (disabled).
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
        /// Gets or sets the source address to use. Default is <see cref="IPAddress.None"/>.
        /// </summary>
        public IPAddress SourceAddress {
            get { return this._srcAddress; }
            set { this._srcAddress = value; }
        }

        /// <summary>
        /// Gets or sets the target hostname or IP address to trace.
        /// </summary>
        public String TargetHost {
            get { return this._targetHost; }
            set {
                if (value == null) {
                    value = String.Empty;
                }
                this._targetHost = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of queries per hop.
        /// </summary>
        public Int32 QueriesPerHop {
            get { return this._queriesPerHop; }
            set {
                if (value < 0) {
                    value = 0;
                }

                this._queriesPerHop = value;
            }
        }

        /// <summary>
        /// Gets or sets the amount of time to wait (in milliseconds) between replies.
        /// </summary>
        public Int32 Wait {
            get { return this._wait; }
            set { this._wait = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Performs any neccessary preflight work prior to pathping execution.
        /// This method is intended to validate parameters and input and throw
        /// the appropriate exception if any invalid parameter values are
        /// detected or if the instance instance is in an invalid state.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Hostlist cannot be delimited by anything other than spaces.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The pathping executable could not be found.
        /// </exception>
        protected override void Preflight() {
            base._exitCode = 0;
            base._wasCancelled = false;
            base._isRunning = false;

            // **** Perform sanity checks ****

            // Name resolution
            var args = String.Empty;
            if (!this._resolveNames) {
                args += " -n";
            }

            // Max hops
            if (this._maxHops > 0) {
                args += " -h " + this._maxHops.ToString();
            }

            // Timeout
            if (this._timeout > 0) {
                args += " -w " + this._timeout.ToString();
            }

            // IP Protocol
            switch (this._ipVer) {
                case IPVersion.IPv4:
                    args += " -4";
                    break;
                case IPVersion.IPv6:
                    args += " -6";
                    break;
            }

            // Host list
            if (!String.IsNullOrEmpty(this._hostList)) {
                if ((this._hostList.Contains(",")) ||
                        (this._hostList.Contains("\\")) ||
                        (this._hostList.Contains("/"))) {
                    throw new InvalidOperationException("PathpingModule.HostList can only be delimited by spaces.");
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
                        args += " -g " + this._hostList;
                    }
                }
            }

            // Source address
            if (this._srcAddress != null && this._srcAddress != IPAddress.None) {
                args += " -i " + this._srcAddress.ToString();
            }

            // Queries-per-hop
            if (this._queriesPerHop > 0) {
                args += " -q " + this._queriesPerHop.ToString();
            }

            // Wait period
            if (this._wait > 0) {
                args += " -p " + this._wait.ToString();
            }

            // Combine arguments with target if specified. BETTER BE!
            if (String.IsNullOrEmpty(this._targetHost)) {
                throw new InvalidOperationException("Target host not specified.");
            }

            args += " " + this._targetHost;

            // Destroy previous process object (if present).
            if (this._pathpingProc != null) {
                this._pathpingProc.Dispose();
            }

            // Validate exec path.
            this._pathpingProc = new Process();
            if (this._exec.Exists) {
                this._pathpingProc.StartInfo.FileName = this._exec.FullName;
                this._pathpingProc.StartInfo.WorkingDirectory = this._exec.DirectoryName;
            }
            else {
                throw new FileNotFoundException("Pathping executable not found.", this._exec.FullName);
            }

            // Setup remaining start parameters.
            this._pathpingProc.StartInfo.UseShellExecute = false;
            this._pathpingProc.StartInfo.RedirectStandardError = false;
            this._pathpingProc.StartInfo.RedirectStandardInput = false;
            this._pathpingProc.StartInfo.RedirectStandardOutput = true;
            this._pathpingProc.StartInfo.CreateNoWindow = true;
            this._pathpingProc.StartInfo.Arguments = args;
        }

        /// <summary>
        /// Monitors the process and reads it's output. This method should only
        /// execute in the reader thread.
        /// </summary>
        protected override void ProcessReader() {
            ProcessOutputEventArgs outArgs = null;
            var output = String.Empty;
            var errMsg = String.Empty;

            try {
                // Start the process and raise the running event.
                this._pathpingProc.Start();
                base.OnProcessStarted(new ProcessStartedEventArgs(this._pathpingProc.Id));

                // TODO Compute progress like we did in TraceRouteModule? Will need regex.

                // Read the output until the process terminates.
                output = this._pathpingProc.StandardOutput.ReadLine();
                while (output != null && !base.WasCancelled) {
                    // Notify output listeners.
                    outArgs = new ProcessOutputEventArgs(output);
                    base.OnOutputReceived(outArgs);

                    // This loop is blocking, which is why we are using a thread.
                    output = this._pathpingProc.StandardOutput.ReadLine();
                }

                // Wait for the process to finish up, then get the exit code.
                this._pathpingProc.WaitForExit();
                base._exitCode = this._pathpingProc.ExitCode;

                // If the process terminated normally, then notify listeners that
                // we're done. Otherwise, notify output and cancellation listeners.
                if (base._exitCode == 0) {
                    base.OnProcessFinished(new ProccessDoneEventArgs(base._exitCode));
                }
                else {
                    errMsg = "A pathping error occurred. Error code: " + base._exitCode.ToString();
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
                if (this._pathpingProc != null) {
                    this._pathpingProc.Dispose();
                }

                lock (_flagLock) {
                    base._isRunning = false;
                }
            }
        }

        /// <summary>
        /// Launches the pathping process on a separate thread.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Hostlist cannot be delimited by anything other than spaces.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The pathping executable could not be found.
        /// </exception>
        public override void Start() {
            if (base.IsRunning) {
                return;
            }

            this.Preflight();
            this._procReader = new Thread(new ThreadStart(this.ProcessReader));
            this._procReader.IsBackground = true;
            this._procReader.Name = "pathpingReader";
            this._procReader.Start();
            base.Start();
        }

        /// <summary>
        /// Cancels the current pathping if it is running. This will attempt to
        /// gracefully terminate the process first, and then force-kill the
        /// process if graceful termination fails.
        /// </summary>
        /// <exception cref="Win32Exception">
        /// Unable to terminate process.
        /// </exception>
        public override void Cancel() {
            if ((!base.WasCancelled) || (base.IsRunning)) {
                if (this._pathpingProc != null) {
                    try {
                        // Try to do this gracefully first.
                        base.Cancel();
                        Thread.Sleep(1000);

                        // Force terminate.
                        if (!this._pathpingProc.HasExited) {
                            this._pathpingProc.Kill();
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
                        this._pathpingProc.Close();
                    }
                }
            }
        }
        #endregion
    }
}
