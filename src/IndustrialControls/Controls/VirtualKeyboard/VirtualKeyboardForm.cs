using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IndustrialControls.Controls.VirtualKeyboard
{
    /// <summary>
    /// 虚拟键盘窗体 - 全新版本（Designer 模式）
    /// </summary>
    public partial class VirtualKeyboardForm : Form
    {
        private Timer _layoutMonitorTimer; // 布局监控计时器
        private Control _targetControl; // 记录目标输入控件
        
        private Point _dragStartPoint;
        private bool _isDragging = false;
        private KeyboardLayoutMode _lastLayoutMode; // 记录上一次的布局模式
        
        private const int TitleBarHeight = 48;
        private const int CornerRadius = 8;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        public VirtualKeyboardForm()
        {
            InitializeComponent();
            InitializeForm();
            InitializeLogic();
            // 不要在构造函数中调用 UpdateKeyboardSize()
            // 应该在 Shown 事件中调用，此时控件已完成布局
        }

        /// <summary>
        /// 按键输入事件（包含解析后的目标控件）。
        /// </summary>
        public event EventHandler<VirtualKeyboardKeyInputEventArgs> KeyInput;

        private void InitializeForm()
        {
            ShowInTaskbar = false;
            TopMost = true;
        }
        
        private void InitializeLogic()
        {
            // 设置键盘面板初始布局模式
            _keyboardPanel.KeyInput += OnKeyInput;
            _keyboardPanel.LayoutMode = KeyboardLayoutMode.QWERTY;
            
            // 初始化布局监控计时器
            InitializeLayoutMonitor();
            
            // 在窗体首次显示时计算正确的尺寸（使用 BeginInvoke 确保布局完成）
            this.Shown += (s, e) => 
            {
                // 延迟执行，确保面板的 RebuildLayout() 已完成
                this.BeginInvoke(new Action(() => 
                {
                    UpdateKeyboardSize();
                    UpdateRegion();
                }));
            };

            VisibleChanged += VirtualKeyboardForm_VisibleChanged;
        }

        /// <summary>
        /// 键盘面板（用于外部查询布局模式等，避免依赖 Controls 集合顺序）。
        /// </summary>
        public VirtualKeyboardPanel KeyboardPanel => _keyboardPanel;

        private void VirtualKeyboardForm_VisibleChanged(object sender, EventArgs e)
        {
            if (_layoutMonitorTimer == null) return;
            if (Visible)
            {
                _lastLayoutMode = _keyboardPanel.LayoutMode;
                _layoutMonitorTimer.Start();
            }
            else
            {
                _layoutMonitorTimer.Stop();
            }
        }

        /// <summary>
        /// 处理按键输入事件
        /// </summary>
        private void OnKeyInput(object sender, string key)
        {
            Control target = ResolveInputTarget();
            if (target == null)
            {
                System.Diagnostics.Debug.WriteLine("[VirtualKeyboardForm] 未找到可输入的目标控件");
                return;
            }

            KeyInput?.Invoke(this, new VirtualKeyboardKeyInputEventArgs(key, target));
        }

        /// <summary>
        /// 优先使用 <see cref="SetTargetControl"/> 指定的控件（若仍有效），否则使用焦点管理器中的活动控件。
        /// </summary>
        private Control ResolveInputTarget()
        {
            if (_targetControl != null && !_targetControl.IsDisposed && _targetControl.IsHandleCreated)
            {
                Form root = _targetControl.FindForm();
                if (root != null && !root.IsDisposed && root.IsHandleCreated)
                    return _targetControl;
            }

            return VirtualKeyboardManager.GetActiveControl();
        }
        
        /// <summary>
        /// 关闭按钮点击事件（Designer 绑定）
        /// </summary>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        /// <summary>
        /// 初始化布局监控计时器
        /// </summary>
        private void InitializeLayoutMonitor()
        {
            _layoutMonitorTimer = new Timer();
            _layoutMonitorTimer.Interval = 200; // 每200ms检查一次
            _layoutMonitorTimer.Tick += LayoutMonitorTimer_Tick;
            // 仅在窗体 Visible 时启动，避免隐藏状态下空转（见 VirtualKeyboardForm_VisibleChanged）
            if (Visible)
                _layoutMonitorTimer.Start();

            _lastLayoutMode = _keyboardPanel.LayoutMode;
        }

        /// <summary>
        /// 计时器回调：检测布局变化并刷新尺寸
        /// </summary>
        private void LayoutMonitorTimer_Tick(object sender, EventArgs e)
        {
            if (_keyboardPanel == null || !this.Visible) return;
            
            // 检测布局模式是否改变
            if (_keyboardPanel.LayoutMode != _lastLayoutMode)
            {
                _lastLayoutMode = _keyboardPanel.LayoutMode;
                
                // 布局切换后刷新窗体尺寸
                UpdateKeyboardSize();
            }
        }

        /// <summary>
        /// 根据键盘面板内容自动计算窗体尺寸
        /// </summary>
        private void UpdateKeyboardSize()
        {
            if (_keyboardPanel == null) return;

            // 强制面板重新构建布局（确保按钮已创建）
            _keyboardPanel.RebuildLayout();
            _keyboardPanel.PerformLayout();

            // 使用配置计算正确的尺寸（不依赖面板当前尺寸）
            var config = _keyboardPanel.Config;
            int panelWidth, panelHeight;
            
            // 根据布局模式计算正确的尺寸
            if (_keyboardPanel.LayoutMode == KeyboardLayoutMode.QWERTY)
            {
                // QWERTY: 13个按键宽，5行高
                panelWidth = config.PanelPadding * 2 + 
                             13 * (config.ButtonWidth + config.ButtonSpacing) - 
                             config.ButtonSpacing;
                panelHeight = config.PanelPadding * 2 + 
                              5 * (config.ButtonHeight + config.ButtonSpacing) - 
                              config.ButtonSpacing;
            }
            else
            {
                // NumberPad: 5个按键宽，4行高
                panelWidth = config.PanelPadding * 2 + 
                             5 * (config.ButtonWidth + config.ButtonSpacing) - 
                             config.ButtonSpacing;
                panelHeight = config.PanelPadding * 2 + 
                              4 * (config.ButtonHeight + config.ButtonSpacing) - 
                              config.ButtonSpacing;
            }

            Size = new Size(panelWidth, panelHeight + TitleBarHeight);
            UpdateRegion();
        }
        /// <summary>
        /// 在指定位置显示（不抢占焦点）
        /// </summary>
        public void ShowAt(Point location)
        {
            // 确保在显示前已经计算了正确的尺寸
            UpdateKeyboardSize();
            
            var screen = Screen.FromPoint(location);
            int x = Math.Max(screen.WorkingArea.X, Math.Min(location.X, screen.WorkingArea.Right - Width));
            int y = Math.Max(screen.WorkingArea.Y, Math.Min(location.Y, screen.WorkingArea.Bottom - Height));

            Location = new Point(x, y);
            
            // 使用 Show 而不是 ShowDialog，不激活窗体（不抢占焦点）
            Show();
        }

        /// <summary>
        /// 设置目标输入控件（用于焦点管理）
        /// </summary>
        public void SetTargetControl(Control target)
        {
            _targetControl = target;
        }

        /// <summary>
        /// 更新圆角区域（窗体尺寸变化时调用）
        /// </summary>
        private void UpdateRegion()
        {
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));
        }

        #region 拖拽支持

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = e.Location;
            }
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Location = new Point(
                    Location.X + (e.X - _dragStartPoint.X),
                    Location.Y + (e.Y - _dragStartPoint.Y)
                );
            }
        }

        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        #endregion

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            UpdateRegion();
        }

    }
}
