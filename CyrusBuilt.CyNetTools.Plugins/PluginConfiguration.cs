using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CyrusBuilt.CyNetTools.Plugins
{
    /// <summary>
    /// Configuration storage for plugin settings using key/value pairs.
    /// </summary>
    public class PluginConfiguration : IDisposable
    {
        #region Fields
        private Dictionary<String, Object> _backingStore = null;
        private Boolean _isDisposed = false;
        private Boolean _isDirty = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <b>MAT.EventDispatchCoordinator.Plugins.PluginConfiguration</b>
        /// class. This is the default constructor.
        /// </summary>
        public PluginConfiguration() {
            this._backingStore = new Dictionary<String, Object>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether or not the backing store contains
        /// any settings.
        /// </summary>
        public Boolean IsEmpty {
            get { return ((this._backingStore == null) || (this._backingStore.Count == 0)); }
        }

        /// <summary>
        /// Gets a value indicating whether or not changes have been made to
        /// one or more settings that has not yet been saved.
        /// </summary>
        public Boolean IsDirty {
            get { return this._isDirty; }
        }

        /// <summary>
        /// Gets a value indicating whether or not this instance has been
        /// disposed and now in an unusable state.
        /// </summary>
        public Boolean IsDisposed {
            get { return this._isDisposed; }
        }

        /// <summary>
        /// Gets a collection of the names of all the settings (keys) in the
        /// backing store.
        /// </summary>
        public Dictionary<String, Object>.KeyCollection AllKeys {
            get { return this._backingStore.Keys; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Clears the dirty flag. This method should be called *after* the
        /// settings have been persisted. If called before, any class
        /// implementing <see cref="IPlugin"/> that only saves changes based
        /// on whether or not the configuration is dirty may not pickup changes
        /// that have been made.
        /// </summary>
        /// <remarks>
        /// This method should really only be called by <see cref="PluginManager"/>
        /// or classes implementing <see cref="IPluginHost"/>.
        /// </remarks>
        public void ClearDirty() {
            lock (this) {
                this._isDirty = false;
            }
        }

        /// <summary>
        /// Adds a setting to the configuration. If the specified setting
        /// already exists, then this will simply assign its value.
        /// </summary>
        /// <param name="key">
        /// The name of the setting (key).
        /// </param>
        /// <param name="value">
        /// The value of the setting.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        /// This instance hass been disposed.
        /// </exception>
        public void AddConfigurationSetting(String key, Object value) {
            if (this._isDisposed) {
                throw new ObjectDisposedException("PluginConfiguration");
            }

            if (!this._backingStore.ContainsKey(key)) {
                this._backingStore.Add(key, value);
            }
            else {
                if (this._backingStore[key] != value) {
                    this._backingStore[key] = value;
                    this._isDirty = true;
                }
            }
        }

        /// <summary>
        /// Gets the value of a setting.
        /// </summary>
        /// <param name="key">
        /// The setting to retrieve the value from.
        /// </param>
        /// <returns>
        /// If successful, the value of the setting; Otherwise, null.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        /// This instance hass been disposed.
        /// </exception>
        public Object GetValue(String key) {
            if (this._isDisposed) {
                throw new ObjectDisposedException("PluginConfiguration");
            }

            if (this._backingStore.ContainsKey(key)) {
                return this._backingStore[key];
            }
            return null;
        }

        /// <summary>
        /// Sets the value of a setting.
        /// </summary>
        /// <param name="key">
        /// The name of the setting to assign a value to.
        /// </param>
        /// <param name="value">
        /// The value to set.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        /// This instance hass been disposed.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public void SetValue(String key, Object value) {
            if (this._isDisposed) {
                throw new ObjectDisposedException("PluginConfiguration");
            }

            if (!this._backingStore.ContainsKey(key)) {
                throw new ArgumentException("The specified key does not exist.", "key");
            }

            if (this._backingStore[key] != value) {
                this._backingStore[key] = value;
                this._isDirty = true;
            }
        }

        /// <summary>
        /// Copies configuration values from the specified configuration if the
        /// specified config contains matching settings.
        /// </summary>
        /// <param name="config">
        /// The configuration to copy values from.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        /// This instance hass been disposed.
        /// </exception>
        public void CopyFromConfig(PluginConfiguration config) {
            if (this._isDisposed) {
                throw new ObjectDisposedException("PluginConfiguration");
            }

            if ((!config.IsDisposed) && (!config.IsEmpty)) {
                foreach (var key in config.AllKeys) {
                    if (this._backingStore.ContainsKey(key)) {
                        this.SetValue(key, config.GetValue(key));
                    }
                }
            }
        }

        /// <summary>
        /// Clears the values of all settings in this configuration instance.
        /// </summary>
        public void Clear() {
            if ((!this._isDisposed) && (this._backingStore != null)) {
                if (this._backingStore.Count > 0) {
                    foreach (var key in this._backingStore.Keys) {
                        this._backingStore[key] = null;
                    }
                    this._isDirty = true;
                }
            }
        }

        /// <summary>
        /// Disposes of all managed resources used by this component.
        /// </summary>
        public void Dispose() {
            if (this._isDisposed) {
                return;
            }

            if (this._backingStore != null) {
                this._backingStore.Clear();
                this._backingStore = null;
            }
            this._isDirty = false;
            this._isDisposed = true;
        }

        /// <summary>
        /// Reads the plugin configuration from file.
        /// </summary>
        /// <param name="config">
        /// The configuration file to read.
        /// </param>
        /// <exception cref="FileNotFoundException">
        /// The configuration file does not exist.
        /// </exception>
        public void ReadFromFile(FileInfo config) {
            if ((config == null) || (!config.Exists)) {
                throw new FileNotFoundException("Plugin configuration file not found.");
            }

            using (var reader = new XmlTextReader(config.FullName)) {
                try {
                    var name = String.Empty;
                    Object val = null;
                    this._backingStore.Clear();
                    while (reader.Read()) {
                        name = String.Empty;
                        val = null;
                        if (reader.NodeType == XmlNodeType.Element) {
                            switch (reader.Name) {
                                case "setting":
                                    name = reader.GetAttribute("name");
                                    break;
                                case "value":
                                    val = reader.ReadContentAsObject();
                                    break;
                                default:
                                    break;
                            }
                        }

                        if ((!String.IsNullOrEmpty(name)) && (val != null)) {
                            this._backingStore.Add(name, val);
                        }
                    }
                }
                catch {
                    throw;
                }
            }
        }

        /// <summary>
        /// Perists the plugin configuration data to disk.
        /// </summary>
        /// <param name="config">
        /// The file holding the plugin configuration data.
        /// </param>
        /// <param name="t">
        /// The type of the plugin.
        /// </param>
        /// <exception cref="FileNotFoundException">
        /// The configuration file does not exist.
        /// </exception>
        public void SaveToFile(FileInfo config, Type t) {
            if ((config == null) || (!config.Exists)) {
                throw new FileNotFoundException("Plugin configuration file not found.");
            }

            XDocument doc = XDocument.Load(config.FullName);
            var query = from x in doc.Descendants("configuration")
                                        .Descendants("applicationSettings")
                                        .Descendants(t.Namespace)
                                        .Descendants("setting")
                        select x;
            var key = String.Empty;
            foreach (XElement setting in query) {
                key = setting.Attribute("name").Value;
                if (this._backingStore.ContainsKey(key)) {
                    setting.Descendants("value").Single().SetValue(this.GetValue(key));
                }
            }
            doc.Save(config.FullName);
        }
        #endregion
    }
}
