using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IndustrialControls.Controls.VirtualKeyboard
{
    public static class VirtualKeyboardManager
    {
        private static Control _currentFocusedControl;
        private static Timer _focusMonitorTimer;
        private const int DefaultMonitorInterval = 150; // 默认监控间隔（毫秒），降低到更合理的值减少CPU占用
        private static int _monitorInterval = DefaultMonitorInterval;
        
        // Windows API 声明：获取焦点窗口句柄
        [DllImport("user32.dll")]
        private static extern IntPtr GetFocus();
        
        public static Control CurrentFocusedControl
        {
            get { return _currentFocusedControl; }
        }

        /// <summary>
        /// 焦点监控间隔（毫秒）
        /// </summary>
        public static int MonitorInterval
        {
            get { return _monitorInterval; }
            set 
            { 
                _monitorInterval = Math.Max(50, Math.Min(500, value)); 
                if (_focusMonitorTimer != null)
                {
                    _focusMonitorTimer.Interval = _monitorInterval;
                }
            }
        }

        public static void Initialize()
        {
            Initialize(_monitorInterval);
        }

        public static void Initialize(int interval)
        {
            _monitorInterval = Math.Max(50, Math.Min(500, interval));
            
            if (_focusMonitorTimer != null)
            {
                _focusMonitorTimer.Stop();
                _focusMonitorTimer.Dispose();
            }
            
            _focusMonitorTimer = new Timer();
            _focusMonitorTimer.Interval = _monitorInterval;
            _focusMonitorTimer.Tick += FocusMonitorTimer_Tick;
            _focusMonitorTimer.Start();
        }

        private static void FocusMonitorTimer_Tick(object sender, EventArgs e)
        {
            Control activeControl = null;
            
            try
            {
                // 方法1：优先使用 Windows API 获取焦点窗口（更高效）
                IntPtr focusHwnd = GetFocus();
                if (focusHwnd != IntPtr.Zero)
                {
                    activeControl = Control.FromHandle(focusHwnd);
                    
                    // 如果是虚拟键盘的控件，忽略
                    if (activeControl != null)
                    {
                        Form parentForm = activeControl.FindForm();
                        if (parentForm != null && IsVirtualKeyboardForm(parentForm))
                        {
                            // 焦点在虚拟键盘上，保留上一次记录的有效控件
                            return;
                        }
                    }
                }
                
                // 方法2：WinForms 方式作为备用（处理 API 无法获取的情况）
                if (activeControl == null)
                {
                    foreach (Form form in Application.OpenForms)
                    {
                        if (IsVirtualKeyboardForm(form))
                            continue;
                        
                        // 检查窗体是否有焦点或其子控件有焦点
                        if (form.ContainsFocus || form.Focused)
                        {
                            activeControl = GetDeepestFocusedControl(form.ActiveControl);
                            if (activeControl != null)
                            {
                                break;
                            }
                        }
                    }
                }
                
                // 更新当前焦点控件（只在变化时更新）
                if (activeControl != _currentFocusedControl)
                {
                    _currentFocusedControl = activeControl;
                    
                    if (activeControl != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"[Focus] 焦点变更: {activeControl.Name ?? activeControl.GetType().Name} ({activeControl.GetType().Name})");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Focus] 错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取最深层次的焦点控件
        /// </summary>
        private static Control GetDeepestFocusedControl(Control control)
        {
            if (control == null)
                return null;
            
            // 如果是容器控件，递归查找内部真正有焦点的控件
            if ((control is UserControl || control is Panel || control is GroupBox || control is ContainerControl) 
                && control.HasChildren)
            {
                return FindFocusedControl(control);
            }
            
            return control;
        }
        
        /// <summary>
        /// 递归查找控件树中真正获得焦点的控件
        /// </summary>
        private static Control FindFocusedControl(Control parent)
        {
            if (parent == null || !parent.HasChildren)
                return null;
            
            // 优先检查 ActiveControl
            if (parent is ContainerControl container && container.ActiveControl != null)
            {
                // 如果 ActiveControl 还有子控件，继续递归
                Control child = container.ActiveControl;
                if (child is ContainerControl childContainer && childContainer.ActiveControl != null)
                {
                    return FindFocusedControl(child);
                }
                return child;
            }
            
            // 遍历所有子控件，查找获得焦点的控件
            foreach (Control child in parent.Controls)
            {
                if (child.Focused)
                    return child;
                
                // 递归检查子控件
                Control focused = FindFocusedControl(child);
                if (focused != null)
                    return focused;
            }
            
            return null;
        }

        public static Control GetActiveControl()
        {
            return _currentFocusedControl;
        }

        public static void Dispose()
        {
            if (_focusMonitorTimer != null)
            {
                _focusMonitorTimer.Stop();
                _focusMonitorTimer.Dispose();
                _focusMonitorTimer = null;
            }
        }

        private static bool IsVirtualKeyboardForm(Form form)
        {
            return form != null && typeof(VirtualKeyboardForm).IsAssignableFrom(form.GetType());
        }
    }
}
