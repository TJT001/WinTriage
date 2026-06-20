namespace WinTriage
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSystem = new System.Windows.Forms.TabPage();
            this.clbSystem = new System.Windows.Forms.CheckedListBox();
            this.cbSystemAll = new System.Windows.Forms.CheckBox();
            this.tabNetwork = new System.Windows.Forms.TabPage();
            this.clbNetwork = new System.Windows.Forms.CheckedListBox();
            this.cbNetworkAll = new System.Windows.Forms.CheckBox();
            this.tabForensic = new System.Windows.Forms.TabPage();
            this.clbForensic = new System.Windows.Forms.CheckedListBox();
            this.cbForensicAll = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblOutputDir = new System.Windows.Forms.Label();
            this.txtOutputDir = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.tabSystem.SuspendLayout();
            this.tabNetwork.SuspendLayout();
            this.tabForensic.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSystem);
            this.tabControl1.Controls.Add(this.tabNetwork);
            this.tabControl1.Controls.Add(this.tabForensic);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 228);
            this.tabControl1.TabIndex = 0;
            // 
            // tabSystem
            // 
            this.tabSystem.BackColor = System.Drawing.SystemColors.Control;
            this.tabSystem.Controls.Add(this.clbSystem);
            this.tabSystem.Controls.Add(this.cbSystemAll);
            this.tabSystem.Location = new System.Drawing.Point(4, 26);
            this.tabSystem.Name = "tabSystem";
            this.tabSystem.Padding = new System.Windows.Forms.Padding(3);
            this.tabSystem.Size = new System.Drawing.Size(776, 198);
            this.tabSystem.TabIndex = 0;
            this.tabSystem.Text = "系统状态";
            // 
            // clbSystem
            // 
            this.clbSystem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbSystem.CheckOnClick = true;
            this.clbSystem.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clbSystem.FormattingEnabled = true;
            this.clbSystem.IntegralHeight = false;
            this.clbSystem.Location = new System.Drawing.Point(8, 8);
            this.clbSystem.Name = "clbSystem";
            this.clbSystem.Size = new System.Drawing.Size(590, 182);
            this.clbSystem.TabIndex = 0;
            // 
            // cbSystemAll
            // 
            this.cbSystemAll.AutoSize = true;
            this.cbSystemAll.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSystemAll.Location = new System.Drawing.Point(619, 91);
            this.cbSystemAll.Name = "cbSystemAll";
            this.cbSystemAll.Size = new System.Drawing.Size(51, 21);
            this.cbSystemAll.TabIndex = 1;
            this.cbSystemAll.Text = "全选";
            this.cbSystemAll.UseVisualStyleBackColor = true;
            this.cbSystemAll.CheckedChanged += new System.EventHandler(this.cbSystemAll_CheckedChanged);
            // 
            // tabNetwork
            // 
            this.tabNetwork.BackColor = System.Drawing.SystemColors.Control;
            this.tabNetwork.Controls.Add(this.clbNetwork);
            this.tabNetwork.Controls.Add(this.cbNetworkAll);
            this.tabNetwork.Location = new System.Drawing.Point(4, 26);
            this.tabNetwork.Name = "tabNetwork";
            this.tabNetwork.Padding = new System.Windows.Forms.Padding(3);
            this.tabNetwork.Size = new System.Drawing.Size(776, 198);
            this.tabNetwork.TabIndex = 1;
            this.tabNetwork.Text = "网络信息";
            // 
            // clbNetwork
            // 
            this.clbNetwork.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbNetwork.CheckOnClick = true;
            this.clbNetwork.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clbNetwork.FormattingEnabled = true;
            this.clbNetwork.IntegralHeight = false;
            this.clbNetwork.Location = new System.Drawing.Point(8, 8);
            this.clbNetwork.Name = "clbNetwork";
            this.clbNetwork.Size = new System.Drawing.Size(590, 182);
            this.clbNetwork.TabIndex = 0;
            // 
            // cbNetworkAll
            // 
            this.cbNetworkAll.AutoSize = true;
            this.cbNetworkAll.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbNetworkAll.Location = new System.Drawing.Point(619, 91);
            this.cbNetworkAll.Name = "cbNetworkAll";
            this.cbNetworkAll.Size = new System.Drawing.Size(51, 21);
            this.cbNetworkAll.TabIndex = 1;
            this.cbNetworkAll.Text = "全选";
            this.cbNetworkAll.UseVisualStyleBackColor = true;
            this.cbNetworkAll.CheckedChanged += new System.EventHandler(this.cbNetworkAll_CheckedChanged);
            // 
            // tabForensic
            // 
            this.tabForensic.BackColor = System.Drawing.SystemColors.Control;
            this.tabForensic.Controls.Add(this.clbForensic);
            this.tabForensic.Controls.Add(this.cbForensicAll);
            this.tabForensic.Location = new System.Drawing.Point(4, 26);
            this.tabForensic.Name = "tabForensic";
            this.tabForensic.Padding = new System.Windows.Forms.Padding(3);
            this.tabForensic.Size = new System.Drawing.Size(776, 198);
            this.tabForensic.TabIndex = 2;
            this.tabForensic.Text = "痕迹取证";
            // 
            // clbForensic
            // 
            this.clbForensic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbForensic.CheckOnClick = true;
            this.clbForensic.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clbForensic.FormattingEnabled = true;
            this.clbForensic.IntegralHeight = false;
            this.clbForensic.Location = new System.Drawing.Point(8, 8);
            this.clbForensic.Name = "clbForensic";
            this.clbForensic.Size = new System.Drawing.Size(590, 182);
            this.clbForensic.TabIndex = 0;
            // 
            // cbForensicAll
            // 
            this.cbForensicAll.AutoSize = true;
            this.cbForensicAll.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbForensicAll.Location = new System.Drawing.Point(619, 91);
            this.cbForensicAll.Name = "cbForensicAll";
            this.cbForensicAll.Size = new System.Drawing.Size(51, 21);
            this.cbForensicAll.TabIndex = 1;
            this.cbForensicAll.Text = "全选";
            this.cbForensicAll.UseVisualStyleBackColor = true;
            this.cbForensicAll.CheckedChanged += new System.EventHandler(this.cbForensicAll_CheckedChanged);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(10, 244);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(95, 30);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "▶  开始采集";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.Location = new System.Drawing.Point(110, 244);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 30);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "■  停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(200, 248);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 22);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 3;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProgress.Location = new System.Drawing.Point(508, 250);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(32, 17);
            this.lblProgress.TabIndex = 4;
            this.lblProgress.Text = "就绪";
            // 
            // lblOutputDir
            // 
            this.lblOutputDir.AutoSize = true;
            this.lblOutputDir.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOutputDir.Location = new System.Drawing.Point(10, 280);
            this.lblOutputDir.Name = "lblOutputDir";
            this.lblOutputDir.Size = new System.Drawing.Size(59, 17);
            this.lblOutputDir.TabIndex = 5;
            this.lblOutputDir.Text = "输出目录:";
            // 
            // txtOutputDir
            // 
            this.txtOutputDir.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOutputDir.Location = new System.Drawing.Point(70, 278);
            this.txtOutputDir.Name = "txtOutputDir";
            this.txtOutputDir.Size = new System.Drawing.Size(250, 23);
            this.txtOutputDir.TabIndex = 6;
            this.txtOutputDir.Text = ".\\Output";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBrowse.Location = new System.Drawing.Point(324, 277);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(28, 24);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "…";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbLog.DetectUrls = false;
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.rtbLog.Location = new System.Drawing.Point(8, 310);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(784, 200);
            this.rtbLog.TabIndex = 8;
            this.rtbLog.Text = "";
            this.rtbLog.WordWrap = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 518);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(52, 17);
            this.toolStripStatusLabel1.Text = "✅ 就绪";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(87, 17);
            this.toolStripStatusLabel2.Text = "输出: .\\Output";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 540);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtOutputDir);
            this.Controls.Add(this.lblOutputDir);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinTriage - 应急响应信息采集 v2.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabSystem.ResumeLayout(false);
            this.tabSystem.PerformLayout();
            this.tabNetwork.ResumeLayout(false);
            this.tabNetwork.PerformLayout();
            this.tabForensic.ResumeLayout(false);
            this.tabForensic.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSystem;
        private System.Windows.Forms.TabPage tabNetwork;
        private System.Windows.Forms.TabPage tabForensic;
        private System.Windows.Forms.CheckedListBox clbSystem;
        private System.Windows.Forms.CheckedListBox clbNetwork;
        private System.Windows.Forms.CheckedListBox clbForensic;
        private System.Windows.Forms.CheckBox cbSystemAll;
        private System.Windows.Forms.CheckBox cbNetworkAll;
        private System.Windows.Forms.CheckBox cbForensicAll;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblOutputDir;
        private System.Windows.Forms.TextBox txtOutputDir;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}
