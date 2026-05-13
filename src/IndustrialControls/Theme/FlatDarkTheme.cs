using System.Drawing;

namespace IndustrialControls.Theme
{
    /// <summary>
    /// 扁平化深色主题
    /// </summary>
    public class FlatDarkTheme : ITheme
    {
        public string Name => "FlatDark";
        public bool IsDark => true;
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
            // 主色调
            Primary = Color.FromArgb(96, 165, 250),
            PrimaryLight = Color.FromArgb(147, 197, 253),
            PrimaryDark = Color.FromArgb(59, 130, 246),
            Secondary = Color.FromArgb(156, 163, 175),

            // 语义色
            Success = Color.FromArgb(52, 211, 153),
            Danger = Color.FromArgb(248, 113, 113),
            Warning = Color.FromArgb(251, 191, 36),
            Info = Color.FromArgb(96, 165, 250),

            // 背景色
            Background = Color.FromArgb(30, 30, 30),
            Surface = Color.FromArgb(45, 45, 45),
            SurfaceVariant = Color.FromArgb(55, 55, 55),
            CardBackground = Color.FromArgb(50, 50, 50),

            // 文字色
            TextPrimary = Color.FromArgb(240, 240, 240),
            TextSecondary = Color.FromArgb(170, 170, 170),
            TextDisabled = Color.FromArgb(100, 100, 100),
            TextOnPrimary = Color.FromArgb(255, 255, 255),

            // 边框色
            Border = Color.FromArgb(70, 70, 70),
            BorderLight = Color.FromArgb(60, 60, 60),
            BorderFocus = Color.FromArgb(96, 165, 250),

            // 状态色
            Hover = Color.FromArgb(20, 255, 255, 255),
            Pressed = Color.FromArgb(40, 255, 255, 255),
            Disabled = Color.FromArgb(40, 40, 40),

            // 工业专用色
            Running = Color.FromArgb(52, 211, 153),
            Stopped = Color.FromArgb(107, 114, 128),
            Fault = Color.FromArgb(248, 113, 113),
            Idle = Color.FromArgb(251, 191, 36),

            // 报警色
            AlarmEmergency = Color.FromArgb(248, 113, 113),
            AlarmImportant = Color.FromArgb(251, 146, 60),
            AlarmGeneral = Color.FromArgb(250, 204, 21),
            AlarmInfo = Color.FromArgb(96, 165, 250),
        };
    }
}
