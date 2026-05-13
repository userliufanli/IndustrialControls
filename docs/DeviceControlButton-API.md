# 设备控制按钮 (DeviceControlButton) API 使用手册

## 概述

`DeviceControlButton` 是一个支持多状态切换的设备控制按钮,具备长按确认、图标显示、动作/状态双模式等功能,适用于工业设备的启停控制。

## 命名空间

```csharp
IndustrialControls.Controls.DeviceButton
```

## 继承关系

```
System.Object
  └─ System.MarshalByRefObject
     └─ System.ComponentModel.Component
        └─ System.Windows.Forms.Control
           └─ IndustrialControls.Core.DoubleBufferedControl
              └─ DeviceControlButton
```

## 快速开始

### 1. 基本使用(默认启停按钮)

```csharp
using IndustrialControls.Controls.DeviceButton;

// 创建设备控制按钮(默认包含停止/运行两个状态)
var deviceButton = new DeviceControlButton
{
    Size = new Size(150, 50),
    Location = new Point(10, 10),
    ButtonText = "控制"
};

// 监听状态变化
deviceButton.StateChanged += (sender, state) =>
{
    Console.WriteLine($"状态切换: {state.DisplayText}");
    
    if (state == DeviceButtonState.Start)
    {
        StartDevice();
    }
    else if (state == DeviceButtonState.Stop)
    {
        StopDevice();
    }
};

this.Controls.Add(deviceButton);
```

### 2. 自定义状态列表

```csharp
// 定义多个状态
var states = new[]
{
    new DeviceButtonState("停止", Color.Red),
    new DeviceButtonState("运行", Color.Green),
    new DeviceButtonState("暂停", Color.Orange),
    new DeviceButtonState("维护", Color.Gray)
};

deviceButton.SetStates(states);
deviceButton.DefaultStateIndex = 0; // 初始状态为"停止"
```

### 3. 启用长按确认

```csharp
// 启用长按确认模式(防止误操作)
deviceButton.EnableLongPress = true;
deviceButton.LongPressTime = 1500; // 长按1.5秒

// 长按完成时触发
deviceButton.ButtonActivated += (sender, e) =>
{
    Console.WriteLine("长按确认完成,执行操作");
    ExecuteDeviceCommand(deviceButton.CurrentState);
};
```

### 4. 显示模式切换

```csharp
// 动作模式:显示将要执行的动作(默认)
deviceButton.Mode = DisplayMode.ActionMode;
// 当前状态为"停止"时,按钮显示"启动"

// 状态模式:显示当前运行状态
deviceButton.Mode = DisplayMode.StatusMode;
// 当前状态为"停止"时,按钮显示"停止"
```

### 5. 添加图标

```csharp
// 设置按钮图标
deviceButton.Icon = Properties.Resources.power_icon;

// 图标会自动显示在文本左侧
```

## 属性

### ButtonText

- **类型**: `string`
- **默认值**: `"控制"`
- **说明**: 按钮显示文本(在没有状态列表时作为后备文本)
- **示例**:
  ```csharp
  deviceButton.ButtonText = "设备控制";
  ```

### Mode

- **类型**: `DisplayMode`
- **默认值**: `DisplayMode.ActionMode`
- **说明**: 按钮显示模式(动作模式/状态模式)
- **示例**:
  ```csharp
  deviceButton.Mode = DisplayMode.StatusMode;
  ```

### DefaultStateIndex

- **类型**: `int`
- **默认值**: `0`
- **说明**: 调用SetStates时的默认初始状态索引
- **示例**:
  ```csharp
  deviceButton.DefaultStateIndex = 1; // 初始为第2个状态
  ```

### CurrentStateIndex

- **类型**: `int`
- **说明**: 当前状态索引(可读写)
- **示例**:
  ```csharp
  int currentIndex = deviceButton.CurrentStateIndex;
  deviceButton.CurrentStateIndex = 2; // 切换到第3个状态
  ```

### CurrentState

- **类型**: `DeviceButtonState` (只读)
- **说明**: 当前状态对象
- **示例**:
  ```csharp
  var state = deviceButton.CurrentState;
  Console.WriteLine($"当前: {state.DisplayText}");
  ```

### EnableLongPress

- **类型**: `bool`
- **默认值**: `false`
- **说明**: 是否启用长按确认模式
- **示例**:
  ```csharp
  deviceButton.EnableLongPress = true;
  ```

### LongPressTime

- **类型**: `int`
- **默认值**: `1000`
- **说明**: 长按确认所需时间(毫秒)
- **示例**:
  ```csharp
  deviceButton.LongPressTime = 2000; // 2秒长按
  ```

### Icon

- **类型**: `Image`
- **说明**: 按钮图标
- **示例**:
  ```csharp
  deviceButton.Icon = Image.FromFile("power.png");
  ```

## 方法

### SetStates(params DeviceButtonState[] states)

设置按钮状态列表。

**参数**:
- `states`: `DeviceButtonState[]` 状态数组

**示例**:
```csharp
var states = new[]
{
    DeviceButtonState.Stop,
    DeviceButtonState.Start,
    DeviceButtonState.Pause
};
deviceButton.SetStates(states);
```

### NextState()

切换到下一个状态(循环切换)。

**示例**:
```csharp
// 手动切换到下一个状态
deviceButton.NextState();
```

## 事件

### StateChanged

状态切换时触发。

**事件参数**: `EventHandler<DeviceButtonState>`

**示例**:
```csharp
deviceButton.StateChanged += (sender, newState) =>
{
    Console.WriteLine($"切换到: {newState.DisplayText}");
    Console.WriteLine($"颜色: {newState.Color}");
    
    // 根据状态执行不同操作
    if (newState == DeviceButtonState.Start)
    {
        Motor.Start();
    }
    else if (newState == DeviceButtonState.Stop)
    {
        Motor.Stop();
    }
};
```

### ButtonActivated

按钮激活时触发(普通点击或长按完成)。

**事件参数**: `EventHandler`

**示例**:
```csharp
deviceButton.ButtonActivated += (sender, e) =>
{
    // 在长按模式下,只有长按完成才触发
    // 在普通模式下,每次点击都触发
    SendCommandToDevice(deviceButton.CurrentState);
};
```

## 枚举类型

### DisplayMode

按钮显示模式枚举。

```csharp
public enum DisplayMode
{
    ActionMode,   // 动作模式:显示将要执行的动作
    StatusMode    // 状态模式:显示当前运行状态
}
```

**模式说明**:

| 模式 | 当前状态 | 按钮显示 | 按钮颜色 |
|------|---------|---------|---------|
| ActionMode | 停止 | 启动 | 启动状态的颜色(绿色) |
| ActionMode | 运行 | 停止 | 停止状态的颜色(红色) |
| StatusMode | 停止 | 停止 | 停止状态的颜色(红色) |
| StatusMode | 运行 | 运行 | 运行状态的颜色(绿色) |

## 关联类型

### DeviceButtonState

设备按钮状态类。

**主要属性**:
- `DisplayText`: `string` - 显示文本
- `Color`: `Color` - 状态颜色
- `Id`: `string` - 状态唯一标识

**预定义状态**:
```csharp
// 停止状态(红色)
DeviceButtonState.Stop

// 启动状态(绿色)
DeviceButtonState.Start

// 暂停状态(橙色)
DeviceButtonState.Pause

// 自定义状态
var customState = new DeviceButtonState("维护", Color.Gray);
```

**示例**:
```csharp
var state = DeviceButtonState.Start;
Console.WriteLine($"文本: {state.DisplayText}"); // "启动"
Console.WriteLine($"颜色: {state.Color}");       // Green
```

## 交互说明

### 普通点击模式

1. 用户点击按钮
2. 立即切换到下一个状态
3. 触发 `ButtonActivated` 事件
4. 触发 `StateChanged` 事件

### 长按确认模式

1. 用户按下按钮
2. 显示长按进度条(半透明白色覆盖)
3. 持续按住 `LongPressTime` 毫秒
4. 进度条满后松开鼠标
5. 切换到下一个状态
6. 触发 `ButtonActivated` 事件
7. 触发 `StateChanged` 事件

**如果在长按完成前松开或移出按钮**:
- 取消长按
- 不切换状态
- 不触发任何事件

## 视觉效果

### 按钮背景

- 圆角矩形背景
- 使用当前状态的颜色
- 支持按下/悬停高亮效果

### 长按进度条

- 半透明白色覆盖层
- 从左到右填充
- 进度与按压时间成正比

### 图标和文本

- 图标显示在左侧(如设置)
- 文本居中对齐
- 自动适配按钮大小

### 状态颜色

- **按下状态**: 颜色变暗(80%亮度)
- **悬停状态**: 颜色变亮(110%亮度)
- **禁用状态**: 灰色显示

## 完整示例

### 电机控制面板

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
using IndustrialControls.Controls.DeviceButton;
using IndustrialControls.Controls.StatusIndicator;

namespace MotorControl
{
    public partial class MainForm : Form
    {
        private DeviceControlButton motorButton;
        private StatusIndicator motorIndicator;

        public MainForm()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // 状态指示器
            motorIndicator = new StatusIndicator
            {
                Size = new Size(100, 100),
                Location = new Point(50, 30),
                State = IndicatorState.Stopped,
                Label = "电机状态",
                Shape = IndicatorShape.Circle
            };
            this.Controls.Add(motorIndicator);

            // 设备控制按钮
            motorButton = new DeviceControlButton
            {
                Size = new Size(180, 60),
                Location = new Point(30, 160),
                EnableLongPress = true,
                LongPressTime = 1500, // 1.5秒长按确认
                Mode = DisplayMode.ActionMode
            };

            // 设置状态列表
            motorButton.SetStates(
                DeviceButtonState.Stop,
                DeviceButtonState.Start
            );

            // 订阅事件
            motorButton.StateChanged += OnMotorStateChanged;
            motorButton.ButtonActivated += OnMotorButtonActivated;

            this.Controls.Add(motorButton);
        }

        private void OnMotorStateChanged(object sender, DeviceButtonState newState)
        {
            Console.WriteLine($"电机状态变更: {newState.DisplayText}");
            
            // 更新指示器
            if (newState == DeviceButtonState.Start)
            {
                motorIndicator.State = IndicatorState.Running;
            }
            else if (newState == DeviceButtonState.Stop)
            {
                motorIndicator.State = IndicatorState.Stopped;
            }
        }

        private void OnMotorButtonActivated(object sender, EventArgs e)
        {
            // 执行实际控制命令
            var state = motorButton.CurrentState;
            
            if (state == DeviceButtonState.Start)
            {
                StartMotor();
            }
            else if (state == DeviceButtonState.Stop)
            {
                StopMotor();
            }
        }

        private void StartMotor()
        {
            try
            {
                // 发送启动命令到PLC或设备
                Console.WriteLine("发送启动命令...");
                
                // 模拟成功
                MessageBox.Show("电机启动成功!", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // 回滚按钮状态
                motorButton.NextState(); // 切回停止
            }
        }

        private void StopMotor()
        {
            try
            {
                // 发送停止命令
                Console.WriteLine("发送停止命令...");
                
                MessageBox.Show("电机已停止", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"停止失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
```

### 多状态设备控制

```csharp
// 定义多状态:停止 -> 运行 -> 暂停 -> 维护
var multiStateButton = new DeviceControlButton
{
    Size = new Size(200, 60),
    EnableLongPress = true,
    LongPressTime = 2000,
    Mode = DisplayMode.StatusMode
};

multiStateButton.SetStates(
    new DeviceButtonState("停止", Color.Red),
    new DeviceButtonState("运行", Color.Green),
    new DeviceButtonState("暂停", Color.Orange),
    new DeviceButtonState("维护", Color.Gray)
);

multiStateButton.StateChanged += (sender, state) =>
{
    switch (state.DisplayText)
    {
        case "停止":
            Device.Stop();
            break;
        case "运行":
            Device.Run();
            break;
        case "暂停":
            Device.Pause();
            break;
        case "维护":
            Device.EnterMaintenanceMode();
            break;
    }
};
```

## 注意事项

1. **长按确认**: 长按模式下,只有长按完成才切换状态
2. **状态循环**: 调用 `NextState()` 会循环切换状态
3. **线程安全**: 可在后台线程修改状态
4. **设计器支持**: 支持Visual Studio设计器拖放
5. **主题适配**: 自动跟随系统主题
6. **禁用状态**: 设置 `Enabled = false` 时按钮变灰,不响应点击

## 最佳实践

1. **长按确认**: 对重要操作(如启停设备)启用长按确认,防止误触
2. **状态同步**: 在 `StateChanged` 事件中同步其他UI控件状态
3. **错误回滚**: 操作失败时回滚按钮状态
4. **颜色语义**: 使用符合工业标准的颜色(红停绿启)
5. **图标辅助**: 添加图标增强可识别性
6. **模式选择**: 根据场景选择 ActionMode 或 StatusMode

## 相关控件

- **StatusIndicator**: 状态指示器,显示设备运行状态
- **CommunicationControl**: 通讯控件,发送控制命令到设备
- **AlarmDisplay**: 报警显示,显示设备报警信息
- **NumericInputBox**: 数值输入框,设置设备参数
