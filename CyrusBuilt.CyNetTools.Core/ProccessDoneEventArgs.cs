using System;

namespace CyrusBuilt.CyNetTools.Core
{
    /// <summary>
    /// Process done event arguments class.
    /// </summary>
    public class ProccessDoneEventArgs : EventArgs
    {
        private Int32 _exitCode = 0;
        private Boolean _cancelled = false;

        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.ProccessDoneEventArgs</b>
        /// class with the exit code returned by the process.
        /// </summary>
        /// <param name="exitCode">
        /// The exit (status/error) code returned by the process.
        /// </param>
        public ProccessDoneEventArgs(Int32 exitCode)
            : base() {
                this._exitCode = exitCode;
        }

        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.ProccessDoneEventArgs</b>
        /// class with the exit code returned by the process and whether or not
        /// the process was cancelled by the user.
        /// </summary>
        /// <param name="exitCode">
        /// The exit (status/error) code returned by the process.
        /// </param>
        /// <param name="cancelled">
        /// A flag indicating whether or not the process was cancelled.
        /// </param>
        public ProccessDoneEventArgs(Int32 exitCode, Boolean cancelled)
            : base() {
                this._exitCode = exitCode;
                this._cancelled = cancelled;
        }

        /// <summary>
        /// Gets the exit code returned by the process when it terminated.
        /// </summary>
        public Int32 ExitCode {
            get { return this._exitCode; }
        }

        /// <summary>
        /// Gets a flag indicating whether or not the process was cancelled.
        /// </summary>
        public Boolean Cancelled {
            get { return this._cancelled; }
        }
    }
}
