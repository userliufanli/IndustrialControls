using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Theme;
using IndustrialControls.Utilities;

namespace IndustrialControls.Controls.StatusIndicator
{
    /// <summary>
    /// 状态指示器控件，支持多种颜色状态、闪烁效果和状态历史记录
    /// </summary>
    [ToolboxItem(true)]
    [DefaultProperty("State")]
    public class StatusIndicator : DoubleBufferedControl
    {
        private IndicatorState _state = IndicatorState.Stopped;
        private IndicatorShape _shape = IndicatorShape.Circle;
        private bool _enableBlink;
        private int _blinkInterval = 500;
        private Color _customColor = Color.Gray;
        private string _label = "";
        private bool _showLabel = true;
        private int _maxHistoryCount = 100;
        private readonly List<StatusHistoryRecord> _history = new List<StatusHistoryRecord>();
        private AnimationHelper _animation;

        /// <summary>
        /// 状态变更事件
        /// </summary>
        public event EventHandler<IndicatorState> StateChanged;

        /// <summary>
        /// 当前状态
        /// </summary>
        [Category("状态")]
        [Description("指示器当前状态")]
        [DefaultValue(IndicatorState.Stopped)]
        public IndicatorState State
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    var oldState = _state;
                    _state = value;
                    RecordStateChange(oldState, value);
                    UpdateBlinkState();
                    SafeInvalidate();
                    StateChanged?.Invoke(this, value);
                }
            }
        }

        /// <summary>
        /// 外观形状
        /// </summary>
        [Category("外观")]
        [Description("指示器形状")]
        [DefaultValue(IndicatorShape.Circle)]
        public IndicatorShape Shape
        {
            get => _shape;
            set { _shape = value; Invalidate(); }
        }

        /// <summary>
        /// 是否启用闪烁
        /// </summary>
        [Category("行为")]
        [Description("是否启用闪烁效果")]
        [DefaultValue(false)]
        public bool EnableBlink
        {
            get => _enableBlink;
            set
            {
                _enableBlink = value;
                UpdateBlinkState();
            }
        }

        /// <summary>
        /// 闪烁间隔（毫秒）
        /// </summary>
        [Category("行为")]
        [Description("闪烁间隔（毫秒）")]
        [DefaultValue(500)]
        public int BlinkInterval
        {
            get => _blinkInterval;
            set
            {
                _blinkInterval = Math.Max(50, value);
                if (_animation != null)
                    _animation.BlinkInterval = _blinkInterval;
            }
        }

        /// <summary>
        /// 自定义颜色（State为Custom时使用）
        /// </summary>
        [Category("外观")]
        [Description("自定义状态颜色")]
        public Color CustomColor
        {
            get => _customColor;
            set { _customColor = value; Invalidate(); }
        }

        /// <summary>
        /// 标签文本
        /// </summary>
        [Category("外观")]
        [Description("指示器标签")]
        [DefaultValue("")]
        public string Label
        {
            get => _label;
            set { _label = value ?? ""; Invalidate(); }
        }

        /// <summary>
        /// 是否显示标签
        /// </summary>
        [Category("外观")]
        [Description("是否显示标签")]
        [DefaultValue(true)]
        public bool ShowLabel
        {
            get => _showLabel;
            set { _showLabel = value; Invalidate(); }
        }

        /// <summary>
        /// 最大历史记录数
        /// </summary>
        [Category("行为")]
        [Description("最大状态历史记录条数")]
        [DefaultValue(100)]
        public int MaxHistoryCount
        {
            get => _maxHistoryCount;
            set => _maxHistoryCount = Math.Max(1, value);
        }

        /// <summary>
        /// 状态历史记录
        /// </summary>
        [Browsable(false)]
        public IReadOnlyList<StatusHistoryRecord> History => _history.AsReadOnly();

        public StatusIndicator()
        {
            Size = new Size(60, 60);

            // 设计模式下不创建动画助手，避免Timer在设计器中运行
            if (!IsDesignMode)
            {
                _animation = new AnimationHelper();
                _animation.BlinkStateChanged += (s, state) => SafeInvalidate();
            }
        }

        protected override void RenderControl(Graphics g, Rectangle bounds)
        {
            var color = GetStateColor();

            // 闪烁效果：闪烁时降低透明度
            if (_enableBlink && _animation != null && !_animation.BlinkState)
            {
                color = Color.FromArgb(60, color);
            }

            var indicatorBounds = bounds;
            if (_showLabel && !string.IsNullOrEmpty(_label))
            {
                // 预留标签空间
                int labelHeight = (int)(Font.Height * 1.2f);
                indicatorBounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height - labelHeight);

                // 绘制标签
                var labelBounds = new Rectangle(bounds.X, bounds.Bottom - labelHeight, bounds.Width, labelHeight);
                GraphicsHelper.DrawCenteredText(g, _label, CurrentTheme.SmallFont, ForeColor, labelBounds);
            }

            // 计算指示器区域（保持正方形）
            int size = Math.Min(indicatorBounds.Width, indicatorBounds.Height) - 4;
            int x = indicatorBounds.X + (indicatorBounds.Width - size) / 2;
            int y = indicatorBounds.Y + (indicatorBounds.Height - size) / 2;
            var rect = new Rectangle(x, y, size, size);

            using (var brush = new SolidBrush(color))
            {
                switch (_shape)
                {
                    case IndicatorShape.Circle:
                        g.FillEllipse(brush, rect);
                        using (var pen = new Pen(GraphicsHelper.AdjustBrightness(color, 0.7f), 1.5f))
                            g.DrawEllipse(pen, rect);
                        break;

                    case IndicatorShape.Square:
                        g.FillRectangle(brush, rect);
                        using (var pen = new Pen(GraphicsHelper.AdjustBrightness(color, 0.7f), 1.5f))
                            g.DrawRectangle(pen, rect);
                        break;

                    case IndicatorShape.RoundedRectangle:
                        GraphicsHelper.FillRoundedRectangle(g, brush, rect, CornerRadius);
                        using (var pen = new Pen(GraphicsHelper.AdjustBrightness(color, 0.7f), 1.5f))
                            GraphicsHelper.DrawRoundedRectangle(g, pen, rect, CornerRadius);
                        break;
                }
            }

            // 绘制高光效果
            DrawHighlight(g, rect);
        }

        private void DrawHighlight(Graphics g, Rectangle rect)
        {
            int highlightSize = rect.Width / 3;
            var highlightRect = new Rectangle(
                rect.X + rect.Width / 4,
                rect.Y + rect.Height / 6,
                highlightSize, highlightSize);

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(highlightRect);
                using (var brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = Color.FromArgb(80, 255, 255, 255);
                    brush.SurroundColors = new[] { Color.FromArgb(0, 255, 255, 255) };
                    g.FillPath(brush, path);
                }
            }
        }

        private Color GetStateColor()
        {
            var colors = CurrentTheme.Colors;
            switch (_state)
            {
                case IndicatorState.Running: return colors.Running;
                case IndicatorState.Stopped: return colors.Stopped;
                case IndicatorState.Fault: return colors.Fault;
                case IndicatorState.Warning: return colors.Warning;
                case IndicatorState.Idle: return colors.Idle;
                case IndicatorState.Custom: return _customColor;
                default: return colors.Stopped;
            }
        }

        private void UpdateBlinkState()
        {
            // 设计模式下或动画未初始化时跳过
            if (_animation == null) return;

            if (_enableBlink && (_state == IndicatorState.Fault || _state == IndicatorState.Warning))
            {
                _animation.StartBlink(_blinkInterval);
            }
            else if (_enableBlink && _state == IndicatorState.Running)
            {
                _animation.StartBlink(_blinkInterval);
            }
            else
            {
                _animation.StopBlink();
            }
        }

        private void RecordStateChange(IndicatorState oldState, IndicatorState newState)
        {
            // 更新上一条记录的持续时间
            if (_history.Count > 0)
            {
                var last = _history[_history.Count - 1];
                last.Duration = DateTime.Now - last.Timestamp;
            }

            // 添加新记录
            _history.Add(new StatusHistoryRecord(newState, DateTime.Now));

            // 限制历史数量
            while (_history.Count > _maxHistoryCount)
            {
                _history.RemoveAt(0);
            }
        }

        /// <summary>
        /// 清除历史记录
        /// </summary>
        public void ClearHistory()
        {
            _history.Clear();
        }

        protected override void DisposeManagedResources()
        {
            _animation?.Dispose();
            base.DisposeManagedResources();
        }
    }
}
