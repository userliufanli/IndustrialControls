# VirtualKeyboard（虚拟键盘）

**命名空间**：`IndustrialControls.Controls.VirtualKeyboard`

## 组成

| 类型 | 说明 |
|------|------|
| `VirtualKeyboardPanel` | **UserControl**：QWERTY / 数字布局，按键通过事件传出逻辑键名。 |
| `VirtualKeyboardForm` | **Form**：浮动键盘窗体，可指定目标控件与屏幕位置。 |
| `VirtualKeyboardManager` | **静态类**：焦点轮询，推断当前应接收输入的控件（排除键盘窗体内控件）。 |
| `KeyboardLayoutMode` | `QWERTY`、`NumberPad` |
| `VirtualKeyboardKeyInputEventArgs` | `Key`、`Target`（建议写入的目标控件） |

## VirtualKeyboardPanel

| 成员 | 说明 |
|------|------|
| `LayoutMode` | 当前布局 |
| `Config` | `KeyboardConfig`（键大小、间距、字体等） |
| `KeyInput` | `EventHandler<string>`，参数为逻辑键名（如 `Backspace`、`Enter`、普通字符） |

## VirtualKeyboardForm

| 成员 | 说明 |
|------|------|
| `KeyboardPanel` | 内部面板 |
| `SetTargetControl(Control)` | 优先将输入发往该控件 |
| `ShowAt(Point)` | 屏幕坐标附近非模态显示 |
| `KeyInput` | `EventHandler<VirtualKeyboardKeyInputEventArgs>` |

## VirtualKeyboardManager

| 成员 | 说明 |
|------|------|
| `Initialize()` / `Initialize(int intervalMs)` | 启动焦点轮询（间隔约 50–500 ms） |
| `GetActiveControl()` | 当前推断的焦点控件 |
| `MonitorInterval` | 轮询间隔 |
| `Dispose()` | 停止计时器 |

宿主需根据 `Key` 自行写入 `TextBox` 等（退格、回车等需分支处理）；可参考 `samples` 中虚拟键盘演示页的完整逻辑。

## 代码示例

```csharp
using IndustrialControls.Controls.VirtualKeyboard;

VirtualKeyboardManager.Initialize();

var form = new VirtualKeyboardForm();
form.KeyInput += (s, e) =>
{
    if (e.Target is TextBox tb && e.Key.Length == 1)
        tb.SelectedText = e.Key;
};
form.SetTargetControl(myTextBox);
form.ShowAt(myTextBox.PointToScreen(new Point(0, myTextBox.Height + 4)));
```

## 另见

- [文档索引](./README.md)  
