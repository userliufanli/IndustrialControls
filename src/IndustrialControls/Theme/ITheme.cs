using System.Drawing;

namespace IndustrialControls.Theme
{
    /// <summary>
    /// 主题接口定义
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// 主题名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 颜色方案
        /// </summary>
        ThemeColors Colors { get; }

        /// <summary>
        /// 默认字体
        /// </summary>
        Font DefaultFont { get; }

        /// <summary>
        /// 标题字体
        /// </summary>
        Font HeaderFont { get; }

        /// <summary>
        /// 小号字体
        /// </summary>
        Font SmallFont { get; }

        /// <summary>
        /// 默认圆角半径
        /// </summary>
        int CornerRadius { get; }

        /// <summary>
        /// 大圆角半径
        /// </summary>
        int LargeCornerRadius { get; }

        /// <summary>
        /// 默认内边距
        /// </summary>
        int Padding { get; }

        /// <summary>
        /// 控件间距
        /// </summary>
        int Spacing { get; }

        /// <summary>
        /// 边框宽度
        /// </summary>
        int BorderWidth { get; }

        /// <summary>
        /// 是否为深色主题
        /// </summary>
        bool IsDark { get; }
    }
}
