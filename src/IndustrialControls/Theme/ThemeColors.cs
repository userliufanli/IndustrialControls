using System.Drawing;

namespace IndustrialControls.Theme
{
    /// <summary>
    /// 颜色方案定义，包含所有控件使用的标准颜色
    /// </summary>
    public class ThemeColors
    {
        // 主色调
        public Color Primary { get; set; }
        public Color PrimaryLight { get; set; }
        public Color PrimaryDark { get; set; }
        public Color Secondary { get; set; }

        // 语义色
        public Color Success { get; set; }
        public Color Danger { get; set; }
        public Color Warning { get; set; }
        public Color Info { get; set; }

        // 背景色
        public Color Background { get; set; }
        public Color Surface { get; set; }
        public Color SurfaceVariant { get; set; }
        public Color CardBackground { get; set; }

        // 文字色
        public Color TextPrimary { get; set; }
        public Color TextSecondary { get; set; }
        public Color TextDisabled { get; set; }
        public Color TextOnPrimary { get; set; }

        // 边框色
        public Color Border { get; set; }
        public Color BorderLight { get; set; }
        public Color BorderFocus { get; set; }

        // 状态色
        public Color Hover { get; set; }
        public Color Pressed { get; set; }
        public Color Disabled { get; set; }

        // 工业专用色
        public Color Running { get; set; }
        public Color Stopped { get; set; }
        public Color Fault { get; set; }
        public Color Idle { get; set; }

        // 报警色
        public Color AlarmEmergency { get; set; }
        public Color AlarmImportant { get; set; }
        public Color AlarmGeneral { get; set; }
        public Color AlarmInfo { get; set; }
    }
}
