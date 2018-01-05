using System;
using System.Reflection;
using System.Windows.Forms;

namespace CyrusBuilt.CyNetTools.Plugins
{
    /// <summary>
    /// The plugin settings form. This displays the plugin's configuration and
    /// provides a means of changing the values within.
    /// </summary>
    public partial class FormSettingsDialog : Form
    {
        #region Fields
        private PluginConfiguration _config = null;
        private String _name = String.Empty;
        private String _asmName = String.Empty;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Plugins.FormSettingsDialog</b>
        /// class with the plugin to load the configuration from.
        /// </summary>
        /// <param name="plugin">
        /// The plugin to load the configuration from.
        /// </param>
        public FormSettingsDialog(AvailablePlugin plugin) {
            this.InitializeComponent();
            if (plugin != null) {
                this._name = plugin.Instance.Name;
                this._config = plugin.Instance.GetConfiguration();
                Assembly asm = Assembly.ReflectionOnlyLoadFrom(plugin.AssemblyPath);
                this._asmName = asm.GetName().Name;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the loaded (or modified) configuration.
        /// </summary>
        public PluginConfiguration Configuration {
            get { return this._config; }
        }
        #endregion

        #region Methods and Event Handlers
        /// <summary>
        /// Loads the configuration from the plugin.
        /// </summary>
        private void LoadConfig() {
            if (!String.IsNullOrEmpty(this._name)) {
                this.Text = this._name + " Settings";
            }

            if (this._config == null) {
                this.listViewSettings.Hide();
                this.buttonSave.Hide();
                return;
            }

            Object value = null;
            ListViewItem lvi = null;
            foreach (String key in this._config.AllKeys) {
                lvi = this.listViewSettings.Items.Add(key);
                value = this._config.GetValue(key);
                lvi.Tag = value;
                lvi.SubItems.Add(value.GetType().FullName);
                lvi.SubItems.Add(value.ToString());
            }
        }

        /// <summary>
        /// Handles the form load event. Loads the configuration into the form.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void FormSettingsDialog_Load(Object sender, EventArgs e) {
            this.LoadConfig();
        }

        /// <summary>
        /// Handles the save button click event. This sets the values of all the
        /// loaded settings back into the configuration, then accepts and closes
        /// this dialog.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void buttonSave_Click(Object sender, EventArgs e) {
            String key = String.Empty;
            String val = String.Empty;
            Object obj = null;

            foreach (ListViewItem lvi in this.listViewSettings.Items) {
                key = lvi.Text;
                val = lvi.SubItems[2].Text;
                obj = this._config.GetValue(key);
                if (obj is String) {
                    this._config.SetValue(key, val);
                }
                else if (obj is Boolean) {
                    this._config.SetValue(key, Boolean.Parse(val));
                }
                else if (obj is Char) {
                    this._config.SetValue(key, Char.Parse(val));
                }
                else if (obj is Decimal) {
                    this._config.SetValue(key, Decimal.Parse(val));
                }
                else if (obj is Double) {
                    this._config.SetValue(key, Double.Parse(val));
                }
                else if (obj is float) {
                    this._config.SetValue(key, float.Parse(val));
                }
                else if (obj is Int32) {
                    this._config.SetValue(key, Int32.Parse(val));
                }
                else if (obj is long) {
                    this._config.SetValue(key, long.Parse(val));
                }
                else if (obj is short) {
                    this._config.SetValue(key, short.Parse(val));
                }
                else if (obj is DateTime) {
                    this._config.SetValue(key, DateTime.Parse(val));
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Handles the "Close" button click event. This cancels and closes
        /// this dialog.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void buttonClose_Click(Object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Handles the listview double-click event. This opens the configuration
        /// for the selected plugin.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void listViewSettings_DoubleClick(Object sender, EventArgs e) {
            if (this.listViewSettings.SelectedItems.Count > 0) {
                ListViewItem lvi = this.listViewSettings.SelectedItems[0];
                using (FormChangeValue editor = new FormChangeValue(lvi.Text, lvi.Tag)) {
                    if (editor.ShowDialog() == DialogResult.OK) {
                        lvi.Tag = editor.Value;
                        lvi.SubItems[2].Text = editor.Value.ToString();
                    }
                }
            }
        }
        #endregion
    }
}
