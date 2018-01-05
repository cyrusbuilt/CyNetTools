using System;


namespace CyrusBuilt.CyNetTools.Core
{
    /// <summary>
    /// Process started event arguments class.
    /// </summary>
    public class ProcessStartedEventArgs : EventArgs
    {
        private Int32 _pid = 0;

        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.ProcessStartedEventArgs</b>
        /// class with the process ID (PID).
        /// </summary>
        /// <param name="pid">
        /// The ID of the process.
        /// </param>
        public ProcessStartedEventArgs(Int32 pid)
            : base() {
                this._pid = pid;
        }

        /// <summary>
        /// Gets the ID of the process (PID).
        /// </summary>
        public Int32 ProcessID {
            get { return this._pid; }
        }
    }
}
