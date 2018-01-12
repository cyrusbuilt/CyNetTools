using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Security;
using System.Windows.Forms;
using CyrusBuilt.CyNetTools.Core;
using CyrusBuilt.CyNetTools.Core.Ping;

namespace CyrusBuilt.CyNetTools
{
    /// <summary>
    /// The UI form for the ping tool.
    /// </summary>
    public partial class FormPing : Form
    {
        #region Fields
        private PrintDocument _printDoc = null;
        private StringReader _sr = null;
        private Int32 _pageIndex = 1;
        private String _lastDir = String.Empty;
        private Font _currentFont = null;
        private DateTime _startTime = DateTime.MinValue;
        private PingModule _pm = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CyrusBuilt.CyNetTools.FormPinge"/>
        /// class. This is the default constructor.
        /// </summary>
        /// <param name="mdiParent">
        /// The form that is the MDI parent of this form.
        /// </param>
        public FormPing(Form mdiParent) {
            this.InitializeComponent();
            this.MdiParent = mdiParent;
            this._printDoc = new PrintDocument();
            this._pm = new PingModule();
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
        /// Registers event handlers for PingModule events.
        /// </summary>
        private void RegisterPingEvents() {
            if (this._pm != null) {
                this._pm.OutputReceived += new ProcessOutputEventHandler(this.Pm_OutputReceived);
                this._pm.ProcessCancelled += new ProcessCancelledEventHandler(this.Pm_ProcessCancelled);
                this._pm.ProcessFinished += new ProcessDoneEventHandler(this.Pm_ProcessFinished);
                this._pm.ProcessStarted += new ProcessRunningEventHandler(this.Pm_ProcessStarted);
                this._pm.Progress += new PingProgressEventHandler(this.Pm_Progress);
            }
        }

        /// <summary>
        /// Unregisters PingModule event handlers.
        /// </summary>
        private void UnregisterPingEvents() {
            if (this._pm != null) {
                this._pm.OutputReceived -= new ProcessOutputEventHandler(this.Pm_OutputReceived);
                this._pm.ProcessCancelled -= new ProcessCancelledEventHandler(this.Pm_ProcessCancelled);
                this._pm.ProcessFinished -= new ProcessDoneEventHandler(this.Pm_ProcessFinished);
                this._pm.ProcessStarted -= new ProcessRunningEventHandler(this.Pm_ProcessStarted);
                this._pm.Progress -= new PingProgressEventHandler(this.Pm_Progress);
            }
        }

        /// <summary>
        /// Initializes the IP Protocol drop-down list.
        /// </summary>
        private void InitProtocolDropDown() {
            this.comboBoxProtocol.BeginUpdate();
            this.comboBoxProtocol.Items.Clear();
            this.comboBoxProtocol.Items.Add(Enum.GetName(typeof(IPVersion), IPVersion.IPv4));
            this.comboBoxProtocol.Items.Add(Enum.GetName(typeof(IPVersion), IPVersion.IPv6));
            var def = Enum.GetName(typeof(IPVersion), this._pm.IPProtocolVersion);
            this.comboBoxProtocol.SelectedItem = def;
            this.comboBoxProtocol.EndUpdate();
        }

        /// <summary>
        /// Initializes the error provider.
        /// </summary>
        private void InitErrorProvider() {
            this.errorProviderPing.Clear();
            this.errorProviderPing.SetError(this.textBoxHost, String.Empty);
            this.errorProviderPing.SetError(this.maskedTextBoxSrcAddr, String.Empty);
            this.errorProviderPing.SetError(this.textBoxTTL, String.Empty);
        }

        /// <summary>
        /// Enables protocol-specific controls based on the selected protocol.
        /// </summary>
        private void EnableProtocolSpecificControls() {
            var selection = this.comboBoxSrcRouteMode.SelectedIndex;
            this._pm.IPProtocolVersion = (selection == 0 ? IPVersion.IPv4 : IPVersion.IPv6);
            var isIPv4 = (this._pm.IPProtocolVersion == IPVersion.IPv4);
            var isIPv6 = (this._pm.IPProtocolVersion == IPVersion.IPv6);
            this.checkBoxNoFrag.Enabled = isIPv4;
            this.numericUpDownRecordHopCount.Enabled = isIPv4;
            this.comboBoxSrcRouteMode.Enabled = isIPv4;
            this.textBoxSrcRouteList.Enabled = ((isIPv4) && ((RouteMode)selection != RouteMode.None));
            this.numericUpDownTimestamp.Enabled = isIPv4;
            this.checkBoxReverseRoute.Enabled = isIPv6;
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
        /// Initializes the source route drop-down menu.
        /// </summary>
        private void InitSrcRouteDropDown() {
            this.comboBoxSrcRouteMode.BeginUpdate();
            this.comboBoxSrcRouteMode.Items.Clear();
            this.comboBoxSrcRouteMode.Items.Add(Enum.GetName(typeof(RouteMode), RouteMode.None));
            this.comboBoxSrcRouteMode.Items.Add(Enum.GetName(typeof(RouteMode), RouteMode.Loose));
            this.comboBoxSrcRouteMode.Items.Add(Enum.GetName(typeof(RouteMode), RouteMode.Strict));
            this.comboBoxSrcRouteMode.SelectedIndex = (Int32)RouteMode.None;
            this.comboBoxSrcRouteMode.EndUpdate();
        }

        /// <summary>
        /// Configures the ping module for default execution settings.
        /// </summary>
        private void SetDefaults() {
            // Set all control defaults.
            this.checkBoxContinuous.Checked = this._pm.ContinuousPing;
            this.checkBoxResolve.Checked = this._pm.ResolveHostNames;
            this.textBoxReqCount.Text = PingModule.DEFAULT_REQ_COUNT.ToString();
            this.numericUpDownBufferSize.Value = this._pm.BufferSize;
            this.numericUpDownBufferSize.Maximum = PingModule.MAX_BUFFER_SIZE;
            this.checkBoxNoFrag.Checked = this._pm.DoNotFragment;
            this.maskedTextBoxSrcAddr.ValidatingType = typeof(IPAddress);
            this.numericUpDownRecordHopCount.Maximum = PingModule.MAX_ROUTE_HOPS;
            this.numericUpDownRecordHopCount.Value = this._pm.RecordHopCount;
            this.checkBoxReverseRoute.Checked = this._pm.TestReversRoute;
            this.comboBoxSrcRouteMode.SelectedIndex = (Int32)this._pm.SourceRouteMode;
            this.numericUpDownTimestamp.Maximum = PingModule.MAX_TIMESTAMP_HOPS;
            this.numericUpDownTimestamp.Value = this._pm.TimestampCount;
            this.textBoxTTL.Text = this._pm.TimeToLive.ToString();
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
        private void FormPing_Load(object sender, EventArgs e) {
            this.SuspendLayout();
            this.InitPrintDocument();
            this.InitSaveFileDialog();
            this.InitProtocolDropDown();
            this.InitErrorProvider();
            this.InitSrcRouteDropDown();
            this.RegisterPingEvents();
            this.SetDefaults();
            this.EnableProtocolSpecificControls();
            this.textBoxHost.Select();
            this.ResumeLayout();
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

            this.UnregisterPingEvents();
            if (this._pm != null) {
                if (this._pm.IsRunning) {
                    this._pm.Cancel();
                }
                this._pm.Dispose();
                this._pm = null;
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
            this._pm.Cancel();
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
            var host = this.textBoxHost.Text.Trim();
            if (String.IsNullOrEmpty(host)) {
                this.errorProviderPing.SetError(this.textBoxHost, "No host name or IP specified.");
                this.tabControlMain.SelectedTab = this.tabPageOutput;
                this.textBoxHost.Select();
                return;
            }

            var countStr = this.textBoxReqCount.Text.Trim();
            if (!String.IsNullOrEmpty(countStr)) {
                var count = 0;
                if (Int32.TryParse(countStr, out count)) {
                    this._pm.RequestCount = count;
                }
            }

            this._pm.TargetHost = host;
            this._startTime = DateTime.Now;
            this._pm.Start();
        }

        /// <summary>
        /// Handles the ping module progress event. This updates the progress
        /// bar and statistics labels.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void Pm_Progress(object sender, PingProgressEventArgs e) {
            this.SafeSetProgressBarValue(e.PercentComplete);
            this.SafeSetLabelText(this.labelLostField, e.RequestsFailed.ToString());
            this.SafeSetLabelText(this.labelReceivedField, e.RequestsCompleted.ToString());
            this.SafeSetLabelText(this.labelPacketLossField, e.PacketLoss.ToString() + "%");
        }

        /// <summary>
        /// Handles the ping module start event. This turns on the run animation
        /// and run timer and notifies the user that the process has started.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void Pm_ProcessStarted(object sender, ProcessStartedEventArgs e) {
            this.SafeToggleStatusAnim(true);
            this.SafeUpdateStatus("Running");
            this.SafeWriteConsole("Process started with PID: " + e.ProcessID.ToString());
            this.SafeToggleToolbarButton(this.toolStripButtonExec, false);
            this.SafeToggleToolbarButton(this.toolStripButtonCancel, true);
            this.SafeToggleTimer(true);
        }

        /// <summary>
        /// Handles this ping module finished event. This turns off the run
        /// timer and animation and notifies the user that the process has
        /// stopped.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void Pm_ProcessFinished(object sender, ProccessDoneEventArgs e) {
            this.SafeToggleStatusAnim(false);
            this.SafeUpdateStatus("Finished");
            this.SafeWriteConsole("Process exit code: " + e.ExitCode.ToString());
            this.SafeToggleToolbarButton(this.toolStripButtonCancel, false);
            this.SafeToggleToolbarButton(this.toolStripButtonExec, true);
            this.SafeToggleTimer(false);
        }

        /// <summary>
        /// Handles the ping module cancelled event. This turns off the run
        /// timer and animation and notifies the user that the process was
        /// terminated due to force from the user or due to an error.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void Pm_ProcessCancelled(object sender, ProcessCancelledEventArgs e) {
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
        /// Handles the output event from the ping module. This writes the
        /// output received to the console textbox.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void Pm_OutputReceived(object sender, ProcessOutputEventArgs e) {
            this.SafeWriteConsole(e.StandardOutput);
        }

        /// <summary>
        /// Handles the continuous ping checkbox checked event. This enables/disables
        /// continuous ping option and the request count textbox based on the
        /// checkbox checked state.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void checkBoxContinuous_CheckedChanged(object sender, EventArgs e) {
            var check = this.checkBoxContinuous.Checked;
            this._pm.ContinuousPing = check;
            this.textBoxReqCount.Enabled = !check;
        }

        /// <summary>
        /// Handles the protocol dropdown menu selection change event. The will enable specific
        /// controls related to the selected protocol.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void comboBoxProtocol_SelectedIndexChanged(object sender, EventArgs e) {
            this.EnableProtocolSpecificControls();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void checkBoxResolve_CheckedChanged(object sender, EventArgs e) {
            this._pm.ResolveHostNames = this.checkBoxResolve.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void numericUpDownBufferSize_ValueChanged(object sender, EventArgs e) {
            this._pm.BufferSize = (Int32)this.numericUpDownBufferSize.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void checkBoxNoFrag_CheckedChanged(object sender, EventArgs e) {
            this._pm.DoNotFragment = this.checkBoxNoFrag.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void maskedTextBoxSrcAddr_TypeValidationCompleted(object sender, TypeValidationEventArgs e) {
            this.errorProviderPing.SetError(this.maskedTextBoxSrcAddr, String.Empty);
            if (e.IsValidInput) {
                this._pm.SourceAddress = (IPAddress)e.ReturnValue;
            }
            else {
                this.errorProviderPing.SetError(this.maskedTextBoxSrcAddr, e.Message);
                this.maskedTextBoxSrcAddr.Select();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void numericUpDownRecordHopCount_ValueChanged(object sender, EventArgs e) {
            this._pm.RecordHopCount = (Int32)this.numericUpDownRecordHopCount.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void checkBoxReverseRoute_CheckedChanged(object sender, EventArgs e) {
            this._pm.TestReversRoute = this.checkBoxReverseRoute.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void comboBoxSrcRouteMode_SelectedIndexChanged(object sender, EventArgs e) {
            this._pm.SourceRouteMode = (RouteMode)this.comboBoxSrcRouteMode.SelectedIndex;
            this.textBoxSrcRouteList.Enabled = (this._pm.SourceRouteMode != RouteMode.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">
        /// The object sending the event call.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void numericUpDownTimestamp_ValueChanged(object sender, EventArgs e) {
            this._pm.TimestampCount = (Int32)this.numericUpDownTimestamp.Value;
        }
        #endregion
    }
}
