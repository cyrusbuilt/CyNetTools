using System;

namespace CyrusBuilt.CyNetTools.Plugins
{
    /// <summary>
    /// Data class for an available plugin. Holds an instance of the loaded
    /// plugin, as well as the plugin's assembly path.
    /// </summary>
    public class AvailablePlugin
    {
        #region Type Constants
        private const Int32 HASH_MULTIPLIER = 31;
        #endregion

        #region Fields
        private IPlugin _pluginInstance = null;
        private String _assemblyPath = String.Empty;
        #endregion

        #region Contructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Plugins.AvailablePlugin</b>
        /// class with the plugin instance and path to the plugin assembly.
        /// </summary>
        public AvailablePlugin(IPlugin instance, String path) {
            this._pluginInstance = instance;
            this._assemblyPath = path;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or the plugin instance.
        /// </summary>
        public IPlugin Instance {
            get { return this._pluginInstance; }
        }

        /// <summary>
        /// Gets the full path to the plugin assembly.
        /// </summary>
        public String AssemblyPath {
            get { return this._assemblyPath; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Provides a hashcode identifier of this instance.
        /// </summary>
        /// <returns>
        /// The hashcode identifier for this instance.
        /// </returns>
        public override Int32 GetHashCode() {
            unchecked {
                var verHash = this._pluginInstance.Version.GetHashCode();
                var nameHash = this._pluginInstance.Name.GetHashCode();
                var hashCode = base.GetType().GetHashCode();
                return (hashCode * HASH_MULTIPLIER) ^ verHash ^ nameHash;
            }
        }

        /// <summary>
        /// Gets a string that represents the current instance.
        /// </summary>
        /// <returns>
        /// A string representing the plugin instance name.
        /// </returns>
        public override string ToString() {
            return this._pluginInstance.Name;
        }

        /// <summary>
        /// Compares this instance to the specified object to see if they are
        /// equal.
        /// </summary>
        /// <param name="obj">
        /// The object to compare to.
        /// </param>
        /// <returns>
        /// true if the two are equal; Otherwise, false.
        /// </returns>
        public override Boolean Equals(Object obj) {
            if (obj == null) {
                return false;
            }

            var ap = obj as AvailablePlugin;
            if ((Object)ap == null) {
                return false;
            }

            return ((this._assemblyPath == ap.AssemblyPath) &&
                    (this._pluginInstance == ap.Instance));
        }

        /// <summary>
        /// Compares this instance to the specified plugin to see if they are
        /// equal.
        /// </summary>
        /// <param name="plugin">
        /// The plugin to compare to.
        /// </param>
        /// <returns>
        /// true if the two are equal; Otherwise, false.
        /// </returns>
        public Boolean Equals(AvailablePlugin plugin) {
            if (plugin == null) {
                return false;
            }

            if ((Object)plugin == null) {
                return false;
            }

            return ((this._assemblyPath == plugin.AssemblyPath) &&
                    (this._pluginInstance == plugin.Instance));
        }
        #endregion
    }
}
