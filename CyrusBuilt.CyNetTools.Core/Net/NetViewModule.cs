using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CyrusBuilt.CyNetTools.Core.Net
{
    /// <summary>
    /// Provides a threaded wrapper for the Microsoft Net utility using the
    /// 'View' command.
    /// </summary>
    public class NetViewModule : NetToolBase
    {
        #region Fields
        private Process _netViewProc = null;
        private ProcessStartInfo _startInfo = null;
        private Thread _netViewMonitor = null;
        private Boolean _allShares = false;
        private Boolean _showCacheSettings = false;
        private Boolean _showDomain = false;
        private String _computerName = String.Empty;
        private String _domainName = String.Empty;
        private static readonly Object _flagLock = new Object();
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.Net.NetViewModule</b>
        /// class. This is the default constructor.
        /// </summary>
        public NetViewModule()
            : base() {
        }

        /// <summary>
        /// Releases all resources used by this component.
        /// </summary>
        /// <param name="disposing">
        /// Set true if disposing all managed resources.
        /// </param>
        protected void Dispose(Boolean disposing) {
            if (disposing) {
                if (this._netViewProc != null) {
                    try {
                        if (!this._netViewProc.HasExited) {
                            this._netViewProc.Kill();
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
                        this._netViewProc.Close();
                        this._netViewProc.Dispose();
                        this._netViewProc = null;
                    }
                }
            }

            if (this._netViewMonitor != null) {
                try {
                    if (this._netViewMonitor.IsAlive) {
                        this._netViewMonitor.Abort();
                    }
                }
                catch (ThreadAbortException) {
                }
                this._netViewMonitor = null;
            }

            base._isRunning = false;
            this._startInfo = null;
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
        ~NetViewModule() {
            this.Dispose(false);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets whether or not to display all shares including
        /// administrative shares. Default is false.
        /// </summary>
        public Boolean ShowAllShares {
            get { return this._allShares; }
            set { this._allShares = value; }
        }

        /// <summary>
        /// Gets or sets whether or not to display the offline client caching
        /// settings for the resources on the specified computer. Default is false.
        /// </summary>
        public Boolean ShowOfflineCacheSettings {
            get { return this._showCacheSettings; }
            set { this._showCacheSettings = value; }
        }

        /// <summary>
        /// Gets or sets the name of the computer that contains the shared
        /// resources that you want to view.
        /// </summary>
        public String ComputerName {
            get { return this._computerName; }
            set { this._computerName = value; }
        }

        /// <summary>
        /// Gets or sets whether or not show all domains on the network or if
        /// <see cref="DomainName"/> is specified, then all computers on the
        /// specified domain. Default is false.
        /// </summary>
        public Boolean ShowDomain {
            get { return this._showDomain; }
            set { this._showDomain = value; }
        }

        /// <summary>
        /// Gets or sets the domain to view the available computers for. Ignored
        /// if <see cref="ShowDomain"/> is false.
        /// </summary>
        public String DomainName {
            get { return this._domainName; }
            set { this._domainName = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Performs any neccessary preflight work prior to net execution.
        /// This method is intended to validate parameters and input and throw
        /// the appropriate exception if any invalid parameter values are
        /// detected or if the instance instance is in an invalid state.
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// The net executable could not be found.
        /// </exception>
        protected override void Preflight() {
            base._exitCode = 0;
            base._isRunning = false;
            base._wasCancelled = false;

            // **** Perform sanity checks ****

            // Computer name
            var args = String.Empty;
            if (!String.IsNullOrEmpty(this._computerName)) {
                if (!this._computerName.StartsWith("\\\\")) {
                    if (!this._computerName.StartsWith("\\")) {
                        this._computerName = "\\" + this._computerName;
                    }
                    else {
                        this._computerName = "\\\\" + this._computerName;
                    }
                }
                args += this._computerName;
            }

            // Cache
            if (this._showCacheSettings) {
                args += " /CACHE";
            }

            // All shares
            if (this._allShares) {
                args += " /ALL";
            }

            // Show domain
            if (this._showDomain) {
                args += " /domain";
                if (!String.IsNullOrEmpty(this._domainName)) {
                    args += ":" + this._domainName;
                }
            }

            // Validate exec path.
            this._startInfo = new ProcessStartInfo();
            if (base.NetExec.Exists) {
                this._startInfo.FileName = base.NetExec.FullName;
                this._startInfo.WorkingDirectory = base.NetExec.DirectoryName;
            }
            else {
                throw new FileNotFoundException("Net executable not found.", base.NetExec.FullName);
            }

            // Destroy previous process object (if present).
            if (this._netViewProc != null) {
                this._netViewProc.Close();
                this._netViewProc.Dispose();
            }

            // Setup remaining start parameters.
            this._startInfo.UseShellExecute = true;
            this._startInfo.RedirectStandardError = false;
            this._startInfo.RedirectStandardInput = false;
            this._startInfo.RedirectStandardOutput = true;
            this._startInfo.CreateNoWindow = true;
            this._startInfo.Arguments = args;

            // Setup the process.
            this._netViewProc = new Process();
            this._netViewProc.StartInfo = this._startInfo;
        }

        /// <summary>
        /// Monitors the process and reads it's output. This method should only
        /// execute in the reader thread.
        /// </summary>
        protected override void ProcessReader() {
            var errMsg = String.Empty;
            try {
                // Start the process and raise the running event.
                this._netViewProc.Start();
                base.OnProcessStarted(new ProcessStartedEventArgs(this._netViewProc.Id));

                // Read the output until the process terminates.
                var output = this._netViewProc.StandardOutput.ReadLine();
                while ((!base.WasCancelled) && (output != null)) {
                    // Notify output listeners.
                    base.OnOutputReceived(new ProcessOutputEventArgs(output));
                    output = this._netViewProc.StandardOutput.ReadLine();
                }

                // Wait for the process to finish up, then get the exit code.
                this._netViewProc.WaitForExit();
                base._exitCode = this._netViewProc.ExitCode;

                // If the process exit code was 0, then the process completed successfully.
                if (base._exitCode == 0) {
                    base.OnProcessFinished(new ProccessDoneEventArgs(base._exitCode));
                }
                else {
                    errMsg = "The netstat process failed. Exit code: " + this._exitCode.ToString();
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
                base.OnOutputReceived(new ProcessOutputEventArgs(errMsg));
                base.OnProcessCancelled(new ProcessCancelledEventArgs(ex));
                lock (_flagLock) {
                    base._wasCancelled = true;
                }
            }
            finally {
                // Destroy the process and update the state flag.
                if (this._netViewProc != null) {
                    this._netViewProc.Close();
                    this._netViewProc.Dispose();
                }

                lock (_flagLock) {
                    base._isRunning = true;
                }
            }
        }

        /// <summary>
        /// Launches the net process on a separate thread.
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// The net executable could not be found.
        /// </exception>
        public override void Start() {
            if (base.IsRunning) {
                return;
            }

            this.Preflight();
            this._netViewMonitor = new Thread(new ThreadStart(this.ProcessReader));
            this._netViewMonitor.IsBackground = true;
            this._netViewMonitor.Name = "netViewMonitor";
            this._netViewMonitor.Start();
            base.Start();
        }

        /// <summary>
        /// Cancels the current net view if it is running. This will attempt to
        /// gracefully terminate the process first, and then force-kill the
        /// process if graceful termination fails.
        /// </summary>
        /// <exception cref="Win32Exception">
        /// Unable to terminate process.
        /// </exception>
        public override void Cancel() {
            if ((!base.WasCancelled) || (base.IsRunning)) {
                if (this._netViewProc != null) {
                    try {
                        // Try to do this gracefully first.
                        base.Cancel();
                        Thread.Sleep(1000);

                        // Force terminate.
                        if (!this._netViewProc.HasExited) {
                            this._netViewProc.Kill();
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
                        this._netViewProc.Close();
                    }
                }
            }
        }
        #endregion
    }
}
