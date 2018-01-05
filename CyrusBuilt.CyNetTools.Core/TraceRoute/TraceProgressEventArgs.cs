using System;

namespace CyrusBuilt.CyNetTools.Core.TraceRoute
{
    /// <summary>
    /// Trace route progress event arguments class.
    /// </summary>
    public class TraceProgressEventArgs : EventArgs
    {
        private Int32 _currentHop = 0;
        private Int32 _totalHops = 0;

        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.TraceRoute.TraceProgressEventArgs</b>
        /// class with the current hop in progress and the total number of hops.
        /// </summary>
        /// <param name="currentHop">
        /// The current hop in progress.
        /// </param>
        /// <param name="totalHops">
        /// The total number of hops.
        /// </param>
        public TraceProgressEventArgs(Int32 currentHop, Int32 totalHops)
            : base() {
                this._currentHop = currentHop;
                this._totalHops = totalHops;
        }

        /// <summary>
        /// Gets the current hop in progress.
        /// </summary>
        public Int32 CurrentHop {
            get { return this._currentHop; }
        }

        /// <summary>
        /// Gets the total number of hops.
        /// </summary>
        public Int32 TotalHops {
            get { return this._totalHops; }
        }

        /// <summary>
        /// Gets the trace progress percentage.
        /// </summary>
        public Int32 Progress {
            get {
                if (this._totalHops > 0) {
                    return ((this._currentHop * 100) / this._totalHops);
                }
                return 0;
            }
        }
    }
}
