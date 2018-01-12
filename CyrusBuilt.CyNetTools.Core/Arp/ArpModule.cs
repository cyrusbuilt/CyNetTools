using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace CyrusBuilt.CyNetTools.Core.Arp
{
    /// <summary>
    /// Provides a threaded wrapper for the Microsoft ARP utility.
    /// </summary>
    public class ArpModule : ExternalToolBase
    {
        #region Fields
        private Process _arpProc = null;
        private ProcessStartInfo _startInfo = null;
        private Thread _arpMonitor = null;
        private IPAddress _inetAddr = null;
        private IPAddress _ifAddr = null;
        private PhysicalAddress _ethAddr = null;
        private ArpCommand _command = ArpCommand.None;
        private Boolean _verbose = false;
        private FileInfo _exec = null;
        private static readonly Object _syncLock = new Object();
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.Arp.ArpModule</b>
        /// class. This is the default constructor.
        /// </summary>
        public ArpModule()
            : base() {
                var execPath = Path.Combine(Environment.SystemDirectory, "arp.exe");
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
                if (this._arpProc != null) {
                    try {
                        this._arpProc.Kill();
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
                        this._arpProc.Close();
                        this._arpProc.Dispose();
                        this._arpProc = null;
                    }
                }
            }

            if (this._arpMonitor != null) {
                if (this._arpMonitor.IsAlive) {
                    try {
                        this._arpMonitor.Abort();
                    }
                    catch (ThreadAbortException) {
                    }
                }
                this._arpMonitor = null;
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
        ~ArpModule() {
            this.Dispose(false);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a flag indicating whether or not to display current ARP
        /// entries in verbose mode. All valid entries (including those on the
        /// loopback interface) will be shown. Default is false.
        /// </summary>
        public Boolean Verbose {
            get { return this._verbose; }
            set { this._verbose = value; }
        }

        /// <summary>
        /// Gets or sets the ARP command to execute. Default is <see cref="ArpCommand.None"/>
        /// which will only show basic info.
        /// </summary>
        public ArpCommand Command {
            get { return this._command; }
            set { this._command = value; }
        }

        /// <summary>
        /// Used with <see cref="ArpModule.Command"/> to specify the internet
        /// address to add or delete a host entry for.
        /// </summary>
        public IPAddress InetAddress {
            get { return this._inetAddr; }
            set { this._inetAddr = value; }
        }

        /// <summary>
        /// Used with <see cref="ArpModule.Command"/> when the specified command
        /// is <see cref="ArpCommand.AddHost"/>. This is the physical (ethernet)
        /// address to associate with the specified <see cref="ArpModule.InetAddress"/>.
        /// </summary>
        public PhysicalAddress EthernetAddress {
            get { return this._ethAddr; }
            set { this._ethAddr = value; }
        }

        /// <summary>
        /// Gets or sets the internet address of the interface whose address
        /// translation table should be modified. If null, the first applicable
        /// interface will be used.
        /// </summary>
        public IPAddress InterfaceAddress {
            get { return this._ifAddr; }
            set { this._ifAddr = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Performs any neccessary preflight work prior to arp execution.
        /// This method is intended to validate parameters and input and throw
        /// the appropriate exception if any invalid parameter values are
        /// detected or if the instance instance is in an invalid state.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// An attempt was made to add a host with a missing IP and/or ethernet
        /// address - or - an attempt was made to delete a host without providing
        /// and IP address.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The arp executable could not be found.
        /// </exception>
        protected override void Preflight() {
            base._exitCode = 0;
            base._wasCancelled = false;
            base._isRunning = false;

            // **** Perform sanity checks ****
            var args = ArpUtils.GetArpCommandString(this._command);
            switch (this._command) {
                case ArpCommand.ShowAll:
                    if ((this._inetAddr != null) && (this._inetAddr != IPAddress.None)) {
                        args += " " + this._inetAddr.ToString();
                    }
                    break;
                case ArpCommand.ShowForInterface:
                    if ((this._ifAddr != null) && (this._ifAddr != IPAddress.None)) {
                        args += this._ifAddr.ToString();
                    }
                    break;
                case ArpCommand.AddHost:
                    if ((this._inetAddr != null) && (this._inetAddr != IPAddress.None) &&
                        (this._ethAddr != null) && (this._ethAddr != PhysicalAddress.None)) {
                        args += " " + this._inetAddr.ToString() + " " + this._ethAddr.ToString();
                    }
                    else {
                        throw new InvalidOperationException("Cannot add host. Missing IP address and/or physical address.");
                    }

                    if ((this._ifAddr != null) && (this._ifAddr != IPAddress.None)) {
                        args += " " + this._ifAddr.ToString();
                    }
                    break;
                case ArpCommand.DeleteHost:
                    if ((this._inetAddr != null) && (this._inetAddr != IPAddress.None)) {
                        args += this._inetAddr.ToString();
                    }
                    else {
                        throw new InvalidOperationException("Cannot delete host. No IP address specified.");
                    }

                    if ((this._ifAddr != null) && (this._ifAddr != IPAddress.None)) {
                        args += " " + this._ifAddr.ToString();
                    }
                    break;
                case ArpCommand.None:
                default:
                    break;
            }

            // Enable verbosity?
            if (this._verbose) {
                args += " -v";
            }

            // Validate exec path.
            this._startInfo = new ProcessStartInfo();
            if (this._exec.Exists) {
                this._startInfo.FileName = this._exec.FullName;
                this._startInfo.WorkingDirectory = this._exec.DirectoryName;
            }
            else {
                throw new FileNotFoundException("ARP executable not found.", this._exec.FullName);
            }

            // Destroy previous process object (if present).
            if (this._arpProc != null) {
                this._arpProc.Close();
                this._arpProc.Dispose();
            }

            // Setup remaining start parameters.
            this._startInfo.UseShellExecute = true;
            this._startInfo.RedirectStandardError = false;
            this._startInfo.RedirectStandardInput = false;
            this._startInfo.RedirectStandardOutput = true;
            this._startInfo.CreateNoWindow = true;
            this._startInfo.Arguments = args;

            // Setup the process.
            this._arpProc = new Process();
            this._arpProc.StartInfo = this._startInfo;
        }

        /// <summary>
        /// Monitors the process and reads it's output. This method should only
        /// execute in the reader thread.
        /// </summary>
        protected override void ProcessReader() {
            var errMsg = String.Empty;
            try {
                // Start the process and raise the running event.
                this._arpProc.Start();
                base.OnProcessStarted(new ProcessStartedEventArgs(this._arpProc.Id));

                // Read the output until the process terminates.
                var output = this._arpProc.StandardOutput.ReadLine();
                while ((output != null) & (!base.WasCancelled)) {
                    // Notify output listeners.
                    base.OnOutputReceived(new ProcessOutputEventArgs(output));
                    output = this._arpProc.StandardOutput.ReadLine();
                }

                // Wait for the process to finish up, then get the exit code.
                this._arpProc.WaitForExit();
                base._exitCode = this._arpProc.ExitCode;

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
                if (this._arpProc != null) {
                    this._arpProc.Close();
                    this._arpProc.Dispose();
                }

                lock (_syncLock) {
                    base._isRunning = true;
                }
            }
        }

        /// <summary>
        /// Launches the arp process on a separate thread.
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// The arp executable could not be found.
        /// </exception>
        public override void Start() {
            if (base.IsRunning) {
                return;
            }

            this.Preflight();
            this._arpMonitor = new Thread(new ThreadStart(this.ProcessReader));
            this._arpMonitor.IsBackground = true;
            this._arpMonitor.Name = "arpReader";
            this._arpMonitor.Start();
            base.Start();
        }

        /// <summary>
        /// Cancels the current arp if it is running. This will attempt to
        /// gracefully terminate the process first, and then force-kill the
        /// process if graceful termination fails.
        /// </summary>
        /// <exception cref="Win32Exception">
        /// Unable to terminate process.
        /// </exception>
        public override void Cancel() {
            if ((!base.WasCancelled) || (base.IsRunning)) {
                if (this._arpProc != null) {
                    try {
                        // Try to do this gracefully first.
                        base.Cancel();
                        Thread.Sleep(1000);

                        // Force terminate.
                        if (!this._arpProc.HasExited) {
                            this._arpProc.Kill();
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
                        this._arpProc.Close();
                    }
                }
            }
        }
        #endregion
    }
}
