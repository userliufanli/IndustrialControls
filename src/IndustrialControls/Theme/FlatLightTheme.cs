using System.Drawing;

namespace IndustrialControls.Theme
{
    /// <summary>
    /// Windows 11 扁平化浅色主题
    /// </summary>
    public class FlatLightTheme : ITheme
    {
        public string Name => "FlatLight";
        public bool IsDark => false;
        public int CornerRadius => 6;
        public int LargeCornerRadius => 10;
        public int Padding => 12;
        public int Spacing => 8;
        public int BorderWidth => 1;

        public Font DefaultFont { get; } = new Font("Microsoft YaHei UI", 9f, FontStyle.Regular);
        public Font HeaderFont { get; } = new Font("Microsoft YaHei UI", 12f, FontStyle.Bold);
        public Font SmallFont { get; } = new Font("Microsoft YaHei UI", 8f, FontStyle.Regular);

        public ThemeColors Colors { get; } = new ThemeColors
        {
            // 主色调 - Win11蓝
            Primary = Color.FromArgb(0, 103, 192),
            PrimaryLight = Color.FromArgb(53, 132, 209),
            PrimaryDark = Color.FromArgb(0, 78, 153),
            Secondary = Color.FromArgb(96, 96, 96),

            // 语义色
            Success = Color.FromArgb(16, 124, 16),
            Danger = Color.FromArgb(196, 43, 28),
            Warning = Color.FromArgb(157, 93, 0),
            Info = Color.FromArgb(0, 120, 212),

            // 背景色
            Background = Color.FromArgb(243, 243, 243),
            Surface = Color.FromArgb(255, 255, 255),
            SurfaceVariant = Color.FromArgb(249, 249, 249),
            CardBackground = Color.FromArgb(255, 255, 255),

            // 文字色
            TextPrimary = Color.FromArgb(26, 26, 26),
            TextSecondary = Color.FromArgb(96, 96, 96),
            TextDisabled = Color.FromArgb(160, 160, 160),
            TextOnPrimary = Color.FromArgb(255, 255, 255),

            // 边框色
            Border = Color.FromArgb(209, 209, 209),
            BorderLight = Color.FromArgb(229, 229, 229),
            BorderFocus = Color.FromArgb(0, 103, 192),

            // 状态色
            Hover = Color.FromArgb(20, 0, 0, 0),
            Pressed = Color.FromArgb(40, 0, 0, 0),
            Disabled = Color.FromArgb(243, 243, 243),

            // 工业专用色
            Running = Color.FromArgb(16, 185, 129),
            Stopped = Color.FromArgb(156, 163, 175),
            Fault = Color.FromArgb(239, 68, 68),
            Idle = Color.FromArgb(251, 191, 36),

            // 报警色
            AlarmEmergency = Color.FromArgb(220, 38, 38),
            AlarmImportant = Color.FromArgb(234, 88, 12),
            AlarmGeneral = Color.FromArgb(202, 138, 4),
            AlarmInfo = Color.FromArgb(37, 99, 235),
        };
    }
}
