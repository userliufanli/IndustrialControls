namespace IndustrialControls.Controls.StatusIndicator
{
    /// <summary>
    /// 状态指示器状态枚举
    /// </summary>
    public enum IndicatorState
    {
        /// <summary>运行中</summary>
        Running,
        /// <summary>已停止</summary>
        Stopped,
        /// <summary>故障</summary>
        Fault,
        /// <summary>警告</summary>
        Warning,
        /// <summary>空闲</summary>
        Idle,
        /// <summary>自定义</summary>
        Custom
    }

    /// <summary>
    /// 指示器外观形状
    /// </summary>
    public enum IndicatorShape
    {
        /// <summary>圆形</summary>
        Circle,
        /// <summary>方形</summary>
        Square,
        /// <summary>圆角矩形</summary>
        RoundedRectangle
    }
}
