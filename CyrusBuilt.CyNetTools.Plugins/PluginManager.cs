using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CyrusBuilt.CyNetTools.Plugins.Events;

namespace CyrusBuilt.CyNetTools.Plugins
{
    /// <summary>
    /// Manages the locating, loading, and unloading of plugins. This class is
    /// a singleton because there should only be one plugin manager instance
    /// for the entire lifetime of the application.
    /// </summary>
    public sealed class PluginManager : IPluginHost, IDisposable
    {
        #region Fields
        private String _startupPath = String.Empty;
        private AvailablePluginCollection _plugins = null;
        private static Boolean _initialized = false;
        private static Boolean _isDisposed = false;
        private static Int32 _refCount = 0;
        private static readonly Object _padlock = new Object();
        private static volatile PluginManager _instance = null;
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a plugin is found.
        /// </summary>
        public event FoundPluginEventHandler FoundPlugin;

        /// <summary>
        /// Occurs when a plugin is loaded.
        /// </summary>
        public event PluginLoadedEventHandler PluginLoaded;
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Plugins.PluginManager</b>
        /// class. This is the default constructor.
        /// </summary>
        private PluginManager() {
            this._startupPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// Gaurantees a singleton instance of this class (thread safe).
        /// </summary>
        /// <returns>
        /// The first call to this instantiator will return a new instance of this
        /// class object.  Any additional calls that follow will return a reference
        /// to the already created object instance (double-check locking method).
        /// </returns>
        public static PluginManager Instance {
            get {
                lock (_padlock) {
                    if (_instance == null) {
                        _instance = new PluginManager();
                        _isDisposed = false;
                    }
                    _refCount++;
                    return _instance;
                }
            }
        }

        /// <summary>
        /// Method for disposing object references, only if all other references
        /// to this class have already been disposed.  This also disposes
        /// managed resources.
        /// </summary>
        /// <param name="disposing">
        /// Flag for indicating that this was called from the public <see cref="Dispose()"/>
        /// method, and thus we really do want to dispose this class reference.
        /// </param>
        private void Dispose(Boolean disposing) {
            if (_isDisposed) {
                return;
            }

            if (disposing) {
                if (_refCount == 0) {
                    _instance = null;
                    _initialized = false;
                    if (this._plugins != null) {
                        foreach (AvailablePlugin p in this._plugins) {
                            if ((p.Instance.IsInitialized) || (!p.Instance.IsDisposed)) {
                                p.Instance.Dispose();
                            }
                        }
                        this._plugins.Clear();
                        this._plugins = null;
                    }
                    _isDisposed = true;
                }
            }
        }

        /// <summary>
        /// Decrements the count of references to this class, then releases
        /// all resources used by this class when all other references have
        /// been disposed. Managed resources are disposed first.
        /// </summary>
        public void Dispose() {
            _refCount--;
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the full path to the directory where the plugin manager assembly is located.
        /// </summary>
        public String StartupPath {
            get { return this._startupPath; }
        }

        /// <summary>
        /// Gets the collection of all plugins found and loaded by the
        /// <see cref="FindPlugins()"/> method.
        /// </summary>
        public AvailablePluginCollection AvailablePlugins {
            get { return this._plugins; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Raises the <see cref="FoundPlugin"/> event.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void OnPluginFound(FoundPluginEventArgs e) {
            if (this.FoundPlugin != null) {
                this.FoundPlugin(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="PluginLoaded"/> event.
        /// </summary>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void OnPluginLoaded(PluginLoadedEventArgs e) {
            if (this.PluginLoaded != null) {
                this.PluginLoaded(this, e);
            }
        }

        /// <summary>
        /// Initializes the plugin manager.
        /// </summary>
        public void Initialize() {
            if (_initialized) {
                return;
            }
            this._plugins = new AvailablePluginCollection();
            _initialized = true;
        }

        /// <summary>
        /// Loads the specified plugin and adds it to the managed plugin
        /// collection.
        /// </summary>
        /// <param name="file">
        /// The assembly (*.dll file) that is the plugin.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        /// This instance has been disposed.
        /// </exception>
        private void AddPlugin(FileInfo file) {
            if (_isDisposed) {
                throw new ObjectDisposedException("PluginManager");
            }

            if ((file == null) || (!file.Exists)) {
                return;
            }

            Type typeInterface = null;
            IPlugin instance = null;
            AvailablePlugin newPlugin = null;
            var pluginAssembly = Assembly.LoadFrom(file.FullName);
            foreach (var pluginType in pluginAssembly.GetTypes()) {
                if ((pluginType.IsPublic) && (!pluginType.IsAbstract)) {
                    typeInterface = pluginType.GetInterface("MAT.EventDispatchCoordinator.Plugins.IPlugin", true);
                    if (typeInterface != null) {
                        // Load the assembly instance if it is a valid plugin.
                        this.OnPluginFound(new FoundPluginEventArgs(file.FullName));
                        instance = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));

                        // Initialize the plugin and add it to the managed collection.
                        newPlugin = new AvailablePlugin(instance, file.FullName);
                        newPlugin.Instance.Host = this;
                        newPlugin.Instance.Initialize();
                        _plugins.Add(newPlugin);
                        this.OnPluginLoaded(new PluginLoadedEventArgs(instance.Name, instance.Version));
                    }
                }
            }
        }

        /// <summary>
        /// Finds and loads plugins located in the specified directory.
        /// </summary>
        /// <param name="directory">
        /// The directory where the plugins are located.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// The plugin manager is not intialized.
        /// </exception>
        public void FindPlugins(DirectoryInfo directory) {
            if (!_initialized) {
                throw new InvalidOperationException("PluginManager has not been initialized.");
            }

            if (directory == null) {
                return;
            }

            this._plugins.Clear();
            if (directory.Exists) {
                foreach (var fi in directory.GetFiles("*.dll")) {
                    this.AddPlugin(fi);
                }
            }
        }

        /// <summary>
        /// Finds and loads plugins located in the same directory as this
        /// assembly.
        /// </summary>
        public void FindPlugins() {
            this.FindPlugins(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory));
        }

        /// <summary>
        /// Closes all loaded plugins.
        /// </summary>
        public void ClosePlugins() {
            if ((!_initialized) || (_isDisposed)) {
                return;
            }

            foreach (AvailablePlugin plugin in this._plugins) {
                plugin.Instance.Dispose();
            }
            this._plugins.Clear();
        }

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
        /// <exception cref="ArgumentNullException">
        /// <paramref name="plugin"/> cannot be null.
        /// </exception>
        public PluginConfiguration LoadPluginConfiguration(IPlugin plugin) {
            if (plugin == null) {
                throw new ArgumentNullException("plugin");
            }
            return plugin.GetConfiguration();
        }

        /// <summary>
        /// Saves the specified plugin's configuration.
        /// </summary>
        /// <param name="plugin">
        /// The plugin containing the configuration to save.
        /// </param>
        /// <param name="config">
        /// The configuration to save.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="plugin"/> cannot be null.
        /// </exception>
        public void SavePluginConfiguration(IPlugin plugin, PluginConfiguration config) {
            if (plugin == null) {
                throw new ArgumentNullException("plugin");
            }

            if (config != null) {
                if ((!config.IsEmpty) && (config.IsDirty)) {
                    plugin.SaveConfiguration(config);
                    config.ClearDirty();
                }
            }
        }

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
        /// <exception cref="ArgumentNullException">
        /// <paramref name="plugin"/> cannot be null.
        /// </exception>
        public FormSettingsDialog GetConfigurationDialog(AvailablePlugin plugin) {
            if (plugin == null) {
                throw new ArgumentNullException("plugin");
            }
            return new FormSettingsDialog(plugin);
        }
        #endregion
    }
}
