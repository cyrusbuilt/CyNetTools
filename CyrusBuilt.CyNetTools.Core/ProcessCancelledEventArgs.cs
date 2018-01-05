using System;

namespace CyrusBuilt.CyNetTools.Core
{
    /// <summary>
    /// Process cancelled event arguments class.
    /// </summary>
    public class ProcessCancelledEventArgs : EventArgs
    {
        private Exception _cancelCause = null;

        /// <summary>
        /// Provides a value to use with cancellation events that do not have
        /// event data.
        /// </summary>
        public static readonly new ProcessCancelledEventArgs Empty = new ProcessCancelledEventArgs();

        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.ProcessCancelledEventArgs</b>
        /// class. This is the default constructor.
        /// </summary>
        public ProcessCancelledEventArgs()
            : base() {
        }

        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.ProcessCancelledEventArgs</b>
        /// class with the exception that is the cause of the cancellation.
        /// </summary>
        /// <param name="cancelCause">
        /// The exception that is the cause of the cancellation.
        /// </param>
        public ProcessCancelledEventArgs(Exception cancelCause)
            : base() {
                this._cancelCause = cancelCause;
        }

        /// <summary>
        /// Gets the exception that is the cause of the cancellation. Returns
        /// null if not cancelled due to exception being thrown (user initiated
        /// cancellation).
        /// </summary>
        public Exception CancelCause {
            get { return this._cancelCause; }
        }
    }
}
