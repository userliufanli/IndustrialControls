using System.Drawing;
using System.Windows.Forms;

namespace IndustrialControls.Template.Core
{
    /// <summary>各 <see cref="UserControl"/> 页面统一背景与分组样式。</summary>
    public static class TemplateUiChrome
    {
        public static readonly Color Surface = Color.FromArgb(247, 248, 252);

        public static void ApplyPage(UserControl page, bool compactPadding = false)
        {
            if (page == null)
                return;
            page.SuspendLayout();
            page.BackColor = Surface;
            page.Padding = compactPadding
                ? new Padding(12, 10, 12, 10)
                : new Padding(16, 12, 16, 12);
            page.Font = new Font("Microsoft YaHei UI", 9f);
            foreach (Control c in page.Controls)
            {
                if (c is GroupBox gb)
                    StyleGroupBox(gb);
            }

            page.ResumeLayout();
        }

        public static void StyleGroupBox(GroupBox box)
        {
            if (box == null)
                return;
            box.Font = new Font("Microsoft YaHei UI", 9.75f, FontStyle.Bold);
            box.Margin = new Padding(0, 12, 0, 0);
            box.Padding = new Padding(12, 10, 12, 12);
        }
    }
}
