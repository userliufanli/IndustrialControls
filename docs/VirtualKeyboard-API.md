# 虚拟键盘控件 (VirtualKeyboard) API 使用手册

## 概述

虚拟键盘控件组提供完整的屏幕键盘解决方案,包含 `VirtualKeyboardPanel`(面板)、`VirtualKeyboardForm`(窗体)和 `VirtualKeyboardManager`(全局管理器),支持跨窗体全局输入和焦点管理。

## 命名空间

```csharp
IndustrialControls.Controls.VirtualKeyboard
```

---

## VirtualKeyboardPanel - 虚拟键盘面板

### 概述

可嵌入窗体的虚拟键盘面板控件,支持多种布局模式。

### 快速开始

```csharp
using IndustrialControls.Controls.VirtualKeyboard;

// 创建虚拟键盘面板
var keyboard = new VirtualKeyboardPanel
{
    Size = new Size(800, 300),
    Location = new Point(10, 300),
    LayoutMode = KeyboardLayoutMode.Standard
};

// 绑定到输入控件
keyboard.TargetTextBox = textBox1;

this.Controls.Add(keyboard);
```

### 属性

#### LayoutMode

- **类型**: `KeyboardLayoutMode`
- **默认值**: `KeyboardLayoutMode.Standard`
- **说明**: 键盘布局模式
- **示例**:
  ```csharp
  keyboard.LayoutMode = KeyboardLayoutMode.Numeric; // 数字键盘
  ```

#### TargetTextBox

- **类型**: `TextBoxBase`
- **说明**: 目标输入控件
- **示例**:
  ```csharp
  keyboard.TargetTextBox = txtUserName;
  ```

### 布局模式

```csharp
public enum KeyboardLayoutMode
{
    Standard,  // 标准键盘(全键盘)
    Numeric,   // 数字键盘
    Custom     // 自定义布局
}
```

---

## VirtualKeyboardForm - 虚拟键盘窗体

### 概述

独立的虚拟键盘窗体,可浮动显示,支持全局焦点管理。

### 快速开始

```csharp
using IndustrialControls.Controls.VirtualKeyboard;

// 显示虚拟键盘窗体
var keyboardForm = new VirtualKeyboardForm();
keyboardForm.Show();

// 绑定到输入控件
keyboardForm.AttachToControl(textBox1);
```

### 方法

#### AttachToControl(Control control)

绑定到输入控件。

**参数**:
- `control`: `Control` 目标输入控件

**示例**:
```csharp
keyboardForm.AttachToControl(txtInput);
```

#### Show()

显示键盘窗体。

**示例**:
```csharp
keyboardForm.Show();
```

---

## VirtualKeyboardManager - 全局键盘管理器

### 概述

管理虚拟键盘的全局实例,提供自动焦点追踪和跨窗体输入支持。

### 快速开始

```csharp
using IndustrialControls.Controls.VirtualKeyboard;

// 在应用程序启动时初始化
public static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        
        // 初始化虚拟键盘管理器
        VirtualKeyboardManager.Initialize();
        
        Application.Run(new MainForm());
    }
}

// 在需要输入的地方激活键盘
private void txtInput_Enter(object sender, EventArgs e)
{
    VirtualKeyboardManager.Instance.ShowForControl(txtInput);
}
```

### 方法

#### Initialize()

初始化全局键盘管理器实例(静态方法)。

**示例**:
```csharp
VirtualKeyboardManager.Initialize();
```

#### Instance

获取全局管理器实例。

**示例**:
```csharp
var manager = VirtualKeyboardManager.Instance;
```

#### ShowForControl(Control control)

为指定控件显示虚拟键盘。

**参数**:
- `control`: `Control` 目标输入控件

**示例**:
```csharp
VirtualKeyboardManager.Instance.ShowForControl(textBox1);
```

#### Hide()

隐藏虚拟键盘。

**示例**:
```csharp
VirtualKeyboardManager.Instance.Hide();
```

### 属性

#### Instance

- **类型**: `VirtualKeyboardManager` (只读)
- **说明**: 全局管理器单例实例

#### KeyboardForm

- **类型**: `VirtualKeyboardForm`
- **说明**: 键盘窗体实例

## 事件

### KeyInput

按键输入时触发。

**事件参数**: `EventHandler<VirtualKeyboardKeyInputEventArgs>`

**示例**:
```csharp
keyboard.KeyInput += (sender, e) =>
{
    Console.WriteLine($"按键: {e.KeyValue}");
    Console.WriteLine($"是否特殊键: {e.IsSpecialKey}");
};
```

## 完整示例

### 全局虚拟键盘系统

```csharp
using System;
using System.Windows.Forms;
using IndustrialControls.Controls.VirtualKeyboard;

namespace GlobalKeyboardDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeVirtualKeyboard();
        }

        private void InitializeVirtualKeyboard()
        {
            // 为所有需要虚拟键盘的控件订阅Enter事件
            SubscribeKeyboard(txtUserName);
            SubscribeKeyboard(txtPassword);
            SubscribeKeyboard(txtEmail);
            SubscribeKeyboard(txtPhone);
            SubscribeKeyboard(txtAddress);
        }

        private void SubscribeKeyboard(Control control)
        {
            control.Enter += (sender, e) =>
            {
                // 自动显示虚拟键盘并绑定到当前控件
                VirtualKeyboardManager.Instance.ShowForControl(control);
            };

            control.Leave += (sender, e) =>
            {
                // 可选:离开时隐藏键盘
                // VirtualKeyboardManager.Instance.Hide();
            };
        }

        private void chkEnableKeyboard_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableKeyboard.Checked)
            {
                VirtualKeyboardManager.Initialize();
                MessageBox.Show("虚拟键盘已启用");
            }
            else
            {
                VirtualKeyboardManager.Instance?.Hide();
                MessageBox.Show("虚拟键盘已禁用");
            }
        }

        private void btnNumericKeyboard_Click(object sender, EventArgs e)
        {
            // 显示数字键盘
            var keyboard = new VirtualKeyboardForm
            {
                LayoutMode = KeyboardLayoutMode.Numeric
            };
            keyboard.AttachToControl(txtPhone);
            keyboard.Show();
        }
    }

    // 应用程序入口
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // 初始化虚拟键盘管理器
            VirtualKeyboardManager.Initialize();
            
            Application.Run(new MainForm());
        }
    }
}
```

### 跨窗体焦点管理

```csharp
// 在子窗体中使用虚拟键盘
public partial class ChildForm : Form
{
    private TextBox txtInput1;
    private TextBox txtInput2;

    public ChildForm()
    {
        InitializeComponent();

        // 子窗体中的输入控件自动使用全局虚拟键盘
        txtInput1.Enter += (s, e) =>
        {
            VirtualKeyboardManager.Instance.ShowForControl(txtInput1);
        };

        txtInput2.Enter += (s, e) =>
        {
            VirtualKeyboardManager.Instance.ShowForControl(txtInput2);
        };
    }
}
```

### 自定义键盘布局

```csharp
// 创建自定义布局的键盘
var customKeyboard = new VirtualKeyboardPanel
{
    LayoutMode = KeyboardLayoutMode.Custom,
    Size = new Size(600, 250)
};

// 自定义按键配置(需要查看源代码了解具体配置方式)
// customKeyboard.ConfigureKeys(...);

// 绑定到目标控件
customKeyboard.TargetTextBox = txtCustomInput;

this.Controls.Add(customKeyboard);
```

## 焦点管理

### 自动焦点追踪

`VirtualKeyboardManager` 自动追踪全局焦点变化:

```csharp
// 当任何TextBox获得焦点时,虚拟键盘自动绑定
// 无需手动处理

// 焦点切换时,键盘自动更新目标
// txt1 -> txt2 -> txt3 (键盘跟随)
```

### 手动控制

```csharp
// 手动绑定到特定控件
VirtualKeyboardManager.Instance.ShowForControl(txtSpecific);

// 手动隐藏
VirtualKeyboardManager.Instance.Hide();

// 检查是否显示
bool isVisible = VirtualKeyboardManager.Instance.KeyboardForm?.Visible ?? false;
```

## 注意事项

1. **全局实例**: 整个应用程序应只初始化一次 `VirtualKeyboardManager`
2. **焦点管理**: 虚拟键盘自动追踪焦点,无需手动绑定
3. **跨窗体**: 支持在多个窗体间自动切换目标控件
4. **线程安全**: 在UI线程中操作虚拟键盘
5. **布局模式**: 根据输入类型选择合适的键盘布局

## 最佳实践

1. **应用启动初始化**: 在 `Program.Main()` 中初始化全局管理器
2. **自动绑定**: 在控件的 `Enter` 事件中调用 `ShowForControl`
3. **数字输入**: 对数字输入字段使用 `Numeric` 布局
4. **全局配置**: 统一配置键盘样式和行为
5. **焦点优化**: 避免在 `Leave` 事件中立即隐藏键盘,影响用户体验

## 相关控件

- **DataInputPanel**: 数据输入面板,可配合虚拟键盘使用
- **ValidatedTextBox**: 验证文本框,虚拟键盘输入时自动验证
- **NumericInputBox**: 数值输入框,使用数字键盘输入
- **FlatLoginControl**: 登录控件,虚拟键盘输入用户名密码
