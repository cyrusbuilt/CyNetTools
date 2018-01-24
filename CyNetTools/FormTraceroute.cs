using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CyrusBuilt.CyNetTools.Core;
using CyrusBuilt.CyNetTools.Core.TraceRoute;

namespace CyrusBuilt.CyNetTools
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormTraceroute : Form
    {
        #region Fields
        private PrintDocument _printDoc = null;
        private StringReader _sr = null;
        private Int32 _pageIndex = 1;
        private String _lastDir = String.Empty;
        private Font _currentFont = null;
        private DateTime _startTime = DateTime.MinValue;
        private TraceRouteModule _trm = null;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mdiParent">
        /// 
        /// </param>
        public FormTraceroute(Form mdiParent) {
            this.InitializeComponent();
            this.MdiParent = mdiParent;
            this._printDoc = new PrintDocument();
            this._trm = new TraceRouteModule();
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
                var initial = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
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
            this.richTextBoxOutput.Text = "Ready.\r\n";
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
                line = "\r\n" + line;
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

        /// <summary>
        /// Registers event handlers for TraceRouteModule events.
        /// </summary>
        private void RegisterTraceEvents() {
            this._trm.OutputReceived += new ProcessOutputEventHandler(this.Trm_OutputReceived);
            this._trm.ProcessCancelled += new ProcessCancelledEventHandler(this.Trm_ProcessCancelled);
            this._trm.ProcessFinished += new ProcessDoneEventHandler(this.Trm_ProcessFinished);
            this._trm.ProcessStarted += new ProcessRunningEventHandler(this.Trm_ProcessStarted);
            this._trm.Progress += new TraceProgressEventHandler(this.Trm_Progress);
        }

        /// <summary>
        /// Unregisters TraceRouteModule event handlers.
        /// </summary>
        private void UnregisterTraceEvents() {
            this._trm.OutputReceived -= new ProcessOutputEventHandler(this.Trm_OutputReceived);
            this._trm.ProcessCancelled -= new ProcessCancelledEventHandler(this.Trm_ProcessCancelled);
            this._trm.ProcessFinished -= new ProcessDoneEventHandler(this.Trm_ProcessFinished);
            this._trm.ProcessStarted -= new ProcessRunningEventHandler(this.Trm_ProcessStarted);
            this._trm.Progress -= new TraceProgressEventHandler(this.Trm_Progress);
        }

        /// <summary>
        /// Initializes the IP Protocol drop-down list.
        /// </summary>
        private void InitProtocolDropDown() {
            this.comboBoxProtocol.BeginUpdate();
            this.comboBoxProtocol.Items.Clear();
            this.comboBoxProtocol.Items.Add(Enum.GetName(typeof(IPVersion), IPVersion.IPv4));
            this.comboBoxProtocol.Items.Add(Enum.GetName(typeof(IPVersion), IPVersion.IPv6));
            var def = Enum.GetName(typeof(IPVersion), this._trm.IPProtocolVersion);
            this.comboBoxProtocol.SelectedItem = def;
            this.comboBoxProtocol.EndUpdate();
        }

        /// <summary>
        /// Initializes the error provider.
        /// </summary>
        private void InitErrorProvider() {
            this.errorProviderTrace.Clear();
            this.errorProviderTrace.SetError(this.textBoxTargetHost, String.Empty);
            this.errorProviderTrace.SetError(this.maskedTextBoxSourceAddr, String.Empty);
        }

        /// <summary>
        /// Enables protocol-specific controls based on the selected protocol.
        /// </summary>
        private void EnableProtocolSpecificControls() {
            var selection = this.comboBoxProtocol.SelectedIndex;
            this._trm.IPProtocolVersion = (selection == 0 ? IPVersion.IPv4 : IPVersion.IPv6);
            var isIPv6 = (this._trm.IPProtocolVersion == IPVersion.IPv6);
            var isIPv4 = (this._trm.IPProtocolVersion == IPVersion.IPv4);
            this.checkBoxRoundTrip.Enabled = isIPv6;
            this.maskedTextBoxSourceAddr.Enabled = isIPv6;
            this.textBoxHostList.Enabled = isIPv4;
        }

        /// <summary>
        /// Thread-safe method for updating the text of the specified label.
        /// </summary>
        /// <param name="lbl">
        /// The label control to update.
        /// </param>
        /// <param name="text">
        /// The text to set in the control.
        /// </param>
        private void SafeSetLabelText(Label lbl, String text) {
            if ((lbl != null) && (!lbl.IsDisposed)) {
                if (lbl.InvokeRequired) {
                    lbl.Invoke(new MethodInvoker(delegate {
                        lbl.Text = text;
                        lbl.Width = text.Length + 10;
                    }));
                }
                else {
                    lbl.Text = text;
                    lbl.Width = text.Length + 10;
                }
            }
        }

        /// <summary>
        /// Thread-safe method for updating the value of the progress bar.
        /// </summary>
        /// <param name="value">
        /// The progress bar value.
        /// </param>
        private void SafeSetProgressBarValue(Int32 value) {
            var pb = this.progressBarPing;
            if ((value < pb.Minimum) || (value > pb.Maximum)) {
                return;
            }

            if (pb.InvokeRequired) {
                pb.Invoke(new MethodInvoker(delegate {
                    pb.Value = value;
                }));
            }
            else {
                pb.Value = value;
            }
        }

        /// <summary>
        /// Thread-safe method for toggling the run timer on/off.
        /// </summary>
        /// <param name="enabled">
        /// Set true to enable; false to disable.
        /// </param>
        private void SafeToggleTimer(Boolean enabled) {
            if (this.InvokeRequired) {
                this.Invoke(new MethodInvoker(delegate {
                    this.timerRuntime.Enabled = enabled;
                }));
            }
            else {
                this.timerRuntime.Enabled = enabled;
            }
        }
        #endregion

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
        private void Printdoc_PrintPage(Object sender, PrintPageEventArgs e) {
            if ((this._currentFont == null) || (this._sr == null)) {
                return;
            }

            // Calculate how many lines we can get on a page and get our margins.
            float linesPerPage = (e.MarginBounds.Height / this._currentFont.GetHeight(e.Graphics));
            float vertOffset = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            var linesPrinted = 0;
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
            this.InitErrorProvider();
            var host = this.textBoxTargetHost.Text.Trim();
            if (String.IsNullOrEmpty(host)) {
                this.errorProviderTrace.SetError(this.textBoxTargetHost, "No host name or IP specified.");
                this.tabControlMain.SelectedTab = this.tabPageOutput;
                this.textBoxTargetHost.Select();
                return;
            }

            var timeoutStr = this.textBoxTimeout.Text.Trim();
            if (!String.IsNullOrEmpty(timeoutStr)) {
                var timeout = 0;
                if (Int32.TryParse(timeoutStr, out timeout)) {
                    this._trm.Timeout = timeout;
                }
            }

            this._trm.HostList = this.textBoxHostList.Text.Trim();
            this._trm.TargetHost = host;
            this._startTime = DateTime.Now;
            this._trm.Start();
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
            this._trm.Cancel();
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
        /// 
        /// </summary>
        /// <param name="sender">
        /// 
        /// </param>
        /// <param name="e">
        /// 
        /// </param>
        private void Trm_OutputReceived(object sender, ProcessOutputEventArgs e) {
            this.SafeWriteConsole(e.StandardOutput);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// 
        /// </param>
        /// <param name="e">
        /// 
        /// </param>
        private void Trm_ProcessCancelled(object sender, ProcessCancelledEventArgs e) {
            this.SafeToggleStatusAnim(false);
            this.SafeUpdateStatus("Cancelled");
            this.SafeToggleToolbarButton(this.toolStripButtonCancel, false);
            this.SafeToggleToolbarButton(this.toolStripButtonExec, true);
            this.SafeToggleTimer(false);
            if (e.CancelCause != null) {
                this.SafeWriteConsole("ERROR: " + e.CancelCause.Message, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// 
        /// </param>
        /// <param name="e">
        /// 
        /// </param>
        private void Trm_ProcessFinished(object sender, ProccessDoneEventArgs e) {
            this.SafeToggleStatusAnim(false);
            this.SafeUpdateStatus("Finished");
            this.SafeWriteConsole("Process exit code: " + e.ExitCode.ToString());
            this.SafeToggleToolbarButton(this.toolStripButtonCancel, false);
            this.SafeToggleToolbarButton(this.toolStripButtonExec, true);
            this.SafeToggleTimer(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// 
        /// </param>
        /// <param name="e">
        /// 
        /// </param>
        private void Trm_ProcessStarted(object sender, ProcessStartedEventArgs e) {
            this.SafeToggleStatusAnim(true);
            this.SafeUpdateStatus("Running");
            this.SafeWriteConsole("Process started with PID: " + e.ProcessID.ToString());
            this.SafeToggleToolbarButton(this.toolStripButtonExec, false);
            this.SafeToggleToolbarButton(this.toolStripButtonCancel, true);
            this.SafeToggleTimer(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// 
        /// </param>
        /// <param name="e">
        /// 
        /// </param>
        private void Trm_Progress(object sender, TraceProgressEventArgs e) {
            this.SafeSetProgressBarValue(e.Progress);
            this.SafeSetLabelText(this.labelCurrentHopActual, e.CurrentHop.ToString());
            this.SafeSetLabelText(this.labelTotalHopsActual, e.TotalHops.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// 
        /// </param>
        /// <param name="e">
        /// 
        /// </param>
        private void comboBoxProtocol_SelectedIndexChanged(object sender, EventArgs e) {
            this.EnableProtocolSpecificControls();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// 
        /// </param>
        /// <param name="e">
        /// 
        /// </param>
        private void checkBoxRoundTrip_CheckedChanged(object sender, EventArgs e) {
            this._trm.RoundTrip = this.checkBoxRoundTrip.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// 
        /// </param>
        /// <param name="e">
        /// 
        /// </param>
        private void numericUpDownMaxHops_ValueChanged(object sender, EventArgs e) {
            this._trm.MaxHops = (Int32)this.numericUpDownMaxHops.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// 
        /// </param>
        /// <param name="e">
        /// 
        /// </param>
        private void maskedTextBoxSourceAddr_TypeValidationCompleted(object sender, TypeValidationEventArgs e) {
            this.errorProviderTrace.SetError(this.maskedTextBoxSourceAddr, String.Empty);
            if (e.IsValidInput) {
                this._trm.SourceAddress = (IPAddress)e.ReturnValue;
            }
            else {
                this.errorProviderTrace.SetError(this.maskedTextBoxSourceAddr, e.Message);
                this.maskedTextBoxSourceAddr.Select();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// 
        /// </param>
        /// <param name="e">
        /// 
        /// </param>
        private void checkBoxNoResolve_CheckedChanged(object sender, EventArgs e) {
            this._trm.DoNotResolveNames = this.checkBoxNoResolve.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// 
        /// </param>
        /// <param name="e">
        /// 
        /// </param>
        private void FormTraceroute_Load(object sender, EventArgs e) {
            this.SuspendLayout();
            this.InitPrintDocument();
            this.InitSaveFileDialog();
            this.InitProtocolDropDown();
            this.InitErrorProvider();
            this.RegisterTraceEvents();
            this.EnableProtocolSpecificControls();
            this.textBoxTargetHost.Select();
            this.ResumeLayout();
        }
    }
}
