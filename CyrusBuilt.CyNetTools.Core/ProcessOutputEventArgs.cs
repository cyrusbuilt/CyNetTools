using System;

namespace CyrusBuilt.CyNetTools.Core
{
    /// <summary>
    /// Process output event arguments class.
    /// </summary>
    public class ProcessOutputEventArgs : EventArgs
    {
        private String _output = String.Empty;

        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.ProcessOutputEventArgs</b>
        /// class with the output received from stdout and/or stderr.
        /// </summary>
        /// <param name="output">
        /// The process output string.
        /// </param>
        public ProcessOutputEventArgs(String output)
            : base() {
                this._output = output;
        }

        /// <summary>
        /// Gets the output returned by the process.
        /// </summary>
        public String StandardOutput {
            get { return this._output; }
        }
    }
}
