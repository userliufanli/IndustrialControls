using System;
using System.ComponentModel;
using System.Drawing;

namespace IndustrialControls.Controls.DeviceButton
{
    /// <summary>
    /// 按钮状态定义
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DeviceButtonState
    {
        /// <summary>
        /// 无参数构造函数（集合编辑器需要）
        /// </summary>
        public DeviceButtonState()
        {
            Name = "";
            DisplayText = "";
            Color = Color.Gray;
            Icon = null;
        }

        /// <summary>
        /// 状态名称
        /// </summary>
        [DisplayName("名称")]
        [Description("状态的内部标识名称")]
        [Category("数据")]
        public string Name { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        [DisplayName("显示文本")]
        [Description("按钮上显示的文本")]
        [Category("外观")]
        public string DisplayText { get; set; }

        /// <summary>
        /// 状态颜色
        /// </summary>
        [DisplayName("颜色")]
        [Description("按钮的背景颜色")]
        [Category("外观")]
        public Color Color { get; set; }

        /// <summary>
        /// 状态图标
        /// </summary>
        [DisplayName("图标")]
        [Description("按钮显示的图标（可选）")]
        [Category("外观")]
        public Image Icon { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? "(未命名)" : Name;
        }

        public DeviceButtonState(string name, string displayText, Color color, Image icon = null)
        {
            Name = name;
            DisplayText = displayText;
            Color = color;
            Icon = icon;
        }

        /// <summary>
        /// 预定义 - 启动状态
        /// </summary>
        public static DeviceButtonState Start => new DeviceButtonState("Start", "启动", Color.FromArgb(16, 185, 129));

        /// <summary>
        /// 预定义 - 停止状态
        /// </summary>
        public static DeviceButtonState Stop => new DeviceButtonState("Stop", "停止", Color.FromArgb(239, 68, 68));

        /// <summary>
        /// 预定义 - 暂停状态
        /// </summary>
        public static DeviceButtonState Pause => new DeviceButtonState("Pause", "暂停", Color.FromArgb(251, 191, 36));

        /// <summary>
        /// 预定义 - 复位状态
        /// </summary>
        public static DeviceButtonState Reset => new DeviceButtonState("Reset", "复位", Color.FromArgb(96, 165, 250));
    }
}
