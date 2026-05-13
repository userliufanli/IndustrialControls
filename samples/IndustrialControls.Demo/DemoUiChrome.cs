using System;
using System.Drawing;
using System.Windows.Forms;

namespace IndustrialControls.Demo
{
    /// <summary>
    /// 演示程序统一的页面背景、间距与分组样式。
    /// </summary>
    public static class DemoUiChrome
    {
        public static readonly Color Surface = Color.FromArgb(247, 248, 252);
        public static readonly Color Subtitle = Color.FromArgb(82, 88, 102);
        public static readonly Color PrimaryButton = Color.FromArgb(0, 103, 192);
        public static readonly Color MutedBorder = Color.FromArgb(220, 226, 234);

        /// <summary>
        /// 应用到各演示 UserControl：浅底、内边距、默认字体，并美化直接子级中的 <see cref="GroupBox"/>。
        /// </summary>
        /// <param name="page">演示页</param>
        /// <param name="compactPadding">复杂页（如通讯测试）使用略小的边距，避免挤压 Dock 布局。</param>
        public static void ApplyPage(UserControl page, bool compactPadding = false)
        {
            if (page == null) return;
            page.SuspendLayout();
            page.BackColor = Surface;
            page.Padding = compactPadding
                ? new Padding(12, 10, 12, 10)
                : new Padding(0);
            page.Font = new Font("Microsoft YaHei UI", 9f);

            foreach (Control c in page.Controls)
            {
                if (c is GroupBox gb)
                    StyleGroupBox(gb);
                if (c is Label lb && lb.Name != null && lb.Name.StartsWith("descLabel", System.StringComparison.Ordinal))
                    StyleDescriptionLabel(lb);
            }

            page.ResumeLayout();
        }

        public static void StyleDescriptionLabel(Label label)
        {
            if (label == null) return;
            label.ForeColor = Subtitle;
            label.Font = new Font("Microsoft YaHei UI", 10f);
        }

        public static void StyleGroupBox(GroupBox box)
        {
            if (box == null) return;
            box.Font = new Font("Microsoft YaHei UI", 9.75f, FontStyle.Bold);
            box.Margin = new Padding(0, 12, 0, 0);
            box.Padding = new Padding(12, 10, 12, 12);
        }

        public static void StylePrimaryButton(Button button)
        {
            if (button == null) return;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = PrimaryButton;
            button.ForeColor = Color.White;
            button.Cursor = Cursors.Hand;
            button.Margin = new Padding(0, 0, 10, 0);
            button.Height = Math.Max(button.Height, 32);
        }

        public static void StyleSecondaryButton(Button button)
        {
            if (button == null) return;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = MutedBorder;
            button.BackColor = Color.White;
            button.Cursor = Cursors.Hand;
            button.Margin = new Padding(0, 0, 10, 0);
            button.Height = Math.Max(button.Height, 32);
        }
    }
}
