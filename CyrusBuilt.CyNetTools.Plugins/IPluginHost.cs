using System;

namespace CyrusBuilt.CyNetTools.Plugins
{
    /// <summary>
    /// A plugin host interface. Any type that will act as a host for plugin
    /// assemblies must implement this interface.
    /// </summary>
    public interface IPluginHost
    {
        /// <summary>
        /// Initializes the plugin host. This will perform necessary tasks for
        /// initializing plugin reference storage.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Loads the specified plugin's configuration.
        /// </summary>
        /// <param name="plugin">
        /// The plugin to load the configuration from.
        /// </param>
        /// <returns>
        /// If successful, the plugin's configuration; Otherwise, null. A null
        /// return can occur if the specified plugin does not have any settings
        /// to configure.
        /// </returns>
        PluginConfiguration LoadPluginConfiguration(IPlugin plugin);

        /// <summary>
        /// Saves the specified plugin's configuration.
        /// </summary>
        /// <param name="plugin">
        /// The plugin containing the configuration to save.
        /// </param>
        /// <param name="config">
        /// The configuration to save.
        /// </param>
        void SavePluginConfiguration(IPlugin plugin, PluginConfiguration config);

        /// <summary>
        /// Gets the plugin's configuration dialog.
        /// </summary>
        /// <param name="plugin">
        /// The plugin to get the configuration from.
        /// </param>
        /// <returns>
        /// A dialog form containing the settings read from the specified
        /// plugin's configuration.
        /// </returns>
        FormSettingsDialog GetConfigurationDialog(AvailablePlugin plugin);
    }
}
