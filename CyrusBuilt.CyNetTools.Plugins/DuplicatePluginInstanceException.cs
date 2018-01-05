using System;

namespace CyrusBuilt.CyNetTools.Plugins
{
    /// <summary>
    /// The exception that is thrown when a duplicate plugin instance is detected.
    /// </summary>
    public class DuplicatePluginInstanceException : Exception
    {
        private IPlugin _instance = null;

        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Plugins.DuplicatePluginInstanceException</b>
        /// class with the plugin that is a duplicate.
        /// </summary>
        /// <param name="plugin">
        /// The plugin that is an instance duplicate of another.
        /// </param>
        public DuplicatePluginInstanceException(IPlugin plugin)
            : base("Duplicate plugin instance detected: " + plugin.Name + " v" + plugin.Version.ToString()) {
            this._instance = plugin;
        }

        /// <summary>
        /// Gets the plugin duplicate.
        /// </summary>
        public IPlugin PluginDuplicate {
            get { return this._instance; }
        }
    }
}
