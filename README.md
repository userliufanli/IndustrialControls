# IndustrialControls

基于 **.NET Framework 4.8** 的 **WinForms** 工业上位机控件库：状态指示、报警、通信、趋势与仪表盘、虚拟键盘、主题与参数持久化等。仓库含类库 `src/IndustrialControls`、演示 `samples/IndustrialControls.Demo`、模板 `samples/IndustrialControls.Template`。

**本文档为仓库内唯一的 Markdown 说明。** WinForms 设计器约定摘要见 `docs/.cursorrules`。与源码不一致时以 `src/IndustrialControls` 为准。

---

## 目录

- [仓库概览](#仓库概览)
- [使用手册](#使用手册)
- [快速入门示例代码](#快速入门示例代码)
- [ParameterManager 进阶](#parametermanager-进阶)
- [附录控件与命名空间](#附录控件与命名空间)
- [版本与反馈](#版本与反馈)

---

## 仓库概览

### 技术栈与运行环境

| 项目 | 说明 |
|------|------|
| 目标框架 | .NET Framework 4.8 |
| UI | Windows Forms |
| 语言 | C# |
| 推荐环境 | Visual Studio 2019+（建议 2022），Windows 10 / 11 |

类库为传统 `csproj`，建议用 Visual Studio 打开 `IndustrialControls.sln` 生成；含 `.resx` 的工程以 VS 内编译为准。

### 目录说明

| 路径 | 作用 |
|------|------|
| `IndustrialControls.sln` | 解决方案 |
| `src/IndustrialControls/` | 类库：Controls、Core、Theme、Utilities、Automation 等 |
| `samples/IndustrialControls.Demo/` | 控件与能力演示 |
| `samples/IndustrialControls.Template/` | 多页面上位机模板 |
| `docs/.cursorrules` | 设计器协作约定（摘要） |

### 五分钟上手

1. VS 打开 `IndustrialControls.sln`。  
2. 将 **IndustrialControls.Demo** 设为启动项目并运行。  
3. 需要页面骨架时改用 **IndustrialControls.Template**。

### 在你自己的工程中引用

对宿主 WinForms（.NET 4.8）项目添加 **项目引用** → `src/IndustrialControls/IndustrialControls.csproj`（或引用 `IndustrialControls.dll` 及相同依赖）。控件在工具箱或代码中使用；业务逻辑写在非 Designer 的 `partial` 类中。

### 功能模块（摘要）

- **界面与状态**：状态指示器、设备按钮、报警列表。  
- **数据**：数值框、验证文本框、标签+数值行、仪表盘、工业进度条、多通道趋势图。  
- **通信**：TCP 客户端/服务端、串口（`CommunicationMode`，以源码为准）。  
- **人机**：虚拟键盘。  
- **登录与主题**：`FlatLoginControl`、`LoginUserManagementForm`、`ThemeManager`。  
- **参数**：`ParameterManager`、`ParameterAccessor`、`ParameterSection`。

---

## 使用手册

### 1. 概述

**IndustrialControls** 提供工业常用 UI、通信与参数基础设施。推荐用静态类（如 Demo 中 `AppParameters`）封装 `ParameterAccessor` / `ParameterSection`，业务代码只访问强类型属性。

### 2. 环境与引用

| 项目 | 要求 |
|------|------|
| 操作系统 | Windows 10 / 11 |
| 目标框架 | .NET Framework 4.8 |
| 推荐 IDE | Visual Studio 2022（.NET 桌面开发） |

引用方式：**项目引用** 或 **程序集引用**（依赖见类库 `.csproj`）。

```csharp
using IndustrialControls.Core;
// using IndustrialControls.Controls.StatusIndicator;
// using IndustrialControls.Theme;
```

### 3. 项目与程序集结构

| 路径 | 说明 |
|------|------|
| `src/IndustrialControls/Controls/` | 各控件子目录 |
| `src/IndustrialControls/Core/` | `ParameterManager`、`ParameterAccessor`、`ParameterSection`、`BaseControl` 等 |
| `src/IndustrialControls/Theme/` | 主题 |
| `src/IndustrialControls/Utilities/` | 工具类 |
| `samples/IndustrialControls.Demo/` | 演示程序 |

### 4. 参数与配置（推荐用法）

**推荐**：`ParameterAccessor.Default` → 默认 `%AppData%\IndustrialControls\parameters.json`（`GetDefaultConfigPath()`）；`ParameterAccessor.Section("分组")` → `ParameterSection`，用 `Get`/`Set` 封装字段；其它文件用 `ParameterAccessor.Register("逻辑名", path)` 再 `.Section(...)`。

```csharp
public static class AppParameters
{
    public static ParameterManager Default => ParameterAccessor.Default;
    private static readonly ParameterSection _app = ParameterAccessor.Section("App");

    public static class App
    {
        public static string Language
        {
            get => _app.Get("Language", "zh-CN");
            set => _app.Set("Language", value);
        }
    }

    public static ParameterManager CommConfig => ParameterAccessor.Register(
        "Communication",
        System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "communication.json"));

    private static readonly ParameterSection _comm = CommConfig.Section("CommConfig");

    public static class Comm
    {
        public static string TcpIp
        {
            get => _comm.Get("TcpIp", "127.0.0.1");
            set => _comm.Set("TcpIp", value);
        }
    }
}
```

窗体中：`AppParameters.App.Language = "zh-CN";`

**直接使用 `ParameterManager`**：与内置控件共享同一路径单例、脚本工具等；同一路径勿多实例互盖。

### 5. 参数系统 API 参考

**`ParameterAccessor`**：`Default`、`GetDefaultConfigPath()`、`Section(group)`、`Register`、`Resolve`/`TryResolve`/`Remove`、`Get<T>`/`Set<T>`/`SetBatch`。

**`ParameterSection`**：`Get<T>`、`Set<T>`、`Contains`、`TryGet`、`GetOrSet`、`Delete`、`SetBatch`/`GetBatch`。

**`ParameterManager`**：`GetValue`/`SetValue`、`Delete`/`DeleteGroup`、`GetGroupNames`/`GetKeys`、`LoadFromFile`/`SaveToFile`、`SuspendAutoSave`/`ResumeAutoSave`、`Section`、`EnableHotReload`、`ParameterChanged`/`SaveFailed`/`LoadFailed`、`Dispose`。设计器模式下会跳过文件与监视器。

### 6. JSON 配置文件说明

根对象含版本、时间、**分组**；键值带类型元数据（`JavaScriptSerializer`）。保存为临时文件替换并尝试 `.bak`。读入数值可能为 `long`/`double`，管理器会转换到目标类型。勿手改破坏结构；外部保存可触发热重载。

### 7. 事件、热更新与线程安全

- **`ParameterChanged`**：含 `GroupName`、`Key`、`Value`、`ChangeType`、`Timestamp`；非 UI 线程订阅时应用 `Invoke`/`BeginInvoke`。  
- **热更新**：`EnableHotReload` 为 true 时监视目录变更，防抖后 `LoadFromFile()`，发出 `Reloaded`。  
- **防抖**：`SetValue` 后短时合并写盘；`SaveToFile()` 立即保存。  
- **线程安全**：内部读写锁；界面更新仍在 UI 线程为宜。

### 8. WinForms 窗体开发约定

见 `docs/.cursorrules`：设计器维护 `partial` + `Designer.cs`；禁止在运行时 `new` 可见控件并挂到窗体（`InitializeComponent` 内除外）。

### 9. 演示工程 IndustrialControls.Demo

展示主要控件、`AppParameters`、`ParameterManagerTestPage`（参数编辑与日志）。在 VS 中设为启动项目生成运行；命令行 `dotnet msbuild` 可能需额外资源选项，**以 VS 为准**。

### 10. 控件与主题一览

各控件说明见文末 **[附录控件与命名空间](#附录控件与命名空间)**。

| 区域 | 典型内容 |
|------|-----------|
| `Controls/StatusIndicator` | 状态指示器 |
| `Controls/DeviceButton` | 设备按钮 |
| `Controls/Alarm` | 报警 |
| `Controls/Communication` | 通信 |
| `Controls/DataInput` | 数据输入与校验 |
| `Controls/DataVisualization` | 趋势、仪表、进度条 |
| `Controls/VirtualKeyboard` | 虚拟键盘 |
| `Controls/Login` | 登录与用户管理 |
| `Theme` | `ThemeManager` |

### 11. 常见问题

1. **默认配置路径**：`ParameterAccessor.GetDefaultConfigPath()`，通常为 `%AppData%\IndustrialControls\parameters.json`。  
2. **参数变更 UI 不刷新**：订阅对应 `ParameterManager.ParameterChanged` 或 `Reloaded` 后重绑。  
3. **`Register` 重复路径**：同一逻辑名单例，以首次注册为准，路径应一致。  
4. **设计器报错**：确认目标框架 4.8 与引用一致（如 `System.Web.Extensions`）。  
5. **旧资料冲突**：以本 README 与当前源码为准；核心类型为 `ParameterManager`、`ParameterAccessor`、`ParameterSection`。

---

## 快速入门示例代码

### 环境

VS 2022、.NET Framework 4.8；宿主项目 **项目引用** `IndustrialControls.csproj`。

### 状态指示器

```csharp
using IndustrialControls.Controls.StatusIndicator;

var indicator = new StatusIndicator
{
    State = IndicatorState.Running,
    Label = "电机1",
    EnableBlink = true,
    BlinkInterval = 500,
    Shape = IndicatorShape.Circle,
    Size = new Size(60, 80)
};
indicator.StateChanged += (s, state) => { };
```

### 设备按钮

```csharp
using IndustrialControls.Controls.DeviceButton;

var btn = new DeviceControlButton { Size = new Size(120, 40), EnableLongPress = true, LongPressTime = 1000 };
btn.SetStates(
    new DeviceButtonState { DisplayText = "运行", Color = Color.Green },
    new DeviceButtonState { DisplayText = "停止", Color = Color.Red });
btn.StateChanged += (s, state) => { };
```

### 报警

```csharp
using IndustrialControls.Controls.Alarm;

var alarmDisplay = new AlarmDisplay { Size = new Size(400, 300), AutoScroll = true };
alarmDisplay.AddAlarm(AlarmLevel.Emergency, "电机过载", "设备A");
alarmDisplay.AcknowledgeAll();
```

### 参数（直接 `ParameterManager`）

```csharp
using IndustrialControls.Core;

var pm = new ParameterManager("config/appsettings.json");
pm.SetValue("Communication", "ServerIP", "192.168.1.100");
string ip = pm.GetValue("Communication", "ServerIP", "127.0.0.1");
pm.ParameterChanged += (s, e) => { };
```

### 主题

```csharp
using IndustrialControls.Theme;

ThemeManager.Instance.ApplyTheme<FlatDarkTheme>();
var colors = ThemeManager.Instance.CurrentTheme.Colors;
```

### 通信（异步，以当前控件 API 为准）

```csharp
using IndustrialControls.Controls.Communication;

var comm = new CommunicationControl
{
    Mode = CommunicationMode.Tcp,
    TcpIp = "192.168.1.100",
    TcpPort = 502
};
comm.StateChanged += (s, st) => { };
// await comm.ConnectAsync();
// await comm.SendAsync(new byte[] { 0x01, 0x02 });
```

### 虚拟键盘

```csharp
using IndustrialControls.Controls.VirtualKeyboard;

VirtualKeyboardManager.Initialize();
var vk = new VirtualKeyboardForm();
vk.SetTargetControl(myTextBox);
vk.KeyInput += (s, e) => { /* 按 e.Key / e.Target 写入，或参考 Demo */ };
vk.ShowAt(myTextBox.PointToScreen(new Point(0, myTextBox.Height + 4)));
```

---

## ParameterManager 进阶

### 概述

JSON 持久化、分组、类型转换、读写锁、热重载、防抖保存。

### 基础

```csharp
var paramManager = new ParameterManager(
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "settings.json"));
paramManager.SetValue("Network", "ServerIP", "192.168.1.100");
int port = paramManager.GetValue("Network", "Port", 80);
```

### 变更监听

订阅 `ParameterChanged`，按 `ParameterChangeType`（Added、Modified、Deleted、GroupDeleted、Reloaded）分支处理。

### 删除与枚举

`Delete(group,key)`、`DeleteGroup(group)`、`GetGroupNames()`、`GetKeys(group)`、`Contains`。

### 配置文件格式（示意）

根含 `Version`、`LastModified`、`Groups`；每组含 `Name`、`Parameters` 字典，键下含 `Value`、`Type` 等（以实际序列化结果为准）。

### 最佳实践

单例或 `ParameterAccessor` 管理路径；启动时补默认值；订阅 `SaveFailed`/`LoadFailed`；批量修改用 `SuspendAutoSave`/`ResumeAutoSave`。

### 性能与线程

防抖合并写盘；原子写入（临时文件 + 替换）；同值不写。多线程可调 `Get`/`Set`，UI 更新仍在 UI 线程。

### 常见问题

配置放在可写目录（如 `ApplicationData`）；热重载需目录存在且 `EnableHotReload` 为 true；整数从 JSON 可能为 `long`，用 `Convert.ToInt32` 等安全转换。

---

## 附录控件与命名空间

| 主题 | 命名空间 | 说明 |
|------|----------|------|
| StatusIndicator | `IndustrialControls.Controls.StatusIndicator` | `IndicatorState`、`IndicatorShape`、闪烁与历史 |
| DeviceControlButton | `IndustrialControls.Controls.DeviceButton` | `SetStates`、`DisplayMode`、长按 |
| AlarmDisplay | `IndustrialControls.Controls.Alarm` | `AlarmLevel`、确认与清除 |
| NumericInputBox | `IndustrialControls.Controls.DataInput` | `double` 值、`Step`、`ShowButtons` |
| ValidatedTextBox | `IndustrialControls.Controls.DataInput` | `ValidationPreset`、`ValidationMode` |
| DataInputPanel | `IndustrialControls.Controls.DataInput` | 单行 标签+数值+单位 |
| GaugeControl | `IndustrialControls.Controls.DataVisualization` | 圆弧仪表、阈值 |
| IndustrialProgressBar | `IndustrialControls.Controls.DataVisualization` | 分段、阈值色 |
| TrendChart | `IndustrialControls.Controls.DataVisualization` | `AddChannel`、`AddDataPoint` |
| CommunicationControl / Manager | `IndustrialControls.Controls.Communication` | `Tcp`/`TcpServer`/`Serial`，异步收发 |
| VirtualKeyboard | `IndustrialControls.Controls.VirtualKeyboard` | Panel、Form、`VirtualKeyboardManager` |
| FlatLoginControl 等 | `IndustrialControls.Controls.Login` | 登录、`LoginUserManagementForm` |
| ParameterManager | `IndustrialControls.Core` | 见上文 |
| ThemeManager | `IndustrialControls.Theme` | `FlatLightTheme` / `FlatDarkTheme` |

---

## 版本与反馈

- 程序集版本：`src/IndustrialControls/Properties/AssemblyInfo.cs`。  
- Issue / Pull Request 欢迎。
