using System;
using System.Windows.Forms;

namespace CyrusBuilt.CyNetTools.Plugins
{
    /// <summary>
    /// A plugin interface. All classes representing listener plugins for the
    /// Event Dispatch Coordinator must contain a type that implements this
    /// interface.
    /// </summary>
    public interface IPlugin : IDisposable
    {
        #region Properties
        /// <summary>
        /// Gets whether or not the plugin instance is disposed.
        /// </summary>
        Boolean IsDisposed { get; }

        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Gets the plugin's author.
        /// </summary>
        String Author { get; }

        /// <summary>
        /// Gets the plugin's copyright info.
        /// </summary>
        String Copyright { get; }

        /// <summary>
        /// Gets the plugin version.
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Gets a decription of the plugin.
        /// </summary>
        String Description { get; }

        /// <summary>
        /// Gets or sets plugin host. This would be the application (class)
        /// that implements <see cref="IPluginHost"/> for using this plugin.
        /// </summary>
        IPluginHost Host { get; set; }

        /// <summary>
        /// Gets a flag to indicate whether or not the plugin has been
        /// initialized.
        /// </summary>
        Boolean IsInitialized { get; }

        /// <summary>
        /// Gets or sets the instance index. This is useful for save file dialogs.
        /// </summary>
        Int32 Index { get; set; }

        /// <summary>
        /// Gets a flag to indicate whether or no the plugin is busy processing.
        /// </summary>
        Boolean IsBusy { get; }

        /// <summary>
        /// Gets or sets the object to tag this instance with.
        /// </summary>
        Object Tag { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the plugin.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Gets the plugin's configuration.
        /// </summary>
        /// <returns>
        /// The plugin configuration.
        /// </returns>
        PluginConfiguration GetConfiguration();

        /// <summary>
        /// Saves the plugin's configuration.
        /// </summary>
        /// <param name="config">
        /// The plugin configuration to save to the plugin's configuration file.
        /// </param>
        void SaveConfiguration(PluginConfiguration config);

        /// <summary>
        /// Displays the main form of the plugin's UI.
        /// </summary>
        /// <param name="mdiParent">
        /// The form that is the MDI parent of the plugin's main UI form.
        /// </param>
        /// <returns>
        /// A reference to the displayed form.
        /// </returns>
        Form ShowUI(Form mdiParent);
        #endregion
    }
}
