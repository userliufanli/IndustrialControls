using System;
using System.Collections.Generic;
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
    /// 数据通道配置
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ChannelConfig
    {
        private string _name = "通道";
        private Color _color = Color.Blue;

        /// <summary>
        /// 通道名称
        /// </summary>
        [Category("数据")]
        [Description("通道名称")]
        [DisplayName("名称")]
        [DefaultValue("通道")]
        public string Name
        {
            get => _name;
            set => _name = value ?? "";
        }

        /// <summary>
        /// 通道颜色
        /// </summary>
        [Category("外观")]
        [Description("通道曲线颜色")]
        [DisplayName("颜色")]
        [DefaultValue(typeof(Color), "Blue")]
        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        public ChannelConfig()
        {
        }

        public ChannelConfig(string name, Color color)
        {
            _name = name;
            _color = color;
        }

        public override string ToString()
        {
            return $"{Name} ({Color.R}, {Color.G}, {Color.B})";
        }
    }

    /// <summary>
    /// 图例位置枚举
    /// </summary>
    public enum LegendPosition
    {
        /// <summary>
        /// 左上角
        /// </summary>
        TopLeft,
        /// <summary>
        /// 右上角
        /// </summary>
        TopRight,
        /// <summary>
        /// 左下角
        /// </summary>
        BottomLeft,
        /// <summary>
        /// 右下角
        /// </summary>
        BottomRight
    }

    /// <summary>
    /// 实时趋势图控件
    /// </summary>
    [ToolboxItem(true)]
    [DefaultProperty("Title")]
    [Description("实时数据趋势显示控件，支持多通道数据曲线显示")]
    public class TrendChart : DoubleBufferedControl
    {
        private readonly Dictionary<string, DataBuffer> _channels = new Dictionary<string, DataBuffer>();
        private readonly Dictionary<string, Color> _channelColors = new Dictionary<string, Color>();
        private List<ChannelConfig> _channelConfigs = new List<ChannelConfig>();
        private string _title = "趋势图";
        private bool _showGrid = true;
        private bool _showXAxis = true;
        private bool _showYAxis = true;
        private bool _showLegend = true;
        private bool _autoScaleY = true;
        private double _yMin = 0;
        private double _yMax = 100;
        private int _visiblePoints = 200;
        private int _bufferCapacity = 2000;
        private int _xTickCount = 5;
        private int _yTickCount = 4;
        private int _margin = 50;
        private int _lineThickness = 2;
        private bool _showLegendBackground = true;
        private LegendPosition _legendPosition = LegendPosition.TopRight;
        private int _gridLineCount = 5; // 网格线数量
        private double _timeInterval = 0.1; // 时间间隔（秒）

        private static readonly Color[] DefaultColors = {
            Color.FromArgb(59, 130, 246),
            Color.FromArgb(16, 185, 129),
            Color.FromArgb(239, 68, 68),
            Color.FromArgb(245, 158, 11),
            Color.FromArgb(139, 92, 246),
            Color.FromArgb(236, 72, 153)
        };

        /// <summary>
        /// 图表标题
        /// </summary>
        [Category("外观")]
        [Description("趋势图标题")]
        [DefaultValue("趋势图")]
        public string Title
        {
            get => _title;
            set { _title = value ?? ""; Invalidate(); }
        }

        /// <summary>
        /// 是否显示网格
        /// </summary>
        [Category("外观")]
        [Description("是否显示图表背景网格线")]
        [DefaultValue(true)]
        public bool ShowGrid
        {
            get => _showGrid;
            set { _showGrid = value; Invalidate(); }
        }

        /// <summary>
        /// 是否显示X轴
        /// </summary>
        [Category("外观")]
        [Description("是否显示X轴刻度和标签")]
        [DefaultValue(true)]
        public bool ShowXAxis
        {
            get => _showXAxis;
            set { _showXAxis = value; Invalidate(); }
        }

        /// <summary>
        /// 是否显示Y轴
        /// </summary>
        [Category("外观")]
        [Description("是否显示Y轴刻度和标签")]
        [DefaultValue(true)]
        public bool ShowYAxis
        {
            get => _showYAxis;
            set { _showYAxis = value; Invalidate(); }
        }

        /// <summary>
        /// 是否显示图例
        /// </summary>
        [Category("外观")]
        [Description("是否显示通道图例")]
        [DefaultValue(true)]
        public bool ShowLegend
        {
            get => _showLegend;
            set { _showLegend = value; Invalidate(); }
        }

        /// <summary>
        /// 通道配置列表
        /// </summary>
        [Category("数据")]
        [Description("数据通道配置列表")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        [Editor("System.ComponentModel.Design.CollectionEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        public List<ChannelConfig> Channels
        {
            get => _channelConfigs;
        }

        /// <summary>
        /// 图例位置
        /// </summary>
        [Category("外观")]
        [Description("图例显示位置")]
        [DefaultValue(LegendPosition.TopRight)]
        public LegendPosition LegendPosition
        {
            get => _legendPosition;
            set { _legendPosition = value; Invalidate(); }
        }

        /// <summary>
        /// 是否显示图例背景
        /// </summary>
        [Category("外观")]
        [Description("是否为图例显示半透明背景")]
        [DefaultValue(true)]
        public bool ShowLegendBackground
        {
            get => _showLegendBackground;
            set { _showLegendBackground = value; Invalidate(); }
        }

        /// <summary>
        /// 自动缩放Y轴
        /// </summary>
        [Category("行为")]
        [Description("是否根据数据自动调整Y轴显示范围")]
        [DefaultValue(true)]
        public bool AutoScaleY
        {
            get => _autoScaleY;
            set => _autoScaleY = value;
        }

        /// <summary>
        /// Y轴最小值
        /// </summary>
        [Category("数据")]
        [Description("Y轴显示范围的最小值，AutoScaleY关闭时生效")]
        [DefaultValue(0.0)]
        public double YMin
        {
            get => _yMin;
            set { _yMin = value; Invalidate(); }
        }

        /// <summary>
        /// Y轴最大值
        /// </summary>
        [Category("数据")]
        [Description("Y轴显示范围的最大值，AutoScaleY关闭时生效")]
        [DefaultValue(100.0)]
        public double YMax
        {
            get => _yMax;
            set { _yMax = value; Invalidate(); }
        }

        /// <summary>
        /// 可见数据点数
        /// </summary>
        [Category("数据")]
        [Description("图表上同时可见的数据点数量")]
        [DefaultValue(200)]
        public int VisiblePoints
        {
            get => _visiblePoints;
            set { _visiblePoints = Math.Max(10, value); Invalidate(); }
        }

        /// <summary>
        /// 缓冲区容量
        /// </summary>
        [Category("数据")]
        [Description("每个通道可存储的最大数据点数")]
        [DefaultValue(2000)]
        public int BufferCapacity
        {
            get => _bufferCapacity;
            set
            {
                if (value != _bufferCapacity)
                {
                    _bufferCapacity = Math.Max(100, value);
                    // 重建所有缓冲区
                    foreach (var key in _channels.Keys)
                    {
                        var oldBuffer = _channels[key];
                        _channels[key] = new DataBuffer(_bufferCapacity);
                    }
                }
            }
        }

        /// <summary>
        /// X轴刻度数量
        /// </summary>
        [Category("坐标轴")]
        [Description("X轴显示的刻度数量")]
        [DefaultValue(5)]
        public int XTickCount
        {
            get => _xTickCount;
            set { _xTickCount = Math.Max(2, Math.Min(20, value)); Invalidate(); }
        }

        /// <summary>
        /// Y轴刻度数量
        /// </summary>
        [Category("坐标轴")]
        [Description("Y轴显示的刻度数量")]
        [DefaultValue(4)]
        public int YTickCount
        {
            get => _yTickCount;
            set { _yTickCount = Math.Max(2, Math.Min(20, value)); Invalidate(); }
        }

        /// <summary>
        /// 左边距
        /// </summary>
        [Category("布局")]
        [Description("图表左边距（用于Y轴标签空间）")]
        [DefaultValue(50)]
        public int LeftMargin
        {
            get => _margin;
            set { _margin = Math.Max(30, Math.Min(100, value)); Invalidate(); }
        }

        /// <summary>
        /// 线条粗细
        /// </summary>
        [Category("外观")]
        [Description("数据曲线的线条粗细（像素）")]
        [DefaultValue(2)]
        public int LineThickness
        {
            get => _lineThickness;
            set { _lineThickness = Math.Max(1, Math.Min(10, value)); Invalidate(); }
        }

        /// <summary>
        /// 网格线数量
        /// </summary>
        [Category("外观")]
        [Description("图表背景网格线数量")]
        [DefaultValue(5)]
        public int GridLineCount
        {
            get => _gridLineCount;
            set { _gridLineCount = Math.Max(2, Math.Min(20, value)); Invalidate(); }
        }

        /// <summary>
        /// 数据点时间间隔（秒）
        /// </summary>
        [Category("数据")]
        [Description("相邻数据点之间的时间间隔（秒），用于X轴时间显示")]
        [DefaultValue(0.1)]
        public double TimeInterval
        {
            get => _timeInterval;
            set { _timeInterval = Math.Max(0.001, value); Invalidate(); }
        }

        public TrendChart()
        {
            Size = new Size(500, 300);
            CornerRadius = 6;
            
            // 在设计器模式下，如果通道配置列表不为空，应用配置
            if (!ControlDesignerHelper.IsDesignMode(this))
            {
                // 运行时应用通道配置
                if (_channelConfigs.Count > 0)
                {
                    ApplyChannelConfigs();
                }
            }
        }

        /// <summary>
        /// 添加数据通道
        /// </summary>
        public void AddChannel(string name, Color? color = null)
        {
            if (!_channels.ContainsKey(name))
            {
                _channels[name] = new DataBuffer(_bufferCapacity);
                _channelColors[name] = color ?? DefaultColors[_channels.Count % DefaultColors.Length];
                
                // 同步到配置列表
                if (!_channelConfigs.Exists(c => c.Name == name))
                {
                    _channelConfigs.Add(new ChannelConfig(name, _channelColors[name]));
                }
            }
        }

        /// <summary>
        /// 移除数据通道
        /// </summary>
        public void RemoveChannel(string name)
        {
            _channels.Remove(name);
            _channelColors.Remove(name);
            _channelConfigs.RemoveAll(c => c.Name == name);
        }

        /// <summary>
        /// 应用通道配置
        /// </summary>
        private void ApplyChannelConfigs()
        {
            // 清空现有通道
            _channels.Clear();
            _channelColors.Clear();
            
            // 根据配置重新创建通道
            foreach (var config in _channelConfigs)
            {
                if (!string.IsNullOrEmpty(config.Name))
                {
                    _channels[config.Name] = new DataBuffer(_bufferCapacity);
                    _channelColors[config.Name] = config.Color;
                }
            }
        }

        /// <summary>
        /// 当控件句柄创建后应用通道配置
        /// </summary>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            
            // 确保在运行时应用通道配置
            if (!ControlDesignerHelper.IsDesignMode(this) && _channelConfigs.Count > 0)
            {
                ApplyChannelConfigs();
            }
        }

        /// <summary>
        /// 添加数据点
        /// </summary>
        public void AddDataPoint(string channel, double value)
        {
            if (_channels.ContainsKey(channel))
            {
                _channels[channel].Add(value);
                ThrottledInvalidate();
            }
        }

        /// <summary>
        /// 清空所有通道数据
        /// </summary>
        public void ClearData()
        {
            foreach (var buffer in _channels.Values)
                buffer.Clear();
            Invalidate();
        }

        protected override void RenderControl(Graphics g, Rectangle bounds)
        {
            // 绘制背景
            using (var brush = new SolidBrush(BackColor))
            {
                GraphicsHelper.FillRoundedRectangle(g, brush, bounds, CornerRadius);
            }

            // 绘制标题
            if (!string.IsNullOrEmpty(_title))
            {
                using (var brush = new SolidBrush(ForeColor))
                    g.DrawString(_title, CurrentTheme.HeaderFont, brush, _margin, 8);
            }

            // 图表区域
            var chartRect = new Rectangle(
                bounds.X + _margin,
                bounds.Y + 30,
                bounds.Width - _margin - 20,
                bounds.Height - 50);

            if (chartRect.Width <= 0 || chartRect.Height <= 0) return;

            // 计算Y轴范围
            double yMin = _yMin, yMax = _yMax;
            if (_autoScaleY)
            {
                yMin = double.MaxValue;
                yMax = double.MinValue;
                foreach (var buffer in _channels.Values)
                {
                    if (buffer.Count == 0) continue;
                    double bMin = buffer.Min;
                    double bMax = buffer.Max;
                    if (bMin < yMin) yMin = bMin;
                    if (bMax > yMax) yMax = bMax;
                }
                if (Math.Abs(yMin - yMax) < 0.001) { yMin -= 1; yMax += 1; }
                double padding = (yMax - yMin) * 0.1;
                yMin -= padding;
                yMax += padding;
            }

            // 绘制网格
            if (_showGrid)
            {
                DrawGrid(g, chartRect, yMin, yMax);
            }

            // 绘制Y轴刻度
            if (_showYAxis)
            {
                DrawYAxis(g, chartRect, yMin, yMax);
            }

            // 绘制X轴刻度
            if (_showXAxis)
            {
                DrawXAxis(g, chartRect);
            }

            // 绘制数据线
            foreach (var kvp in _channels)
            {
                DrawChannel(g, chartRect, kvp.Value, _channelColors[kvp.Key], yMin, yMax);
            }

            // 绘制边框
            using (var pen = new Pen(CurrentTheme.Colors.Border))
            {
                g.DrawRectangle(pen, chartRect);
            }

            // 绘制图例
            if (_showLegend)
            {
                DrawLegend(g, bounds);
            }
        }

        private void DrawGrid(Graphics g, Rectangle chartRect, double yMin, double yMax)
        {
            using (var pen = new Pen(Color.FromArgb(40, CurrentTheme.Colors.TextSecondary)))
            {
                pen.DashStyle = DashStyle.Dot;
                // 水平网格线
                for (int i = 1; i < _gridLineCount; i++)
                {
                    int y = chartRect.Y + (int)(chartRect.Height * i / (double)_gridLineCount);
                    g.DrawLine(pen, chartRect.Left, y, chartRect.Right, y);
                }
                // 垂直网格线
                for (int i = 1; i < _gridLineCount; i++)
                {
                    int x = chartRect.X + (int)(chartRect.Width * i / (double)_gridLineCount);
                    g.DrawLine(pen, x, chartRect.Top, x, chartRect.Bottom);
                }
            }
        }

        private void DrawYAxis(Graphics g, Rectangle chartRect, double yMin, double yMax)
        {
            using (var brush = new SolidBrush(CurrentTheme.Colors.TextSecondary))
            {
                for (int i = 0; i <= _yTickCount; i++)
                {
                    double val = yMax - (yMax - yMin) * i / (double)_yTickCount;
                    int y = chartRect.Y + (int)(chartRect.Height * i / (double)_yTickCount);
                    string label = val.ToString("F1");
                    var size = g.MeasureString(label, CurrentTheme.SmallFont);
                    g.DrawString(label, CurrentTheme.SmallFont, brush, chartRect.X - size.Width - 4, y - size.Height / 2);
                }
            }
        }

        private void DrawXAxis(Graphics g, Rectangle chartRect)
        {
            using (var brush = new SolidBrush(CurrentTheme.Colors.TextSecondary))
            {
                for (int i = 0; i <= _xTickCount; i++)
                {
                    int x = chartRect.X + (int)(chartRect.Width * i / (double)_xTickCount);
                    int pointIndex = i * (_visiblePoints / _xTickCount);

                    // 计算该刻度对应的绝对时间（使用可配置的时间间隔）
                    double secondsAgo = (_visiblePoints - pointIndex) * _timeInterval;
                    DateTime time = DateTime.Now.AddSeconds(-secondsAgo);
                    
                    // 根据时间间隔选择合适的时间格式
                    string label = GetTimeLabel(secondsAgo);

                    var size = g.MeasureString(label, CurrentTheme.SmallFont);
                    
                    // 绘制刻度线
                    g.DrawLine(Pens.Gray, x, chartRect.Bottom, x, chartRect.Bottom + 4);
                    
                    // 绘制标签
                    g.DrawString(label, CurrentTheme.SmallFont, brush, x - size.Width / 2, chartRect.Bottom + 6);
                }
            }
        }

        /// <summary>
        /// 根据时间跨度选择合适的时间显示格式
        /// </summary>
        private string GetTimeLabel(double secondsAgo)
        {
            if (secondsAgo < 60)
            {
                return DateTime.Now.AddSeconds(-secondsAgo).ToString("HH:mm:ss");
            }
            else if (secondsAgo < 3600)
            {
                return DateTime.Now.AddSeconds(-secondsAgo).ToString("HH:mm");
            }
            else
            {
                return DateTime.Now.AddSeconds(-secondsAgo).ToString("MM-dd HH:mm");
            }
        }

        private void DrawChannel(Graphics g, Rectangle chartRect, DataBuffer buffer, Color color, double yMin, double yMax)
        {
            var data = buffer.GetLatest(_visiblePoints);
            if (data.Length < 2) return;

            float xStep = (float)chartRect.Width / (_visiblePoints - 1);
            double yRange = yMax - yMin;

            var points = new PointF[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                float x = chartRect.X + (i + (_visiblePoints - data.Length)) * xStep;
                float y = chartRect.Bottom - (float)((data[i] - yMin) / yRange * chartRect.Height);
                y = Math.Max(chartRect.Top, Math.Min(chartRect.Bottom, y));
                points[i] = new PointF(x, y);
            }

            using (var pen = new Pen(color, _lineThickness))
            {
                pen.LineJoin = LineJoin.Round;
                if (points.Length > 1)
                    g.DrawLines(pen, points);
            }
        }

        private void DrawLegend(Graphics g, Rectangle bounds)
        {
            // 计算图例总宽度
            int totalWidth = 0;
            var legendItems = new List<(string name, Color color, int width)>();
            
            foreach (var kvp in _channelColors)
            {
                using (var brush = new SolidBrush(kvp.Value))
                {
                    int textWidth = (int)g.MeasureString(kvp.Key, CurrentTheme.SmallFont).Width;
                    int itemWidth = 12 + 4 + textWidth; // 色块(12) + 间距(4) + 文字
                    legendItems.Add((kvp.Key, kvp.Value, itemWidth));
                    totalWidth += itemWidth + 12; // 项之间间距
                }
            }
            
            // 根据位置计算坐标
            int x = 0, y = 0;
            switch (_legendPosition)
            {
                case LegendPosition.TopLeft:
                    x = bounds.X + _margin + 10;
                    y = 10;
                    break;
                case LegendPosition.TopRight:
                    x = bounds.Right - totalWidth - 10;
                    y = 10;
                    break;
                case LegendPosition.BottomLeft:
                    x = bounds.X + _margin + 10;
                    y = bounds.Bottom - 25;
                    break;
                case LegendPosition.BottomRight:
                    x = bounds.Right - totalWidth - 10;
                    y = bounds.Bottom - 25;
                    break;
            }
            
            // 绘制背景
            if (_showLegendBackground)
            {
                using (var bgBrush = new SolidBrush(Color.FromArgb(200, BackColor)))
                {
                    g.FillRectangle(bgBrush, x - 5, y - 2, totalWidth + 5, 20);
                }
            }
            
            // 绘制图例项
            foreach (var item in legendItems)
            {
                // 绘制色块
                using (var brush = new SolidBrush(item.color))
                {
                    g.FillRectangle(brush, x, y, 12, 12);
                }
                x += 16;
                
                // 绘制文字
                using (var brush = new SolidBrush(CurrentTheme.Colors.TextSecondary))
                {
                    g.DrawString(item.name, CurrentTheme.SmallFont, brush, x, y - 1);
                }
                x += item.width - 12 + 12;
            }
        }
    }
}
