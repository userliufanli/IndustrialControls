using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IndustrialControls.Core
{
    /// <summary>
    /// 双缓冲渲染基类，优化GDI+绘图性能，减少闪烁。
    /// 设计模式下跳过渲染节流以确保设计器正常显示。
    /// </summary>
    [System.ComponentModel.TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<DoubleBufferedControl, DesignTimeDoubleBufferedControl>))]
    public class DoubleBufferedControl : BaseControl
    {
        private DateTime _lastRenderTime = DateTime.MinValue;
        private int _minRenderInterval = 16; // ~60fps
        private bool _invalidatePending;
        private Timer _throttleTimer; // 复用定时器对象，避免对象泄漏

        /// <summary>
        /// 最小渲染间隔（毫秒），用于渲染节流
        /// </summary>
        [System.ComponentModel.Category("性能")]
        [System.ComponentModel.Description("最小渲染间隔（毫秒）")]
        [System.ComponentModel.DefaultValue(16)]
        public int MinRenderInterval
        {
            get => _minRenderInterval;
            set => _minRenderInterval = Math.Max(1, value);
        }

        protected DoubleBufferedControl()
        {
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _lastRenderTime = DateTime.Now;
            _invalidatePending = false;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            RenderControl(e.Graphics, ClientRectangle);
        }

        /// <summary>
        /// 子类实现此方法进行自定义绘制
        /// </summary>
        protected virtual void RenderControl(Graphics g, Rectangle bounds)
        {
            // 子类重写此方法进行自定义绘制
        }

        /// <summary>
        /// 带节流的刷新：在最小渲染间隔内的多次调用会合并为一次
        /// </summary>
        protected void ThrottledInvalidate()
        {
            if (_invalidatePending) return;

            var elapsed = (DateTime.Now - _lastRenderTime).TotalMilliseconds;
            if (elapsed >= _minRenderInterval)
            {
                Invalidate();
            }
            else
            {
                // 延迟到间隔结束后再刷新，避免丢帧
                _invalidatePending = true;
                var delay = Math.Max(1, _minRenderInterval - (int)elapsed);
                
                // 复用定时器对象，避免频繁创建和销毁
                if (_throttleTimer == null)
                {
                    _throttleTimer = new Timer();
                    _throttleTimer.Tick += ThrottleTimer_Tick;
                }
                
                _throttleTimer.Stop();
                _throttleTimer.Interval = delay;
                _throttleTimer.Start();
            }
        }

        private void ThrottleTimer_Tick(object sender, EventArgs e)
        {
            if (_throttleTimer != null)
            {
                _throttleTimer.Stop();
            }
            _invalidatePending = false;
            if (!IsDisposed) Invalidate();
        }

        /// <summary>
        /// 安全的局部刷新
        /// </summary>
        protected void InvalidateRegion(Rectangle region)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => Invalidate(region)));
            }
            else
            {
                Invalidate(region);
            }
        }

        /// <summary>
        /// 线程安全的全部刷新
        /// </summary>
        protected void SafeInvalidate()
        {
            if (IsDisposed) return;
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => Invalidate()));
            }
            else
            {
                Invalidate();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 释放定时器资源
                if (_throttleTimer != null)
                {
                    _throttleTimer.Stop();
                    _throttleTimer.Tick -= ThrottleTimer_Tick;
                    _throttleTimer.Dispose();
                    _throttleTimer = null;
                }
            }
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// DoubleBufferedControl的设计时具体替代类，仅供VS设计器实例化使用。
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    public class DesignTimeDoubleBufferedControl : DoubleBufferedControl
    {
        protected override void RenderControl(Graphics g, Rectangle bounds)
        {
            using (var brush = new SolidBrush(ForeColor))
            {
                g.DrawString(GetType().Name, Font, brush, 5, 5);
            }
        }
    }
}
