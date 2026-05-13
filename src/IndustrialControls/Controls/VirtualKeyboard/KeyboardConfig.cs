using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IndustrialControls.Controls.VirtualKeyboard
{
    /// <summary>
    /// 虚拟键盘配置类
    /// </summary>
    public class KeyboardConfig
    {
        public int ButtonWidth { get; set; } = 60;
        public int ButtonHeight { get; set; } = 50;
        public int ButtonSpacing { get; set; } = 6;
        public int PanelPadding { get; set; } = 10;
        public Font NormalFont { get; set; } = new Font("Segoe UI", 13F, FontStyle.Regular);
        public Font SpecialFont { get; set; } = new Font("Segoe UI", 11F, FontStyle.Regular);
        
        public KeyboardConfig()
        {
            // 确保字体不为null
            NormalFont = new Font("Segoe UI", 13F, FontStyle.Regular);
            SpecialFont = new Font("Segoe UI", 11F, FontStyle.Regular);
        }
    }
}
