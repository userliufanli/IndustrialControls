# API 使用说明书

> IndustrialControls 控件库完整 API 参考文档

---

## 目录

1. [StatusIndicator（状态指示器）](#1-statusindicator状态指示器)
2. [DeviceControlButton（设备控制按钮）](#2-devicecontrolbutton设备控制按钮)
3. [AlarmDisplay（报警显示）](#3-alarmdisplay报警显示)
4. [NumericInputBox（数值输入框）](#4-numericinputbox数值输入框)
5. [DataInputPanel（数据输入面板）](#5-datainputpanel数据输入面板)
6. [GaugeControl（仪表盘控件）](#6-gaugecontrol仪表盘控件)
7. [IndustrialProgressBar（工业进度条）](#7-industrialprogressbar工业进度条)
8. [TrendChart（趋势图表）](#8-trendchart趋势图表)
9. [CommunicationControl（通信控制）](#9-communicationcontrol通信控制)
10. [VirtualKeyboard（虚拟键盘）](#10-virtualkeyboard虚拟键盘)
11. [ParameterManager（参数管理器）](#11-parametermanager参数管理器)
12. [ThemeManager（主题管理器）](#12-thememanager主题管理器)

---

## 1. StatusIndicator（状态指示器）

状态指示器控件，支持多种颜色状态、闪烁效果和状态历史记录。

### 1.1 枚举类型

#### IndicatorState（指示器状态）

| 值 | 说明 | 颜色 |
|---|---|---|
| `Stopped` | 停止状态 | 灰色 |
| `Running` | 运行状态 | 绿色 |
| `Fault` | 故障状态 | 红色 |
| `Warning` | 警告状态 | 黄色 |
| `Idle` | 空闲状态 | 蓝色 |
| `Custom` | 自定义状态 | 使用 CustomColor |

#### IndicatorShape（指示器形状）

| 值 | 说明 |
|---|---|
| `Circle` | 圆形 |
| `Square` | 正方形 |
| `RoundedRectangle` | 圆角矩形 |

### 1.2 属性

| 属性名 | 类型 | 默认值 | 分类 | 说明 |
|--------|------|--------|------|------|
| `State` | `IndicatorState` | `Stopped` | 状态 | 当前指示器状态 |
| `Shape` | `IndicatorShape` | `Circle` | 外观 | 指示器形状 |
| `EnableBlink` | `bool` | `false` | 行为 | 是否启用闪烁效果 |
| `BlinkInterval` | `int` | `500` | 行为 | 闪烁间隔（毫秒） |
| `CustomColor` | `Color` | `Gray` | 外观 | 自定义状态颜色 |
| `Label` | `string` | `""` | 外观 | 指示器标签文本 |
| `ShowLabel` | `bool` | `true` | 外观 | 是否显示标签 |
| `MaxHistoryCount` | `int` | `100` | 行为 | 最大状态历史记录条数 |
| `History` | `IReadOnlyList<StatusHistoryRecord>` | - | 状态 | 状态历史记录（只读） |

### 1.3 事件

| 事件名 | 参数 | 说明 |
|--------|------|------|
| `StateChanged` | `IndicatorState` | 状态变更时触发 |

### 1.4 方法

| 方法名 | 参数 | 返回值 | 说明 |
|--------|------|--------|------|
| `ClearHistory()` | 无 | `void` | 清除状态历史记录 |

### 1.5 示例

```csharp
var indicator = new StatusIndicator
{
    State = IndicatorState.Running,
    Label = "电机1",
    EnableBlink = true,
    BlinkInterval = 500,
    Size = new Size(60, 80)
};

indicator.StateChanged += (s, state) =>
{
    Console.WriteLine($"状态变更: {state}");
};
```

---

## 2. DeviceControlButton（设备控制按钮）

设备控制按钮，支持多状态切换、长按确认、图标显示。

### 2.1 枚举类型

#### DisplayMode（显示模式）

| 值 | 说明 |
|---|---|
| `ActionMode` | 动作模式：显示将要执行的动作 |
| `StatusMode` | 状态模式：显示设备当前状态 |

### 2.2 属性

| 属性名 | 类型 | 默认值 | 分类 | 说明 |
|--------|------|--------|------|------|
| `Mode` | `DisplayMode` | `ActionMode` | 行为 | 按钮显示模式 |
| `DefaultStateIndex` | `int` | `0` | 行为 | 默认初始状态索引 |
| `ButtonText` | `string` | `"控制"` | 外观 | 按钮显示文本 |
| `CurrentStateIndex` | `int` | `0` | 状态 | 当前状态索引 |
| `CurrentState` | `DeviceButtonState` | `null` | 状态 | 当前状态对象 |
| `EnableLongPress` | `bool` | `false` | 行为 | 是否启用长按确认 |
| `LongPressTime` | `int` | `1000` | 行为 | 长按确认时间（毫秒） |
| `Icon` | `Image` | `null` | 外观 | 按钮图标 |

### 2.3 事件

| 事件名 | 参数 | 说明 |
|--------|------|------|
| `StateChanged` | `DeviceButtonState` | 状态切换时触发 |
| `ButtonActivated` | `EventArgs` | 按钮激活时触发（点击或长按完成） |

### 2.4 方法

| 方法名 | 参数 | 返回值 | 说明 |
|--------|------|--------|------|
| `SetStates(params DeviceButtonState[] states)` | 状态数组 | `void` | 设置按钮状态列表 |
| `NextState()` | 无 | `void` | 切换到下一个状态 |

### 2.5 示例

```csharp
var btn = new DeviceControlButton
{
    Size = new Size(120, 40),
    EnableLongPress = true,
    LongPressTime = 1000
};

btn.SetStates(
    new DeviceButtonState { DisplayText = "启动", Color = Color.Green },
    new DeviceButtonState { DisplayText = "停止", Color = Color.Red },
    new DeviceButtonState { DisplayText = "故障", Color = Color.Orange }
);

btn.StateChanged += (s, state) =>
{
    Console.WriteLine($"当前状态: {state.DisplayText}");
};
```

---

## 3. AlarmDisplay（报警显示）

报警显示控件，支持分级报警、自动排序、确认清除功能。

### 3.1 枚举类型

#### AlarmLevel（报警等级）

| 值 | 说明 | 颜色 |
|---|---|---|
| `Emergency` | 紧急报警 | 红色 |
| `Important` | 重要报警 | 橙色 |
| `General` | 一般报警 | 黄色 |
| `Info` | 信息提示 | 蓝色 |

### 3.2 属性

| 属性名 | 类型 | 默认值 | 分类 | 说明 |
|--------|------|--------|------|------|
| `ItemHeight` | `int` | `32` | 外观 | 每条报警的显示高度 |
| `MaxAlarms` | `int` | `1000` | 行为 | 最大保留报警条数 |
| `AutoScroll` | `bool` | `true` | 行为 | 新报警是否自动滚动到可见 |
| `TotalCount` | `int` | - | 状态 | 报警总数（只读） |
| `UnacknowledgedCount` | `int` | - | 状态 | 未确认报警数（只读） |
| `EmergencyCount` | `int` | - | 状态 | 紧急报警数（只读） |

### 3.3 事件

| 事件名 | 参数 | 说明 |
|--------|------|------|
| `AlarmAcknowledged` | `AlarmItem` | 报警被确认时触发 |
| `AlarmAdded` | `AlarmItem` | 新报警添加时触发 |
| `AlarmCleared` | `EventArgs` | 报警被清除时触发 |

### 3.4 方法

| 方法名 | 参数 | 返回值 | 说明 |
|--------|------|--------|------|
| `AddAlarm(AlarmItem alarm)` | 报警对象 | `void` | 添加报警 |
| `AddAlarm(AlarmLevel level, string message, string source = "")` | 等级、消息、来源 | `void` | 添加报警（简便方法） |
| `AcknowledgeSelected(string user = "")` | 确认用户 | `void` | 确认选中的报警 |
| `AcknowledgeAll(string user = "")` | 确认用户 | `void` | 确认所有报警 |
| `ClearAcknowledged()` | 无 | `void` | 清除已确认的报警 |
| `ClearAll()` | 无 | `void` | 清除所有报警 |
| `ApplyTimeFilter(DateTime? startTime, DateTime? endTime)` | 开始时间、结束时间 | `void` | 应用时间筛选 |
| `ClearTimeFilter()` | 无 | `void` | 清除时间筛选 |

### 3.5 示例

```csharp
var alarmDisplay = new AlarmDisplay
{
    Size = new Size(400, 300),
    AutoScroll = true
};

// 添加报警
alarmDisplay.AddAlarm(AlarmLevel.Emergency, "电机过载", "设备A");
alarmDisplay.AddAlarm(AlarmLevel.Important, "温度过高", "设备B");

// 确认报警
alarmDisplay.AcknowledgeSelected();
alarmDisplay.AcknowledgeAll();

// 清除报警
alarmDisplay.ClearAcknowledged();
```

---

## 4. NumericInputBox（数值输入框）

数值输入框，支持范围验证、步长调整、格式化显示。

### 4.1 属性

| 属性名 | 类型 | 默认值 | 分类 | 说明 |
|--------|------|--------|------|------|
| `Value` | `decimal` | `0` | 状态 | 当前数值 |
| `Minimum` | `decimal` | `0` | 行为 | 最小值 |
| `Maximum` | `decimal` | `100` | 行为 | 最大值 |
| `Step` | `decimal` | `1` | 行为 | 步长 |
| `DecimalPlaces` | `int` | `0` | 外观 | 小数位数 |
| `Unit` | `string` | `""` | 外观 | 单位文本 |
| `ShowUpDown` | `bool` | `true` | 外观 | 是否显示上下按钮 |
| `AllowNegative` | `bool` | `false` | 行为 | 是否允许负数 |

### 4.2 事件

| 事件名 | 参数 | 说明 |
|--------|------|------|
| `ValueChanged` | `decimal` | 值变更时触发 |
| `ValidationFailed` | `string` | 验证失败时触发 |

### 4.3 示例

```csharp
var inputBox = new NumericInputBox
{
    Size = new Size(150, 30),
    Minimum = 0,
    Maximum = 1000,
    Step = 10,
    DecimalPlaces = 2,
    Unit = "rpm"
};

inputBox.ValueChanged += (s, value) =>
{
    Console.WriteLine($"当前值: {value} rpm");
};
```

---

## 5. DataInputPanel（数据输入面板）

数据输入面板，支持多个输入项的分组管理。

### 5.1 属性

| 属性名 | 类型 | 默认值 | 分类 | 说明 |
|--------|------|--------|------|------|
| `Title` | `string` | `""` | 外观 | 面板标题 |
| `ShowTitle` | `bool` | `true` | 外观 | 是否显示标题 |
| `InputItems` | `List<InputItem>` | - | 状态 | 输入项列表 |

### 5.2 方法

| 方法名 | 参数 | 返回值 | 说明 |
|--------|------|--------|------|
| `AddInputItem(InputItem item)` | 输入项 | `void` | 添加输入项 |
| `RemoveInputItem(string name)` | 名称 | `bool` | 移除输入项 |
| `GetInputValue(string name)` | 名称 | `object` | 获取输入值 |
| `SetInputValue(string name, object value)` | 名称、值 | `void` | 设置输入值 |
| `ValidateAll()` | 无 | `bool` | 验证所有输入项 |

### 5.3 示例

```csharp
var panel = new DataInputPanel
{
    Size = new Size(300, 200),
    Title = "参数设置"
};

panel.AddInputItem(new InputItem
{
    Name = "Temperature",
    Label = "温度",
    Type = InputType.Numeric,
    MinValue = 0,
    MaxValue = 100,
    DefaultValue = 25
});

panel.AddInputItem(new InputItem
{
    Name = "Enabled",
    Label = "启用",
    Type = InputType.Boolean,
    DefaultValue = true
});
```

---

## 6. GaugeControl（仪表盘控件）

仪表盘控件，支持指针显示、刻度标注、范围指示。

### 6.1 属性

| 属性名 | 类型 | 默认值 | 分类 | 说明 |
|--------|------|--------|------|------|
| `Value` | `decimal` | `0` | 状态 | 当前值 |
| `Minimum` | `decimal` | `0` | 行为 | 最小值 |
| `Maximum` | `decimal` | `100` | 行为 | 最大值 |
| `MajorTickCount` | `int` | `10` | 外观 | 主刻度数量 |
| `MinorTickCount` | `int` | `5` | 外观 | 每主刻度间的小刻度数 |
| `ShowScale` | `bool` | `true` | 外观 | 是否显示刻度 |
| `ShowValue` | `bool` | `true` | 外观 | 是否显示数值 |
| `Unit` | `string` | `""` | 外观 | 单位文本 |
| `NeedleColor` | `Color` | `Red` | 外观 | 指针颜色 |

### 6.2 示例

```csharp
var gauge = new GaugeControl
{
    Size = new Size(200, 200),
    Minimum = 0,
    Maximum = 200,
    Unit = "°C",
    NeedleColor = Color.Red
};

gauge.Value = 85.5m;
```

---

## 7. IndustrialProgressBar（工业进度条）

工业风格进度条，支持分段显示、状态指示。

### 7.1 属性

| 属性名 | 类型 | 默认值 | 分类 | 说明 |
|--------|------|--------|------|------|
| `Value` | `int` | `0` | 状态 | 当前进度值 |
| `Minimum` | `int` | `0` | 行为 | 最小值 |
| `Maximum` | `int` | `100` | 行为 | 最大值 |
| `ShowPercentage` | `bool` | `true` | 外观 | 是否显示百分比 |
| `Segments` | `int` | `10` | 外观 | 分段数量 |
| `SegmentGap` | `int` | `2` | 外观 | 分段间距（像素） |

### 7.2 示例

```csharp
var progressBar = new IndustrialProgressBar
{
    Size = new Size(200, 30),
    Maximum = 100,
    Segments = 10,
    ShowPercentage = true
};

progressBar.Value = 75;
```

---

## 8. TrendChart（趋势图表）

趋势图表控件，支持实时数据显示、历史回溯、缩放操作。

### 8.1 属性

| 属性名 | 类型 | 默认值 | 分类 | 说明 |
|--------|------|--------|------|------|
| `DataSource` | `DataBuffer` | - | 状态 | 数据源 |
| `MaxPoints` | `int` | `1000` | 行为 | 最大数据点数 |
| `ShowGrid` | `bool` | `true` | 外观 | 是否显示网格 |
| `ShowLegend` | `bool` | `true` | 外观 | 是否显示图例 |
| `LineColor` | `Color` | `Blue` | 外观 | 线条颜色 |
| `AutoScaleY` | `bool` | `true` | 行为 | 是否自动缩放Y轴 |

### 8.2 方法

| 方法名 | 参数 | 返回值 | 说明 |
|--------|------|--------|------|
| `AddPoint(DateTime time, double value)` | 时间、值 | `void` | 添加数据点 |
| `Clear()` | 无 | `void` | 清除所有数据 |
| `ZoomIn()` | 无 | `void` | 放大 |
| `ZoomOut()` | 无 | `void` | 缩小 |
| `ResetZoom()` | 无 | `void` | 重置缩放 |

### 8.3 示例

```csharp
var chart = new TrendChart
{
    Size = new Size(400, 200),
    MaxPoints = 500,
    LineColor = Color.Green
};

// 添加数据点
chart.AddPoint(DateTime.Now, 25.5);
chart.AddPoint(DateTime.Now.AddSeconds(1), 26.3);
chart.AddPoint(DateTime.Now.AddSeconds(2), 25.8);
```

---

## 9. CommunicationControl（通信控制）

通信控制控件，支持 TCP、UDP、串口等多种通信方式。

### 9.1 枚举类型

#### CommunicationMode（通信模式）

| 值 | 说明 |
|---|---|
| `TcpClient` | TCP 客户端 |
| `TcpServer` | TCP 服务器 |
| `Udp` | UDP 通信 |
| `SerialPort` | 串口通信 |

#### CommunicationState（通信状态）

| 值 | 说明 |
|---|---|
| `Disconnected` | 未连接 |
| `Connecting` | 连接中 |
| `Connected` | 已连接 |
| `Error` | 错误 |

### 9.2 属性

| 属性名 | 类型 | 默认值 | 分类 | 说明 |
|--------|------|--------|------|------|
| `CommunicationMode` | `CommunicationMode` | `TcpClient` | 行为 | 当前通信模式 |
| `Host` | `string` | `"127.0.0.1"` | 配置 | 服务器地址 |
| `Port` | `int` | `8080` | 配置 | 端口号 |
| `SerialPortName` | `string` | `"COM1"` | 配置 | 串口名称 |
| `BaudRate` | `int` | `9600` | 配置 | 波特率 |
| `State` | `CommunicationState` | `Disconnected` | 状态 | 当前通信状态 |
| `AutoReconnect` | `bool` | `true` | 行为 | 是否自动重连 |
| `ReconnectInterval` | `int` | `3000` | 行为 | 重连间隔（毫秒） |

### 9.3 事件

| 事件名 | 参数 | 说明 |
|--------|------|------|
| `StateChanged` | `CommunicationState` | 状态变更时触发 |
| `DataReceived` | `byte[]` | 收到数据时触发 |
| `ErrorOccurred` | `Exception` | 发生错误时触发 |

### 9.4 方法

| 方法名 | 参数 | 返回值 | 说明 |
|--------|------|--------|------|
| `Connect()` | 无 | `void` | 连接 |
| `Disconnect()` | 无 | `void` | 断开连接 |
| `SendData(byte[] data)` | 数据 | `void` | 发送数据 |
| `SendData(string text)` | 文本 | `void` | 发送文本数据 |

### 9.5 示例

```csharp
var commControl = new CommunicationControl
{
    Size = new Size(300, 150),
    CommunicationMode = CommunicationMode.TcpClient,
    Host = "192.168.1.100",
    Port = 502,
    AutoReconnect = true
};

commControl.StateChanged += (s, state) =>
{
    Console.WriteLine($"通信状态: {state}");
};

commControl.DataReceived += (s, data) =>
{
    Console.WriteLine($"收到数据: {BitConverter.ToString(data)}");
};

commControl.Connect();
commControl.SendData(new byte[] { 0x01, 0x02, 0x03 });
```

---

## 10. VirtualKeyboard（虚拟键盘）

虚拟键盘模块：QWERTY / 数字键盘布局、`VirtualKeyboardForm` 浮动窗体、全局焦点轮询（`VirtualKeyboardManager`）。按键通过事件传出逻辑键名（如 `Backspace`、`Enter`、普通字符），由宿主将字符写入目标控件。

### 10.1 枚举类型

#### KeyboardLayoutMode

| 值 | 说明 |
|---|---|
| `QWERTY` | 字母主布局 |
| `NumberPad` | 数字布局 |

### 10.2 VirtualKeyboardPanel

| 成员 | 说明 |
|------|------|
| `LayoutMode` | 当前布局 |
| `Config` | `KeyboardConfig`（按键宽高、间距、字体等） |
| 事件 `KeyInput` | `EventHandler<string>`，参数为逻辑键名 |

### 10.3 VirtualKeyboardForm

| 成员 | 说明 |
|------|------|
| `KeyboardPanel` | 内部 `VirtualKeyboardPanel`（勿依赖 `Controls` 下标） |
| `SetTargetControl(Control)` | 优先将输入发往该控件（句柄有效且所在窗体未释放时生效） |
| `ShowAt(Point)` | 在屏幕坐标附近显示（不模态） |
| 事件 `KeyInput` | `EventHandler<VirtualKeyboardKeyInputEventArgs>`，含 `Key` 与解析后的 `Target` 控件 |

### 10.4 VirtualKeyboardManager（静态）

| 成员 | 说明 |
|------|------|
| `Initialize()` / `Initialize(int intervalMs)` | 启动焦点轮询（50–500ms） |
| `GetActiveControl()` | 当前线程下推断的输入焦点控件（排除 `VirtualKeyboardForm` 内控件） |
| `MonitorInterval` | 轮询间隔 |
| `Dispose()` | 停止计时器 |

### 10.5 VirtualKeyboardKeyInputEventArgs

| 属性 | 说明 |
|------|------|
| `Key` | 逻辑键名 |
| `Target` | 窗体解析后的建议目标控件（`SetTargetControl` 或焦点管理器） |

### 10.6 示例

```csharp
// 程序入口或主窗体 Load 中调用一次
VirtualKeyboardManager.Initialize();

var form = new VirtualKeyboardForm();
form.KeyInput += (s, e) =>
{
    if (e.Target is TextBox tb)
    {
        // 根据 e.Key 自行写入，或使用 Demo 中 VirtualKeyboardPage 的完整处理逻辑
    }
};
form.SetTargetControl(myTextBox);
form.ShowAt(myTextBox.PointToScreen(new Point(0, myTextBox.Height + 4)));
```

---

## 11. ParameterManager（参数管理器）

本地参数管理器，统一管理应用程序的持久化参数。

### 11.1 枚举类型

#### ParameterChangeType（参数变更类型）

| 值 | 说明 |
|---|---|
| `Added` | 参数添加 |
| `Modified` | 参数修改 |
| `Deleted` | 参数删除 |
| `GroupDeleted` | 分组删除 |
| `Reloaded` | 配置重载 |

### 11.2 属性

| 属性名 | 类型 | 默认值 | 分类 | 说明 |
|--------|------|--------|------|------|
| `ConfigFilePath` | `string` | - | 配置 | 配置文件路径 |
| `EnableHotReload` | `bool` | `true` | 行为 | 是否启用热更新监控 |

### 11.3 事件

| 事件名 | 参数 | 说明 |
|--------|------|------|
| `ParameterChanged` | `ParameterChangedEventArgs` | 参数变更时触发 |
| `SaveFailed` | `Exception` | 保存失败时触发 |
| `LoadFailed` | `Exception` | 加载失败时触发 |

### 11.4 方法

| 方法名 | 参数 | 返回值 | 说明 |
|--------|------|--------|------|
| `GetValue<T>(string groupName, string key, T defaultValue)` | 分组、键、默认值 | `T` | 获取参数值 |
| `SetValue<T>(string groupName, string key, T value)` | 分组、键、值 | `void` | 设置参数值 |
| `Delete(string groupName, string key)` | 分组、键 | `bool` | 删除参数 |
| `DeleteGroup(string groupName)` | 分组 | `bool` | 删除分组 |
| `Contains(string groupName, string key)` | 分组、键 | `bool` | 检查参数是否存在 |
| `GetGroupNames()` | 无 | `IEnumerable<string>` | 获取所有分组名称 |
| `GetKeys(string groupName)` | 分组 | `IEnumerable<string>` | 获取分组下的所有键 |
| `LoadFromFile()` | 无 | `void` | 从文件加载配置 |
| `SaveToFile()` | 无 | `void` | 保存配置到文件 |

### 11.5 示例

```csharp
var paramManager = new ParameterManager("config/appsettings.json");

// 设置参数
paramManager.SetValue("Communication", "ServerIP", "192.168.1.100");
paramManager.SetValue("Communication", "Port", 8080);
paramManager.SetValue("System", "AutoStart", true);

// 获取参数
string ip = paramManager.GetValue("Communication", "ServerIP", "127.0.0.1");
int port = paramManager.GetValue("Communication", "Port", 80);

// 监听变更
paramManager.ParameterChanged += (s, e) =>
{
    Console.WriteLine($"{e.GroupName}.{e.Key} = {e.Value} [{e.ChangeType}]");
};
```

---

## 12. ThemeManager（主题管理器）

主题管理器，支持主题切换、颜色管理。

### 12.1 属性

| 属性名 | 类型 | 默认值 | 分类 | 说明 |
|--------|------|--------|------|------|
| `Instance` | `ThemeManager` | - | 静态 | 单例实例 |
| `CurrentTheme` | `ITheme` | `FlatLightTheme` | 状态 | 当前主题 |

### 12.2 方法

| 方法名 | 参数 | 返回值 | 说明 |
|--------|------|--------|------|
| `ApplyTheme<T>()` | 主题类型 | `void` | 应用指定主题 |
| `RegisterTheme(Type themeType)` | 主题类型 | `void` | 注册自定义主题 |
| `GetRegisteredThemes()` | 无 | `IEnumerable<Type>` | 获取已注册的主题列表 |

### 12.3 内置主题

| 主题类 | 说明 |
|--------|------|
| `FlatLightTheme` | 亮色扁平化主题 |
| `FlatDarkTheme` | 暗色扁平化主题 |

### 12.4 ThemeColors（主题颜色）

| 属性名 | 类型 | 说明 |
|--------|------|------|
| `Primary` | `Color` | 主色调 |
| `Secondary` | `Color` | 次要颜色 |
| `Background` | `Color` | 背景色 |
| `Surface` | `Color` | 表面色 |
| `SurfaceVariant` | `Color` | 表面变体色 |
| `TextPrimary` | `Color` | 主要文本色 |
| `TextSecondary` | `Color` | 次要文本色 |
| `TextDisabled` | `Color` | 禁用文本色 |
| `TextOnPrimary` | `Color` | 主色调上的文本色 |
| `Border` | `Color` | 边框色 |
| `BorderLight` | `Color` | 浅色边框 |
| `Running` | `Color` | 运行状态色 |
| `Stopped` | `Color` | 停止状态色 |
| `Fault` | `Color` | 故障状态色 |
| `Idle` | `Color` | 空闲状态色 |
| `Success` | `Color` | 成功状态色 |
| `AlarmEmergency` | `Color` | 紧急报警色 |
| `AlarmImportant` | `Color` | 重要报警色 |
| `AlarmGeneral` | `Color` | 一般报警色 |
| `AlarmInfo` | `Color` | 信息报警色 |
| `Disabled` | `Color` | 禁用状态色 |

### 12.5 示例

```csharp
// 应用暗色主题
ThemeManager.Instance.ApplyTheme<FlatDarkTheme>();

// 获取当前主题颜色
var colors = ThemeManager.Instance.CurrentTheme.Colors;
button.BackColor = colors.Primary;
button.ForeColor = colors.TextOnPrimary;

// 切换回亮色主题
ThemeManager.Instance.ApplyTheme<FlatLightTheme>();
```

---

## 附录：控件命名空间对照

| 控件 | 命名空间 |
|------|----------|
| StatusIndicator | `IndustrialControls.Controls.StatusIndicator` |
| DeviceControlButton | `IndustrialControls.Controls.DeviceButton` |
| AlarmDisplay | `IndustrialControls.Controls.Alarm` |
| NumericInputBox | `IndustrialControls.Controls.DataInput` |
| DataInputPanel | `IndustrialControls.Controls.DataInput` |
| GaugeControl | `IndustrialControls.Controls.DataVisualization` |
| IndustrialProgressBar | `IndustrialControls.Controls.DataVisualization` |
| TrendChart | `IndustrialControls.Controls.DataVisualization` |
| CommunicationControl | `IndustrialControls.Controls.Communication` |
| CommunicationManager | `IndustrialControls.Controls.Communication` |
| VirtualKeyboardPanel | `IndustrialControls.Controls.VirtualKeyboard` |
| VirtualKeyboardForm | `IndustrialControls.Controls.VirtualKeyboard` |
| VirtualKeyboardManager | `IndustrialControls.Controls.VirtualKeyboard` |
| ParameterManager | `IndustrialControls.Core` |
| ThemeManager | `IndustrialControls.Theme` |