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
            // ── 自定义控件实例化 ──────────────────────────────────────
            this.tabControl1   = new WinTriage.DarkTabControl();
            this.tabSystem     = new System.Windows.Forms.TabPage();
            this.clbSystem     = new WinTriage.DarkCheckedListBox();
            this.cbSystemAll   = new System.Windows.Forms.CheckBox();
            this.tabNetwork    = new System.Windows.Forms.TabPage();
            this.clbNetwork    = new WinTriage.DarkCheckedListBox();
            this.cbNetworkAll  = new System.Windows.Forms.CheckBox();
            this.tabForensic   = new System.Windows.Forms.TabPage();
            this.clbForensic   = new WinTriage.DarkCheckedListBox();
            this.cbForensicAll = new System.Windows.Forms.CheckBox();

            this.pnlHeader     = new System.Windows.Forms.Panel();
            this.lblTitle      = new System.Windows.Forms.Label();
            this.lblSubtitle   = new System.Windows.Forms.Label();
            this.lblAdminBadge = new System.Windows.Forms.Label();

            this.btnStart      = new WinTriage.FlatButton();
            this.btnStop       = new WinTriage.FlatButton();
            this.btnBrowse     = new WinTriage.FlatButton();
            this.progressBar1  = new WinTriage.GradientProgressBar();
            this.lblProgress   = new System.Windows.Forms.Label();

            this.pnlOutput     = new System.Windows.Forms.Panel();
            this.lblOutputDir  = new System.Windows.Forms.Label();
            this.txtOutputDir  = new WinTriage.DarkTextBox();

            this.pnlLogHeader  = new System.Windows.Forms.Panel();
            this.lblLogTitle   = new System.Windows.Forms.Label();
            this.rtbLog        = new System.Windows.Forms.RichTextBox();
            this.statusStrip1  = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();

            this.tabControl1.SuspendLayout();
            this.tabSystem.SuspendLayout();
            this.tabNetwork.SuspendLayout();
            this.tabForensic.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlOutput.SuspendLayout();
            this.pnlLogHeader.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();

            // ── Header Panel ─────────────────────────────────────────
            this.pnlHeader.BackColor  = WinTriage.Theme.BgPanel;
            this.pnlHeader.Dock       = System.Windows.Forms.DockStyle.None;
            this.pnlHeader.Location   = new System.Drawing.Point(8, 8);
            this.pnlHeader.Size       = new System.Drawing.Size(784, 56);
            this.pnlHeader.Name       = "pnlHeader";
            this.pnlHeader.Anchor     = System.Windows.Forms.AnchorStyles.Top
                                      | System.Windows.Forms.AnchorStyles.Left
                                      | System.Windows.Forms.AnchorStyles.Right;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblAdminBadge);

            // 主标题
            this.lblTitle.AutoSize  = true;
            this.lblTitle.Font      = WinTriage.Theme.FontUI(16f, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = WinTriage.Theme.AccentBlue;
            this.lblTitle.Location  = new System.Drawing.Point(16, 8);
            this.lblTitle.Text      = "WinTriage";
            this.lblTitle.Name      = "lblTitle";

            // 副标题
            this.lblSubtitle.AutoSize  = true;
            this.lblSubtitle.Font      = WinTriage.Theme.FontUI(8.5f);
            this.lblSubtitle.ForeColor = WinTriage.Theme.TextMuted;
            this.lblSubtitle.Location  = new System.Drawing.Point(18, 34);
            this.lblSubtitle.Text      = "Windows 应急响应信息采集工具  v2.0";
            this.lblSubtitle.Name      = "lblSubtitle";

            // 管理员徽章（右侧）
            this.lblAdminBadge.AutoSize  = false;
            this.lblAdminBadge.Font      = WinTriage.Theme.FontUI(8f, System.Drawing.FontStyle.Bold);
            this.lblAdminBadge.ForeColor = WinTriage.Theme.AccentGreen;
            this.lblAdminBadge.Size      = new System.Drawing.Size(120, 20);
            this.lblAdminBadge.Location  = new System.Drawing.Point(655, 18);
            this.lblAdminBadge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAdminBadge.Text      = "● 管理员";
            this.lblAdminBadge.Name      = "lblAdminBadge";
            this.lblAdminBadge.Anchor    = System.Windows.Forms.AnchorStyles.Top
                                         | System.Windows.Forms.AnchorStyles.Right;

            // ── TabControl ───────────────────────────────────────────
            this.tabControl1.Controls.Add(this.tabSystem);
            this.tabControl1.Controls.Add(this.tabNetwork);
            this.tabControl1.Controls.Add(this.tabForensic);
            this.tabControl1.Font     = WinTriage.Theme.FontUI(9f);
            this.tabControl1.Location = new System.Drawing.Point(8, 72);
            this.tabControl1.Name     = "tabControl1";
            this.tabControl1.Size     = new System.Drawing.Size(784, 220);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Anchor   = System.Windows.Forms.AnchorStyles.Top
                                      | System.Windows.Forms.AnchorStyles.Left
                                      | System.Windows.Forms.AnchorStyles.Right;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SetTabColors(new System.Drawing.Color[] {
                WinTriage.Theme.TabSystem,
                WinTriage.Theme.TabNetwork,
                WinTriage.Theme.TabForensic
            });

            // ── Tab: 系统状态 ─────────────────────────────────────────
            this.tabSystem.BackColor = WinTriage.Theme.BgControl;
            this.tabSystem.Controls.Add(this.clbSystem);
            this.tabSystem.Controls.Add(this.cbSystemAll);
            this.tabSystem.Location  = new System.Drawing.Point(4, 32);
            this.tabSystem.Name      = "tabSystem";
            this.tabSystem.Padding   = new System.Windows.Forms.Padding(4);
            this.tabSystem.Size      = new System.Drawing.Size(776, 184);
            this.tabSystem.TabIndex  = 0;
            this.tabSystem.Text      = "⚙  系统状态";

            this.clbSystem.AccentColor = WinTriage.Theme.TabSystem;
            this.clbSystem.Location    = new System.Drawing.Point(6, 6);
            this.clbSystem.Name        = "clbSystem";
            this.clbSystem.Size        = new System.Drawing.Size(596, 172);
            this.clbSystem.TabIndex    = 0;

            this.cbSystemAll.AutoSize  = true;
            this.cbSystemAll.Font      = WinTriage.Theme.FontUI(9f);
            this.cbSystemAll.ForeColor = WinTriage.Theme.TextPrimary;
            this.cbSystemAll.BackColor = WinTriage.Theme.BgControl;
            this.cbSystemAll.Location  = new System.Drawing.Point(616, 80);
            this.cbSystemAll.Name      = "cbSystemAll";
            this.cbSystemAll.Text      = "全选";
            this.cbSystemAll.TabIndex  = 1;
            this.cbSystemAll.CheckedChanged += new System.EventHandler(this.cbSystemAll_CheckedChanged);

            // ── Tab: 网络信息 ─────────────────────────────────────────
            this.tabNetwork.BackColor = WinTriage.Theme.BgControl;
            this.tabNetwork.Controls.Add(this.clbNetwork);
            this.tabNetwork.Controls.Add(this.cbNetworkAll);
            this.tabNetwork.Location  = new System.Drawing.Point(4, 32);
            this.tabNetwork.Name      = "tabNetwork";
            this.tabNetwork.Padding   = new System.Windows.Forms.Padding(4);
            this.tabNetwork.Size      = new System.Drawing.Size(776, 184);
            this.tabNetwork.TabIndex  = 1;
            this.tabNetwork.Text      = "🌐  网络信息";

            this.clbNetwork.AccentColor = WinTriage.Theme.TabNetwork;
            this.clbNetwork.Location    = new System.Drawing.Point(6, 6);
            this.clbNetwork.Name        = "clbNetwork";
            this.clbNetwork.Size        = new System.Drawing.Size(596, 172);
            this.clbNetwork.TabIndex    = 0;

            this.cbNetworkAll.AutoSize  = true;
            this.cbNetworkAll.Font      = WinTriage.Theme.FontUI(9f);
            this.cbNetworkAll.ForeColor = WinTriage.Theme.TextPrimary;
            this.cbNetworkAll.BackColor = WinTriage.Theme.BgControl;
            this.cbNetworkAll.Location  = new System.Drawing.Point(616, 80);
            this.cbNetworkAll.Name      = "cbNetworkAll";
            this.cbNetworkAll.Text      = "全选";
            this.cbNetworkAll.TabIndex  = 1;
            this.cbNetworkAll.CheckedChanged += new System.EventHandler(this.cbNetworkAll_CheckedChanged);

            // ── Tab: 痕迹取证 ─────────────────────────────────────────
            this.tabForensic.BackColor = WinTriage.Theme.BgControl;
            this.tabForensic.Controls.Add(this.clbForensic);
            this.tabForensic.Controls.Add(this.cbForensicAll);
            this.tabForensic.Location  = new System.Drawing.Point(4, 32);
            this.tabForensic.Name      = "tabForensic";
            this.tabForensic.Padding   = new System.Windows.Forms.Padding(4);
            this.tabForensic.Size      = new System.Drawing.Size(776, 184);
            this.tabForensic.TabIndex  = 2;
            this.tabForensic.Text      = "🔍  痕迹取证";

            this.clbForensic.AccentColor = WinTriage.Theme.TabForensic;
            this.clbForensic.Location    = new System.Drawing.Point(6, 6);
            this.clbForensic.Name        = "clbForensic";
            this.clbForensic.Size        = new System.Drawing.Size(596, 172);
            this.clbForensic.TabIndex    = 0;

            this.cbForensicAll.AutoSize  = true;
            this.cbForensicAll.Font      = WinTriage.Theme.FontUI(9f);
            this.cbForensicAll.ForeColor = WinTriage.Theme.TextPrimary;
            this.cbForensicAll.BackColor = WinTriage.Theme.BgControl;
            this.cbForensicAll.Location  = new System.Drawing.Point(616, 80);
            this.cbForensicAll.Name      = "cbForensicAll";
            this.cbForensicAll.Text      = "全选";
            this.cbForensicAll.TabIndex  = 1;
            this.cbForensicAll.CheckedChanged += new System.EventHandler(this.cbForensicAll_CheckedChanged);

            // ── 控制区：开始/停止/进度条 ──────────────────────────────
            this.btnStart.GradientTop = WinTriage.Theme.BtnStartTop;
            this.btnStart.GradientBot = WinTriage.Theme.BtnStartBot;
            this.btnStart.Location    = new System.Drawing.Point(8, 302);
            this.btnStart.Name        = "btnStart";
            this.btnStart.Size        = new System.Drawing.Size(105, 34);
            this.btnStart.TabIndex    = 1;
            this.btnStart.Text        = "▶  开始采集";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            this.btnStop.GradientTop = WinTriage.Theme.BtnStopTop;
            this.btnStop.GradientBot = WinTriage.Theme.BtnStopBot;
            this.btnStop.Enabled     = false;
            this.btnStop.Location    = new System.Drawing.Point(120, 302);
            this.btnStop.Name        = "btnStop";
            this.btnStop.Size        = new System.Drawing.Size(88, 34);
            this.btnStop.TabIndex    = 2;
            this.btnStop.Text        = "■  停止";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

            this.progressBar1.Location = new System.Drawing.Point(218, 306);
            this.progressBar1.Name     = "progressBar1";
            this.progressBar1.Size     = new System.Drawing.Size(320, 26);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Maximum  = 100;

            this.lblProgress.AutoSize  = true;
            this.lblProgress.Font      = WinTriage.Theme.FontUI(9f);
            this.lblProgress.ForeColor = WinTriage.Theme.TextMuted;
            this.lblProgress.Location  = new System.Drawing.Point(548, 310);
            this.lblProgress.Name      = "lblProgress";
            this.lblProgress.Text      = "就绪";
            this.lblProgress.TabIndex  = 4;

            // ── 输出目录行 ───────────────────────────────────────────
            this.pnlOutput.BackColor = WinTriage.Theme.BgPanel;
            this.pnlOutput.Location  = new System.Drawing.Point(8, 346);
            this.pnlOutput.Name      = "pnlOutput";
            this.pnlOutput.Size      = new System.Drawing.Size(784, 36);
            this.pnlOutput.TabIndex  = 10;
            this.pnlOutput.Anchor    = System.Windows.Forms.AnchorStyles.Top
                                     | System.Windows.Forms.AnchorStyles.Left
                                     | System.Windows.Forms.AnchorStyles.Right;
            this.pnlOutput.Controls.Add(this.lblOutputDir);
            this.pnlOutput.Controls.Add(this.txtOutputDir);
            this.pnlOutput.Controls.Add(this.btnBrowse);

            this.lblOutputDir.AutoSize  = true;
            this.lblOutputDir.Font      = WinTriage.Theme.FontUI(9f);
            this.lblOutputDir.ForeColor = WinTriage.Theme.TextMuted;
            this.lblOutputDir.Location  = new System.Drawing.Point(8, 10);
            this.lblOutputDir.Name      = "lblOutputDir";
            this.lblOutputDir.Text      = "输出目录:";
            this.lblOutputDir.TabIndex  = 5;

            this.txtOutputDir.Location = new System.Drawing.Point(70, 7);
            this.txtOutputDir.Name     = "txtOutputDir";
            this.txtOutputDir.Size     = new System.Drawing.Size(290, 23);
            this.txtOutputDir.TabIndex = 6;
            this.txtOutputDir.Text     = ".\\Output";

            this.btnBrowse.GradientTop = WinTriage.Theme.BtnNeutTop;
            this.btnBrowse.GradientBot = WinTriage.Theme.BtnNeutBot;
            this.btnBrowse.Location    = new System.Drawing.Point(364, 6);
            this.btnBrowse.Name        = "btnBrowse";
            this.btnBrowse.Size        = new System.Drawing.Size(60, 24);
            this.btnBrowse.TabIndex    = 7;
            this.btnBrowse.Text        = "浏览…";
            this.btnBrowse.CornerRadius= 4;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);

            // ── 日志区域标题栏 ───────────────────────────────────────
            this.pnlLogHeader.BackColor = WinTriage.Theme.BgPanel;
            this.pnlLogHeader.Location  = new System.Drawing.Point(8, 390);
            this.pnlLogHeader.Name      = "pnlLogHeader";
            this.pnlLogHeader.Size      = new System.Drawing.Size(784, 28);
            this.pnlLogHeader.TabIndex  = 11;
            this.pnlLogHeader.Anchor    = System.Windows.Forms.AnchorStyles.Top
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right;
            this.pnlLogHeader.Controls.Add(this.lblLogTitle);

            this.lblLogTitle.AutoSize  = true;
            this.lblLogTitle.Font      = WinTriage.Theme.FontUI(8.5f, System.Drawing.FontStyle.Bold);
            this.lblLogTitle.ForeColor = WinTriage.Theme.TextMuted;
            this.lblLogTitle.Location  = new System.Drawing.Point(10, 6);
            this.lblLogTitle.Name      = "lblLogTitle";
            this.lblLogTitle.Text      = "► 采集日志";

            // ── RichTextBox 日志 ─────────────────────────────────────
            this.rtbLog.Anchor = System.Windows.Forms.AnchorStyles.Top
                               | System.Windows.Forms.AnchorStyles.Bottom
                               | System.Windows.Forms.AnchorStyles.Left
                               | System.Windows.Forms.AnchorStyles.Right;
            this.rtbLog.BackColor   = WinTriage.Theme.BgBase;
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.DetectUrls  = false;
            this.rtbLog.Font        = WinTriage.Theme.FontMono(9f);
            this.rtbLog.ForeColor   = WinTriage.Theme.TextPrimary;
            this.rtbLog.Location    = new System.Drawing.Point(8, 418);
            this.rtbLog.Name        = "rtbLog";
            this.rtbLog.ReadOnly    = true;
            this.rtbLog.Size        = new System.Drawing.Size(784, 186);
            this.rtbLog.TabIndex    = 8;
            this.rtbLog.Text        = "";
            this.rtbLog.WordWrap    = false;
            this.rtbLog.Padding     = new System.Windows.Forms.Padding(4);

            // ── StatusStrip ──────────────────────────────────────────
            this.statusStrip1.BackColor = WinTriage.Theme.BgPanel;
            this.statusStrip1.ForeColor = WinTriage.Theme.TextMuted;
            this.statusStrip1.SizingGrip= false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.toolStripStatusLabel1,
                this.toolStripStatusLabel2 });
            this.statusStrip1.Location  = new System.Drawing.Point(0, 612);
            this.statusStrip1.Name      = "statusStrip1";
            this.statusStrip1.Size      = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex  = 9;
            this.statusStrip1.Font      = WinTriage.Theme.FontUI(8.5f);

            this.toolStripStatusLabel1.ForeColor = WinTriage.Theme.AccentGreen;
            this.toolStripStatusLabel1.Name      = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Text      = "✅ 就绪";

            this.toolStripStatusLabel2.ForeColor = WinTriage.Theme.TextMuted;
            this.toolStripStatusLabel2.Name      = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Text      = "输出: .\\Output";

            // ── Form 本体 ────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = WinTriage.Theme.BgBase;
            this.ClientSize          = new System.Drawing.Size(800, 634);
            this.Font                = WinTriage.Theme.FontUI(9f);
            this.ForeColor           = WinTriage.Theme.TextPrimary;
            this.MinimumSize         = new System.Drawing.Size(640, 560);
            this.Name                = "Form1";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "WinTriage — 应急响应信息采集 v2.0";
            this.Load += new System.EventHandler(this.Form1_Load);

            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.pnlOutput);
            this.Controls.Add(this.pnlLogHeader);
            this.Controls.Add(this.rtbLog);

            this.tabControl1.ResumeLayout(false);
            this.tabSystem.ResumeLayout(false);
            this.tabSystem.PerformLayout();
            this.tabNetwork.ResumeLayout(false);
            this.tabNetwork.PerformLayout();
            this.tabForensic.ResumeLayout(false);
            this.tabForensic.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlOutput.ResumeLayout(false);
            this.pnlOutput.PerformLayout();
            this.pnlLogHeader.ResumeLayout(false);
            this.pnlLogHeader.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // ── 控件字段声明 ─────────────────────────────────────────────
        private WinTriage.DarkTabControl   tabControl1;
        private System.Windows.Forms.TabPage tabSystem;
        private System.Windows.Forms.TabPage tabNetwork;
        private System.Windows.Forms.TabPage tabForensic;
        private WinTriage.DarkCheckedListBox clbSystem;
        private WinTriage.DarkCheckedListBox clbNetwork;
        private WinTriage.DarkCheckedListBox clbForensic;
        private System.Windows.Forms.CheckBox cbSystemAll;
        private System.Windows.Forms.CheckBox cbNetworkAll;
        private System.Windows.Forms.CheckBox cbForensicAll;
        private WinTriage.FlatButton       btnStart;
        private WinTriage.FlatButton       btnStop;
        private WinTriage.FlatButton       btnBrowse;
        private WinTriage.GradientProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblAdminBadge;
        private System.Windows.Forms.Panel pnlOutput;
        private System.Windows.Forms.Label lblOutputDir;
        private WinTriage.DarkTextBox      txtOutputDir;
        private System.Windows.Forms.Panel pnlLogHeader;
        private System.Windows.Forms.Label lblLogTitle;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}
