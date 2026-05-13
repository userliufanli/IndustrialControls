using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Theme;
using IndustrialControls.Utilities;

namespace IndustrialControls.Controls.DataVisualization
{
    /// <summary>
    /// 进度条方向
    /// </summary>
    public enum ProgressDirection
    {
        Horizontal,
        Vertical
    }

    /// <summary>
    /// 工业进度条控件，支持分段着色、数值显示
    /// </summary>
    [ToolboxItem(true)]
    [DefaultProperty("Value")]
    public class IndustrialProgressBar : DoubleBufferedControl
    {
        private double _value;
        private double _minimum;
        private double _maximum = 100;
        private double _warningThreshold = 70;
        private double _dangerThreshold = 90;
        private bool _inverseThreshold = false;
        private bool _showValue = true;
        private string _unit = "%";
        private ProgressDirection _direction = ProgressDirection.Horizontal;
        private string _label = "";

        /// <summary>
        /// 值变更事件
        /// </summary>
        public event EventHandler<double> ValueChanged;

        [Category("数据")]
        [Description("当前进度值")]
        [DefaultValue(0.0)]
        public double Value
        {
            get => _value;
            set
            {
                double newVal = ValidationHelper.Clamp(value, _minimum, _maximum);
                bool valueChanged = Math.Abs(_value - newVal) > double.Epsilon;
                _value = newVal;
                ThrottledInvalidate();
                if (valueChanged)
                    ValueChanged?.Invoke(this, _value);
            }
        }

        [Category("数据")]
        [Description("进度条最小值")]
        [DefaultValue(0.0)]
        public double Minimum { get => _minimum; set { _minimum = value; Invalidate(); } }

        [Category("数据")]
        [Description("进度条最大值")]
        [DefaultValue(100.0)]
        public double Maximum { get => _maximum; set { _maximum = value; Invalidate(); } }

        [Category("数据")]
        [Description("警告阈值，超过此值进度条变为警告色")]
        [DefaultValue(70.0)]
        public double WarningThreshold { get => _warningThreshold; set { _warningThreshold = value; Invalidate(); } }

        [Category("数据")]
        [Description("危险阈值，超过此值进度条变为危险色")]
        [DefaultValue(90.0)]
        public double DangerThreshold { get => _dangerThreshold; set { _dangerThreshold = value; Invalidate(); } }

        /// <summary>
        /// 是否反向阈值（数值越小越危险）
        /// </summary>
        [Category("行为")]
        [Description("是否启用反向阈值模式（数值越小越危险，如电池电量）")]
        [DefaultValue(false)]
        public bool InverseThreshold
        {
            get => _inverseThreshold;
            set { _inverseThreshold = value; Invalidate(); }
        }

        [Category("外观")]
        [Description("是否在进度条上显示当前数值")]
        [DefaultValue(true)]
        public bool ShowValue { get => _showValue; set { _showValue = value; Invalidate(); } }

        [Category("外观")]
        [Description("数值显示的单位文本")]
        [DefaultValue("%")]
        public string Unit { get => _unit; set { _unit = value ?? ""; Invalidate(); } }

        [Category("外观")]
        [Description("进度条填充方向，水平或垂直")]
        [DefaultValue(ProgressDirection.Horizontal)]
        public ProgressDirection Direction { get => _direction; set { _direction = value; Invalidate(); } }

        [Category("外观")]
        [Description("进度条前方显示的标签文本")]
        [DefaultValue("")]
        public string Label { get => _label; set { _label = value ?? ""; Invalidate(); } }

        public IndustrialProgressBar()
        {
            Size = new Size(250, 30);
            CornerRadius = 4;
        }

        protected override void RenderControl(Graphics g, Rectangle bounds)
        {
            double ratio = (_maximum > _minimum) ? (_value - _minimum) / (_maximum - _minimum) : 0;
            ratio = Math.Max(0, Math.Min(1, ratio));

            // 进度条区域
            var barRect = bounds;
            if (!string.IsNullOrEmpty(_label))
            {
                if (_direction == ProgressDirection.Horizontal)
                {
                    var labelSize = g.MeasureString(_label, CurrentTheme.SmallFont);
                    barRect = new Rectangle(bounds.X + (int)labelSize.Width + 4, bounds.Y, bounds.Width - (int)labelSize.Width - 4, bounds.Height);
                    using (var brush = new SolidBrush(CurrentTheme.Colors.TextSecondary))
                        g.DrawString(_label, CurrentTheme.SmallFont, brush, bounds.X, bounds.Y + (bounds.Height - labelSize.Height) / 2);
                }
                else
                {
                    // 垂直模式：标签显示在进度条底部
                    var labelSize = g.MeasureString(_label, CurrentTheme.SmallFont);
                    int labelHeight = (int)labelSize.Height + 4;
                    barRect = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height - labelHeight);
                    using (var brush = new SolidBrush(CurrentTheme.Colors.TextSecondary))
                    {
                        // 标签居中显示在底部
                        float labelX = bounds.X + (bounds.Width - labelSize.Width) / 2;
                        g.DrawString(_label, CurrentTheme.SmallFont, brush, labelX, bounds.Bottom - labelSize.Height);
                    }
                }
            }

            // 背景
            using (var brush = new SolidBrush(BackColor))
            {
                GraphicsHelper.FillRoundedRectangle(g, brush, barRect, CornerRadius);
            }

            // 进度填充
            Color fillColor = GetProgressColor();
            Rectangle fillRect;
            if (_direction == ProgressDirection.Horizontal)
            {
                int fillWidth = (int)(barRect.Width * ratio);
                fillRect = new Rectangle(barRect.X, barRect.Y, fillWidth, barRect.Height);
            }
            else
            {
                int fillHeight = (int)(barRect.Height * ratio);
                fillRect = new Rectangle(barRect.X, barRect.Bottom - fillHeight, barRect.Width, fillHeight);
            }

            if (fillRect.Width > 0 && fillRect.Height > 0)
            {
                using (var brush = new SolidBrush(fillColor))
                {
                    GraphicsHelper.FillRoundedRectangle(g, brush, fillRect, CornerRadius);
                }
            }

            // 数值文本
            if (_showValue)
            {
                string text = $"{_value:F1}{_unit}";
                GraphicsHelper.DrawCenteredText(g, text, CurrentTheme.DefaultFont, ForeColor, barRect);
            }

            // 边框
            using (var pen = new Pen(CurrentTheme.Colors.Border))
            {
                GraphicsHelper.DrawRoundedRectangle(g, pen, barRect, CornerRadius);
            }
        }

        private Color GetProgressColor()
        {
            if (!_inverseThreshold)
            {
                // 正常模式：数值越大越危险
                if (_value >= _dangerThreshold) return CurrentTheme.Colors.Danger;
                if (_value >= _warningThreshold) return CurrentTheme.Colors.Warning;
                return CurrentTheme.Colors.Success;
            }
            else
            {
                // 反向模式：数值越小越危险
                if (_value <= _dangerThreshold) return CurrentTheme.Colors.Danger;
                if (_value <= _warningThreshold) return CurrentTheme.Colors.Warning;
                return CurrentTheme.Colors.Success;
            }
        }
    }
}
