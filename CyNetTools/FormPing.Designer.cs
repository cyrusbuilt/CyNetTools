namespace CyrusBuilt.CyNetTools
{
    partial class FormPing
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPing));
            this.toolStripActions = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExec = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPageSetup = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrintPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.labelHost = new System.Windows.Forms.Label();
            this.labelPacketLossField = new System.Windows.Forms.Label();
            this.labelPacketLoss = new System.Windows.Forms.Label();
            this.labelLostField = new System.Windows.Forms.Label();
            this.labelLost = new System.Windows.Forms.Label();
            this.labelReceivedField = new System.Windows.Forms.Label();
            this.labelReceived = new System.Windows.Forms.Label();
            this.labelProgress = new System.Windows.Forms.Label();
            this.progressBarPing = new System.Windows.Forms.ProgressBar();
            this.pictureBoxAnim = new System.Windows.Forms.PictureBox();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.maskedTextBoxSrcAddr = new System.Windows.Forms.MaskedTextBox();
            this.labelSrcAddr = new System.Windows.Forms.Label();
            this.groupBoxIPv4 = new System.Windows.Forms.GroupBox();
            this.labelRecordHopCount = new System.Windows.Forms.Label();
            this.numericUpDownRecordHopCount = new System.Windows.Forms.NumericUpDown();
            this.checkBoxNoFrag = new System.Windows.Forms.CheckBox();
            this.labelProtocol = new System.Windows.Forms.Label();
            this.comboBoxProtocol = new System.Windows.Forms.ComboBox();
            this.labelBufferSize = new System.Windows.Forms.Label();
            this.numericUpDownBufferSize = new System.Windows.Forms.NumericUpDown();
            this.checkBoxResolve = new System.Windows.Forms.CheckBox();
            this.textBoxReqCount = new System.Windows.Forms.TextBox();
            this.labelHopCount = new System.Windows.Forms.Label();
            this.checkBoxContinuous = new System.Windows.Forms.CheckBox();
            this.pageSetupDialogTool = new System.Windows.Forms.PageSetupDialog();
            this.printPreviewDialogTool = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialogTool = new System.Windows.Forms.PrintDialog();
            this.saveFileDialogTool = new System.Windows.Forms.SaveFileDialog();
            this.statusStripStat = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRuntime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerRuntime = new System.Windows.Forms.Timer(this.components);
            this.errorProviderPing = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBoxIPv6 = new System.Windows.Forms.GroupBox();
            this.checkBoxReverseRoute = new System.Windows.Forms.CheckBox();
            this.comboBoxSrcRouteMode = new System.Windows.Forms.ComboBox();
            this.labelSrcRouteMode = new System.Windows.Forms.Label();
            this.labelSrcRouteList = new System.Windows.Forms.Label();
            this.textBoxSrcRouteList = new System.Windows.Forms.TextBox();
            this.numericUpDownTimestamp = new System.Windows.Forms.NumericUpDown();
            this.labelTimestamp = new System.Windows.Forms.Label();
            this.labelTTL = new System.Windows.Forms.Label();
            this.textBoxTTL = new System.Windows.Forms.TextBox();
            this.toolStripActions.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnim)).BeginInit();
            this.tabPageSettings.SuspendLayout();
            this.groupBoxIPv4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecordHopCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBufferSize)).BeginInit();
            this.statusStripStat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPing)).BeginInit();
            this.groupBoxIPv6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimestamp)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripActions
            // 
            this.toolStripActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExec,
            this.toolStripButtonCancel,
            this.toolStripButtonSave,
            this.toolStripButtonPageSetup,
            this.toolStripButtonPrintPreview,
            this.toolStripButtonPrint});
            this.toolStripActions.Location = new System.Drawing.Point(0, 0);
            this.toolStripActions.Name = "toolStripActions";
            this.toolStripActions.Size = new System.Drawing.Size(427, 25);
            this.toolStripActions.TabIndex = 0;
            this.toolStripActions.Text = "toolStripPing";
            // 
            // toolStripButtonExec
            // 
            this.toolStripButtonExec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExec.Image = global::CyrusBuilt.CyNetTools.Properties.Resources.control_play_blue;
            this.toolStripButtonExec.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExec.Name = "toolStripButtonExec";
            this.toolStripButtonExec.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExec.Text = "Execute";
            this.toolStripButtonExec.Click += new System.EventHandler(this.toolStripButtonExec_Click);
            // 
            // toolStripButtonCancel
            // 
            this.toolStripButtonCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCancel.Image = global::CyrusBuilt.CyNetTools.Properties.Resources.cross;
            this.toolStripButtonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCancel.Name = "toolStripButtonCancel";
            this.toolStripButtonCancel.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCancel.Text = "Cancel";
            this.toolStripButtonCancel.Click += new System.EventHandler(this.toolStripButtonCancel_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = global::CyrusBuilt.CyNetTools.Properties.Resources.disk;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.Text = "Save";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonPageSetup
            // 
            this.toolStripButtonPageSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPageSetup.Image = global::CyrusBuilt.CyNetTools.Properties.Resources.page_gear;
            this.toolStripButtonPageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPageSetup.Name = "toolStripButtonPageSetup";
            this.toolStripButtonPageSetup.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPageSetup.Text = "Page Setup";
            this.toolStripButtonPageSetup.Click += new System.EventHandler(this.toolStripButtonPageSetup_Click);
            // 
            // toolStripButtonPrintPreview
            // 
            this.toolStripButtonPrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrintPreview.Image = global::CyrusBuilt.CyNetTools.Properties.Resources.page_white_magnify;
            this.toolStripButtonPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrintPreview.Name = "toolStripButtonPrintPreview";
            this.toolStripButtonPrintPreview.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPrintPreview.Text = "toolStripButton1";
            this.toolStripButtonPrintPreview.ToolTipText = "Print Preview";
            this.toolStripButtonPrintPreview.Click += new System.EventHandler(this.toolStripButtonPrintPreview_Click);
            // 
            // toolStripButtonPrint
            // 
            this.toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrint.Image = global::CyrusBuilt.CyNetTools.Properties.Resources.printer;
            this.toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPrint.Text = "Print";
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageOutput);
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.Location = new System.Drawing.Point(0, 28);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(427, 363);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tabPageOutput.Controls.Add(this.textBoxHost);
            this.tabPageOutput.Controls.Add(this.labelHost);
            this.tabPageOutput.Controls.Add(this.labelPacketLossField);
            this.tabPageOutput.Controls.Add(this.labelPacketLoss);
            this.tabPageOutput.Controls.Add(this.labelLostField);
            this.tabPageOutput.Controls.Add(this.labelLost);
            this.tabPageOutput.Controls.Add(this.labelReceivedField);
            this.tabPageOutput.Controls.Add(this.labelReceived);
            this.tabPageOutput.Controls.Add(this.labelProgress);
            this.tabPageOutput.Controls.Add(this.progressBarPing);
            this.tabPageOutput.Controls.Add(this.pictureBoxAnim);
            this.tabPageOutput.Controls.Add(this.richTextBoxOutput);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(419, 337);
            this.tabPageOutput.TabIndex = 0;
            this.tabPageOutput.Text = "Output";
            // 
            // textBoxHost
            // 
            this.textBoxHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHost.Location = new System.Drawing.Point(151, 298);
            this.textBoxHost.Name = "textBoxHost";
            this.textBoxHost.Size = new System.Drawing.Size(202, 20);
            this.textBoxHost.TabIndex = 11;
            // 
            // labelHost
            // 
            this.labelHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelHost.AutoSize = true;
            this.labelHost.Location = new System.Drawing.Point(10, 301);
            this.labelHost.Name = "labelHost";
            this.labelHost.Size = new System.Drawing.Size(121, 13);
            this.labelHost.TabIndex = 10;
            this.labelHost.Text = "Host name or IP to ping:";
            // 
            // labelPacketLossField
            // 
            this.labelPacketLossField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPacketLossField.AutoSize = true;
            this.labelPacketLossField.Location = new System.Drawing.Point(148, 276);
            this.labelPacketLossField.Name = "labelPacketLossField";
            this.labelPacketLossField.Size = new System.Drawing.Size(21, 13);
            this.labelPacketLossField.TabIndex = 9;
            this.labelPacketLossField.Text = "0%";
            // 
            // labelPacketLoss
            // 
            this.labelPacketLoss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPacketLoss.AutoSize = true;
            this.labelPacketLoss.Location = new System.Drawing.Point(62, 276);
            this.labelPacketLoss.Name = "labelPacketLoss";
            this.labelPacketLoss.Size = new System.Drawing.Size(69, 13);
            this.labelPacketLoss.TabIndex = 8;
            this.labelPacketLoss.Text = "Packet Loss:";
            // 
            // labelLostField
            // 
            this.labelLostField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLostField.AutoSize = true;
            this.labelLostField.Location = new System.Drawing.Point(148, 255);
            this.labelLostField.Name = "labelLostField";
            this.labelLostField.Size = new System.Drawing.Size(13, 13);
            this.labelLostField.TabIndex = 7;
            this.labelLostField.Text = "0";
            // 
            // labelLost
            // 
            this.labelLost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelLost.AutoSize = true;
            this.labelLost.Location = new System.Drawing.Point(45, 255);
            this.labelLost.Name = "labelLost";
            this.labelLost.Size = new System.Drawing.Size(86, 13);
            this.labelLost.TabIndex = 6;
            this.labelLost.Text = "Responses Lost:";
            // 
            // labelReceivedField
            // 
            this.labelReceivedField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelReceivedField.AutoSize = true;
            this.labelReceivedField.Location = new System.Drawing.Point(148, 231);
            this.labelReceivedField.Name = "labelReceivedField";
            this.labelReceivedField.Size = new System.Drawing.Size(13, 13);
            this.labelReceivedField.TabIndex = 5;
            this.labelReceivedField.Text = "0";
            // 
            // labelReceived
            // 
            this.labelReceived.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelReceived.AutoSize = true;
            this.labelReceived.Location = new System.Drawing.Point(19, 231);
            this.labelReceived.Name = "labelReceived";
            this.labelReceived.Size = new System.Drawing.Size(112, 13);
            this.labelReceived.TabIndex = 4;
            this.labelReceived.Text = "Responses Received:";
            // 
            // labelProgress
            // 
            this.labelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelProgress.AutoSize = true;
            this.labelProgress.ForeColor = System.Drawing.Color.White;
            this.labelProgress.Location = new System.Drawing.Point(23, 206);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(108, 13);
            this.labelProgress.TabIndex = 3;
            this.labelProgress.Text = "Operational Progress:";
            // 
            // progressBarPing
            // 
            this.progressBarPing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarPing.Location = new System.Drawing.Point(151, 196);
            this.progressBarPing.Name = "progressBarPing";
            this.progressBarPing.Size = new System.Drawing.Size(260, 23);
            this.progressBarPing.TabIndex = 2;
            // 
            // pictureBoxAnim
            // 
            this.pictureBoxAnim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxAnim.Image = global::CyrusBuilt.CyNetTools.Properties.Resources.status_anim;
            this.pictureBoxAnim.Location = new System.Drawing.Point(365, 302);
            this.pictureBoxAnim.Name = "pictureBoxAnim";
            this.pictureBoxAnim.Size = new System.Drawing.Size(46, 12);
            this.pictureBoxAnim.TabIndex = 1;
            this.pictureBoxAnim.TabStop = false;
            this.pictureBoxAnim.Visible = false;
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOutput.BackColor = System.Drawing.Color.Black;
            this.richTextBoxOutput.ForeColor = System.Drawing.Color.Cyan;
            this.richTextBoxOutput.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.ReadOnly = true;
            this.richTextBoxOutput.Size = new System.Drawing.Size(413, 187);
            this.richTextBoxOutput.TabIndex = 0;
            this.richTextBoxOutput.Text = "Ready.";
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tabPageSettings.Controls.Add(this.textBoxTTL);
            this.tabPageSettings.Controls.Add(this.labelTTL);
            this.tabPageSettings.Controls.Add(this.groupBoxIPv6);
            this.tabPageSettings.Controls.Add(this.maskedTextBoxSrcAddr);
            this.tabPageSettings.Controls.Add(this.labelSrcAddr);
            this.tabPageSettings.Controls.Add(this.groupBoxIPv4);
            this.tabPageSettings.Controls.Add(this.labelProtocol);
            this.tabPageSettings.Controls.Add(this.comboBoxProtocol);
            this.tabPageSettings.Controls.Add(this.labelBufferSize);
            this.tabPageSettings.Controls.Add(this.numericUpDownBufferSize);
            this.tabPageSettings.Controls.Add(this.checkBoxResolve);
            this.tabPageSettings.Controls.Add(this.textBoxReqCount);
            this.tabPageSettings.Controls.Add(this.labelHopCount);
            this.tabPageSettings.Controls.Add(this.checkBoxContinuous);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(419, 337);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            // 
            // maskedTextBoxSrcAddr
            // 
            this.maskedTextBoxSrcAddr.Location = new System.Drawing.Point(96, 144);
            this.maskedTextBoxSrcAddr.Mask = "###.###.###.###";
            this.maskedTextBoxSrcAddr.Name = "maskedTextBoxSrcAddr";
            this.maskedTextBoxSrcAddr.Size = new System.Drawing.Size(101, 20);
            this.maskedTextBoxSrcAddr.TabIndex = 10;
            this.maskedTextBoxSrcAddr.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.maskedTextBoxSrcAddr_TypeValidationCompleted);
            // 
            // labelSrcAddr
            // 
            this.labelSrcAddr.AutoSize = true;
            this.labelSrcAddr.Location = new System.Drawing.Point(5, 147);
            this.labelSrcAddr.Name = "labelSrcAddr";
            this.labelSrcAddr.Size = new System.Drawing.Size(85, 13);
            this.labelSrcAddr.TabIndex = 9;
            this.labelSrcAddr.Text = "Source Address:";
            // 
            // groupBoxIPv4
            // 
            this.groupBoxIPv4.Controls.Add(this.labelTimestamp);
            this.groupBoxIPv4.Controls.Add(this.numericUpDownTimestamp);
            this.groupBoxIPv4.Controls.Add(this.textBoxSrcRouteList);
            this.groupBoxIPv4.Controls.Add(this.labelSrcRouteList);
            this.groupBoxIPv4.Controls.Add(this.labelSrcRouteMode);
            this.groupBoxIPv4.Controls.Add(this.comboBoxSrcRouteMode);
            this.groupBoxIPv4.Controls.Add(this.labelRecordHopCount);
            this.groupBoxIPv4.Controls.Add(this.numericUpDownRecordHopCount);
            this.groupBoxIPv4.Controls.Add(this.checkBoxNoFrag);
            this.groupBoxIPv4.ForeColor = System.Drawing.Color.White;
            this.groupBoxIPv4.Location = new System.Drawing.Point(9, 170);
            this.groupBoxIPv4.Name = "groupBoxIPv4";
            this.groupBoxIPv4.Size = new System.Drawing.Size(393, 161);
            this.groupBoxIPv4.TabIndex = 8;
            this.groupBoxIPv4.TabStop = false;
            this.groupBoxIPv4.Text = "IPv4 Only";
            // 
            // labelRecordHopCount
            // 
            this.labelRecordHopCount.AutoSize = true;
            this.labelRecordHopCount.Location = new System.Drawing.Point(6, 44);
            this.labelRecordHopCount.Name = "labelRecordHopCount";
            this.labelRecordHopCount.Size = new System.Drawing.Size(96, 13);
            this.labelRecordHopCount.TabIndex = 2;
            this.labelRecordHopCount.Text = "Record hop count:";
            // 
            // numericUpDownRecordHopCount
            // 
            this.numericUpDownRecordHopCount.Location = new System.Drawing.Point(108, 42);
            this.numericUpDownRecordHopCount.Name = "numericUpDownRecordHopCount";
            this.numericUpDownRecordHopCount.Size = new System.Drawing.Size(58, 20);
            this.numericUpDownRecordHopCount.TabIndex = 1;
            this.numericUpDownRecordHopCount.ValueChanged += new System.EventHandler(this.numericUpDownRecordHopCount_ValueChanged);
            // 
            // checkBoxNoFrag
            // 
            this.checkBoxNoFrag.AutoSize = true;
            this.checkBoxNoFrag.Location = new System.Drawing.Point(6, 19);
            this.checkBoxNoFrag.Name = "checkBoxNoFrag";
            this.checkBoxNoFrag.Size = new System.Drawing.Size(107, 17);
            this.checkBoxNoFrag.TabIndex = 0;
            this.checkBoxNoFrag.Text = "Do Not Fragment";
            this.checkBoxNoFrag.UseVisualStyleBackColor = true;
            this.checkBoxNoFrag.CheckedChanged += new System.EventHandler(this.checkBoxNoFrag_CheckedChanged);
            // 
            // labelProtocol
            // 
            this.labelProtocol.AutoSize = true;
            this.labelProtocol.Location = new System.Drawing.Point(6, 120);
            this.labelProtocol.Name = "labelProtocol";
            this.labelProtocol.Size = new System.Drawing.Size(62, 13);
            this.labelProtocol.TabIndex = 7;
            this.labelProtocol.Text = "IP Protocol:";
            // 
            // comboBoxProtocol
            // 
            this.comboBoxProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProtocol.FormattingEnabled = true;
            this.comboBoxProtocol.Location = new System.Drawing.Point(75, 117);
            this.comboBoxProtocol.Name = "comboBoxProtocol";
            this.comboBoxProtocol.Size = new System.Drawing.Size(59, 21);
            this.comboBoxProtocol.TabIndex = 6;
            this.comboBoxProtocol.SelectedIndexChanged += new System.EventHandler(this.comboBoxProtocol_SelectedIndexChanged);
            // 
            // labelBufferSize
            // 
            this.labelBufferSize.AutoSize = true;
            this.labelBufferSize.Location = new System.Drawing.Point(5, 93);
            this.labelBufferSize.Name = "labelBufferSize";
            this.labelBufferSize.Size = new System.Drawing.Size(61, 13);
            this.labelBufferSize.TabIndex = 5;
            this.labelBufferSize.Text = "Buffer Size:";
            // 
            // numericUpDownBufferSize
            // 
            this.numericUpDownBufferSize.Location = new System.Drawing.Point(75, 91);
            this.numericUpDownBufferSize.Name = "numericUpDownBufferSize";
            this.numericUpDownBufferSize.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownBufferSize.TabIndex = 4;
            this.numericUpDownBufferSize.ValueChanged += new System.EventHandler(this.numericUpDownBufferSize_ValueChanged);
            // 
            // checkBoxResolve
            // 
            this.checkBoxResolve.AutoSize = true;
            this.checkBoxResolve.Checked = true;
            this.checkBoxResolve.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxResolve.Location = new System.Drawing.Point(8, 68);
            this.checkBoxResolve.Name = "checkBoxResolve";
            this.checkBoxResolve.Size = new System.Drawing.Size(121, 17);
            this.checkBoxResolve.TabIndex = 3;
            this.checkBoxResolve.Text = "Resolve Hostnames";
            this.checkBoxResolve.UseVisualStyleBackColor = true;
            this.checkBoxResolve.CheckedChanged += new System.EventHandler(this.checkBoxResolve_CheckedChanged);
            // 
            // textBoxReqCount
            // 
            this.textBoxReqCount.Location = new System.Drawing.Point(96, 42);
            this.textBoxReqCount.Name = "textBoxReqCount";
            this.textBoxReqCount.Size = new System.Drawing.Size(38, 20);
            this.textBoxReqCount.TabIndex = 2;
            // 
            // labelHopCount
            // 
            this.labelHopCount.AutoSize = true;
            this.labelHopCount.Location = new System.Drawing.Point(6, 45);
            this.labelHopCount.Name = "labelHopCount";
            this.labelHopCount.Size = new System.Drawing.Size(79, 13);
            this.labelHopCount.TabIndex = 1;
            this.labelHopCount.Text = "Ping Requests:";
            // 
            // checkBoxContinuous
            // 
            this.checkBoxContinuous.AutoSize = true;
            this.checkBoxContinuous.Location = new System.Drawing.Point(8, 16);
            this.checkBoxContinuous.Name = "checkBoxContinuous";
            this.checkBoxContinuous.Size = new System.Drawing.Size(109, 17);
            this.checkBoxContinuous.TabIndex = 0;
            this.checkBoxContinuous.Text = "Ping continuously";
            this.checkBoxContinuous.UseVisualStyleBackColor = true;
            this.checkBoxContinuous.CheckedChanged += new System.EventHandler(this.checkBoxContinuous_CheckedChanged);
            // 
            // printPreviewDialogTool
            // 
            this.printPreviewDialogTool.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialogTool.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialogTool.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialogTool.Enabled = true;
            this.printPreviewDialogTool.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialogTool.Icon")));
            this.printPreviewDialogTool.Name = "printPreviewDialogTool";
            this.printPreviewDialogTool.Visible = false;
            // 
            // printDialogTool
            // 
            this.printDialogTool.UseEXDialog = true;
            // 
            // statusStripStat
            // 
            this.statusStripStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.statusStripStat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStat,
            this.toolStripStatusLabelRuntime});
            this.statusStripStat.Location = new System.Drawing.Point(0, 395);
            this.statusStripStat.Name = "statusStripStat";
            this.statusStripStat.Size = new System.Drawing.Size(427, 22);
            this.statusStripStat.TabIndex = 3;
            this.statusStripStat.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStat
            // 
            this.toolStripStatusLabelStat.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabelStat.Name = "toolStripStatusLabelStat";
            this.toolStripStatusLabelStat.Size = new System.Drawing.Size(77, 17);
            this.toolStripStatusLabelStat.Text = "Status: Ready";
            // 
            // toolStripStatusLabelRuntime
            // 
            this.toolStripStatusLabelRuntime.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabelRuntime.Name = "toolStripStatusLabelRuntime";
            this.toolStripStatusLabelRuntime.Size = new System.Drawing.Size(85, 17);
            this.toolStripStatusLabelRuntime.Text = "Runtime: 00:00";
            // 
            // timerRuntime
            // 
            this.timerRuntime.Interval = 1000;
            this.timerRuntime.Tick += new System.EventHandler(this.timerRuntime_Tick);
            // 
            // errorProviderPing
            // 
            this.errorProviderPing.ContainerControl = this;
            // 
            // groupBoxIPv6
            // 
            this.groupBoxIPv6.Controls.Add(this.checkBoxReverseRoute);
            this.groupBoxIPv6.ForeColor = System.Drawing.Color.White;
            this.groupBoxIPv6.Location = new System.Drawing.Point(211, 6);
            this.groupBoxIPv6.Name = "groupBoxIPv6";
            this.groupBoxIPv6.Size = new System.Drawing.Size(200, 112);
            this.groupBoxIPv6.TabIndex = 11;
            this.groupBoxIPv6.TabStop = false;
            this.groupBoxIPv6.Text = "IPv6 Only";
            // 
            // checkBoxReverseRoute
            // 
            this.checkBoxReverseRoute.AutoSize = true;
            this.checkBoxReverseRoute.ForeColor = System.Drawing.Color.White;
            this.checkBoxReverseRoute.Location = new System.Drawing.Point(13, 37);
            this.checkBoxReverseRoute.Name = "checkBoxReverseRoute";
            this.checkBoxReverseRoute.Size = new System.Drawing.Size(112, 17);
            this.checkBoxReverseRoute.TabIndex = 0;
            this.checkBoxReverseRoute.Text = "Test reverse route";
            this.checkBoxReverseRoute.UseVisualStyleBackColor = true;
            this.checkBoxReverseRoute.CheckedChanged += new System.EventHandler(this.checkBoxReverseRoute_CheckedChanged);
            // 
            // comboBoxSrcRouteMode
            // 
            this.comboBoxSrcRouteMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSrcRouteMode.FormattingEnabled = true;
            this.comboBoxSrcRouteMode.Location = new System.Drawing.Point(108, 68);
            this.comboBoxSrcRouteMode.Name = "comboBoxSrcRouteMode";
            this.comboBoxSrcRouteMode.Size = new System.Drawing.Size(69, 21);
            this.comboBoxSrcRouteMode.TabIndex = 3;
            this.comboBoxSrcRouteMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxSrcRouteMode_SelectedIndexChanged);
            // 
            // labelSrcRouteMode
            // 
            this.labelSrcRouteMode.AutoSize = true;
            this.labelSrcRouteMode.Location = new System.Drawing.Point(6, 71);
            this.labelSrcRouteMode.Name = "labelSrcRouteMode";
            this.labelSrcRouteMode.Size = new System.Drawing.Size(100, 13);
            this.labelSrcRouteMode.TabIndex = 4;
            this.labelSrcRouteMode.Text = "Source route mode:";
            // 
            // labelSrcRouteList
            // 
            this.labelSrcRouteList.AutoSize = true;
            this.labelSrcRouteList.Location = new System.Drawing.Point(6, 99);
            this.labelSrcRouteList.Name = "labelSrcRouteList";
            this.labelSrcRouteList.Size = new System.Drawing.Size(86, 13);
            this.labelSrcRouteList.TabIndex = 5;
            this.labelSrcRouteList.Text = "Source route list:";
            // 
            // textBoxSrcRouteList
            // 
            this.textBoxSrcRouteList.Enabled = false;
            this.textBoxSrcRouteList.Location = new System.Drawing.Point(108, 96);
            this.textBoxSrcRouteList.Name = "textBoxSrcRouteList";
            this.textBoxSrcRouteList.Size = new System.Drawing.Size(219, 20);
            this.textBoxSrcRouteList.TabIndex = 6;
            // 
            // numericUpDownTimestamp
            // 
            this.numericUpDownTimestamp.Location = new System.Drawing.Point(108, 122);
            this.numericUpDownTimestamp.Name = "numericUpDownTimestamp";
            this.numericUpDownTimestamp.Size = new System.Drawing.Size(58, 20);
            this.numericUpDownTimestamp.TabIndex = 7;
            this.numericUpDownTimestamp.ValueChanged += new System.EventHandler(this.numericUpDownTimestamp_ValueChanged);
            // 
            // labelTimestamp
            // 
            this.labelTimestamp.AutoSize = true;
            this.labelTimestamp.Location = new System.Drawing.Point(6, 124);
            this.labelTimestamp.Name = "labelTimestamp";
            this.labelTimestamp.Size = new System.Drawing.Size(91, 13);
            this.labelTimestamp.TabIndex = 8;
            this.labelTimestamp.Text = "Timestamp count:";
            // 
            // labelTTL
            // 
            this.labelTTL.AutoSize = true;
            this.labelTTL.Location = new System.Drawing.Point(221, 147);
            this.labelTTL.Name = "labelTTL";
            this.labelTTL.Size = new System.Drawing.Size(30, 13);
            this.labelTTL.TabIndex = 12;
            this.labelTTL.Text = "TTL:";
            // 
            // textBoxTTL
            // 
            this.textBoxTTL.Location = new System.Drawing.Point(257, 144);
            this.textBoxTTL.Name = "textBoxTTL";
            this.textBoxTTL.Size = new System.Drawing.Size(38, 20);
            this.textBoxTTL.TabIndex = 13;
            // 
            // FormPing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(427, 417);
            this.Controls.Add(this.statusStripStat);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.toolStripActions);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "FormPing";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Ping";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExternalToolBase_FormClosing);
            this.Load += new System.EventHandler(this.FormPing_Load);
            this.toolStripActions.ResumeLayout(false);
            this.toolStripActions.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnim)).EndInit();
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            this.groupBoxIPv4.ResumeLayout(false);
            this.groupBoxIPv4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecordHopCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBufferSize)).EndInit();
            this.statusStripStat.ResumeLayout(false);
            this.statusStripStat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPing)).EndInit();
            this.groupBoxIPv6.ResumeLayout(false);
            this.groupBoxIPv6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimestamp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripActions;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonPageSetup;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrintPreview;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrint;
        private System.Windows.Forms.PageSetupDialog pageSetupDialogTool;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialogTool;
        private System.Windows.Forms.PrintDialog printDialogTool;
        private System.Windows.Forms.SaveFileDialog saveFileDialogTool;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.ToolStripButton toolStripButtonExec;
        private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
        private System.Windows.Forms.StatusStrip statusStripStat;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStat;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRuntime;
        private System.Windows.Forms.Timer timerRuntime;
        private System.Windows.Forms.PictureBox pictureBoxAnim;
        private System.Windows.Forms.TextBox textBoxReqCount;
        private System.Windows.Forms.Label labelHopCount;
        private System.Windows.Forms.CheckBox checkBoxContinuous;
        private System.Windows.Forms.CheckBox checkBoxResolve;
        private System.Windows.Forms.Label labelBufferSize;
        private System.Windows.Forms.NumericUpDown numericUpDownBufferSize;
        private System.Windows.Forms.ComboBox comboBoxProtocol;
        private System.Windows.Forms.Label labelProtocol;
        private System.Windows.Forms.GroupBox groupBoxIPv4;
        private System.Windows.Forms.CheckBox checkBoxNoFrag;
        private System.Windows.Forms.ProgressBar progressBarPing;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Label labelReceivedField;
        private System.Windows.Forms.Label labelReceived;
        private System.Windows.Forms.Label labelLostField;
        private System.Windows.Forms.Label labelLost;
        private System.Windows.Forms.Label labelPacketLoss;
        private System.Windows.Forms.Label labelPacketLossField;
        private System.Windows.Forms.TextBox textBoxHost;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.ErrorProvider errorProviderPing;
        private System.Windows.Forms.Label labelSrcAddr;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxSrcAddr;
        private System.Windows.Forms.Label labelRecordHopCount;
        private System.Windows.Forms.NumericUpDown numericUpDownRecordHopCount;
        private System.Windows.Forms.GroupBox groupBoxIPv6;
        private System.Windows.Forms.CheckBox checkBoxReverseRoute;
        private System.Windows.Forms.ComboBox comboBoxSrcRouteMode;
        private System.Windows.Forms.Label labelSrcRouteMode;
        private System.Windows.Forms.TextBox textBoxSrcRouteList;
        private System.Windows.Forms.Label labelSrcRouteList;
        private System.Windows.Forms.NumericUpDown numericUpDownTimestamp;
        private System.Windows.Forms.Label labelTimestamp;
        private System.Windows.Forms.TextBox textBoxTTL;
        private System.Windows.Forms.Label labelTTL;
    }
}