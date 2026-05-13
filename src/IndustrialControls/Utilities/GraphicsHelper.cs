using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace IndustrialControls.Utilities
{
    /// <summary>
    /// GDI+绘图辅助类
    /// </summary>
    public static class GraphicsHelper
    {
        /// <summary>
        /// 创建圆角矩形路径
        /// </summary>
        public static GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            int diameter = radius * 2;
            var arcRect = new Rectangle(bounds.Location, new Size(diameter, diameter));

            // 左上角
            path.AddArc(arcRect, 180, 90);
            // 右上角
            arcRect.X = bounds.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            // 右下角
            arcRect.Y = bounds.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            // 左下角
            arcRect.X = bounds.Left;
            path.AddArc(arcRect, 90, 90);

            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 创建圆角矩形路径（RectangleF版本）
        /// </summary>
        public static GraphicsPath CreateRoundedRectangle(RectangleF bounds, float radius)
        {
            var path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            float diameter = radius * 2;
            var arcRect = new RectangleF(bounds.Location, new SizeF(diameter, diameter));

            path.AddArc(arcRect, 180, 90);
            arcRect.X = bounds.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            arcRect.Y = bounds.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            arcRect.X = bounds.Left;
            path.AddArc(arcRect, 90, 90);

            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 绘制带圆角的填充矩形
        /// </summary>
        public static void FillRoundedRectangle(Graphics g, Brush brush, Rectangle bounds, int radius)
        {
            using (var path = CreateRoundedRectangle(bounds, radius))
            {
                g.FillPath(brush, path);
            }
        }

        /// <summary>
        /// 绘制带圆角的边框矩形
        /// </summary>
        public static void DrawRoundedRectangle(Graphics g, Pen pen, Rectangle bounds, int radius)
        {
            using (var path = CreateRoundedRectangle(bounds, radius))
            {
                g.DrawPath(pen, path);
            }
        }

        /// <summary>
        /// 创建线性渐变画刷
        /// </summary>
        public static LinearGradientBrush CreateGradientBrush(Rectangle bounds, Color startColor, Color endColor, float angle = 90f)
        {
            return new LinearGradientBrush(bounds, startColor, endColor, angle);
        }

        /// <summary>
        /// 混合两种颜色
        /// </summary>
        public static Color BlendColors(Color color1, Color color2, float ratio)
        {
            ratio = Math.Max(0f, Math.Min(1f, ratio));
            int r = (int)(color1.R * (1 - ratio) + color2.R * ratio);
            int g = (int)(color1.G * (1 - ratio) + color2.G * ratio);
            int b = (int)(color1.B * (1 - ratio) + color2.B * ratio);
            int a = (int)(color1.A * (1 - ratio) + color2.A * ratio);
            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// 调整颜色亮度
        /// </summary>
        public static Color AdjustBrightness(Color color, float factor)
        {
            int r = Math.Min(255, Math.Max(0, (int)(color.R * factor)));
            int g = Math.Min(255, Math.Max(0, (int)(color.G * factor)));
            int b = Math.Min(255, Math.Max(0, (int)(color.B * factor)));
            return Color.FromArgb(color.A, r, g, b);
        }

        /// <summary>
        /// 绘制文本居中
        /// </summary>
        public static void DrawCenteredText(Graphics g, string text, Font font, Color color, Rectangle bounds)
        {
            using (var brush = new SolidBrush(color))
            using (var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            })
            {
                g.DrawString(text, font, brush, bounds, sf);
            }
        }

        /// <summary>
        /// 设置高质量绘图
        /// </summary>
        public static void SetHighQuality(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }
    }
}
