using System;
using System.Windows.Forms;

namespace CyrusBuilt.CyNetTools.Plugins
{
    /// <summary>
    /// Settings value change form.
    /// </summary>
    public partial class FormChangeValue : Form
    {
        #region Fields
        private String _key = String.Empty;
        private Object _value = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <b>CyrusBuilt.CyNetTools.Plugins.FormChangeValue</b>
        /// class with the name of the setting (key) to change and the value
        /// to set.
        /// </summary>
        /// <param name="key">
        /// The name of the setting to change.
        /// </param>
        /// <param name="value">
        /// The value to assign to the setting.
        /// </param>
        public FormChangeValue(String key, Object value) {
            this.InitializeComponent();
            this._key = key;
            this._value = value;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the setting being modified.
        /// </summary>
        public String Key {
            get { return this._key; }
        }

        /// <summary>
        /// Gets the value of the setting.
        /// </summary>
        public Object Value {
            get { return this._value; }
        }
        #endregion

        #region Methods and Event Handlers
        /// <summary>
        /// Initializes the error provider.
        /// </summary>
        private void InitErrorProvider() {
            this.errorProviderMain.Clear();
            this.errorProviderMain.SetError(this.textBoxValue, String.Empty);
        }

        /// <summary>
        /// Handles the form load event.  Performs additional initialization of the form controls.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void FormChangeValue_Load(Object sender, EventArgs e) {
            this.InitErrorProvider();
            this.textBoxSetting.Text = this._key;
            String typeName = this._value.GetType().FullName;
            switch (typeName) {
                case "System.Single": typeName = "float"; break;
                case "System.Int64": typeName = "long"; break;
                case "System.Int16": typeName = "short"; break;
                default: break;
            }
            this.textBoxType.Text = typeName;
            this.textBoxValue.Text = this._value.ToString();
        }

        /// <summary>
        /// Handles the "OK" button click event. This converts the specified
        /// value from string to the specified value type and assigns it to
        /// <see cref="FormChangeValue.Value"/>, then accepts the dialog
        /// and closes it.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void buttonOk_Click(Object sender, EventArgs e) {
            this.InitErrorProvider();
            Boolean isBad = false;
            String typeName = this.textBoxType.Text;
            String val = this.textBoxValue.Text.Trim();
            switch (typeName) {
                case "System.Object":
                    this._value = (Object)val;
                    break;
                case "System.String":
                    this._value = val;
                    break;
                case "float":
                    Single s = 0;
                    isBad = (!Single.TryParse(val, out s));
                    if (!isBad) {
                        this._value = s;
                    }
                    break;
                case "long":
                    Int64 l = 0;
                    isBad = (!Int64.TryParse(val, out l));
                    if (!isBad) {
                        this._value = l;
                    }
                    break;
                case "System.Boolean":
                    Boolean b = false;
                    isBad = (!Boolean.TryParse(val, out b));
                    if (!isBad) {
                        this._value = b;
                    }
                    break;
                case "short":
                    Int16 ss = 0;
                    isBad = (!Int16.TryParse(val, out ss));
                    if (!isBad) {
                        this._value = ss;
                    }
                    break;
                case "System.Char":
                    Char c;
                    isBad = (!Char.TryParse(val, out c));
                    if (!isBad) {
                        this._value = c;
                    }
                    break;
                case "System.Decimal":
                    Decimal d = 0;
                    isBad = (!Decimal.TryParse(val, out d));
                    if (!isBad) {
                        this._value = d;
                    }
                    break;
                case "System.Double":
                    Double dd = 0;
                    isBad = (!Double.TryParse(val, out dd));
                    if (!isBad) {
                        this._value = dd;
                    }
                    break;
                case "System.Int32":
                    Int32 i = 0;
                    isBad = (!Int32.TryParse(val, out i));
                    if (!isBad) {
                        this._value = i;
                    }
                    break;
                case "System.DateTime":
                    DateTime dt = DateTime.MinValue;
                    isBad = (!DateTime.TryParse(val, out dt));
                    if (!isBad) {
                        this._value = dt;
                    }
                    break;
            }

            if (isBad) {
                this.errorProviderMain.SetError(this.textBoxValue, "Specified value cannot be parsed into type: " + typeName);
                this.textBoxValue.Select();
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Handles the "Cancel" button click event. This cancels and closes
        /// this dialog.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void buttonCancel_Click(Object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}
