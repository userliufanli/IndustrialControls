using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Theme;
using IndustrialControls.Utilities;

namespace IndustrialControls.Controls.Alarm
{
    /// <summary>
    /// 报警显示控件，支持分级报警、自动排序、确认清除功能
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("AlarmAcknowledged")]
    public class AlarmDisplay : DoubleBufferedControl
    {
        private readonly List<AlarmItem> _alarms = new List<AlarmItem>();
        private List<AlarmItem> _filteredAlarms = null; // 筛选后的报警列表
        private static readonly Comparison<AlarmItem> AlarmComparison = (a, b) =>
        {
            // 第一优先级：未确认的排在前面
            if (a.IsAcknowledged != b.IsAcknowledged)
                return a.IsAcknowledged ? 1 : -1;
            // 第二优先级：按时间降序（最新的在前）
            return b.Timestamp.CompareTo(a.Timestamp);
        };
        private int _scrollOffset;
        private int _itemHeight = 32;
        private int _selectedIndex = -1;
        private int _maxAlarms = 1000;
        private bool _autoScroll = true;
        private AnimationHelper _blinkAnimation;
        private readonly object _lockObj = new object();
        private DateTime? _filterStartTime;
        private DateTime? _filterEndTime;

        /// <summary>
        /// 报警确认事件
        /// </summary>
        public event EventHandler<AlarmItem> AlarmAcknowledged;

        /// <summary>
        /// 新报警添加事件
        /// </summary>
        public event EventHandler<AlarmItem> AlarmAdded;

        /// <summary>
        /// 报警清除事件
        /// </summary>
        public event EventHandler AlarmCleared;

        /// <summary>
        /// 每条报警显示高度
        /// </summary>
        [Category("外观")]
        [Description("每条报警的显示高度")]
        [DefaultValue(32)]
        public int ItemHeight
        {
            get => _itemHeight;
            set { _itemHeight = Math.Max(20, value); Invalidate(); }
        }

        /// <summary>
        /// 最大报警条数
        /// </summary>
        [Category("行为")]
        [Description("最大保留报警条数")]
        [DefaultValue(1000)]
        public int MaxAlarms
        {
            get => _maxAlarms;
            set => _maxAlarms = Math.Max(10, value);
        }

        /// <summary>
        /// 是否自动滚动到最新
        /// </summary>
        [Category("行为")]
        [Description("新报警是否自动滚动到可见")]
        [DefaultValue(true)]
        public bool AutoScroll
        {
            get => _autoScroll;
            set => _autoScroll = value;
        }

        /// <summary>
        /// 报警总数
        /// </summary>
        [Browsable(false)]
        public int TotalCount
        {
            get { lock (_lockObj) return _alarms.Count; }
        }

        /// <summary>
        /// 未确认报警数
        /// </summary>
        [Browsable(false)]
        public int UnacknowledgedCount
        {
            get { lock (_lockObj) return _alarms.Count(a => !a.IsAcknowledged); }
        }

        /// <summary>
        /// 紧急报警数
        /// </summary>
        [Browsable(false)]
        public int EmergencyCount
        {
            get { lock (_lockObj) return _alarms.Count(a => a.Level == AlarmLevel.Emergency && !a.IsAcknowledged); }
        }

        /// <summary>
        /// 获取当前显示的报警列表（可能是筛选后的）
        /// </summary>
        private List<AlarmItem> GetDisplayAlarms()
        {
            return _filteredAlarms ?? _alarms;
        }

        /// <summary>
        /// 应用时间筛选
        /// </summary>
        /// <param name="startTime">开始时间（null表示不限制）</param>
        /// <param name="endTime">结束时间（null表示不限制）</param>
        public void ApplyTimeFilter(DateTime? startTime, DateTime? endTime)
        {
            lock (_lockObj)
            {
                _filterStartTime = startTime;
                _filterEndTime = endTime;

                if (!startTime.HasValue && !endTime.HasValue)
                {
                    // 无筛选条件，清除筛选
                    _filteredAlarms = null;
                }
                else
                {
                    // 应用筛选
                    _filteredAlarms = _alarms.Where(a =>
                    {
                        if (startTime.HasValue && a.Timestamp < startTime.Value) return false;
                        if (endTime.HasValue && a.Timestamp > endTime.Value) return false;
                        return true;
                    }).ToList();

                    // 对筛选结果应用相同的排序规则
                    _filteredAlarms.Sort(AlarmComparison);
                }

                _scrollOffset = 0;
                _selectedIndex = -1;
            }
            SafeInvalidate();
        }

        /// <summary>
        /// 清除时间筛选
        /// </summary>
        public void ClearTimeFilter()
        {
            ApplyTimeFilter(null, null);
        }

        public AlarmDisplay()
        {
            Size = new Size(400, 300);

            // 设计模式下不创建闪烁动画，避免Timer在设计器中运行
            if (!IsDesignMode)
            {
                _blinkAnimation = new AnimationHelper();
                _blinkAnimation.BlinkStateChanged += (s, state) => SafeInvalidate();
                _blinkAnimation.StartBlink(600);
            }
        }

        /// <summary>
        /// 添加报警
        /// </summary>
        public void AddAlarm(AlarmItem alarm)
        {
            if (alarm == null) return;
            lock (_lockObj)
            {
                _alarms.Insert(0, alarm);
                _alarms.Sort(AlarmComparison);

                // 限制数量
                while (_alarms.Count > _maxAlarms)
                    _alarms.RemoveAt(_alarms.Count - 1);

                // 如果有时间筛选，也需要更新筛选列表
                if (_filteredAlarms != null)
                {
                    // 检查新报警是否符合筛选条件
                    bool matchesFilter = true;
                    if (_filterStartTime.HasValue && alarm.Timestamp < _filterStartTime.Value) matchesFilter = false;
                    if (_filterEndTime.HasValue && alarm.Timestamp > _filterEndTime.Value) matchesFilter = false;

                    if (matchesFilter)
                    {
                        _filteredAlarms.Insert(0, alarm);
                        _filteredAlarms.Sort(AlarmComparison);
                    }
                }
            }

            if (_autoScroll) _scrollOffset = 0;
            SafeInvalidate();
            AlarmAdded?.Invoke(this, alarm);
        }

        /// <summary>
        /// 添加报警（简便方法）
        /// </summary>
        public void AddAlarm(AlarmLevel level, string message, string source = "")
        {
            AddAlarm(new AlarmItem(level, message, source));
        }

        /// <summary>
        /// 确认选中的报警
        /// </summary>
        public void AcknowledgeSelected(string user = "")
        {
            var displayAlarms = GetDisplayAlarms();
            lock (_lockObj)
            {
                if (_selectedIndex >= 0 && _selectedIndex < displayAlarms.Count)
                {
                    var alarm = displayAlarms[_selectedIndex];
                    alarm.Acknowledge(user);
                    
                    // 确认后重新排序，使已确认的报警移到下方
                    _alarms.Sort(AlarmComparison);
                    
                    // 如果有筛选，也更新筛选列表
                    if (_filteredAlarms != null)
                    {
                        _filteredAlarms.Sort(AlarmComparison);
                    }
                    
                    AlarmAcknowledged?.Invoke(this, alarm);
                    SafeInvalidate();
                }
            }
        }

        /// <summary>
        /// 确认所有报警
        /// </summary>
        public void AcknowledgeAll(string user = "")
        {
            lock (_lockObj)
            {
                foreach (var alarm in _alarms.Where(a => !a.IsAcknowledged))
                {
                    alarm.Acknowledge(user);
                }
                
                // 确认后重新排序
                _alarms.Sort(AlarmComparison);
                
                // 如果有筛选，也更新筛选列表
                if (_filteredAlarms != null)
                {
                    foreach (var alarm in _filteredAlarms.Where(a => !a.IsAcknowledged))
                    {
                        alarm.Acknowledge(user);
                    }
                    
                    _filteredAlarms.Sort(AlarmComparison);
                }
            }
            SafeInvalidate();
        }

        /// <summary>
        /// 清除已确认的报警
        /// </summary>
        public void ClearAcknowledged()
        {
            lock (_lockObj)
            {
                _alarms.RemoveAll(a => a.IsAcknowledged);
                
                // 如果有筛选，也更新筛选列表
                if (_filteredAlarms != null)
                {
                    _filteredAlarms.RemoveAll(a => a.IsAcknowledged);
                }
            }
            _selectedIndex = -1;
            SafeInvalidate();
            AlarmCleared?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 清除所有报警
        /// </summary>
        public void ClearAll()
        {
            lock (_lockObj)
            {
                _alarms.Clear();
                _filteredAlarms?.Clear();
            }
            _selectedIndex = -1;
            _scrollOffset = 0;
            SafeInvalidate();
            AlarmCleared?.Invoke(this, EventArgs.Empty);
        }

        protected override void RenderControl(Graphics g, Rectangle bounds)
        {
            // 绘制背景
            using (var brush = new SolidBrush(BackColor))
            {
                GraphicsHelper.FillRoundedRectangle(g, brush, bounds, CornerRadius);
            }

            // 绘制标题栏
            int headerHeight = 28;
            var headerRect = new Rectangle(bounds.X, bounds.Y, bounds.Width, headerHeight);
            using (var brush = new SolidBrush(CurrentTheme.Colors.SurfaceVariant))
            {
                g.FillRectangle(brush, headerRect);
            }

            var displayAlarms = GetDisplayAlarms();
            string headerText = $"报警信息 (未确认: {UnacknowledgedCount} / 总计: {TotalCount}";
            if (_filteredAlarms != null)
                headerText += $" / 筛选: {displayAlarms.Count}";
            headerText += ")";
            
            using (var brush = new SolidBrush(ForeColor))
            using (var sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
            {
                g.DrawString(headerText, CurrentTheme.DefaultFont, brush,
                    new RectangleF(headerRect.X + 8, headerRect.Y, headerRect.Width, headerRect.Height), sf);
            }

            // 绘制报警列表
            int listTop = bounds.Y + headerHeight;
            int visibleCount = (bounds.Height - headerHeight) / _itemHeight;

            lock (_lockObj)
            {
                for (int i = _scrollOffset; i < Math.Min(displayAlarms.Count, _scrollOffset + visibleCount + 1); i++)
                {
                    int y = listTop + (i - _scrollOffset) * _itemHeight;
                    if (y + _itemHeight > bounds.Bottom) break;

                    var itemRect = new Rectangle(bounds.X, y, bounds.Width, _itemHeight);
                    RenderAlarmItem(g, displayAlarms[i], itemRect, i == _selectedIndex);
                }
            }

            // 绘制边框
            using (var pen = new Pen(CurrentTheme.Colors.Border))
            {
                GraphicsHelper.DrawRoundedRectangle(g, pen, bounds, CornerRadius);
            }
        }

        private void RenderAlarmItem(Graphics g, AlarmItem alarm, Rectangle bounds, bool isSelected)
        {
            // 背景色
            Color bgColor = isSelected ? CurrentTheme.Colors.SurfaceVariant : Color.Transparent;
            if (alarm.IsNew && _blinkAnimation != null && _blinkAnimation.BlinkState)
            {
                bgColor = Color.FromArgb(30, GetAlarmColor(alarm.Level));
            }

            if (bgColor != Color.Transparent)
            {
                using (var brush = new SolidBrush(bgColor))
                    g.FillRectangle(brush, bounds);
            }

            // 等级指示条
            var indicatorRect = new Rectangle(bounds.X + 2, bounds.Y + 4, 4, bounds.Height - 8);
            using (var brush = new SolidBrush(GetAlarmColor(alarm.Level)))
                g.FillRectangle(brush, indicatorRect);

            // 时间
            string timeStr = alarm.Timestamp.ToString("HH:mm:ss");
            using (var brush = new SolidBrush(CurrentTheme.Colors.TextSecondary))
                g.DrawString(timeStr, CurrentTheme.SmallFont, brush, bounds.X + 12, bounds.Y + (bounds.Height - CurrentTheme.SmallFont.Height) / 2);

            // 消息
            int msgX = bounds.X + 80;
            Color textColor = alarm.IsAcknowledged ? CurrentTheme.Colors.TextDisabled : ForeColor;
            using (var brush = new SolidBrush(textColor))
            using (var sf = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
            {
                var msgRect = new RectangleF(msgX, bounds.Y, bounds.Width - msgX - 60, bounds.Height);
                g.DrawString(alarm.Message, CurrentTheme.DefaultFont, brush, msgRect, sf);
            }

            // 确认状态
            if (alarm.IsAcknowledged)
            {
                using (var brush = new SolidBrush(CurrentTheme.Colors.Success))
                    g.DrawString("✓", CurrentTheme.DefaultFont, brush, bounds.Right - 30, bounds.Y + (bounds.Height - Font.Height) / 2);
            }

            // 底部分割线
            using (var pen = new Pen(CurrentTheme.Colors.BorderLight))
                g.DrawLine(pen, bounds.X + 8, bounds.Bottom - 1, bounds.Right - 8, bounds.Bottom - 1);
        }

        private Color GetAlarmColor(AlarmLevel level)
        {
            switch (level)
            {
                case AlarmLevel.Emergency: return CurrentTheme.Colors.AlarmEmergency;
                case AlarmLevel.Important: return CurrentTheme.Colors.AlarmImportant;
                case AlarmLevel.General: return CurrentTheme.Colors.AlarmGeneral;
                case AlarmLevel.Info: return CurrentTheme.Colors.AlarmInfo;
                default: return CurrentTheme.Colors.TextSecondary;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int headerHeight = 28;
            var displayAlarms = GetDisplayAlarms();
            int clickedIndex = _scrollOffset + (e.Y - headerHeight) / _itemHeight;
            lock (_lockObj)
            {
                if (clickedIndex >= 0 && clickedIndex < displayAlarms.Count)
                {
                    _selectedIndex = clickedIndex;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            AcknowledgeSelected();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int delta = e.Delta > 0 ? -1 : 1;
            var displayAlarms = GetDisplayAlarms();
            lock (_lockObj)
            {
                int maxScroll = Math.Max(0, displayAlarms.Count - (Height - 28) / _itemHeight);
                _scrollOffset = Math.Max(0, Math.Min(maxScroll, _scrollOffset + delta));
            }
            Invalidate();
        }

        protected override void DisposeManagedResources()
        {
            _blinkAnimation?.Dispose();
            base.DisposeManagedResources();
        }
    }
}
