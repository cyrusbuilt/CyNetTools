using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Security;
using System.Windows.Forms;

namespace CyrusBuilt.CyNetTools
{
    /// <summary>
    /// This form should not be implemented directly. This is a base form
    /// that is meant to be the UI for external tools that return output
    /// text from the process.  This should be copied, renamed, then modified
    /// as needed.
    /// </summary>
    public partial class FormExternalToolBase : Form
    {
        /*
         * Ideally this form class would be implemented as an inherited form.
         * However, I've not had much luck getting this to work properly in
         * VS2008 or VS2010. (Yes I'm aware the controls all have to protected
         * instead of private, still doesn't seem to work right). That being
         * said, since the behavior of the form designer sometimes fucks up
         * the code and the behavior of the derived form, I've opted instead
         * to simply make a copy of this form and rename it, then modify as
         * needed. This method duplicates a bunch of code, but at least its
         * right every time.
         */

        #region Fields
        private PrintDocument _printDoc = null;
        private StringReader _sr = null;
        private Int32 _pageIndex = 1;
        private String _lastDir = String.Empty;
        private Font _currentFont = null;
        private DateTime _startTime = DateTime.MinValue;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CyrusBuilt.CyNetTools.FormExternalToolBase"/>
        /// class. This is the default constructor.
        /// </summary>
        /// <param name="mdiParent">
        /// The form that is the MDI parent of this form.
        /// </param>
        public FormExternalToolBase(Form mdiParent) {
            this.InitializeComponent();
            this.MdiParent = mdiParent;
            this._printDoc = new PrintDocument();
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Initializes the print document.
        /// </summary>
        private void InitPrintDocument() {
            if (this._printDoc != null) {
                this._printDoc.BeginPrint += new PrintEventHandler(this.Printdoc_BeginPrint);
                this._printDoc.PrintPage += new PrintPageEventHandler(this.Printdoc_PrintPage);
                this._printDoc.EndPrint += new PrintEventHandler(this.Printdoc_EndPrint);
            }
        }

        /// <summary>
        /// Initializes the save file dialog.
        /// </summary>
        private void InitSaveFileDialog() {
            if ((!String.IsNullOrEmpty(this._lastDir)) && (Directory.Exists(this._lastDir))) {
                this.saveFileDialogTool.InitialDirectory = this._lastDir;
            }
            else {
                String initial = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                this.saveFileDialogTool.InitialDirectory = initial;
            }

            this.saveFileDialogTool.AutoUpgradeEnabled = true;
            this.saveFileDialogTool.AddExtension = true;
            this.saveFileDialogTool.ShowHelp = false;
            this.saveFileDialogTool.ValidateNames = true;
            this.saveFileDialogTool.Title = "Save Tool Output";
            this.saveFileDialogTool.Filter = "Log files (*.log)|*.log|Text files (*.txt)|*.txt";
            this.saveFileDialogTool.FilterIndex = 1;
            this.saveFileDialogTool.FileName = "output";
            this.saveFileDialogTool.OverwritePrompt = true;
        }

        /// <summary>
        /// Clears the output console.
        /// </summary>
        private void ClearConsole() {
            this.richTextBoxOutput.Clear();
            this.richTextBoxOutput.Text = "Ready.";
        }

        /// <summary>
        /// Thread-safe method for clearing the output console.
        /// </summary>
        private void SafeClearConsole() {
            if ((this.richTextBoxOutput != null) && (this.richTextBoxOutput.IsDisposed)) {
                if (this.richTextBoxOutput.InvokeRequired) {
                    this.richTextBoxOutput.Invoke(new MethodInvoker(delegate {
                        this.ClearConsole();
                    }));
                }
                else {
                    this.ClearConsole();
                }
            }
        }

        /// <summary>
        /// Thread-safe method for enabling/disabling the specified toolbar button.
        /// </summary>
        /// <param name="btn">
        /// The toolstripbutton to enable/disable.
        /// </param>
        /// <param name="enable">
        /// Set true to enable or false to disable.
        /// </param>
        private void SafeToggleToolbarButton(ToolStripButton btn, Boolean enable) {
            if ((btn != null) && (!btn.IsDisposed)) {
                if (this.toolStripActions.InvokeRequired) {
                    this.toolStripActions.Invoke(new MethodInvoker(delegate {
                        btn.Enabled = enable;
                    }));
                }
                else {
                    btn.Enabled = enable;
                }
            }
        }

        /// <summary>
        /// Thread-safe method for setting the form cursor.
        /// </summary>
        /// <param name="cur">
        /// The cursor to set.
        /// </param>
        private void SafeSetCursor(Cursor cur) {
            if (this.InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    this.Cursor = cur;
                }));
            }
            else {
                this.Cursor = cur;
            }
        }

        /// <summary>
        /// Write text to the output console.
        /// </summary>
        /// <param name="console">
        /// The richtextbox that acts as the output console.
        /// </param>
        /// <param name="line">
        /// The text to write.
        /// </param>
        private void WriteConsole(RichTextBox console, String line) {
            console.AppendText(line);
            console.SelectionStart = console.Text.Length;
            console.ScrollToCaret();
        }

        /// <summary>
        /// Thread-safe method of writing text to the output console.
        /// </summary>
        /// <param name="line">
        /// The text to write.
        /// </param>
        /// <param name="newLine">
        /// Set true to write the text on a new line.
        /// </param>
        private void SafeWriteConsole(String line, Boolean newLine) {
            if (newLine) {
                line += "\r\n";
            }

            var rtb = this.richTextBoxOutput;
            if ((rtb != null) && (!rtb.IsDisposed)) {
                if (rtb.InvokeRequired) {
                    rtb.Invoke(new MethodInvoker(delegate {
                        this.WriteConsole(rtb, line);
                    }));
                }
                else {
                    this.WriteConsole(rtb, line);
                }
            }
        }

        /// <summary>
        /// Thread-safe method of writing the specified text on a new line.
        /// </summary>
        /// <param name="line">
        /// The line of text to write.
        /// </param>
        private void SafeWriteConsole(String line) {
            this.SafeWriteConsole(line, true);
        }

        /// <summary>
        /// Thread-safe method of writing the specified text to the status line.
        /// </summary>
        /// <param name="status">
        /// The status text to write.
        /// </param>
        private void SafeUpdateStatus(String status) {
            status = "Status: " + status;
            if (this.statusStripStat.InvokeRequired) {
                this.statusStripStat.Invoke(new MethodInvoker(delegate {
                    this.toolStripStatusLabelStat.Text = status;
                }));
            }
            else {
                this.toolStripStatusLabelStat.Text = status;
            }
        }

        /// <summary>
        /// Thread-safe method of writing the runtime text to the status bar.
        /// </summary>
        /// <param name="runtime">
        /// The text representing runtime.
        /// </param>
        private void SafeUpdateRuntime(String runtime) {
            runtime = "Runtime: " + runtime;
            if (this.statusStripStat.InvokeRequired) {
                this.statusStripStat.Invoke(new MethodInvoker(delegate {
                    this.toolStripStatusLabelRuntime.Text = runtime;
                }));
            }
            else {
                this.toolStripStatusLabelRuntime.Text = runtime;
            }
        }

        /// <summary>
        /// Thread-safe method for enabling/disabling the status animation.
        /// </summary>
        /// <param name="show">
        /// Set true to show or false to hide.
        /// </param>
        private void SafeToggleStatusAnim(Boolean show) {
            if (this.pictureBoxAnim.InvokeRequired) {
                this.pictureBoxAnim.Invoke(new MethodInvoker(delegate {
                    this.pictureBoxAnim.Visible = show;
                }));
            }
            else {
                this.pictureBoxAnim.Visible = show;
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the end-of-printing event. This simply closes the text stream.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void Printdoc_EndPrint(object sender, PrintEventArgs e) {
            if (this._sr != null) {
                this._sr.Close();
            }
        }

        /// <summary>
        /// Handles the page printing event. This sets up the page to be printed. This while calculate
        /// the margins and fit the text within the margin boundaries for each page.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void Printdoc_PrintPage(object sender, PrintPageEventArgs e) {
            if ((this._currentFont == null) || (this._sr == null)) {
                return;
            }

            // Calculate how many lines we can get on a page and get our margins.
            float linesPerPage = (e.MarginBounds.Height / this._currentFont.GetHeight(e.Graphics));
            float vertOffset = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            Int32 linesPrinted = 0;
            var line = String.Empty;

            // We use this to force text lines that are too long to fit inside
            // the margins to wrap around to the next line.
            var sf = StringFormat.GenericTypographic;
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            sf.FormatFlags = StringFormatFlags.LineLimit;
            sf.Trimming = StringTrimming.Word;

            // Draw each text line on the page.
            while ((linesPrinted < linesPerPage) && ((line = this._sr.ReadLine()) != null)) {
                vertOffset = (topMargin + (linesPrinted * this._currentFont.GetHeight(e.Graphics)));
                e.Graphics.DrawString(line, this._currentFont, Brushes.Black, leftMargin, vertOffset, sf);
                linesPrinted++;
            }

            // Do we need to print another page?
            //e.HasMorePages = String.IsNullOrEmpty(line) ? false : true;
            this._pageIndex++;
            e.HasMorePages = (this._pageIndex <= this.printDialogTool.PrinterSettings.ToPage);
        }

        /// <summary>
        /// Handles the print begin event. This assigns stream of text from the viewer window
        /// and the current font.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void Printdoc_BeginPrint(object sender, PrintEventArgs e) {
            if (String.IsNullOrEmpty(this.richTextBoxOutput.Text)) {
                return;
            }

            this._sr = new StringReader(this.richTextBoxOutput.Text);
            this._currentFont = this.richTextBoxOutput.Font;
            this._pageIndex = this._printDoc.PrinterSettings.FromPage;
        }

        /// <summary>
        /// Handles the "Save" toolbar button click event. Saves the output in
        /// the console to a user-specified file path.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void toolStripButtonSave_Click(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(this._lastDir)) {
                this.saveFileDialogTool.InitialDirectory = this._lastDir;
            }
            else {
                var initial = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                this.saveFileDialogTool.InitialDirectory = initial;
            }

            var err = String.Empty;
            if (this.saveFileDialogTool.ShowDialog() == DialogResult.OK) {
                try {
                    var path = this.saveFileDialogTool.FileName;
                    using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)) {
                        if (fs.CanWrite) {
                            using (var sw = new StreamWriter(fs)) {
                                sw.Write(this.richTextBoxOutput.Text);
                            }
                        }
                        else {
                            err = "Unable to save the file contents because the file is not writable.";
                        }
                    }
                }
                catch (SecurityException) {
                    err = "You do not have the necessary permissions to write to this location.";
                }
                catch (DirectoryNotFoundException) {
                    err = "Could not save to the specified path because the directory no longer exists.\r\n" +
                                    "If you are saving to a network location or mapped drive, make sure the path\r\n" +
                                    "is still available.";
                }
                catch (PathTooLongException) {
                    err = "Unable to save the file because the path is too long.";
                }
                catch (IOException ioEx) {
                    err = "An I/O error occurred: " + ioEx.Message;
                }

                if (!String.IsNullOrEmpty(err)) {
                    MessageBox.Show(err, "File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the "Page Setup" toolbar button click event. This just
        /// displays the page setup dialog.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void toolStripButtonPageSetup_Click(object sender, EventArgs e) {
            if (this._printDoc == null) {
                return;
            }
            this.pageSetupDialogTool.Document = this._printDoc;
            this.pageSetupDialogTool.ShowDialog();
        }

        /// <summary>
        /// Handles the "Print Preview" toolbar button click event. This just
        /// displays the print preview dialog.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void toolStripButtonPrintPreview_Click(object sender, EventArgs e) {
            if (this._printDoc == null) {
                return;
            }

            if ((this.printPreviewDialogTool == null) ||
                (this.printPreviewDialogTool.IsDisposed)) {
                    this.printPreviewDialogTool = new PrintPreviewDialog();
            }

            this.printPreviewDialogTool.Document = this._printDoc;
            this.printPreviewDialogTool.ShowDialog();
        }

        /// <summary>
        /// Handles the "Print" toolbar button click event. This displays the
        /// print dialog and then prints the output in the console to the selected
        /// printing device unless the user cancels.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void toolStripButtonPrint_Click(object sender, EventArgs e) {
            if (this._printDoc == null) {
                return;
            }

            this.printDialogTool.Document = this._printDoc;
            this.printDialogTool.AllowCurrentPage = true;
            this.printDialogTool.AllowSomePages = true;
            if (this.printDialogTool.ShowDialog() == DialogResult.OK) {
                try {
                    this._printDoc.PrinterSettings = this.printDialogTool.PrinterSettings;
                    var settings = this._printDoc.PrinterSettings;
                    // What pages are we printing? All of them, or just certain ones?
                    settings.MinimumPage = this.printDialogTool.PrinterSettings.FromPage = 1;
                    settings.MaximumPage = this.printDialogTool.PrinterSettings.ToPage;
                    this._printDoc.PrinterSettings = settings;
                    this._printDoc.Print();
                }
                catch (InvalidPrinterException ipEx) {
                    MessageBox.Show(ipEx.Message, "Printing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the form load event. This initializes the additional controls
        /// and subsystems.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void FormExternalToolBase_Load(object sender, EventArgs e) {
            this.InitPrintDocument();
            this.InitSaveFileDialog();
        }

        /// <summary>
        /// Handles the form closing event. This stops and destroys all additional
        /// subsystems prior to form destruction.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void FormExternalToolBase_FormClosing(object sender, FormClosingEventArgs e) {
            if (this._sr != null) {
                this._sr.Dispose();
                this._sr = null;
            }

            if (this._printDoc != null) {
                this._printDoc.Dispose();
                this._printDoc = null;
            }
        }

        /// <summary>
        /// Handles the timer tick which will fire once per second while the
        /// timer is running.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void timerRuntime_Tick(object sender, EventArgs e) {
            var span = (DateTime.Now - this._startTime);
            var t = span.Hours.ToString() + ":" + span.Minutes.ToString() + ":" + span.Seconds.ToString() + "." + span.Milliseconds.ToString();
            this.SafeUpdateRuntime(t);
        }

        /// <summary>
        /// Handles the "Cancel" toolbar button click event. This should terminate
        /// whatever process is spawned by the <see cref="toolStripButtonExec_Click"/>
        /// handler.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void toolStripButtonCancel_Click(object sender, EventArgs e) {
            
        }

        /// <summary>
        /// Handles the "Execute" toolbar button click event. This should run whatever
        /// process or main method represented by this form.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void toolStripButtonExec_Click(object sender, EventArgs e) {
            
        }
        #endregion
    }
}
