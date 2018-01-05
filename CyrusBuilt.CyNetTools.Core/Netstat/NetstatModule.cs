using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CyrusBuilt.CyNetTools.Core.Netstat
{
    /// <summary>
    /// A threaded wrapper for the Microsoft netstat utility.
    /// </summary>
    public class NetstatModule : ExternalToolBase
    {
        #region Fields
        private Process _nsProc = null;
        private ProcessStartInfo _startInfo = null;
        private Thread _nsMonitor = null;
        private Boolean _showAll = false;
        private Boolean _showExec = false;
        private Boolean _showEtherStats = false;
        private Boolean _showNumeric = false;
        private Boolean _showOwningProc = false;
        private Boolean _showRoutes = false;
        private Boolean _showFQDN = false;
        private Boolean _showOffload = false;
        private ConnectionProtocol _cp = ConnectionProtocol.None;
        private StatsProtocol _sp = StatsProtocol.None;
        private Int32 _interval = 0;
        private FileInfo _exec = null;
        private static readonly Object _syncLock = new Object();
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.Netstat.NetstatModule</b>
        /// class. This is the default constructor.
        /// </summary>
        public NetstatModule()
            : base() {
                String execPath = Path.Combine(Environment.SystemDirectory, "netstat.exe");
                this._exec = new FileInfo(execPath);
        }

        /// <summary>
        /// Releases all resources used by this component.
        /// </summary>
        /// <param name="disposing">
        /// Set true if disposing all managed resources.
        /// </param>
        protected void Dispose(Boolean disposing) {
            if (disposing) {
                if (this._nsProc != null) {
                    try {
                        if (!this._nsProc.HasExited) {
                            this._nsProc.Kill();
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
                        this._nsProc.Close();
                        this._nsProc.Dispose();
                        this._nsProc = null;
                    }
                }
            }

            if (this._nsMonitor != null) {
                try {
                    if (this._nsMonitor.IsAlive) {
                        this._nsMonitor.Abort();
                    }
                }
                catch (ThreadAbortException) {
                }
                this._nsMonitor = null;
            }

            this._startInfo = null;
            base._isRunning = false;
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
        ~NetstatModule() {
            this.Dispose(false);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets whether or not to display all connections and
        /// listening ports. Default is false.
        /// </summary>
        public Boolean ShowAll {
            get { return this._showAll; }
            set { this._showAll = value; }
        }

        /// <summary>
        /// Gets or sets whether or not to display the executable involved in
        /// creating each connection or listening port. Default is false.
        /// </summary>
        /// <remarks>
        /// In some cases, well-known executables host multiple independent
        /// components, and in these cases the sequence of components involved
        /// in creating the connection or listening port is displayed.  In this
        /// case, the executable is in the square brackets at the bottom, on top
        /// is the component it called, and so forth until TCP/IP was reached.
        /// 
        /// <b>Note:</b> This option can be time-consuming and will fail unless
        /// you have sufficient permissions.
        /// </remarks>
        public Boolean ShowExecutable {
            get { return this._showExec; }
            set { this._showExec = value; }
        }

        /// <summary>
        /// Gets or sets whether or not to display ethernet statistics. Default
        /// is false.
        /// </summary>
        /// <remarks>
        /// If <see cref="ShowStatsForProtocol"/> is set to a protocol and not
        /// <see cref="StatsProtocol.None"/>, then this will show ethernet
        /// statistics for the specified protocol.
        /// </remarks>
        public Boolean ShowEthernetStats {
            get { return this._showEtherStats; }
            set { this._showEtherStats = value; }
        }

        /// <summary>
        /// Gets or sets whether or not to display addresses and port numbers
        /// in numeric form. Default is false.
        /// </summary>
        public Boolean ShowNumeric {
            get { return this._showNumeric; }
            set { this._showNumeric = value; }
        }

        /// <summary>
        /// Gets or sets whether or not to display the owning process ID
        /// associated with each connection. Default is false.
        /// </summary>
        public Boolean ShowOwner {
            get { return this._showOwningProc; }
            set { this._showOwningProc = value; }
        }

        /// <summary>
        /// Gets or sets whether or not to show connections for the
        /// specified protocol. Default is <see cref="ConnectionProtocol.None"/>
        /// (disabled).
        /// </summary>
        public ConnectionProtocol ShowConnectionsForProtocol {
            get { return this._cp; }
            set { this._cp = value; }
        }

        /// <summary>
        /// Gets or sets whether not to display the routing table. Default is
        /// false.
        /// </summary>
        public Boolean ShowRoutingTable {
            get { return this._showRoutes; }
            set { this._showRoutes = value; }
        }

        /// <summary>
        /// Gets or sets the protocol to display per-protocol statistics for.
        /// Default is <see cref="StatsProtocol.None"/>.
        /// </summary>
        /// <remarks>
        /// By default, statistics are displayed for all protocols defined in
        /// <see cref="StatsProtocol"/>. The <see cref="ShowConnectionsForProtocol"/>
        /// property can be used to specify a subset of the default.
        /// 
        /// Set this property to <see cref="StatsProtocol.None"/> to disable
        /// (default).
        /// </remarks>
        public StatsProtocol ShowStatsForProtocol {
            get { return this._sp; }
            set { this._sp = value; }
        }

        /// <summary>
        /// Gets or sets the pause interval (in seconds) for refreshing
        /// statistics data. Default is zero (disabled).
        /// </summary>
        /// <remarks>
        /// Setting this value greater than zero will redisplay selected
        /// statistics, pausing the interval seconds between display.  Use the
        /// <see cref="Cancel"/> method to stop redisplaying the statistics. 
        /// If less than or equal to zero (or null), the current statistics
        /// will be displayed only once.
        /// </remarks>
        public Int32 IntervalSeconds {
            get { return this._interval; }
            set {
                if (value < 0) {
                    value = 0;
                }
                this._interval = value;
            }
        }

        /// <summary>
        /// Gets or sets whether or not to show the Fully Qualified Domain Names
        /// (FQDN) for foreign addresses. Default is false.
        /// </summary>
        public Boolean ShowFQDN {
            get { return this._showFQDN; }
            set { this._showFQDN = value; }
        }

        /// <summary>
        /// Gets or sets whether or not to show the current connection offload
        /// state. Default is false.
        /// </summary>
        public Boolean ShowOffload {
            get { return this._showOffload; }
            set { this._showOffload = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Performs any neccessary preflight work prior to netstat execution.
        /// This method is intended to validate parameters and input and throw
        /// the appropriate exception if any invalid parameter values are
        /// detected or if the instance instance is in an invalid state.
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// The netstat executable could not be found.
        /// </exception>
        protected override void Preflight() {
            base._exitCode = 0;
            base._wasCancelled = false;
            base._isRunning = false;

            // **** Perform sanity checks ****

            // Display connections and listening ports.
            var args = String.Empty;
            if (this._showAll) {
                args += " -a";
            }

            // Display executables associated with connection or port.
            if (this._showExec) {
                args += " -b";
            }

            // Show ethernet stats.
            if (this._showEtherStats) {
                args += " -e";
            }

            // Show addresses and ports numerically.
            if (this._showNumeric) {
                args += " -n";
            }

            // Show owning process.
            if (this._showOwningProc) {
                args += " -o";
            }

            // Show stats for connection.
            if ((this._cp != ConnectionProtocol.None) || (this._sp != StatsProtocol.None)) {
                var proto = NetstatUtils.GetConnectionProtocolName(this._cp);
                if (this._sp != StatsProtocol.None) {
                    proto = NetstatUtils.GetStatsProtocolName(this._sp);
                    args += " -s";
                }
                args += " -p " + proto;
            }

            // Routing table.
            if (this._showRoutes) {
                args += " -r";
            }

            // Show FQDNs.
            if (this._showFQDN) {
                args += " -f";
            }

            // Connection offload.
            if (this._showOffload) {
                args += " -t";
            }

            // Refresh interval.
            if (this._interval > 0) {
                args += " " + this._interval.ToString();
            }

            // Validate exec path.
            this._startInfo = new ProcessStartInfo();
            if (this._exec.Exists) {
                this._startInfo.FileName = this._exec.FullName;
                this._startInfo.WorkingDirectory = this._exec.DirectoryName;
            }
            else {
                throw new FileNotFoundException("Netstat executable not found.", this._exec.FullName);
            }

            // Destroy previous process object (if present).
            if (this._nsProc != null) {
                this._nsProc.Close();
                this._nsProc.Dispose();
            }

            // Setup remaining start parameters.
            this._startInfo.UseShellExecute = true;
            this._startInfo.RedirectStandardError = false;
            this._startInfo.RedirectStandardInput = false;
            this._startInfo.RedirectStandardOutput = true;
            this._startInfo.CreateNoWindow = true;
            this._startInfo.Arguments = args;

            // Setup the process.
            this._nsProc = new Process();
            this._nsProc.StartInfo = this._startInfo;
        }

        /// <summary>
        /// Monitors the process and reads it's output. This method should only
        /// execute in the reader thread.
        /// </summary>
        protected override void ProcessReader() {
            var errMsg = String.Empty;
            try {
                // Start the process, set the flag, and raise the running event.
                this._nsProc.Start();
                base.OnProcessStarted(new ProcessStartedEventArgs(this._nsProc.Id));

                // Read the output until the process terminates.
                var output = this._nsProc.StandardOutput.ReadLine();
                while ((output != null) && (!base.WasCancelled)) {
                    // Notify output listeners.
                    base.OnOutputReceived(new ProcessOutputEventArgs(output));
                    output = this._nsProc.StandardOutput.ReadLine();
                }

                // Wait for the process to finish up, then get the exit code.
                this._nsProc.WaitForExit();
                base._exitCode = this._nsProc.ExitCode;

                // If the process exit code was 0, then the process completed successfully.
                if (base._exitCode == 0) {
                    base.OnProcessFinished(new ProccessDoneEventArgs(base._exitCode));
                }
                else {
                    errMsg = "The netstat process failed. Exit code: " + this._exitCode.ToString();
                    base.OnOutputReceived(new ProcessOutputEventArgs(errMsg));
                    base.OnProcessCancelled(ProcessCancelledEventArgs.Empty);
                    lock (_syncLock) {
                        base._wasCancelled = true;
                    }
                }
            }
            catch (InvalidOperationException) {
                // This will occur if the Cancel() or Dispose() methods are called,
                // since this is expected to occur under these conditions, then just
                // raise the ProcessCancelled event.
                base.OnProcessCancelled(ProcessCancelledEventArgs.Empty);
                lock (_syncLock) {
                    base._wasCancelled = true;
                }
            }
            catch (Exception ex) {
                errMsg = "An error occurred while reading process output: " + ex.ToString();
                base.OnOutputReceived(new ProcessOutputEventArgs(errMsg));
                base.OnProcessCancelled(new ProcessCancelledEventArgs(ex));
                lock (_syncLock) {
                    base._wasCancelled = true;
                }
            }
            finally {
                // Destroy the process and update the state flag.
                if (this._nsProc != null) {
                    this._nsProc.Close();
                    this._nsProc.Dispose();
                }

                lock (_syncLock) {
                    base._isRunning = true;
                }
            }
        }

        /// <summary>
        /// Launches the netstat process on a separate thread.
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// The netstat executable could not be found.
        /// </exception>
        public override void Start() {
            if (base.IsRunning) {
                return;
            }

            this.Preflight();
            this._nsMonitor = new Thread(new ThreadStart(this.ProcessReader));
            this._nsMonitor.IsBackground = true;
            this._nsMonitor.Name = "netstatReader";
            this._nsMonitor.Start();
            base.Start();
        }

        /// <summary>
        /// Cancels the current netstat if it is running. This will attempt to
        /// gracefully terminate the process first, and then force-kill the
        /// process if graceful termination fails.
        /// </summary>
        /// <exception cref="Win32Exception">
        /// Unable to terminate process.
        /// </exception>
        public override void Cancel() {
            if ((!base.WasCancelled) || (base.IsRunning)) {
                if (this._nsProc != null) {
                    try {
                        // Try to do this gracefully first.
                        base.Cancel();
                        Thread.Sleep(1000);

                        // Force terminate.
                        if (!this._nsProc.HasExited) {
                            this._nsProc.Kill();
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
                        this._nsProc.Close();
                    }
                }
            }
        }
        #endregion
    }
}
