using System;
using System.Windows.Forms;

namespace CyrusBuilt.CyNetTools.Plugins
{
    /// <summary>
    /// Base class for CyNetTools plugins.
    /// </summary>
    public abstract class PluginBase : IPlugin
    {
        #region Fields
        private Boolean _isDisposed = false;
        private Boolean _initialized = false;
        private IPluginHost _host = null;
        private Int32 _index = 0;
        private Object _tag = null;
        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Plugins.PluginBase</b>
        /// class. This is the default constructor.
        /// </summary>
        protected PluginBase() {
        }

        /// <summary>
        /// Releases all resources used by this component.
        /// </summary>
        public virtual void Dispose() {
            if (this._isDisposed) {
                return;
            }

            this._tag = null;
            this._host = null;
            this._initialized = false;
            this._isDisposed = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Checks to see if the plugin instance has been disposed.
        /// </summary>
        public Boolean IsDisposed {
            get { return this._isDisposed; }
        }

        /// <summary>
        /// Gets or sets plugin host. This would be the application (class)
        /// that implements <see cref="IPluginHost"/> for using this plugin.
        /// </summary>
        public IPluginHost Host {
            get { return this._host; }
            set { this._host = value; }
        }

        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        public abstract String Name { get; }

        /// <summary>
        /// Gets a decription of the plugin.
        /// </summary>
        public abstract String Description { get; }

        /// <summary>
        /// ets the plugin's author.
        /// </summary>
        public abstract String Author { get; }

        /// <summary>
        /// Gets the plugin version.
        /// </summary>
        public abstract Version Version { get; }

        /// <summary>
        /// Gets the copyright.
        /// </summary>
        public abstract String Copyright { get; }

        /// <summary>
        /// Gets a flag indicating whether or not the plugin is busy.
        /// </summary>
        public abstract Boolean IsBusy { get; }

        /// <summary>
        /// Gets a flag to indicate whether or not the plugin has been
        /// initialized.
        /// </summary>
        public Boolean IsInitialized {
            get { return this._initialized; }
        }

        /// <summary>
        /// Gets or sets the instance index. This is useful for save file dialogs.
        /// </summary>
        public Int32 Index {
            get { return this._index; }
            set { this._index = value; }
        }

        /// <summary>
        /// Gets or sets the object to tag this instance with.
        /// </summary>
        public Object Tag {
            get { return this._tag; }
            set { this._tag = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the plugin.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// This instance has been disposed and cannot be initialized.
        /// </exception>
        public virtual void Initialize() {
            if (this._isDisposed) {
                throw new ObjectDisposedException("CyrusBuilt.CyNetTools.Plugins.PluginBase");
            }

            if (this._initialized) {
                return;
            }
            this._initialized = true;
        }

        /// <summary>
        /// In a derived class, gets the plugin's configuration.
        /// </summary>
        /// <returns>
        /// The plugin configuration.
        /// </returns>
        public abstract PluginConfiguration GetConfiguration();

        /// <summary>
        /// In a derived class, saves the plugin's configuration.
        /// </summary>
        /// <param name="config">
        /// The plugin configuration to save to the plugin's configuration file.
        /// </param>
        public abstract void SaveConfiguration(PluginConfiguration config);

        /// <summary>
        /// In a derived class, displays the main form of the plugin's UI.
        /// </summary>
        /// <param name="mdiParent">
        /// The form that is the MDI parent of the plugin's main UI form.
        /// </param>
        /// <returns>
        /// A reference to the displayed form.
        /// </returns>
        public abstract Form ShowUI(Form mdiParent);
        #endregion
    }
}
