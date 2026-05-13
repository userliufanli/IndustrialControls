using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Theme;
using IndustrialControls.Utilities;

namespace IndustrialControls.Controls.DeviceButton
{
    /// <summary>
    /// 按钮显示模式
    /// </summary>
    public enum DisplayMode
    {
        /// <summary>
        /// 动作模式：按钮显示将要执行的动作（如"启动"、"停止"）
        /// </summary>
        ActionMode,
        
        /// <summary>
        /// 状态模式：按钮显示设备当前状态（如"运行中"、"已停止"）
        /// </summary>
        StatusMode
    }

    /// <summary>
    /// 设备控制按钮，支持多状态切换、长按确认、图标显示
    /// </summary>
    [ToolboxItem(true)]
    [DefaultProperty("ButtonText")]
    [DefaultEvent("StateChanged")]
    public class DeviceControlButton : DoubleBufferedControl
    {
        private List<DeviceButtonState> _states = new List<DeviceButtonState>();
        private int _currentStateIndex;
        private bool _enableLongPress;
        private int _longPressMs = 1000;
        private Timer _longPressTimer;
        private DateTime _pressStartTime;
        private float _longPressProgress;
        private Image _icon;
        private string _buttonText = "控制";
        private DisplayMode _displayMode = DisplayMode.ActionMode;
        private int _defaultStateIndex = 0;

        /// <summary>
        /// 状态切换事件
        /// </summary>
        public event EventHandler<DeviceButtonState> StateChanged;

        /// <summary>
        /// 点击事件（长按模式下长按完成触发）
        /// </summary>
        public event EventHandler ButtonActivated;

        /// <summary>
        /// 按钮显示模式
        /// </summary>
        [Category("行为")]
        [Description("按钮显示模式：动作模式显示将要执行的动作，状态模式显示当前运行状态")]
        [DefaultValue(DisplayMode.ActionMode)]
        public DisplayMode Mode
        {
            get => _displayMode;
            set { _displayMode = value; Invalidate(); }
        }

        /// <summary>
        /// 默认状态索引（调用SetStates时使用）
        /// </summary>
        [Category("行为")]
        [Description("设置状态列表时的默认初始状态索引")]
        [DefaultValue(0)]
        public int DefaultStateIndex
        {
            get => _defaultStateIndex;
            set { _defaultStateIndex = Math.Max(0, value); }
        }

        /// <summary>
        /// 按钮文本
        /// </summary>
        [Category("外观")]
        [Description("按钮显示文本")]
        [DefaultValue("控制")]
        public string ButtonText
        {
            get => _buttonText;
            set { _buttonText = value ?? ""; Invalidate(); }
        }

        /// <summary>
        /// 当前状态索引
        /// </summary>
        [Browsable(false)]
        public int CurrentStateIndex
        {
            get => _currentStateIndex;
            set
            {
                if (_states.Count > 0)
                {
                    _currentStateIndex = value % _states.Count;
                    Invalidate();
                    StateChanged?.Invoke(this, CurrentState);
                }
            }
        }


        /// <summary>
        /// 当前状态
        /// </summary>
        [Browsable(false)]
        public DeviceButtonState CurrentState =>
            _states.Count > 0 ? _states[_currentStateIndex] : null;

        /// <summary>
        /// 是否启用长按确认
        /// </summary>
        [Category("行为")]
        [Description("是否启用长按确认模式")]
        [DefaultValue(false)]
        public bool EnableLongPress
        {
            get => _enableLongPress;
            set => _enableLongPress = value;
        }

        /// <summary>
        /// 长按确认时间（毫秒）
        /// </summary>
        [Category("行为")]
        [Description("长按确认所需时间（毫秒）")]
        [DefaultValue(1000)]
        public int LongPressTime
        {
            get => _longPressMs;
            set => _longPressMs = Math.Max(200, value);
        }

        /// <summary>
        /// 按钮图标
        /// </summary>
        [Category("外观")]
        [Description("按钮图标")]
        public Image Icon
        {
            get => _icon;
            set { _icon = value; Invalidate(); }
        }

        public DeviceControlButton()
        {
            Size = new Size(120, 40);
            Cursor = Cursors.Hand;
        
            // 设计模式下不创建Timer，避免设计器中运行运行时逻辑
            if (!IsDesignMode)
            {
                _longPressTimer = new Timer { Interval = 30 };
                _longPressTimer.Tick += LongPressTimer_Tick;
            }
                    
            // 初始化默认状态列表（停止和运行状态）
            InitializeDefaultStates();
        
            // 默认初始状态为索引0
            _currentStateIndex = 0;
        }

        /// <summary>
        /// 初始化默认状态列表
        /// </summary>
        private void InitializeDefaultStates()
        {
            _states = new List<DeviceButtonState>();
            
            // 设计模式下使用固定颜色，避免访问主题系统
            if (IsDesignMode)
            {
                _states.Add(DeviceButtonState.Stop);
                _states.Add(DeviceButtonState.Start);
            }
            else
            {
                _states.Add(DeviceButtonState.Stop);
                _states.Add(DeviceButtonState.Start);
            }
        }

        /// <summary>
        /// 应用主题，按钮默认使用 TextOnPrimary 作为文本颜色
        /// </summary>
        protected override void ApplyTheme()
        {
            var theme = CurrentTheme;
            if (theme == null) return;
            SetThemeBackColor(theme.Colors.Background);
            ForeColor = TextColor != Color.Empty ? TextColor : theme.Colors.TextOnPrimary;
        }

        /// <summary>
        /// 设置按钮状态列表
        /// </summary>
        [Browsable(false)]
        public void SetStates(params DeviceButtonState[] states)
        {
            _states.Clear();
            _states.AddRange(states);
            
            // 根据 DefaultStateIndex 设置初始状态索引
            if (states.Length > 0)
            {
                _currentStateIndex = Math.Min(_defaultStateIndex, states.Length - 1);
            }
            else
            {
                _currentStateIndex = 0;
            }
            
            Invalidate();
        }

        /// <summary>
        /// 切换到下一个状态
        /// </summary>
        public void NextState()
        {
            if (_states.Count > 0)
            {
                CurrentStateIndex = (_currentStateIndex + 1) % _states.Count;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!Enabled) return;

            if (_enableLongPress && _longPressTimer != null)
            {
                _pressStartTime = DateTime.Now;
                _longPressProgress = 0;
                _longPressTimer.Start();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!Enabled) return;

            _longPressTimer?.Stop();

            if (_enableLongPress)
            {
                if (_longPressProgress >= 1.0f)
                {
                    // 长按完成
                    NextState();
                    ButtonActivated?.Invoke(this, EventArgs.Empty);
                }
                _longPressProgress = 0;
                Invalidate();
            }
            else
            {
                // 普通点击
                NextState();
                ButtonActivated?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _longPressTimer?.Stop();
            _longPressProgress = 0;
            Invalidate();
        }

        private void LongPressTimer_Tick(object sender, EventArgs e)
        {
            var elapsed = (DateTime.Now - _pressStartTime).TotalMilliseconds;
            _longPressProgress = Math.Min(1.0f, (float)(elapsed / _longPressMs));
            Invalidate();

            if (_longPressProgress >= 1.0f)
            {
                _longPressTimer.Stop();
            }
        }

        protected override void RenderControl(Graphics g, Rectangle bounds)
        {
            var state = CurrentState;
            Color bgColor;
            
            // 根据显示模式决定使用哪个状态的颜色
            if (_displayMode == DisplayMode.StatusMode)
            {
                // 状态模式：使用当前状态的颜色
                bgColor = state?.Color ?? CurrentTheme.Colors.Primary;
            }
            else
            {
                // 动作模式：使用下一个状态的颜色（将要执行的动作）
                if (_states.Count > 1)
                {
                    int nextIndex = (_currentStateIndex + 1) % _states.Count;
                    bgColor = _states[nextIndex].Color;
                }
                else
                {
                    bgColor = state?.Color ?? CurrentTheme.Colors.Primary;
                }
            }
            
            Color textColor = ForeColor;

            if (!Enabled)
            {
                bgColor = CurrentTheme.Colors.Disabled;
                textColor = CurrentTheme.Colors.TextDisabled;
            }
            else if (IsPressed)
            {
                bgColor = GraphicsHelper.AdjustBrightness(bgColor, 0.8f);
            }
            else if (IsHovered)
            {
                bgColor = GraphicsHelper.AdjustBrightness(bgColor, 1.1f);
            }

            // 绘制按钮背景
            var btnRect = new Rectangle(1, 1, bounds.Width - 2, bounds.Height - 2);
            using (var brush = new SolidBrush(bgColor))
            {
                GraphicsHelper.FillRoundedRectangle(g, brush, btnRect, CornerRadius);
            }

            // 长按进度条
            if (_enableLongPress && _longPressProgress > 0)
            {
                int progressWidth = (int)(btnRect.Width * _longPressProgress);
                var progressRect = new Rectangle(btnRect.X, btnRect.Y, progressWidth, btnRect.Height);
                using (var brush = new SolidBrush(Color.FromArgb(60, 255, 255, 255)))
                {
                    GraphicsHelper.FillRoundedRectangle(g, brush, progressRect, CornerRadius);
                }
            }

            // 绘制图标和文本
            string displayText;
            
            // 根据显示模式决定显示哪个状态的文本
            if (_displayMode == DisplayMode.StatusMode)
            {
                // 状态模式：显示当前状态
                displayText = state?.DisplayText ?? _buttonText;
            }
            else
            {
                // 动作模式：显示下一个状态的文本（将要执行的动作）
                if (_states.Count > 1)
                {
                    int nextIndex = (_currentStateIndex + 1) % _states.Count;
                    displayText = _states[nextIndex].DisplayText;
                }
                else
                {
                    displayText = state?.DisplayText ?? _buttonText;
                }
            }
            
            var textRect = bounds;

            if (_icon != null)
            {
                int iconSize = Math.Min(24, bounds.Height - 8);
                int iconX = bounds.X + 8;
                int iconY = bounds.Y + (bounds.Height - iconSize) / 2;
                g.DrawImage(_icon, iconX, iconY, iconSize, iconSize);
                textRect = new Rectangle(iconX + iconSize + 4, bounds.Y, bounds.Width - iconSize - 16, bounds.Height);
            }

            GraphicsHelper.DrawCenteredText(g, displayText, Font, textColor, textRect);
        }

        protected override void DisposeManagedResources()
        {
            _longPressTimer?.Stop();
            _longPressTimer?.Dispose();
            base.DisposeManagedResources();
        }
    }
}
