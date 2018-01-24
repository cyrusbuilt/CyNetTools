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
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.textBoxTTL = new System.Windows.Forms.TextBox();
            this.labelTTL = new System.Windows.Forms.Label();
            this.groupBoxIPv6 = new System.Windows.Forms.GroupBox();
            this.checkBoxReverseRoute = new System.Windows.Forms.CheckBox();
            this.maskedTextBoxSrcAddr = new System.Windows.Forms.MaskedTextBox();
            this.labelSrcAddr = new System.Windows.Forms.Label();
            this.groupBoxIPv4 = new System.Windows.Forms.GroupBox();
            this.labelTimestamp = new System.Windows.Forms.Label();
            this.numericUpDownTimestamp = new System.Windows.Forms.NumericUpDown();
            this.textBoxSrcRouteList = new System.Windows.Forms.TextBox();
            this.labelSrcRouteList = new System.Windows.Forms.Label();
            this.labelSrcRouteMode = new System.Windows.Forms.Label();
            this.comboBoxSrcRouteMode = new System.Windows.Forms.ComboBox();
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
            this.pictureBoxAnim = new System.Windows.Forms.PictureBox();
            this.toolStripButtonExec = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPageSetup = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrintPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripActions.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.groupBoxIPv6.SuspendLayout();
            this.groupBoxIPv4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimestamp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecordHopCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBufferSize)).BeginInit();
            this.statusStripStat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnim)).BeginInit();
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
            this.toolStripActions.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripActions.Size = new System.Drawing.Size(854, 25);
            this.toolStripActions.TabIndex = 0;
            this.toolStripActions.Text = "toolStripPing";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageOutput);
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.Location = new System.Drawing.Point(0, 45);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(6);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(854, 698);
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
            this.tabPageOutput.Location = new System.Drawing.Point(8, 39);
            this.tabPageOutput.Margin = new System.Windows.Forms.Padding(6);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(6);
            this.tabPageOutput.Size = new System.Drawing.Size(838, 651);
            this.tabPageOutput.TabIndex = 0;
            this.tabPageOutput.Text = "Output";
            // 
            // textBoxHost
            // 
            this.textBoxHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHost.Location = new System.Drawing.Point(302, 573);
            this.textBoxHost.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxHost.Name = "textBoxHost";
            this.textBoxHost.Size = new System.Drawing.Size(400, 31);
            this.textBoxHost.TabIndex = 11;
            // 
            // labelHost
            // 
            this.labelHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelHost.AutoSize = true;
            this.labelHost.Location = new System.Drawing.Point(20, 579);
            this.labelHost.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelHost.Name = "labelHost";
            this.labelHost.Size = new System.Drawing.Size(242, 25);
            this.labelHost.TabIndex = 10;
            this.labelHost.Text = "Host name or IP to ping:";
            // 
            // labelPacketLossField
            // 
            this.labelPacketLossField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPacketLossField.AutoSize = true;
            this.labelPacketLossField.Location = new System.Drawing.Point(296, 531);
            this.labelPacketLossField.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPacketLossField.Name = "labelPacketLossField";
            this.labelPacketLossField.Size = new System.Drawing.Size(43, 25);
            this.labelPacketLossField.TabIndex = 9;
            this.labelPacketLossField.Text = "0%";
            // 
            // labelPacketLoss
            // 
            this.labelPacketLoss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPacketLoss.AutoSize = true;
            this.labelPacketLoss.Location = new System.Drawing.Point(124, 531);
            this.labelPacketLoss.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPacketLoss.Name = "labelPacketLoss";
            this.labelPacketLoss.Size = new System.Drawing.Size(136, 25);
            this.labelPacketLoss.TabIndex = 8;
            this.labelPacketLoss.Text = "Packet Loss:";
            // 
            // labelLostField
            // 
            this.labelLostField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLostField.AutoSize = true;
            this.labelLostField.Location = new System.Drawing.Point(296, 490);
            this.labelLostField.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelLostField.Name = "labelLostField";
            this.labelLostField.Size = new System.Drawing.Size(24, 25);
            this.labelLostField.TabIndex = 7;
            this.labelLostField.Text = "0";
            // 
            // labelLost
            // 
            this.labelLost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelLost.AutoSize = true;
            this.labelLost.Location = new System.Drawing.Point(90, 490);
            this.labelLost.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelLost.Name = "labelLost";
            this.labelLost.Size = new System.Drawing.Size(173, 25);
            this.labelLost.TabIndex = 6;
            this.labelLost.Text = "Responses Lost:";
            // 
            // labelReceivedField
            // 
            this.labelReceivedField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelReceivedField.AutoSize = true;
            this.labelReceivedField.Location = new System.Drawing.Point(296, 444);
            this.labelReceivedField.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelReceivedField.Name = "labelReceivedField";
            this.labelReceivedField.Size = new System.Drawing.Size(24, 25);
            this.labelReceivedField.TabIndex = 5;
            this.labelReceivedField.Text = "0";
            // 
            // labelReceived
            // 
            this.labelReceived.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelReceived.AutoSize = true;
            this.labelReceived.Location = new System.Drawing.Point(38, 444);
            this.labelReceived.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelReceived.Name = "labelReceived";
            this.labelReceived.Size = new System.Drawing.Size(222, 25);
            this.labelReceived.TabIndex = 4;
            this.labelReceived.Text = "Responses Received:";
            // 
            // labelProgress
            // 
            this.labelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelProgress.AutoSize = true;
            this.labelProgress.ForeColor = System.Drawing.Color.White;
            this.labelProgress.Location = new System.Drawing.Point(46, 396);
            this.labelProgress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(221, 25);
            this.labelProgress.TabIndex = 3;
            this.labelProgress.Text = "Operational Progress:";
            // 
            // progressBarPing
            // 
            this.progressBarPing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarPing.Location = new System.Drawing.Point(302, 377);
            this.progressBarPing.Margin = new System.Windows.Forms.Padding(6);
            this.progressBarPing.Name = "progressBarPing";
            this.progressBarPing.Size = new System.Drawing.Size(520, 44);
            this.progressBarPing.TabIndex = 2;
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOutput.BackColor = System.Drawing.Color.Black;
            this.richTextBoxOutput.ForeColor = System.Drawing.Color.Cyan;
            this.richTextBoxOutput.Location = new System.Drawing.Point(6, 6);
            this.richTextBoxOutput.Margin = new System.Windows.Forms.Padding(6);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.ReadOnly = true;
            this.richTextBoxOutput.Size = new System.Drawing.Size(822, 356);
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
            this.tabPageSettings.Location = new System.Drawing.Point(8, 39);
            this.tabPageSettings.Margin = new System.Windows.Forms.Padding(6);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(6);
            this.tabPageSettings.Size = new System.Drawing.Size(838, 651);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            // 
            // textBoxTTL
            // 
            this.textBoxTTL.Location = new System.Drawing.Point(514, 277);
            this.textBoxTTL.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxTTL.Name = "textBoxTTL";
            this.textBoxTTL.Size = new System.Drawing.Size(72, 31);
            this.textBoxTTL.TabIndex = 13;
            // 
            // labelTTL
            // 
            this.labelTTL.AutoSize = true;
            this.labelTTL.Location = new System.Drawing.Point(442, 283);
            this.labelTTL.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelTTL.Name = "labelTTL";
            this.labelTTL.Size = new System.Drawing.Size(56, 25);
            this.labelTTL.TabIndex = 12;
            this.labelTTL.Text = "TTL:";
            // 
            // groupBoxIPv6
            // 
            this.groupBoxIPv6.Controls.Add(this.checkBoxReverseRoute);
            this.groupBoxIPv6.ForeColor = System.Drawing.Color.White;
            this.groupBoxIPv6.Location = new System.Drawing.Point(422, 12);
            this.groupBoxIPv6.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxIPv6.Name = "groupBoxIPv6";
            this.groupBoxIPv6.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxIPv6.Size = new System.Drawing.Size(400, 215);
            this.groupBoxIPv6.TabIndex = 11;
            this.groupBoxIPv6.TabStop = false;
            this.groupBoxIPv6.Text = "IPv6 Only";
            // 
            // checkBoxReverseRoute
            // 
            this.checkBoxReverseRoute.AutoSize = true;
            this.checkBoxReverseRoute.ForeColor = System.Drawing.Color.White;
            this.checkBoxReverseRoute.Location = new System.Drawing.Point(26, 71);
            this.checkBoxReverseRoute.Margin = new System.Windows.Forms.Padding(6);
            this.checkBoxReverseRoute.Name = "checkBoxReverseRoute";
            this.checkBoxReverseRoute.Size = new System.Drawing.Size(219, 29);
            this.checkBoxReverseRoute.TabIndex = 0;
            this.checkBoxReverseRoute.Text = "Test reverse route";
            this.checkBoxReverseRoute.UseVisualStyleBackColor = true;
            this.checkBoxReverseRoute.CheckedChanged += new System.EventHandler(this.checkBoxReverseRoute_CheckedChanged);
            // 
            // maskedTextBoxSrcAddr
            // 
            this.maskedTextBoxSrcAddr.Location = new System.Drawing.Point(192, 277);
            this.maskedTextBoxSrcAddr.Margin = new System.Windows.Forms.Padding(6);
            this.maskedTextBoxSrcAddr.Mask = "###.###.###.###";
            this.maskedTextBoxSrcAddr.Name = "maskedTextBoxSrcAddr";
            this.maskedTextBoxSrcAddr.Size = new System.Drawing.Size(198, 31);
            this.maskedTextBoxSrcAddr.TabIndex = 10;
            this.maskedTextBoxSrcAddr.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.maskedTextBoxSrcAddr_TypeValidationCompleted);
            // 
            // labelSrcAddr
            // 
            this.labelSrcAddr.AutoSize = true;
            this.labelSrcAddr.Location = new System.Drawing.Point(10, 283);
            this.labelSrcAddr.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelSrcAddr.Name = "labelSrcAddr";
            this.labelSrcAddr.Size = new System.Drawing.Size(171, 25);
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
            this.groupBoxIPv4.Location = new System.Drawing.Point(18, 327);
            this.groupBoxIPv4.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxIPv4.Name = "groupBoxIPv4";
            this.groupBoxIPv4.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxIPv4.Size = new System.Drawing.Size(786, 310);
            this.groupBoxIPv4.TabIndex = 8;
            this.groupBoxIPv4.TabStop = false;
            this.groupBoxIPv4.Text = "IPv4 Only";
            // 
            // labelTimestamp
            // 
            this.labelTimestamp.AutoSize = true;
            this.labelTimestamp.Location = new System.Drawing.Point(12, 238);
            this.labelTimestamp.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelTimestamp.Name = "labelTimestamp";
            this.labelTimestamp.Size = new System.Drawing.Size(182, 25);
            this.labelTimestamp.TabIndex = 8;
            this.labelTimestamp.Text = "Timestamp count:";
            // 
            // numericUpDownTimestamp
            // 
            this.numericUpDownTimestamp.Location = new System.Drawing.Point(216, 235);
            this.numericUpDownTimestamp.Margin = new System.Windows.Forms.Padding(6);
            this.numericUpDownTimestamp.Name = "numericUpDownTimestamp";
            this.numericUpDownTimestamp.Size = new System.Drawing.Size(116, 31);
            this.numericUpDownTimestamp.TabIndex = 7;
            this.numericUpDownTimestamp.ValueChanged += new System.EventHandler(this.numericUpDownTimestamp_ValueChanged);
            // 
            // textBoxSrcRouteList
            // 
            this.textBoxSrcRouteList.Enabled = false;
            this.textBoxSrcRouteList.Location = new System.Drawing.Point(216, 185);
            this.textBoxSrcRouteList.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxSrcRouteList.Name = "textBoxSrcRouteList";
            this.textBoxSrcRouteList.Size = new System.Drawing.Size(434, 31);
            this.textBoxSrcRouteList.TabIndex = 6;
            // 
            // labelSrcRouteList
            // 
            this.labelSrcRouteList.AutoSize = true;
            this.labelSrcRouteList.Location = new System.Drawing.Point(12, 190);
            this.labelSrcRouteList.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelSrcRouteList.Name = "labelSrcRouteList";
            this.labelSrcRouteList.Size = new System.Drawing.Size(174, 25);
            this.labelSrcRouteList.TabIndex = 5;
            this.labelSrcRouteList.Text = "Source route list:";
            // 
            // labelSrcRouteMode
            // 
            this.labelSrcRouteMode.AutoSize = true;
            this.labelSrcRouteMode.Location = new System.Drawing.Point(12, 137);
            this.labelSrcRouteMode.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelSrcRouteMode.Name = "labelSrcRouteMode";
            this.labelSrcRouteMode.Size = new System.Drawing.Size(200, 25);
            this.labelSrcRouteMode.TabIndex = 4;
            this.labelSrcRouteMode.Text = "Source route mode:";
            // 
            // comboBoxSrcRouteMode
            // 
            this.comboBoxSrcRouteMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSrcRouteMode.FormattingEnabled = true;
            this.comboBoxSrcRouteMode.Location = new System.Drawing.Point(216, 131);
            this.comboBoxSrcRouteMode.Margin = new System.Windows.Forms.Padding(6);
            this.comboBoxSrcRouteMode.Name = "comboBoxSrcRouteMode";
            this.comboBoxSrcRouteMode.Size = new System.Drawing.Size(134, 33);
            this.comboBoxSrcRouteMode.TabIndex = 3;
            this.comboBoxSrcRouteMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxSrcRouteMode_SelectedIndexChanged);
            // 
            // labelRecordHopCount
            // 
            this.labelRecordHopCount.AutoSize = true;
            this.labelRecordHopCount.Location = new System.Drawing.Point(12, 85);
            this.labelRecordHopCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelRecordHopCount.Name = "labelRecordHopCount";
            this.labelRecordHopCount.Size = new System.Drawing.Size(188, 25);
            this.labelRecordHopCount.TabIndex = 2;
            this.labelRecordHopCount.Text = "Record hop count:";
            // 
            // numericUpDownRecordHopCount
            // 
            this.numericUpDownRecordHopCount.Location = new System.Drawing.Point(216, 81);
            this.numericUpDownRecordHopCount.Margin = new System.Windows.Forms.Padding(6);
            this.numericUpDownRecordHopCount.Name = "numericUpDownRecordHopCount";
            this.numericUpDownRecordHopCount.Size = new System.Drawing.Size(116, 31);
            this.numericUpDownRecordHopCount.TabIndex = 1;
            this.numericUpDownRecordHopCount.ValueChanged += new System.EventHandler(this.numericUpDownRecordHopCount_ValueChanged);
            // 
            // checkBoxNoFrag
            // 
            this.checkBoxNoFrag.AutoSize = true;
            this.checkBoxNoFrag.Location = new System.Drawing.Point(12, 37);
            this.checkBoxNoFrag.Margin = new System.Windows.Forms.Padding(6);
            this.checkBoxNoFrag.Name = "checkBoxNoFrag";
            this.checkBoxNoFrag.Size = new System.Drawing.Size(207, 29);
            this.checkBoxNoFrag.TabIndex = 0;
            this.checkBoxNoFrag.Text = "Do Not Fragment";
            this.checkBoxNoFrag.UseVisualStyleBackColor = true;
            this.checkBoxNoFrag.CheckedChanged += new System.EventHandler(this.checkBoxNoFrag_CheckedChanged);
            // 
            // labelProtocol
            // 
            this.labelProtocol.AutoSize = true;
            this.labelProtocol.Location = new System.Drawing.Point(12, 231);
            this.labelProtocol.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelProtocol.Name = "labelProtocol";
            this.labelProtocol.Size = new System.Drawing.Size(122, 25);
            this.labelProtocol.TabIndex = 7;
            this.labelProtocol.Text = "IP Protocol:";
            // 
            // comboBoxProtocol
            // 
            this.comboBoxProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProtocol.FormattingEnabled = true;
            this.comboBoxProtocol.Location = new System.Drawing.Point(150, 225);
            this.comboBoxProtocol.Margin = new System.Windows.Forms.Padding(6);
            this.comboBoxProtocol.Name = "comboBoxProtocol";
            this.comboBoxProtocol.Size = new System.Drawing.Size(114, 33);
            this.comboBoxProtocol.TabIndex = 6;
            this.comboBoxProtocol.SelectedIndexChanged += new System.EventHandler(this.comboBoxProtocol_SelectedIndexChanged);
            // 
            // labelBufferSize
            // 
            this.labelBufferSize.AutoSize = true;
            this.labelBufferSize.Location = new System.Drawing.Point(10, 179);
            this.labelBufferSize.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelBufferSize.Name = "labelBufferSize";
            this.labelBufferSize.Size = new System.Drawing.Size(123, 25);
            this.labelBufferSize.TabIndex = 5;
            this.labelBufferSize.Text = "Buffer Size:";
            // 
            // numericUpDownBufferSize
            // 
            this.numericUpDownBufferSize.Location = new System.Drawing.Point(150, 175);
            this.numericUpDownBufferSize.Margin = new System.Windows.Forms.Padding(6);
            this.numericUpDownBufferSize.Name = "numericUpDownBufferSize";
            this.numericUpDownBufferSize.Size = new System.Drawing.Size(118, 31);
            this.numericUpDownBufferSize.TabIndex = 4;
            this.numericUpDownBufferSize.ValueChanged += new System.EventHandler(this.numericUpDownBufferSize_ValueChanged);
            // 
            // checkBoxResolve
            // 
            this.checkBoxResolve.AutoSize = true;
            this.checkBoxResolve.Checked = true;
            this.checkBoxResolve.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxResolve.Location = new System.Drawing.Point(16, 131);
            this.checkBoxResolve.Margin = new System.Windows.Forms.Padding(6);
            this.checkBoxResolve.Name = "checkBoxResolve";
            this.checkBoxResolve.Size = new System.Drawing.Size(236, 29);
            this.checkBoxResolve.TabIndex = 3;
            this.checkBoxResolve.Text = "Resolve Hostnames";
            this.checkBoxResolve.UseVisualStyleBackColor = true;
            this.checkBoxResolve.CheckedChanged += new System.EventHandler(this.checkBoxResolve_CheckedChanged);
            // 
            // textBoxReqCount
            // 
            this.textBoxReqCount.Location = new System.Drawing.Point(192, 81);
            this.textBoxReqCount.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxReqCount.Name = "textBoxReqCount";
            this.textBoxReqCount.Size = new System.Drawing.Size(72, 31);
            this.textBoxReqCount.TabIndex = 2;
            // 
            // labelHopCount
            // 
            this.labelHopCount.AutoSize = true;
            this.labelHopCount.Location = new System.Drawing.Point(12, 87);
            this.labelHopCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelHopCount.Name = "labelHopCount";
            this.labelHopCount.Size = new System.Drawing.Size(158, 25);
            this.labelHopCount.TabIndex = 1;
            this.labelHopCount.Text = "Ping Requests:";
            // 
            // checkBoxContinuous
            // 
            this.checkBoxContinuous.AutoSize = true;
            this.checkBoxContinuous.Location = new System.Drawing.Point(16, 31);
            this.checkBoxContinuous.Margin = new System.Windows.Forms.Padding(6);
            this.checkBoxContinuous.Name = "checkBoxContinuous";
            this.checkBoxContinuous.Size = new System.Drawing.Size(214, 29);
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
            this.statusStripStat.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStripStat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStat,
            this.toolStripStatusLabelRuntime});
            this.statusStripStat.Location = new System.Drawing.Point(0, 765);
            this.statusStripStat.Name = "statusStripStat";
            this.statusStripStat.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusStripStat.Size = new System.Drawing.Size(854, 37);
            this.statusStripStat.TabIndex = 3;
            this.statusStripStat.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStat
            // 
            this.toolStripStatusLabelStat.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabelStat.Name = "toolStripStatusLabelStat";
            this.toolStripStatusLabelStat.Size = new System.Drawing.Size(155, 32);
            this.toolStripStatusLabelStat.Text = "Status: Ready";
            // 
            // toolStripStatusLabelRuntime
            // 
            this.toolStripStatusLabelRuntime.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabelRuntime.Name = "toolStripStatusLabelRuntime";
            this.toolStripStatusLabelRuntime.Size = new System.Drawing.Size(174, 32);
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
            // pictureBoxAnim
            // 
            this.pictureBoxAnim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxAnim.Image = global::CyrusBuilt.CyNetTools.Properties.Resources.status_anim;
            this.pictureBoxAnim.InitialImage = null;
            this.pictureBoxAnim.Location = new System.Drawing.Point(730, 581);
            this.pictureBoxAnim.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBoxAnim.Name = "pictureBoxAnim";
            this.pictureBoxAnim.Size = new System.Drawing.Size(92, 23);
            this.pictureBoxAnim.TabIndex = 1;
            this.pictureBoxAnim.TabStop = false;
            this.pictureBoxAnim.Visible = false;
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
            this.toolStripButtonCancel.Enabled = false;
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
            this.toolStripButtonPrintPreview.Text = "Print Preview";
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
            // FormPing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(854, 802);
            this.Controls.Add(this.statusStripStat);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.toolStripActions);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
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
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            this.groupBoxIPv6.ResumeLayout(false);
            this.groupBoxIPv6.PerformLayout();
            this.groupBoxIPv4.ResumeLayout(false);
            this.groupBoxIPv4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimestamp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecordHopCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBufferSize)).EndInit();
            this.statusStripStat.ResumeLayout(false);
            this.statusStripStat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnim)).EndInit();
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