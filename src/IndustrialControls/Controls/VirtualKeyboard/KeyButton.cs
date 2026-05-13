using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IndustrialControls.Controls.VirtualKeyboard
{
    /// <summary>
    /// 虚拟键盘按键控件，Windows 11 扁平化设计
    /// </summary>
    public class KeyButton : Button
    {
        private Timer _repeatTimer;
        private bool _isPressed;
        private bool _isHovered;
        private int _repeatPhase; // 0 = 等待首次 Tick，1 = 已进入连发
        
        // Windows 11 配色方案
        private Color _normalColor = Color.FromArgb(252, 252, 252);
        private Color _hoverColor = Color.FromArgb(245, 245, 245);
        private Color _pressedColor = Color.FromArgb(235, 235, 235);
        private Color _specialKeyNormal = Color.FromArgb(243, 243, 243);
        private Color _specialKeyHover = Color.FromArgb(237, 237, 237);
        private Color _specialKeyPressed = Color.FromArgb(227, 227, 227);
        private Color _borderColor = Color.FromArgb(200, 200, 200);
        private Color _textColor = Color.FromArgb(32, 32, 32);
        private int _cornerRadius = 6;

        /// <summary>
        /// 按键文本
        /// </summary>
        public string KeyText { get; set; }

        /// <summary>
        /// 是否支持长按重复（如退格键）
        /// </summary>
        public bool IsRepeatable { get; set; }

        /// <summary>
        /// 是否为特殊键（背景色不同）
        /// </summary>
        public bool IsSpecialKey { get; set; }

        /// <summary>
        /// 按键按下事件
        /// </summary>
        public event EventHandler<KeyButton> KeyPressed;

        public KeyButton()
        {
            // 触摸优化
            MinimumSize = new Size(44, 44);
            Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            BackColor = _normalColor;
            ForeColor = _textColor;
            Cursor = Cursors.Hand;
            // 启用双缓冲和自定义绘制
            SetStyle(ControlStyles.UserPaint | 
                    ControlStyles.AllPaintingInWmPaint | 
                    ControlStyles.OptimizedDoubleBuffer | 
                    ControlStyles.ResizeRedraw, true);
            
            _repeatTimer = new Timer { Interval = 450 };
            _repeatTimer.Tick += RepeatTimer_Tick;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _isPressed = true;
            _isHovered = false;
            Invalidate();

            if (IsRepeatable && e.Button == MouseButtons.Left)
            {
                _repeatPhase = 0;
                _repeatTimer.Interval = 450;
                _repeatTimer.Start();
                Capture = true;
            }
        }
        
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isPressed = false;
            Invalidate();

            if (IsRepeatable && e.Button == MouseButtons.Left)
            {
                Capture = false;
                _repeatTimer.Stop();
                if (_repeatPhase == 0)
                    KeyPressed?.Invoke(this, this);
                _repeatPhase = 0;
            }
        }
        
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (IsRepeatable)
                return;

            KeyPressed?.Invoke(this, this);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            _isPressed = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // 高质量渲染设置
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // 确定背景色
            Color bgColor;
            if (_isPressed)
            {
                bgColor = IsSpecialKey ? _specialKeyPressed : _pressedColor;
            }
            else if (_isHovered)
            {
                bgColor = IsSpecialKey ? _specialKeyHover : _hoverColor;
            }
            else
            {
                bgColor = IsSpecialKey ? _specialKeyNormal : _normalColor;
            }

            // 先清除整个控件背景
            using (SolidBrush clearBrush = new SolidBrush(bgColor))
            {
                e.Graphics.FillRectangle(clearBrush, ClientRectangle);
            }

            // 创建圆角路径
            using (GraphicsPath path = CreateRoundPath(0, 0, Width - 1, Height - 1, _cornerRadius))
            {
                // 填充圆角背景
                using (SolidBrush bgBrush = new SolidBrush(bgColor))
                {
                    e.Graphics.FillPath(bgBrush, path);
                }

                // 绘制边框
                using (Pen borderPen = new Pen(_borderColor, 1.0f))
                {
                    e.Graphics.DrawPath(borderPen, path);
                }
            }

            // 绘制文本
            TextRenderer.DrawText(
                e.Graphics,
                Text,
                Font,
                ClientRectangle,
                ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            // 使用更平滑的 Region 裁剪
            int actualRadius = Math.Min(_cornerRadius, Math.Min(Width, Height) / 2);
            if (actualRadius > 0)
            {
                // 使用更大的路径来创建 Region，避免边缘锯齿
                using (GraphicsPath path = CreateSmoothRoundPath(0, 0, Width, Height, actualRadius))
                {
                    Region = new Region(path);
                }
            }
            else
            {
                Region = new Region(ClientRectangle);
            }
        }

        private GraphicsPath CreateRoundPath(int x, int y, int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            
            // 确保圆角半径不超过控件尺寸的一半
            int actualRadius = Math.Min(radius, Math.Min(width, height) / 2);
            
            if (actualRadius <= 0)
            {
                // 如果半径为0，创建矩形路径
                path.AddRectangle(new Rectangle(x, y, width, height));
                return path;
            }
            
            int diameter = actualRadius * 2;
            
            path.StartFigure();
            // 左上角
            path.AddArc(x, y, diameter, diameter, 180, 90);
            // 右上角
            path.AddArc(x + width - diameter, y, diameter, diameter, 270, 90);
            // 右下角
            path.AddArc(x + width - diameter, y + height - diameter, diameter, diameter, 0, 90);
            // 左下角
            path.AddArc(x, y + height - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            
            return path;
        }

        /// <summary>
        /// 创建平滑的圆角路径（用于 Region 裁剪）
        /// </summary>
        private GraphicsPath CreateSmoothRoundPath(int x, int y, int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Alternate;
            
            int actualRadius = Math.Min(radius, Math.Min(width, height) / 2);
            
            if (actualRadius <= 0)
            {
                path.AddRectangle(new Rectangle(x, y, width, height));
                return path;
            }
            
            int diameter = actualRadius * 2;
            
            // 使用更精确的圆弧绘制
            path.StartFigure();
            
            // 顶部边线
            path.AddLine(x + actualRadius, y, x + width - actualRadius, y);
            
            // 右上角圆弧 - 使用更多点来平滑
            path.AddArc(x + width - diameter, y, diameter, diameter, 270, 90);
            
            // 右侧边线
            path.AddLine(x + width, y + actualRadius, x + width, y + height - actualRadius);
            
            // 右下角圆弧
            path.AddArc(x + width - diameter, y + height - diameter, diameter, diameter, 0, 90);
            
            // 底部边线
            path.AddLine(x + width - actualRadius, y + height, x + actualRadius, y + height);
            
            // 左下角圆弧
            path.AddArc(x, y + height - diameter, diameter, diameter, 90, 90);
            
            // 左侧边线
            path.AddLine(x, y + height - actualRadius, x, y + actualRadius);
            
            // 左上角圆弧
            path.AddArc(x, y, diameter, diameter, 180, 90);
            
            path.CloseFigure();
            
            return path;
        }

        private void RepeatTimer_Tick(object sender, EventArgs e)
        {
            if (!IsRepeatable)
                return;

            KeyPressed?.Invoke(this, this);
            if (_repeatPhase == 0)
            {
                _repeatPhase = 1;
                _repeatTimer.Interval = 65;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repeatTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
