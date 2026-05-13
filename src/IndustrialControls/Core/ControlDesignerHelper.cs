using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IndustrialControls.Core
{
    /// <summary>
    /// 控件设计器辅助类，提供设计时支持
    /// </summary>
    public static class ControlDesignerHelper
    {
        /// <summary>
        /// 判断当前是否处于设计模式
        /// </summary>
        public static bool IsDesignMode(Control control)
        {
            if (control == null) return false;
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;
            while (control != null)
            {
                if (control.Site != null && control.Site.DesignMode)
                    return true;
                control = control.Parent;
            }
            return false;
        }

        /// <summary>
        /// 获取设计时默认大小
        /// </summary>
        public static Size GetDefaultSize(int width, int height)
        {
            return new Size(width, height);
        }
    }
}
