using System;

namespace CyrusBuilt.CyNetTools.Core.Ping
{
    /// <summary>
    /// Ping progress event arguments class.
    /// </summary>
    public sealed class PingProgressEventArgs : EventArgs
    {
        #region Fields
        private Int32 _completed = 0;
        private Int32 _succeeded = 0;
        private Int32 _failed = 0;
        private Int32 _total = 0;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.Ping.PingProgressEventArgs</b>
        /// class with the count of completed pings, count of successful pings,
        /// count of failed pings, and total number of pings.
        /// </summary>
        /// <param name="completed">
        /// The count of pings completed.
        /// </param>
        /// <param name="succeeded">
        /// The count of successful pings.
        /// </param>
        /// <param name="failed">
        /// The count of failed pings.
        /// </param>
        /// <param name="total">
        /// The total number of pings.
        /// </param>
        public PingProgressEventArgs(Int32 completed, Int32 succeeded, Int32 failed, Int32 total)
            : base() {
                this._completed = completed;
                this._succeeded = succeeded;
                this._failed = failed;
                this._total = total;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the number of ping requests completed.
        /// </summary>
        public Int32 RequestsCompleted {
            get { return this._completed; }
        }

        /// <summary>
        /// Gets the number of successful ping requests.
        /// </summary>
        public Int32 RequestsSucceeded {
            get { return this._succeeded; }
        }

        /// <summary>
        /// Gets the number of failed requests.
        /// </summary>
        public Int32 RequestsFailed {
            get { return this._failed; }
        }

        /// <summary>
        /// Gets the total number of ping requests.
        /// </summary>
        public Int32 TotalRequests {
            get { return this._total; }
        }

        /// <summary>
        /// Gets the percentage of packet loss.
        /// </summary>
        public Int32 PacketLoss {
            get {
                if (this._total > 0) {
                    return ((this._failed * 100) / this._total);
                }
                return 0;
            }
        }

        /// <summary>
        /// Gets the percentage of completed requests.
        /// </summary>
        public Int32 PercentComplete {
            get {
                if (this._total > 0) {
                    return ((this._completed * 100) / this._total);
                }
                return 0;
            }
        }
        #endregion
    }
}
