using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinTriage
{
    // ─────────────────────────────────────────────
    //  全局颜色 & 字体主题常量
    // ─────────────────────────────────────────────
    internal static class Theme
    {
        // 背景色系
        public static readonly Color BgBase      = Color.FromArgb(15,  17,  26);   // 窗体最深背景
        public static readonly Color BgPanel     = Color.FromArgb(22,  25,  37);   // 面板/卡片背景
        public static readonly Color BgControl   = Color.FromArgb(30,  33,  48);   // 控件背景
        public static readonly Color BgHover     = Color.FromArgb(40,  44,  62);   // Hover 状态

        // 分割线 / 边框
        public static readonly Color Border      = Color.FromArgb(55,  60,  85);

        // 文字
        public static readonly Color TextPrimary = Color.FromArgb(220, 225, 240);  // 主文字
        public static readonly Color TextMuted   = Color.FromArgb(130, 140, 165);  // 次要文字
        public static readonly Color TextDisabled= Color.FromArgb(80,  88, 110);

        // 强调色
        public static readonly Color AccentBlue  = Color.FromArgb( 82, 157, 255);  // 蓝色强调
        public static readonly Color AccentGreen = Color.FromArgb( 80, 220, 140);  // 绿色（成功/网络）
        public static readonly Color AccentOrange= Color.FromArgb(255, 160,  60);  // 橙色（告警/取证）
        public static readonly Color AccentRed   = Color.FromArgb(255,  90,  90);  // 红色（错误/停止）

        // Tab 分类颜色
        public static readonly Color TabSystem   = Color.FromArgb( 82, 157, 255);  // 系统 - 蓝
        public static readonly Color TabNetwork  = Color.FromArgb( 80, 220, 140);  // 网络 - 绿
        public static readonly Color TabForensic = Color.FromArgb(255, 160,  60);  // 取证 - 橙

        // 按钮渐变
        public static readonly Color BtnStartTop = Color.FromArgb( 50, 130, 220);
        public static readonly Color BtnStartBot = Color.FromArgb( 30,  90, 170);
        public static readonly Color BtnStopTop  = Color.FromArgb(200,  70,  60);
        public static readonly Color BtnStopBot  = Color.FromArgb(150,  40,  35);
        public static readonly Color BtnNeutTop  = Color.FromArgb( 50,  55,  78);
        public static readonly Color BtnNeutBot  = Color.FromArgb( 35,  38,  55);

        // 字体
        public static Font FontUI(float size = 9f, FontStyle style = FontStyle.Regular)
            => new Font("Microsoft YaHei UI", size, style, GraphicsUnit.Point);

        public static Font FontMono(float size = 9f)
            => new Font("Consolas", size, FontStyle.Regular, GraphicsUnit.Point);
    }

    // ─────────────────────────────────────────────
    //  圆角渐变按钮
    // ─────────────────────────────────────────────
    internal class FlatButton : Button
    {
        private bool _isHover;
        private bool _isDown;

        public Color GradientTop { get; set; } = Theme.BtnNeutTop;
        public Color GradientBot { get; set; } = Theme.BtnNeutBot;
        public int   CornerRadius{ get; set; } = 6;

        public FlatButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            ForeColor = Theme.TextPrimary;
            Font      = Theme.FontUI(9f, FontStyle.Bold);
            Cursor    = Cursors.Hand;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnMouseEnter(EventArgs e) { _isHover = true;  Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _isHover = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown  (MouseEventArgs e) { _isDown = true;  Invalidate(); base.OnMouseDown(e);   }
        protected override void OnMouseUp    (MouseEventArgs e) { _isDown = false; Invalidate(); base.OnMouseUp(e);     }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 渐变背景
            Color top = Enabled
                ? (_isDown  ? Darken(GradientTop, 30)
                 : _isHover ? Lighten(GradientTop, 20)
                 : GradientTop)
                : Theme.BgControl;
            Color bot = Enabled
                ? (_isDown  ? Darken(GradientBot, 30)
                 : _isHover ? Lighten(GradientBot, 20)
                 : GradientBot)
                : Theme.BgControl;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = RoundRect(rect, CornerRadius))
            using (var brush = new LinearGradientBrush(rect, top, bot, 90f))
            {
                g.FillPath(brush, path);

                // 顶部高光线
                if (Enabled && !_isDown)
                {
                    var hiRect = new Rectangle(2, 1, Width - 4, Height / 2);
                    using (var hiPath = RoundRect(hiRect, CornerRadius - 1))
                    using (var hiBrush = new LinearGradientBrush(hiRect,
                        Color.FromArgb(50, Color.White), Color.Transparent, 90f))
                    {
                        g.FillPath(hiBrush, hiPath);
                    }
                }

                // 边框
                using (var pen = new Pen(Color.FromArgb(Enabled ? 60 : 30, Color.White), 1))
                    g.DrawPath(pen, path);
            }

            // 文字
            Color fc = Enabled ? ForeColor : Theme.TextDisabled;
            TextRenderer.DrawText(g, Text, Font, rect,
                fc, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private static GraphicsPath RoundRect(Rectangle r, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static Color Darken (Color c, int amount) => Color.FromArgb(c.A, Math.Max(0, c.R - amount), Math.Max(0, c.G - amount), Math.Max(0, c.B - amount));
        private static Color Lighten(Color c, int amount) => Color.FromArgb(c.A, Math.Min(255, c.R + amount), Math.Min(255, c.G + amount), Math.Min(255, c.B + amount));
    }

    // ─────────────────────────────────────────────
    //  暗色自绘 TabControl
    // ─────────────────────────────────────────────
    internal class DarkTabControl : TabControl
    {
        // 每个 Tab 对应的强调色
        private Color[] _tabColors = new[] { Theme.TabSystem, Theme.TabNetwork, Theme.TabForensic };

        public DarkTabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
            DrawMode  = TabDrawMode.OwnerDrawFixed;
            ItemSize  = new Size(110, 32);
            SizeMode  = TabSizeMode.Fixed;
            Appearance= TabAppearance.Normal;
        }

        // 设置每个 Tab 的颜色（必须在添加页之后调用）
        public void SetTabColors(Color[] colors)
        {
            _tabColors = colors;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Theme.BgPanel);

            // Tab 页内容区背景
            if (TabCount > 0)
            {
                var contentRect = GetTabRect(0);
                var bodyRect = new Rectangle(
                    ClientRectangle.X,
                    contentRect.Bottom,
                    ClientRectangle.Width,
                    ClientRectangle.Height - contentRect.Bottom);
                using (var br = new SolidBrush(Theme.BgControl))
                    g.FillRectangle(br, bodyRect);
                using (var pen = new Pen(Theme.Border))
                    g.DrawRectangle(pen, bodyRect.X, bodyRect.Y, bodyRect.Width - 1, bodyRect.Height - 1);
            }

            // 绘制每个 Tab
            for (int i = 0; i < TabCount; i++)
                DrawTab(g, i);
        }

        private void DrawTab(Graphics g, int index)
        {
            var rect   = GetTabRect(index);
            bool sel   = (SelectedIndex == index);
            Color accentColor = index < _tabColors.Length ? _tabColors[index] : Theme.AccentBlue;

            // 背景
            Color bg = sel ? Theme.BgControl : Theme.BgPanel;
            using (var br = new SolidBrush(bg))
                g.FillRectangle(br, rect);

            // 选中时底部彩色条
            if (sel)
            {
                using (var br = new SolidBrush(accentColor))
                    g.FillRectangle(br, rect.X, rect.Bottom - 3, rect.Width, 3);
            }

            // 文字
            Color tc = sel ? accentColor : Theme.TextMuted;
            FontStyle fs = sel ? FontStyle.Bold : FontStyle.Regular;
            using (var font = Theme.FontUI(9f, fs))
                TextRenderer.DrawText(g, TabPages[index].Text, font, rect, tc,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // 确保子控件背景色同步
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control is TabPage tp)
            {
                tp.BackColor = Theme.BgControl;
                tp.ForeColor = Theme.TextPrimary;
            }
        }
    }

    // ─────────────────────────────────────────────
    //  暗色 CheckedListBox（OwnerDraw）
    // ─────────────────────────────────────────────
    internal class DarkCheckedListBox : CheckedListBox
    {
        public Color AccentColor { get; set; } = Theme.AccentBlue;

        public DarkCheckedListBox()
        {
            DrawMode    = DrawMode.OwnerDrawFixed;
            ItemHeight  = 24;
            BorderStyle = BorderStyle.None;
            BackColor   = Theme.BgControl;
            ForeColor   = Theme.TextPrimary;
            CheckOnClick= true;
            IntegralHeight = false;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count) return;

            var g    = e.Graphics;
            bool sel = (e.State & DrawItemState.Selected) != 0;
            bool chk = GetItemChecked(e.Index);

            // 行背景
            Color bg = sel ? Theme.BgHover : (e.Index % 2 == 0 ? Theme.BgControl : Color.FromArgb(35, 38, 55));
            using (var br = new SolidBrush(bg))
                g.FillRectangle(br, e.Bounds);

            // 自定义复选框
            int cbSize = 14;
            int cbX    = e.Bounds.X + 6;
            int cbY    = e.Bounds.Y + (e.Bounds.Height - cbSize) / 2;
            var cbRect = new Rectangle(cbX, cbY, cbSize, cbSize);

            if (chk)
            {
                using (var br = new SolidBrush(AccentColor))
                    g.FillRectangle(br, cbRect);
                // 勾选标记
                using (var pen = new Pen(Color.White, 2))
                {
                    g.DrawLine(pen, cbX + 2, cbY + 7,  cbX + 5,  cbY + 10);
                    g.DrawLine(pen, cbX + 5, cbY + 10, cbX + 11, cbY + 3);
                }
            }
            else
            {
                using (var pen = new Pen(Theme.Border, 1))
                    g.DrawRectangle(pen, cbRect);
            }

            // 文字
            Color fc = chk ? Theme.TextPrimary : Theme.TextMuted;
            using (var font = Theme.FontUI(9f))
                TextRenderer.DrawText(g, Items[e.Index].ToString(), font,
                    new Rectangle(cbX + cbSize + 8, e.Bounds.Y, e.Bounds.Width - cbSize - 20, e.Bounds.Height),
                    fc, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }
    }

    // ─────────────────────────────────────────────
    //  渐变进度条（带百分比文字）
    // ─────────────────────────────────────────────
    internal class GradientProgressBar : ProgressBar
    {
        public Color BarColorStart { get; set; } = Color.FromArgb(50, 180, 120);
        public Color BarColorEnd   { get; set; } = Color.FromArgb(80, 220, 160);
        public bool  ShowText      { get; set; } = true;

        public GradientProgressBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 背景
            using (var br = new SolidBrush(Theme.BgControl))
                g.FillRectangle(br, ClientRectangle);

            // 边框
            using (var pen = new Pen(Theme.Border))
                g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);

            // 进度条
            if (Maximum > 0 && Value > 0)
            {
                int fillW = (int)((double)Value / Maximum * (Width - 2));
                if (fillW > 0)
                {
                    var fillRect = new Rectangle(1, 1, fillW, Height - 2);
                    using (var br = new LinearGradientBrush(new Rectangle(1, 1, Width - 2, Height - 2),
                        BarColorStart, BarColorEnd, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(br, fillRect);
                    }
                    // 顶部高光
                    using (var hiPen = new Pen(Color.FromArgb(40, Color.White)))
                        g.DrawLine(hiPen, 1, 1, fillW, 1);
                }
            }

            // 文字
            if (ShowText)
            {
                string text = Maximum > 0 ? $"{Value * 100 / Maximum}%" : "0%";
                using (var font = Theme.FontUI(8.5f, FontStyle.Bold))
                    TextRenderer.DrawText(g, text, font, ClientRectangle,
                        Theme.TextPrimary, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }
    }

    // ─────────────────────────────────────────────
    //  暗色 TextBox（带边框高亮）
    // ─────────────────────────────────────────────
    internal class DarkTextBox : TextBox
    {
        public DarkTextBox()
        {
            BackColor   = Theme.BgControl;
            ForeColor   = Theme.TextPrimary;
            BorderStyle = BorderStyle.FixedSingle;
            Font        = Theme.FontUI(9f);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            // 父容器刷新边框颜色（简单实现）
            Invalidate();
        }
    }
}
