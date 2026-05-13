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
    /// 仪表盘控件，圆弧刻度、指针动画、多色区域
    /// </summary>
    [ToolboxItem(true)]
    [DefaultProperty("Value")]
    public class GaugeControl : DoubleBufferedControl
    {
        private double _value;
        private double _minimum;
        private double _maximum = 100;
        private double _warningThreshold = 70;
        private double _dangerThreshold = 90;
        private bool _inverseThreshold = false;
        private string _unit = "";
        private string _title = "";
        private int _startAngle = 135;
        private int _sweepAngle = 270;
        private float _targetAngle;
        private float _currentAngle;
        private Timer _animationTimer;

        /// <summary>
        /// 值变更事件
        /// </summary>
        public event EventHandler<double> ValueChanged;

        /// <summary>
        /// 当前值
        /// </summary>
        [Category("数据")]
        [Description("当前数值")]
        [DefaultValue(0.0)]
        public double Value
        {
            get => _value;
            set
            {
                double newVal = ValidationHelper.Clamp(value, _minimum, _maximum);
                bool valueChanged = Math.Abs(_value - newVal) > double.Epsilon;
                _value = newVal;
                _targetAngle = ValueToAngle(_value);
                if (_animationTimer != null)
                {
                    StartAnimation();
                }
                else
                {
                    // 设计模式下直接设置角度，无动画
                    _currentAngle = _targetAngle;
                    Invalidate();
                }
                if (valueChanged)
                    ValueChanged?.Invoke(this, _value);
            }
        }

        [Category("数据")]
        [Description("仪表盘最小刻度值")]
        [DefaultValue(0.0)]
        public double Minimum { get => _minimum; set { _minimum = value; Invalidate(); } }

        [Category("数据")]
        [Description("仪表盘最大刻度值")]
        [DefaultValue(100.0)]
        public double Maximum { get => _maximum; set { _maximum = value; Invalidate(); } }

        [Category("数据")]
        [DefaultValue(70.0)]
        [Description("警告阈值")]
        public double WarningThreshold { get => _warningThreshold; set { _warningThreshold = value; Invalidate(); } }

        [Category("数据")]
        [DefaultValue(90.0)]
        [Description("危险阈值")]
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
        [Description("仪表盘数值显示的单位文本")]
        [DefaultValue("")]
        public string Unit { get => _unit; set { _unit = value ?? ""; Invalidate(); } }

        [Category("外观")]
        [Description("仪表盘底部显示的标题文本")]
        [DefaultValue("")]
        public string Title { get => _title; set { _title = value ?? ""; Invalidate(); } }

        public GaugeControl()
        {
            Size = new Size(200, 200);
            _currentAngle = ValueToAngle(0);
            _targetAngle = _currentAngle;

            // 设计模式下不创建动画Timer
            if (!IsDesignMode)
            {
                _animationTimer = new Timer { Interval = 16 };
                _animationTimer.Tick += AnimationTimer_Tick;
            }
        }

        private void StartAnimation()
        {
            _animationTimer?.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            float diff = _targetAngle - _currentAngle;
            if (Math.Abs(diff) < 0.5f)
            {
                _currentAngle = _targetAngle;
                _animationTimer.Stop();
            }
            else
            {
                _currentAngle += diff * 0.15f;
            }
            SafeInvalidate();
        }

        private float ValueToAngle(double value)
        {
            double ratio = (_maximum > _minimum) ? (value - _minimum) / (_maximum - _minimum) : 0;
            return (float)(_startAngle + _sweepAngle * ratio);
        }

        protected override void RenderControl(Graphics g, Rectangle bounds)
        {
            int size = Math.Min(bounds.Width, bounds.Height);
            int centerX = bounds.X + bounds.Width / 2;
            int centerY = bounds.Y + bounds.Height / 2;
            int radius = size / 2 - 15;

            // 绘制外圈背景弧
            DrawArcBackground(g, centerX, centerY, radius);

            // 绘制刻度
            DrawTicks(g, centerX, centerY, radius);

            // 绘制指针
            DrawNeedle(g, centerX, centerY, radius - 20);

            // 绘制中心圆
            int centerRadius = 8;
            using (var brush = new SolidBrush(ForeColor))
            {
                g.FillEllipse(brush, centerX - centerRadius, centerY - centerRadius, centerRadius * 2, centerRadius * 2);
            }

            // 绘制数值文本（底部区域）
            string valueText = _value.ToString("F1") + (string.IsNullOrEmpty(_unit) ? "" : " " + _unit);
            var valueSize = g.MeasureString(valueText, CurrentTheme.HeaderFont);
            float valueY = bounds.Bottom - valueSize.Height - 4;

            if (!string.IsNullOrEmpty(_title))
            {
                var titleSize = g.MeasureString(_title, CurrentTheme.SmallFont);
                valueY = bounds.Bottom - valueSize.Height - titleSize.Height - 4;

                // 绘制标题（最底部）
                using (var brush = new SolidBrush(CurrentTheme.Colors.TextSecondary))
                using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                {
                    g.DrawString(_title, CurrentTheme.SmallFont, brush, centerX, bounds.Bottom - titleSize.Height - 2, sf);
                }
            }

            using (var brush = new SolidBrush(ForeColor))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
            {
                g.DrawString(valueText, CurrentTheme.HeaderFont, brush, centerX, valueY, sf);
            }
        }

        private void DrawArcBackground(Graphics g, int cx, int cy, int radius)
        {
            int thickness = 12;
            var rect = new Rectangle(cx - radius, cy - radius, radius * 2, radius * 2);

            if (!_inverseThreshold)
            {
                // 正常模式：数值越大越危险
                // 正常区域（绿色）
                float normalSweep = (float)(_sweepAngle * (_warningThreshold - _minimum) / (_maximum - _minimum));
                using (var pen = new Pen(Color.FromArgb(60, CurrentTheme.Colors.Success), thickness))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    g.DrawArc(pen, rect, _startAngle, normalSweep);
                }

                // 警告区域（黄色）
                float warnStart = _startAngle + normalSweep;
                float warnSweep = (float)(_sweepAngle * (_dangerThreshold - _warningThreshold) / (_maximum - _minimum));
                using (var pen = new Pen(Color.FromArgb(60, CurrentTheme.Colors.Warning), thickness))
                {
                    g.DrawArc(pen, rect, warnStart, warnSweep);
                }

                // 危险区域（红色）
                float dangerStart = warnStart + warnSweep;
                float dangerSweep = (float)(_sweepAngle * (_maximum - _dangerThreshold) / (_maximum - _minimum));
                using (var pen = new Pen(Color.FromArgb(60, CurrentTheme.Colors.Danger), thickness))
                {
                    pen.EndCap = LineCap.Round;
                    g.DrawArc(pen, rect, dangerStart, dangerSweep);
                }
            }
            else
            {
                // 反向模式：数值越小越危险
                // 危险区域（红色）- 在开始位置
                float dangerSweep = (float)(_sweepAngle * (_dangerThreshold - _minimum) / (_maximum - _minimum));
                using (var pen = new Pen(Color.FromArgb(60, CurrentTheme.Colors.Danger), thickness))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    g.DrawArc(pen, rect, _startAngle, dangerSweep);
                }

                // 警告区域（黄色）- 在中间
                float warnStart = _startAngle + dangerSweep;
                float warnSweep = (float)(_sweepAngle * (_warningThreshold - _dangerThreshold) / (_maximum - _minimum));
                using (var pen = new Pen(Color.FromArgb(60, CurrentTheme.Colors.Warning), thickness))
                {
                    g.DrawArc(pen, rect, warnStart, warnSweep);
                }

                // 正常区域（绿色）- 在末尾
                float normalStart = warnStart + warnSweep;
                float normalSweep = (float)(_sweepAngle * (_maximum - _warningThreshold) / (_maximum - _minimum));
                using (var pen = new Pen(Color.FromArgb(60, CurrentTheme.Colors.Success), thickness))
                {
                    pen.EndCap = LineCap.Round;
                    g.DrawArc(pen, rect, normalStart, normalSweep);
                }
            }
        }

        private void DrawTicks(Graphics g, int cx, int cy, int radius)
        {
            int majorTickCount = 10;
            using (var pen = new Pen(CurrentTheme.Colors.TextSecondary, 1.5f))
            using (var brush = new SolidBrush(CurrentTheme.Colors.TextSecondary))
            {
                for (int i = 0; i <= majorTickCount; i++)
                {
                    float angle = _startAngle + (float)_sweepAngle * i / majorTickCount;
                    double rad = angle * Math.PI / 180;
                    int outerR = radius - 2;
                    int innerR = radius - 10;

                    float x1 = cx + (float)(outerR * Math.Cos(rad));
                    float y1 = cy + (float)(outerR * Math.Sin(rad));
                    float x2 = cx + (float)(innerR * Math.Cos(rad));
                    float y2 = cy + (float)(innerR * Math.Sin(rad));

                    g.DrawLine(pen, x1, y1, x2, y2);

                    // 刻度值
                    double val = _minimum + (_maximum - _minimum) * i / majorTickCount;
                    float labelR = radius - 22;
                    float lx = cx + (float)(labelR * Math.Cos(rad));
                    float ly = cy + (float)(labelR * Math.Sin(rad));
                    using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        g.DrawString(val.ToString("F0"), CurrentTheme.SmallFont, brush, lx, ly, sf);
                    }
                }
            }
        }

        private void DrawNeedle(Graphics g, int cx, int cy, int length)
        {
            double rad = _currentAngle * Math.PI / 180;
            float x = cx + (float)(length * Math.Cos(rad));
            float y = cy + (float)(length * Math.Sin(rad));

            Color needleColor;
            if (!_inverseThreshold)
            {
                // 正常模式：数值越大越危险
                if (_value < _warningThreshold) needleColor = ForeColor;
                else if (_value < _dangerThreshold) needleColor = CurrentTheme.Colors.Warning;
                else needleColor = CurrentTheme.Colors.Danger;
            }
            else
            {
                // 反向模式：数值越小越危险
                if (_value > _warningThreshold) needleColor = ForeColor;
                else if (_value > _dangerThreshold) needleColor = CurrentTheme.Colors.Warning;
                else needleColor = CurrentTheme.Colors.Danger;
            }

            using (var pen = new Pen(needleColor, 2.5f))
            {
                pen.EndCap = LineCap.ArrowAnchor;
                g.DrawLine(pen, cx, cy, x, y);
            }
        }

        protected override void DisposeManagedResources()
        {
            _animationTimer?.Stop();
            _animationTimer?.Dispose();
            base.DisposeManagedResources();
        }
    }
}
