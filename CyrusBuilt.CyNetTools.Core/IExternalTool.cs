using System;

namespace CyrusBuilt.CyNetTools.Core
{
    /// <summary>
    /// This interface is intended to be implemented by all external tool wrappers.
    /// All tool modules that execute external processes (especially to read
    /// their output) should implement this interface. Most CyNetTools core tool
    /// modules implement this and it is expected that most plugins will too.
    /// </summary>
    public interface IExternalTool : IDisposable
    {
        #region Events
        /// <summary>
        /// Occurs when the external process has started running.
        /// </summary>
        event ProcessRunningEventHandler ProcessStarted;

        /// <summary>
        /// Occurs when output is received from the process.
        /// </summary>
        event ProcessOutputEventHandler OutputReceived;

        /// <summary>
        /// Occurs when the process is cancelled.
        /// </summary>
        event ProcessCancelledEventHandler ProcessCancelled;

        /// <summary>
        /// Occurs when the process completes.
        /// </summary>
        event ProcessDoneEventHandler ProcessFinished;
        #endregion

        #region Properties
        /// <summary>
        /// Gets whether or not the process is running.
        /// </summary>
        Boolean IsRunning { get; }

        /// <summary>
        /// Gets whether or not this instance has been disposed.
        /// </summary>
        Boolean IsDisposed { get; }

        /// <summary>
        /// Gets whether or not the process was cancelled by the user.
        /// </summary>
        Boolean WasCancelled { get; }

        /// <summary>
        /// Gets the exit code returned by the process. Always returns zero
        /// until the process terminates.
        /// </summary>
        Int32 ExitCode { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Launches the external process on a separate thread.
        /// </summary>
        void Start();

        /// <summary>
        /// Cancels the current process if it is running. This will attempt to
        /// gracefully terminate the process first, and then force-kill the
        /// process if graceful termination fails.
        /// </summary>
        void Cancel();
        #endregion
    }
}
