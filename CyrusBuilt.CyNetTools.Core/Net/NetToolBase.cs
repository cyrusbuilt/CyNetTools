using System;
using System.IO;

namespace CyrusBuilt.CyNetTools.Core.Net
{
    /// <summary>
    /// Base class for all tools that wrap Microsoft's Net utility (net.exe),
    /// which provides the 'net' command (ie. net view, net use, etc).
    /// </summary>
    public abstract class NetToolBase : ExternalToolBase
    {
        private FileInfo _exec = null;

        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Core.Net.NetToolBase</b>
        /// class. This is the default constructor.
        /// </summary>
        protected NetToolBase()
            : base() {
                String execPath = Path.Combine(Environment.SystemDirectory, "net.exe");
                this._exec = new FileInfo(execPath);
        }

        /// <summary>
        /// Gets the net executable file.
        /// </summary>
        protected FileInfo NetExec {
            get { return this._exec; }
        }
    }
}
