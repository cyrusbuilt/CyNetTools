namespace CyrusBuilt.CyNetTools
{
    partial class FormTraceroute
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTraceroute));
            this.statusStripStat = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRuntime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripActions = new System.Windows.Forms.ToolStrip();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.labelProgress = new System.Windows.Forms.Label();
            this.progressBarPing = new System.Windows.Forms.ProgressBar();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.labelHostList = new System.Windows.Forms.Label();
            this.comboBoxProtocol = new System.Windows.Forms.ComboBox();
            this.labelIPProtocol = new System.Windows.Forms.Label();
            this.textBoxTimeout = new System.Windows.Forms.TextBox();
            this.labelTimeout = new System.Windows.Forms.Label();
            this.labelMaxHops = new System.Windows.Forms.Label();
            this.numericUpDownMaxHops = new System.Windows.Forms.NumericUpDown();
            this.checkBoxNoResolve = new System.Windows.Forms.CheckBox();
            this.textBoxHostList = new System.Windows.Forms.TextBox();
            this.groupBoxIPV6 = new System.Windows.Forms.GroupBox();
            this.checkBoxRoundTrip = new System.Windows.Forms.CheckBox();
            this.maskedTextBoxSourceAddr = new System.Windows.Forms.MaskedTextBox();
            this.labelSourceAddress = new System.Windows.Forms.Label();
            this.labelCurrentHop = new System.Windows.Forms.Label();
            this.labelTotalHops = new System.Windows.Forms.Label();
            this.labelCurrentHopActual = new System.Windows.Forms.Label();
            this.labelTotalHopsActual = new System.Windows.Forms.Label();
            this.labelTargetHost = new System.Windows.Forms.Label();
            this.textBoxTargetHost = new System.Windows.Forms.TextBox();
            this.printPreviewDialogTool = new System.Windows.Forms.PrintPreviewDialog();
            this.timerRuntime = new System.Windows.Forms.Timer(this.components);
            this.errorProviderTrace = new System.Windows.Forms.ErrorProvider(this.components);
            this.saveFileDialogTool = new System.Windows.Forms.SaveFileDialog();
            this.pageSetupDialogTool = new System.Windows.Forms.PageSetupDialog();
            this.printDialogTool = new System.Windows.Forms.PrintDialog();
            this.groupBoxIPv4 = new System.Windows.Forms.GroupBox();
            this.pictureBoxAnim = new System.Windows.Forms.PictureBox();
            this.toolStripButtonExec = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPageSetup = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrintPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.statusStripStat.SuspendLayout();
            this.toolStripActions.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxHops)).BeginInit();
            this.groupBoxIPV6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTrace)).BeginInit();
            this.groupBoxIPv4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnim)).BeginInit();
            this.SuspendLayout();
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
            this.statusStripStat.Size = new System.Drawing.Size(854, 37);
            this.statusStripStat.TabIndex = 0;
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
            this.toolStripActions.Size = new System.Drawing.Size(854, 25);
            this.toolStripActions.TabIndex = 1;
            this.toolStripActions.Text = "toolStrip1";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageOutput);
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.Location = new System.Drawing.Point(0, 42);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(854, 720);
            this.tabControlMain.TabIndex = 2;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tabPageOutput.Controls.Add(this.pictureBoxAnim);
            this.tabPageOutput.Controls.Add(this.textBoxTargetHost);
            this.tabPageOutput.Controls.Add(this.labelTargetHost);
            this.tabPageOutput.Controls.Add(this.labelTotalHopsActual);
            this.tabPageOutput.Controls.Add(this.labelCurrentHopActual);
            this.tabPageOutput.Controls.Add(this.labelTotalHops);
            this.tabPageOutput.Controls.Add(this.labelCurrentHop);
            this.tabPageOutput.Controls.Add(this.labelProgress);
            this.tabPageOutput.Controls.Add(this.progressBarPing);
            this.tabPageOutput.Controls.Add(this.richTextBoxOutput);
            this.tabPageOutput.Location = new System.Drawing.Point(8, 39);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(838, 673);
            this.tabPageOutput.TabIndex = 0;
            this.tabPageOutput.Text = "Output";
            // 
            // labelProgress
            // 
            this.labelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelProgress.AutoSize = true;
            this.labelProgress.ForeColor = System.Drawing.Color.White;
            this.labelProgress.Location = new System.Drawing.Point(41, 399);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(221, 25);
            this.labelProgress.TabIndex = 2;
            this.labelProgress.Text = "Operational Progress:";
            // 
            // progressBarPing
            // 
            this.progressBarPing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarPing.Location = new System.Drawing.Point(308, 380);
            this.progressBarPing.Name = "progressBarPing";
            this.progressBarPing.Size = new System.Drawing.Size(520, 44);
            this.progressBarPing.TabIndex = 1;
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxOutput.BackColor = System.Drawing.Color.Black;
            this.richTextBoxOutput.ForeColor = System.Drawing.Color.Aqua;
            this.richTextBoxOutput.Location = new System.Drawing.Point(6, 6);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.Size = new System.Drawing.Size(822, 356);
            this.richTextBoxOutput.TabIndex = 0;
            this.richTextBoxOutput.Text = "Ready.";
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tabPageSettings.Controls.Add(this.groupBoxIPv4);
            this.tabPageSettings.Controls.Add(this.groupBoxIPV6);
            this.tabPageSettings.Controls.Add(this.comboBoxProtocol);
            this.tabPageSettings.Controls.Add(this.labelIPProtocol);
            this.tabPageSettings.Controls.Add(this.textBoxTimeout);
            this.tabPageSettings.Controls.Add(this.labelTimeout);
            this.tabPageSettings.Controls.Add(this.labelMaxHops);
            this.tabPageSettings.Controls.Add(this.numericUpDownMaxHops);
            this.tabPageSettings.Controls.Add(this.checkBoxNoResolve);
            this.tabPageSettings.Location = new System.Drawing.Point(8, 39);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(838, 673);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            // 
            // labelHostList
            // 
            this.labelHostList.AutoSize = true;
            this.labelHostList.ForeColor = System.Drawing.Color.White;
            this.labelHostList.Location = new System.Drawing.Point(29, 66);
            this.labelHostList.Name = "labelHostList";
            this.labelHostList.Size = new System.Drawing.Size(230, 25);
            this.labelHostList.TabIndex = 7;
            this.labelHostList.Text = "Loose Route Host List:";
            // 
            // comboBoxProtocol
            // 
            this.comboBoxProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProtocol.FormattingEnabled = true;
            this.comboBoxProtocol.Location = new System.Drawing.Point(205, 178);
            this.comboBoxProtocol.Name = "comboBoxProtocol";
            this.comboBoxProtocol.Size = new System.Drawing.Size(114, 33);
            this.comboBoxProtocol.TabIndex = 6;
            this.comboBoxProtocol.SelectedIndexChanged += new System.EventHandler(this.comboBoxProtocol_SelectedIndexChanged);
            // 
            // labelIPProtocol
            // 
            this.labelIPProtocol.AutoSize = true;
            this.labelIPProtocol.ForeColor = System.Drawing.Color.White;
            this.labelIPProtocol.Location = new System.Drawing.Point(12, 181);
            this.labelIPProtocol.Name = "labelIPProtocol";
            this.labelIPProtocol.Size = new System.Drawing.Size(122, 25);
            this.labelIPProtocol.TabIndex = 5;
            this.labelIPProtocol.Text = "IP Protocol:";
            // 
            // textBoxTimeout
            // 
            this.textBoxTimeout.Location = new System.Drawing.Point(205, 123);
            this.textBoxTimeout.Name = "textBoxTimeout";
            this.textBoxTimeout.Size = new System.Drawing.Size(100, 31);
            this.textBoxTimeout.TabIndex = 4;
            // 
            // labelTimeout
            // 
            this.labelTimeout.AutoSize = true;
            this.labelTimeout.ForeColor = System.Drawing.Color.White;
            this.labelTimeout.Location = new System.Drawing.Point(12, 123);
            this.labelTimeout.Name = "labelTimeout";
            this.labelTimeout.Size = new System.Drawing.Size(143, 25);
            this.labelTimeout.TabIndex = 3;
            this.labelTimeout.Text = "Timeout (ms):";
            // 
            // labelMaxHops
            // 
            this.labelMaxHops.AutoSize = true;
            this.labelMaxHops.ForeColor = System.Drawing.Color.White;
            this.labelMaxHops.Location = new System.Drawing.Point(12, 70);
            this.labelMaxHops.Name = "labelMaxHops";
            this.labelMaxHops.Size = new System.Drawing.Size(167, 25);
            this.labelMaxHops.TabIndex = 2;
            this.labelMaxHops.Text = "Max Hop Count:";
            this.labelMaxHops.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // numericUpDownMaxHops
            // 
            this.numericUpDownMaxHops.Location = new System.Drawing.Point(205, 68);
            this.numericUpDownMaxHops.Name = "numericUpDownMaxHops";
            this.numericUpDownMaxHops.Size = new System.Drawing.Size(120, 31);
            this.numericUpDownMaxHops.TabIndex = 1;
            this.numericUpDownMaxHops.ValueChanged += new System.EventHandler(this.numericUpDownMaxHops_ValueChanged);
            // 
            // checkBoxNoResolve
            // 
            this.checkBoxNoResolve.AutoSize = true;
            this.checkBoxNoResolve.ForeColor = System.Drawing.Color.White;
            this.checkBoxNoResolve.Location = new System.Drawing.Point(17, 20);
            this.checkBoxNoResolve.Name = "checkBoxNoResolve";
            this.checkBoxNoResolve.Size = new System.Drawing.Size(308, 29);
            this.checkBoxNoResolve.TabIndex = 0;
            this.checkBoxNoResolve.Text = "Do Not Resolve Hostnames";
            this.checkBoxNoResolve.UseVisualStyleBackColor = true;
            this.checkBoxNoResolve.CheckedChanged += new System.EventHandler(this.checkBoxNoResolve_CheckedChanged);
            // 
            // textBoxHostList
            // 
            this.textBoxHostList.Location = new System.Drawing.Point(281, 63);
            this.textBoxHostList.Name = "textBoxHostList";
            this.textBoxHostList.Size = new System.Drawing.Size(494, 31);
            this.textBoxHostList.TabIndex = 8;
            // 
            // groupBoxIPV6
            // 
            this.groupBoxIPV6.Controls.Add(this.labelSourceAddress);
            this.groupBoxIPV6.Controls.Add(this.maskedTextBoxSourceAddr);
            this.groupBoxIPV6.Controls.Add(this.checkBoxRoundTrip);
            this.groupBoxIPV6.ForeColor = System.Drawing.Color.White;
            this.groupBoxIPV6.Location = new System.Drawing.Point(17, 419);
            this.groupBoxIPV6.Name = "groupBoxIPV6";
            this.groupBoxIPV6.Size = new System.Drawing.Size(806, 239);
            this.groupBoxIPV6.TabIndex = 9;
            this.groupBoxIPV6.TabStop = false;
            this.groupBoxIPV6.Text = "IPv6 Only";
            // 
            // checkBoxRoundTrip
            // 
            this.checkBoxRoundTrip.AutoSize = true;
            this.checkBoxRoundTrip.Enabled = false;
            this.checkBoxRoundTrip.Location = new System.Drawing.Point(28, 45);
            this.checkBoxRoundTrip.Name = "checkBoxRoundTrip";
            this.checkBoxRoundTrip.Size = new System.Drawing.Size(197, 29);
            this.checkBoxRoundTrip.TabIndex = 0;
            this.checkBoxRoundTrip.Text = "Trace round-trip";
            this.checkBoxRoundTrip.UseVisualStyleBackColor = true;
            this.checkBoxRoundTrip.CheckedChanged += new System.EventHandler(this.checkBoxRoundTrip_CheckedChanged);
            // 
            // maskedTextBoxSourceAddr
            // 
            this.maskedTextBoxSourceAddr.Enabled = false;
            this.maskedTextBoxSourceAddr.Location = new System.Drawing.Point(208, 98);
            this.maskedTextBoxSourceAddr.Mask = "###.###.###.###";
            this.maskedTextBoxSourceAddr.Name = "maskedTextBoxSourceAddr";
            this.maskedTextBoxSourceAddr.Size = new System.Drawing.Size(198, 31);
            this.maskedTextBoxSourceAddr.TabIndex = 1;
            this.maskedTextBoxSourceAddr.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.maskedTextBoxSourceAddr_TypeValidationCompleted);
            // 
            // labelSourceAddress
            // 
            this.labelSourceAddress.AutoSize = true;
            this.labelSourceAddress.Location = new System.Drawing.Point(23, 101);
            this.labelSourceAddress.Name = "labelSourceAddress";
            this.labelSourceAddress.Size = new System.Drawing.Size(171, 25);
            this.labelSourceAddress.TabIndex = 2;
            this.labelSourceAddress.Text = "Source Address:";
            // 
            // labelCurrentHop
            // 
            this.labelCurrentHop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCurrentHop.AutoSize = true;
            this.labelCurrentHop.ForeColor = System.Drawing.Color.White;
            this.labelCurrentHop.Location = new System.Drawing.Point(128, 452);
            this.labelCurrentHop.Name = "labelCurrentHop";
            this.labelCurrentHop.Size = new System.Drawing.Size(134, 25);
            this.labelCurrentHop.TabIndex = 3;
            this.labelCurrentHop.Text = "Current Hop:";
            // 
            // labelTotalHops
            // 
            this.labelTotalHops.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTotalHops.AutoSize = true;
            this.labelTotalHops.ForeColor = System.Drawing.Color.White;
            this.labelTotalHops.Location = new System.Drawing.Point(140, 505);
            this.labelTotalHops.Name = "labelTotalHops";
            this.labelTotalHops.Size = new System.Drawing.Size(122, 25);
            this.labelTotalHops.TabIndex = 4;
            this.labelTotalHops.Text = "Total Hops:";
            // 
            // labelCurrentHopActual
            // 
            this.labelCurrentHopActual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCurrentHopActual.AutoSize = true;
            this.labelCurrentHopActual.ForeColor = System.Drawing.Color.White;
            this.labelCurrentHopActual.Location = new System.Drawing.Point(303, 452);
            this.labelCurrentHopActual.Name = "labelCurrentHopActual";
            this.labelCurrentHopActual.Size = new System.Drawing.Size(24, 25);
            this.labelCurrentHopActual.TabIndex = 5;
            this.labelCurrentHopActual.Text = "0";
            // 
            // labelTotalHopsActual
            // 
            this.labelTotalHopsActual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalHopsActual.AutoSize = true;
            this.labelTotalHopsActual.ForeColor = System.Drawing.Color.White;
            this.labelTotalHopsActual.Location = new System.Drawing.Point(303, 505);
            this.labelTotalHopsActual.Name = "labelTotalHopsActual";
            this.labelTotalHopsActual.Size = new System.Drawing.Size(24, 25);
            this.labelTotalHopsActual.TabIndex = 6;
            this.labelTotalHopsActual.Text = "0";
            // 
            // labelTargetHost
            // 
            this.labelTargetHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTargetHost.AutoSize = true;
            this.labelTargetHost.ForeColor = System.Drawing.Color.White;
            this.labelTargetHost.Location = new System.Drawing.Point(13, 556);
            this.labelTargetHost.Name = "labelTargetHost";
            this.labelTargetHost.Size = new System.Drawing.Size(249, 25);
            this.labelTargetHost.TabIndex = 7;
            this.labelTargetHost.Text = "Host name or IP to trace:";
            // 
            // textBoxTargetHost
            // 
            this.textBoxTargetHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTargetHost.Location = new System.Drawing.Point(308, 553);
            this.textBoxTargetHost.Name = "textBoxTargetHost";
            this.textBoxTargetHost.Size = new System.Drawing.Size(410, 31);
            this.textBoxTargetHost.TabIndex = 8;
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
            // errorProviderTrace
            // 
            this.errorProviderTrace.ContainerControl = this;
            // 
            // printDialogTool
            // 
            this.printDialogTool.UseEXDialog = true;
            // 
            // groupBoxIPv4
            // 
            this.groupBoxIPv4.Controls.Add(this.textBoxHostList);
            this.groupBoxIPv4.Controls.Add(this.labelHostList);
            this.groupBoxIPv4.ForeColor = System.Drawing.Color.White;
            this.groupBoxIPv4.Location = new System.Drawing.Point(17, 247);
            this.groupBoxIPv4.Name = "groupBoxIPv4";
            this.groupBoxIPv4.Size = new System.Drawing.Size(806, 139);
            this.groupBoxIPv4.TabIndex = 10;
            this.groupBoxIPv4.TabStop = false;
            this.groupBoxIPv4.Text = "IPv4 Only";
            // 
            // pictureBoxAnim
            // 
            this.pictureBoxAnim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxAnim.Image = global::CyrusBuilt.CyNetTools.Properties.Resources.status_anim;
            this.pictureBoxAnim.InitialImage = null;
            this.pictureBoxAnim.Location = new System.Drawing.Point(736, 558);
            this.pictureBoxAnim.Name = "pictureBoxAnim";
            this.pictureBoxAnim.Size = new System.Drawing.Size(92, 23);
            this.pictureBoxAnim.TabIndex = 9;
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
            this.toolStripButtonCancel.ToolTipText = "Cancel";
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
            this.toolStripButtonPageSetup.ToolTipText = "Page Setup";
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
            this.toolStripButtonPrint.ToolTipText = "Print";
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // FormTraceroute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(854, 802);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.toolStripActions);
            this.Controls.Add(this.statusStripStat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTraceroute";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "TraceRoute";
            this.Load += new System.EventHandler(this.FormTraceroute_Load);
            this.statusStripStat.ResumeLayout(false);
            this.statusStripStat.PerformLayout();
            this.toolStripActions.ResumeLayout(false);
            this.toolStripActions.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageOutput.PerformLayout();
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxHops)).EndInit();
            this.groupBoxIPV6.ResumeLayout(false);
            this.groupBoxIPV6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderTrace)).EndInit();
            this.groupBoxIPv4.ResumeLayout(false);
            this.groupBoxIPv4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripStat;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStat;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRuntime;
        private System.Windows.Forms.ToolStrip toolStripActions;
        private System.Windows.Forms.ToolStripButton toolStripButtonExec;
        private System.Windows.Forms.ToolStripButton toolStripButtonCancel;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonPageSetup;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrintPreview;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrint;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.ProgressBar progressBarPing;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.CheckBox checkBoxNoResolve;
        private System.Windows.Forms.Label labelMaxHops;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxHops;
        private System.Windows.Forms.Label labelTimeout;
        private System.Windows.Forms.TextBox textBoxTimeout;
        private System.Windows.Forms.Label labelIPProtocol;
        private System.Windows.Forms.ComboBox comboBoxProtocol;
        private System.Windows.Forms.Label labelHostList;
        private System.Windows.Forms.TextBox textBoxHostList;
        private System.Windows.Forms.GroupBox groupBoxIPV6;
        private System.Windows.Forms.CheckBox checkBoxRoundTrip;
        private System.Windows.Forms.Label labelSourceAddress;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxSourceAddr;
        private System.Windows.Forms.Label labelCurrentHop;
        private System.Windows.Forms.Label labelTotalHops;
        private System.Windows.Forms.Label labelCurrentHopActual;
        private System.Windows.Forms.Label labelTotalHopsActual;
        private System.Windows.Forms.Label labelTargetHost;
        private System.Windows.Forms.TextBox textBoxTargetHost;
        private System.Windows.Forms.PictureBox pictureBoxAnim;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialogTool;
        private System.Windows.Forms.Timer timerRuntime;
        private System.Windows.Forms.ErrorProvider errorProviderTrace;
        private System.Windows.Forms.SaveFileDialog saveFileDialogTool;
        private System.Windows.Forms.PageSetupDialog pageSetupDialogTool;
        private System.Windows.Forms.PrintDialog printDialogTool;
        private System.Windows.Forms.GroupBox groupBoxIPv4;
    }
}