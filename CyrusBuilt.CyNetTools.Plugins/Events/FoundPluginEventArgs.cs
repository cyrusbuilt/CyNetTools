using System;

namespace CyrusBuilt.CyNetTools.Plugins.Events
{
    /// <summary>
    /// Plugin found event arguments class.
    /// </summary>
    public class FoundPluginEventArgs : EventArgs
    {
        private String _assemblyPath = String.Empty;

        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Plugins.Events.FoundPluginEventArgs</b>
        /// class with the full path to the plugin assembly found.
        /// </summary>
        /// <param name="assemblyPath">
        /// The full path to the plugin assembly.
        /// </param>
        public FoundPluginEventArgs(String assemblyPath)
            : base() {
            this._assemblyPath = assemblyPath;
        }

        /// <summary>
        /// Gets the full path to the plugin assembly.
        /// </summary>
        public String AssemblyPath {
            get { return this._assemblyPath; }
        }
    }
}
