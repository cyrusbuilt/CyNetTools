namespace CyrusBuilt.CyNetTools
{
    partial class FormExternalToolBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExternalToolBase));
            this.toolStripActions = new System.Windows.Forms.ToolStrip();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.pageSetupDialogTool = new System.Windows.Forms.PageSetupDialog();
            this.printPreviewDialogTool = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialogTool = new System.Windows.Forms.PrintDialog();
            this.saveFileDialogTool = new System.Windows.Forms.SaveFileDialog();
            this.statusStripStat = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRuntime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerRuntime = new System.Windows.Forms.Timer(this.components);
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
            this.statusStripStat.SuspendLayout();
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
            this.toolStripActions.Size = new System.Drawing.Size(391, 25);
            this.toolStripActions.TabIndex = 0;
            this.toolStripActions.Text = "toolStrip1";
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
            this.tabControlMain.Size = new System.Drawing.Size(391, 309);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.pictureBoxAnim);
            this.tabPageOutput.Controls.Add(this.richTextBoxOutput);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(383, 283);
            this.tabPageOutput.TabIndex = 0;
            this.tabPageOutput.Text = "Output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.BackColor = System.Drawing.Color.Black;
            this.richTextBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxOutput.ForeColor = System.Drawing.Color.Cyan;
            this.richTextBoxOutput.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.ReadOnly = true;
            this.richTextBoxOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxOutput.Size = new System.Drawing.Size(377, 277);
            this.richTextBoxOutput.TabIndex = 0;
            this.richTextBoxOutput.Text = "Ready.";
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(383, 283);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
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
            this.statusStripStat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStat,
            this.toolStripStatusLabelRuntime});
            this.statusStripStat.Location = new System.Drawing.Point(0, 341);
            this.statusStripStat.Name = "statusStripStat";
            this.statusStripStat.Size = new System.Drawing.Size(391, 22);
            this.statusStripStat.TabIndex = 3;
            this.statusStripStat.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStat
            // 
            this.toolStripStatusLabelStat.Name = "toolStripStatusLabelStat";
            this.toolStripStatusLabelStat.Size = new System.Drawing.Size(77, 17);
            this.toolStripStatusLabelStat.Text = "Status: Ready";
            // 
            // toolStripStatusLabelRuntime
            // 
            this.toolStripStatusLabelRuntime.Name = "toolStripStatusLabelRuntime";
            this.toolStripStatusLabelRuntime.Size = new System.Drawing.Size(85, 17);
            this.toolStripStatusLabelRuntime.Text = "Runtime: 00:00";
            // 
            // timerRuntime
            // 
            this.timerRuntime.Interval = 1000;
            this.timerRuntime.Tick += new System.EventHandler(this.timerRuntime_Tick);
            // 
            // pictureBoxAnim
            // 
            this.pictureBoxAnim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxAnim.Image = global::CyrusBuilt.CyNetTools.Properties.Resources.status_anim;
            this.pictureBoxAnim.Location = new System.Drawing.Point(329, 265);
            this.pictureBoxAnim.Name = "pictureBoxAnim";
            this.pictureBoxAnim.Size = new System.Drawing.Size(46, 12);
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
            // FormExternalToolBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 363);
            this.Controls.Add(this.statusStripStat);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.toolStripActions);
            this.Name = "FormExternalToolBase";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "FormExternalToolBase";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExternalToolBase_FormClosing);
            this.Load += new System.EventHandler(this.FormExternalToolBase_Load);
            this.toolStripActions.ResumeLayout(false);
            this.toolStripActions.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageOutput.ResumeLayout(false);
            this.statusStripStat.ResumeLayout(false);
            this.statusStripStat.PerformLayout();
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
    }
}