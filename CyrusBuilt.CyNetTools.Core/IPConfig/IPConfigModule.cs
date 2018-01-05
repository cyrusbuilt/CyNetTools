using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;

namespace CyrusBuilt.CyNetTools.Core.IPConfig
{
    /// <summary>
    /// Provides a threaded wrapper for the Microsoft IPConfig utility.
    /// </summary>
    public class IPConfigModule : ExternalToolBase
    {
        #region Fields
        private Process _ipconfigProc = null;
        private ProcessStartInfo _startInfo = null;
        private Thread _ipconfigReader = null;
        private IPVersion _ipVer = IPVersion.IPv4;
        private Boolean _allCompartments = false;
        private IpConfigCommand _command = IpConfigCommand.ShowBasic;
        private FileInfo _exec = null;
        private String _adapterName = String.Empty;
        private String _classID = String.Empty;
        private static readonly Object _syncLock = new Object();
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.IPConfig.IPConfigModule</b>
        /// class. This is the default constructor.
        /// </summary>
        public IPConfigModule()
            : base() {
                String execPath = Path.Combine(Environment.SystemDirectory, "ipconfig.exe");
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
                if (this._ipconfigProc != null) {
                    try {
                        this._ipconfigProc.Kill();
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
                        this._ipconfigProc.Close();
                        this._ipconfigProc.Dispose();
                    }
                }
            }

            if (this._ipconfigReader != null) {
                if (this._ipconfigReader.IsAlive) {
                    try {
                        this._ipconfigReader.Abort();
                    }
                    catch (ThreadAbortException) {
                    }
                }
                this._ipconfigReader = null;
            }

            this._startInfo = null;
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
        ~IPConfigModule() {
            this.Dispose(false);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the IP protocol version to use (default is <see cref="IPVersion.IPv4"/>).
        /// </summary>
        public IPVersion IPProtocolVersion {
            get { return this._ipVer; }
            set { this._ipVer = value; }
        }

        /// <summary>
        /// Gets or sets a flag indicating whether or not to show information
        /// about all compartments.
        /// </summary>
        public Boolean ShowAllCompartments {
            get { return this._allCompartments; }
            set { this._allCompartments = value; }
        }

        /// <summary>
        /// Gets or sets the ipconfig command to execute.
        /// </summary>
        public IpConfigCommand Command {
            get { return this._command; }
            set { this._command = value; }
        }

        /// <summary>
        /// Gets or sets the adapter name. This can be used when releasing or
        /// renewing specific adapters or showing/setting adapter class IDs.
        /// </summary>
        public String AdapterName {
            get { return this._adapterName; }
            set { this._adapterName = value; }
        }

        /// <summary>
        /// Gets or sets the adapter class ID to use when modifying/setting
        /// the class ID of a specific adapter.
        /// </summary>
        public String ClassID {
            get { return this._classID; }
            set { this._classID = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Performs any neccessary preflight work prior to ping execution.
        /// This method is intended to validate parameters and input and throw
        /// the appropriate exception if any invalid parameter values are
        /// detected or if the instance instance is in an invalid state.
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// The ipconfig executable could not be found.
        /// </exception>
        protected override void Preflight() {
            base._exitCode = 0;
            base._wasCancelled = false;
            base._isRunning = false;

            // **** Perform sanity checks ****
            var args = String.Empty;
            if (this._allCompartments) {
                args = " /allcompartments";
            }

            var cmd = IpConfigUtils.GetCommandString(this._command, this._ipVer);
            if (!String.IsNullOrEmpty(cmd)) {
                args += " " + cmd;
            }

            if ((this._command == IpConfigCommand.Renew) ||
                (this._command == IpConfigCommand.Release) ||
                (this._command == IpConfigCommand.ShowClassID) ||
                (this._command == IpConfigCommand.SetClassID)) {
                    if (!String.IsNullOrEmpty(this._adapterName)) {
                        args += " " + '"' + this._adapterName + '"';
                    }

                    if ((this._command == IpConfigCommand.SetClassID) &&
                        (!String.IsNullOrEmpty(this._classID))) {
                        args += " " + this._classID;
                    }
            }

            // Destroy previous process object (if present).
            if (this._ipconfigProc != null) {
                this._ipconfigProc.Close();
                this._ipconfigProc.Dispose();
            }

            // Validate exec path.
            this._startInfo = new ProcessStartInfo();
            if (this._exec.Exists) {
                this._startInfo.FileName = this._exec.FullName;
                this._startInfo.WorkingDirectory = this._exec.DirectoryName;
            }
            else {
                throw new FileNotFoundException("ipconfig executable not found.", this._exec.FullName);
            }

            // Setup remaining start parameters.
            this._startInfo.Arguments = args;
            this._startInfo.CreateNoWindow = true;
            this._startInfo.UseShellExecute = false;
            this._startInfo.RedirectStandardError = false;
            this._startInfo.RedirectStandardInput = false;
            this._startInfo.RedirectStandardOutput = true;

            // Setup the process.
            this._ipconfigProc = new Process();
            this._ipconfigProc.StartInfo = this._startInfo;
        }

        /// <summary>
        /// Monitors the process and reads it's output. This method should only
        /// execute in the reader thread.
        /// </summary>
        protected override void ProcessReader() {
            var errMsg = String.Empty;
            try {
                // Start the process and raise the running event.
                this._ipconfigProc.Start();
                base.OnProcessStarted(new ProcessStartedEventArgs(this._ipconfigProc.Id));

                // Read the output until the process terminates.
                var output = this._ipconfigProc.StandardOutput.ReadLine();
                while ((output != null) && (!base.WasCancelled)) {
                    // Notify output listeners.
                    base.OnOutputReceived(new ProcessOutputEventArgs(output));
                    output = this._ipconfigProc.StandardOutput.ReadLine();
                }

                // Wait for the process to finish up, then get the exit code.
                this._ipconfigProc.WaitForExit();
                base._exitCode = this._ipconfigProc.ExitCode;

                // If the process exit code was 0, then the process completed successfully.
                if (base._exitCode == 0) {
                    base.OnProcessFinished(new ProccessDoneEventArgs(base._exitCode));
                }
                else {
                    errMsg = "The ipconfig process failed. Exit code: " + this._exitCode.ToString();
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
                if (this._ipconfigProc != null) {
                    this._ipconfigProc.Close();
                    this._ipconfigProc.Dispose();
                }

                lock (_syncLock) {
                    base._isRunning = true;
                }
            }
        }

        /// <summary>
        /// Launches the ipconfig process on a separate thread.
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// The ipconfig executable could not be found.
        /// </exception>
        public override void Start() {
            if (base._isRunning) {
                return;
            }

            this.Preflight();
            this._ipconfigReader = new Thread(new ThreadStart(this.ProcessReader));
            this._ipconfigReader.IsBackground = true;
            this._ipconfigReader.Name = "ipconfigReader";
            this._ipconfigReader.Start();
            base.Start();
        }

        /// <summary>
        /// Cancels the current ipconfig if it is running. This will attempt to
        /// gracefully terminate the process first, and then force-kill the
        /// process if graceful termination fails.
        /// </summary>
        /// <exception cref="Win32Exception">
        /// Unable to terminate process.
        /// </exception>
        public override void Cancel() {
            if ((!base.WasCancelled) || (base.IsRunning)) {
                if (this._ipconfigProc != null) {
                    try {
                        // Try to do this gracefully first.
                        base.Cancel();
                        Thread.Sleep(1000);

                        // Force terminate.
                        if (!this._ipconfigProc.HasExited) {
                            this._ipconfigProc.Kill();
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
                        this._ipconfigProc.Close();
                    }
                }
            }
        }
        #endregion
    }
}
