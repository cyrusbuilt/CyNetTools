using System;

namespace CyrusBuilt.CyNetTools.Plugins.Events
{
    /// <summary>
    /// Plugin loaded event arguments class.
    /// </summary>
    public class PluginLoadedEventArgs : EventArgs
    {
        #region Fields
        private String _name = String.Empty;
        private Version _version = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Plugins.Events.PluginLoadedEventArgs</b>
        /// class with the name and version of the plugin.
        /// </summary>
        /// <param name="name">
        /// The plugin name.
        /// </param>
        /// <param name="v">
        /// The plugin version.
        /// </param>
        public PluginLoadedEventArgs(String name, Version v)
            : base() {
            this._name = name;
            this._version = v;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the plugin name.
        /// </summary>
        public String Name {
            get { return this._name; }
        }

        /// <summary>
        /// Gets the plugin version.
        /// </summary>
        public Version PluginVersion {
            get { return this._version; }
        }
        #endregion
    }
}
