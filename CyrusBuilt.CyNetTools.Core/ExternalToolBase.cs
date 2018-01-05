using System;

namespace CyrusBuilt.CyNetTools.Core
{
    /// <summary>
    /// Base implementation of the <see cref="IExternalTool"/> interface. This
    /// is a base class and is intended to implemented by classes that act as
    /// wrappers for external tool processes (ie. ping.exe).  Specifically, this
    /// class should be used in implementations that wrap command-line (CLI)
    /// executables (or scripts) whose output can then be read and interpreted
    /// by the implementation.
    /// </summary>
    public abstract class ExternalToolBase : IExternalTool
    {
        #region Fields
        private Boolean _isDisposed = false;
        protected volatile Boolean _wasCancelled = false;
        protected volatile Boolean _isRunning = false;
        protected Int32 _exitCode = 0;
        private static readonly Object _syncLock = new Object();
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the external process has started running.
        /// </summary>
        public event ProcessRunningEventHandler ProcessStarted;

        /// <summary>
        /// Occurs when output is received from the process.
        /// </summary>
        public event ProcessOutputEventHandler OutputReceived;

        /// <summary>
        /// Occurs when the process is cancelled.
        /// </summary>
        public event ProcessCancelledEventHandler ProcessCancelled;

        /// <summary>
        /// Occurs when the process completes.
        /// </summary>
        public event ProcessDoneEventHandler ProcessFinished;
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.ExternalToolBase</b>
        /// class. This is the default constructor.
        /// </summary>
        protected ExternalToolBase() {
        }

        /// <summary>
        /// Releases all resources used by this component. This method is
        /// intended to be overridden in the implementing class, and then
        /// called by the overriding method to set the disposal flag prior
        /// to returning to the caller.
        /// </summary>
        public virtual void Dispose() {
            if (this._isDisposed) {
                return;
            }

            this._isDisposed = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets whether or not this instance has been disposed.
        /// </summary>
        public Boolean IsDisposed {
            get { return this._isDisposed; }
        }

        /// <summary>
        /// Gets whether or not the process is running.
        /// </summary>
        public Boolean IsRunning {
            get { return this._isRunning; }
        }

        /// <summary>
        /// Gets whether or not the process was cancelled by the user.
        /// </summary>
        public Boolean WasCancelled {
            get { return this._wasCancelled; }
        }

        /// <summary>
        /// Gets the exit code returned by the process. Always returns zero
        /// until the process terminates.
        /// </summary>
        public Int32 ExitCode {
            get { return this._exitCode; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Raises the <see cref="ProcessStarted"/> event.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        protected virtual void OnProcessStarted(ProcessStartedEventArgs e) {
            if (this.ProcessStarted != null) {
                this.ProcessStarted(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="OutputReceived"/> event.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        protected virtual void OnOutputReceived(ProcessOutputEventArgs e) {
            if (this.OutputReceived != null) {
                this.OutputReceived(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="ProcessCancelled"/> event.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        protected virtual void OnProcessCancelled(ProcessCancelledEventArgs e) {
            if (this.ProcessCancelled != null) {
                this.ProcessCancelled(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="ProcessFinished"/> event.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        protected virtual void OnProcessFinished(ProccessDoneEventArgs e) {
            if (this.ProcessFinished != null) {
                this.ProcessFinished(this, e);
            }
        }

        /// <summary>
        /// Performs any neccessary preflight work prior to process execution.
        /// This method is intended to validate parameters and input and throw
        /// the appropriate exception if any invalid parameter values are
        /// detected or if the instance instance is in an invalid state.
        /// </summary>
        protected abstract void Preflight();

        /// <summary>
        /// Monitors the process and reads it's output. This method should only
        /// execute in the reader thread.
        /// </summary>
        protected abstract void ProcessReader();

        /// <summary>
        /// Launches the external process on a separate thread. This base
        /// implementation performs no real work other than setting the run
        /// flag. This method is intended to be overridden in the implementing
        /// class. This should create and run the thread that the external
        /// process will run in, then call this base method prior to returning
        /// to the caller.
        /// </summary>
        public virtual void Start() {
            if (this._isRunning) {
                return;
            }

            lock (_syncLock) {
                this._isRunning = true;
            }
        }

        /// <summary>
        /// Cancels the current process if it is running. This will attempt to
        /// gracefully terminate the process first, and then force-kill the
        /// process if graceful termination fails. This method is intended to
        /// be overridden by the implementing class to terminate the underlying
        /// process and then call this base method prior to return to the caller
        /// in order to set the cancel flag. This method contains no process
        /// termination logic of its own.
        /// </summary>
        public virtual void Cancel() {
            if (this._wasCancelled) {
                return;
            }

            lock (_syncLock) {
                this._wasCancelled = true;
            }
        }
        #endregion
    }
}
