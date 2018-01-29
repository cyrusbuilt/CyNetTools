using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

namespace CyrusBuilt.CyNetTools.Core.GetMac
{
    /// <summary>
    /// Provides a threaded wrapper for the Microsoft GetMac utility.
    /// </summary>
    public class GetMacModule : ExternalToolBase
    {
        #region Fields
        private Process _getMacProc = null;
        private Thread _procReader = null;
        private FileInfo _exec = null;
        private IPHostEntry _remoteHost = null;
        private NetworkCredential _remoteUserCreds = null;
        private GetMacOutputFormat _format = GetMacOutputFormat.Table;
        private Boolean _columnHeaderEnabled = true;
        private Boolean _verbose = false;
        private static readonly Object _flagLock = new Object();
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.GetMac.GetMacModule</b>
        /// class. This is the default constructor.
        /// </summary>
        public GetMacModule()
            : base() {
            var execPath = Path.Combine(Environment.SystemDirectory, "getmac.exe");
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
                if (this._getMacProc != null) {
                    try {
                        this._getMacProc.Kill();
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
                        this._getMacProc.Close();
                        this._getMacProc.Dispose();
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
        /// Class destructor.
        /// </summary>
        ~GetMacModule() {
            this.Dispose(false);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the remote host to get the MAC addresses of. If null,
        /// then reports the MAC addresses of the local host (default).
        /// </summary>
        public IPHostEntry RemoteHost {
            get { return this._remoteHost; }
            set { this._remoteHost = value; }
        }

        /// <summary>
        /// Gets or sets remote user credentials. Ignored if <see cref="GetMacModule.RemoteHost"/>
        /// is null. Default is null.
        /// </summary>
        public NetworkCredential RemoteHostCredentials {
            get { return this._remoteUserCreds; }
            set { this._remoteUserCreds = value; }
        }

        /// <summary>
        /// Gets or sets the output format. Default is <see cref="GetMacOutputFormat.Table"/>.
        /// </summary>
        public GetMacOutputFormat OutputFormat {
            get { return this._format; }
            set { this._format = value; }
        }

        /// <summary>
        /// Gets or sets whether or not to enable the column header when <see cref="GetMacModule.OutputFormat"/>
        /// is <see cref="GetMacOutputFormat.CSV"/> or <see cref="GetMacOutputFormat.Table"/>.
        /// Default is true.
        /// </summary>
        public Boolean ColumnHeaderEnabled {
            get { return this._columnHeaderEnabled; }
            set { this._columnHeaderEnabled = value; }
        }

        /// <summary>
        /// Gets or sets whether or not to use enable verbose mode. Default is
        /// false.
        /// </summary>
        public Boolean Verbose {
            get { return this._verbose; }
            set { this._verbose = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Performs any neccessary preflight work prior to getmac execution.
        /// This method is intended to validate parameters and input and throw
        /// the appropriate exception if any invalid parameter values are
        /// detected or if the instance instance is in an invalid state.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Remote host specified without remote user credentials.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The getmac executable could not be found.
        /// </exception>
        protected override void Preflight() {
            var args = String.Empty;
            base._exitCode = 0;
            base._wasCancelled = false;
            base._isRunning = false;

            if (this._remoteHost != null) {
                if (this._remoteUserCreds == null) {
                    throw new InvalidOperationException("Remote user credentials must be specified when connecting to a remote host.");
                }

                args += "/S ";
                if (String.IsNullOrEmpty(this._remoteHost.HostName)) {
                    args += this._remoteHost.AddressList[0].ToString();
                }
                else {
                    args += this._remoteHost.HostName;
                }

                args += " /U ";
                if (!String.IsNullOrEmpty(this._remoteUserCreds.Domain)) {
                    args += this._remoteUserCreds.Domain + @"\";
                }

                args += this._remoteUserCreds.UserName + " /P ";
                args += this._remoteUserCreds.Password;
            }

            args += " /FO " + GetMacUtils.GetOutputFormatString(this._format);
            if (!this._columnHeaderEnabled && this._format != GetMacOutputFormat.List) {
                args += " /NH";
            }

            if (this._verbose) {
                args += " /V";
            }

            // Destroy previous process object (if present).
            if (this._getMacProc != null) {
                this._getMacProc.Dispose();
            }

            // Validate exec path.
            this._getMacProc = new Process();
            if (this._exec.Exists) {
                this._getMacProc.StartInfo.FileName = this._exec.FullName;
                this._getMacProc.StartInfo.WorkingDirectory = this._exec.DirectoryName;
            }
            else {
                throw new FileNotFoundException("Getmac executable not found.", this._exec.FullName);
            }

            // Setup remaining start parameters.
            this._getMacProc.StartInfo.UseShellExecute = false;
            this._getMacProc.StartInfo.RedirectStandardError = false;
            this._getMacProc.StartInfo.RedirectStandardInput = false;
            this._getMacProc.StartInfo.RedirectStandardOutput = true;
            this._getMacProc.StartInfo.CreateNoWindow = true;
            this._getMacProc.StartInfo.Arguments = args;
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
                this._getMacProc.Start();
                base.OnProcessStarted(new ProcessStartedEventArgs(this._getMacProc.Id));

                // Read the output until the process terminates.
                output = this._getMacProc.StandardOutput.ReadLine();
                while(output != null && !base.WasCancelled) {
                    // Notify output listeners.
                    outArgs = new ProcessOutputEventArgs(output);
                    base.OnOutputReceived(outArgs);

                    // This loop is blocking, which is why we are using a thread.
                    output = this._getMacProc.StandardOutput.ReadLine();
                }

                // Wait for the process to finish up, then get the exit code.
                this._getMacProc.WaitForExit();
                base._exitCode = this._getMacProc.ExitCode;

                // If the process terminated normally, then notify listeners that
                // we're done. Otherwise, notify output and cancellation listeners.
                if (base._exitCode == 0) {
                    base.OnProcessFinished(new ProccessDoneEventArgs(base._exitCode));
                }
                else {
                    errMsg = "A getmac error occurred. Error code: " + base._exitCode.ToString();
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
                if (this._getMacProc != null) {
                    this._getMacProc.Dispose();
                }

                lock (_flagLock) {
                    base._isRunning = false;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Remote host specified without remote user credentials.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The ping executable could not be found.
        /// </exception>
        public override void Start() {
            if (base.IsRunning) {
                return;
            }

            this.Preflight();
            this._procReader = new Thread(new ThreadStart(this.ProcessReader));
            this._procReader.IsBackground = true;
            this._procReader.Name = "getmacReader";
            this._procReader.Start();
            base.Start();
        }

        /// <summary>
        /// gracefully terminate the process first, and then force-kill the
        /// process if graceful termination fails.
        /// </summary>
        /// <exception cref="Win32Exception">
        /// Unable to terminate process.
        /// </exception>
        public override void Cancel() {
            if ((!base.WasCancelled) || (base.IsRunning)) {
                if (this._getMacProc != null) {
                    try {
                        // Try to do this gracefully first.
                        base.Cancel();
                        Thread.Sleep(1000);

                        // Force terminate.
                        if (!this._getMacProc.HasExited) {
                            this._getMacProc.Kill();
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
                        this._getMacProc.Close();
                    }
                }
            }
        }
        #endregion
    }
}
