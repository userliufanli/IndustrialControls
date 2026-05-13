using System;
using System.Windows.Forms;

namespace IndustrialControls.Controls.VirtualKeyboard
{
    /// <summary>
    /// 虚拟键盘按键输入事件参数（包含建议的目标输入控件）。
    /// </summary>
    public sealed class VirtualKeyboardKeyInputEventArgs : EventArgs
    {
        public VirtualKeyboardKeyInputEventArgs(string key, Control target)
        {
            Key = key ?? string.Empty;
            Target = target;
        }

        /// <summary>
        /// 逻辑键名（如 Backspace、Enter、普通字符等）。
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 建议接收输入的控件；由窗体根据 <see cref="VirtualKeyboardForm.SetTargetControl"/> 与焦点管理器解析。
        /// </summary>
        public Control Target { get; }
    }
}
