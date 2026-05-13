# IndustrialControls 控件库 API 文档总览

## 概述

IndustrialControls 是一个专业的 WinForms 工业自动化控件库,提供完整的上位机开发解决方案。本文档索引列出了所有可用控件及其详细API文档链接。

## 控件分类

### 0. 核心基础设施

| 组件 | 文档链接 | 说明 |
|------|---------|------|
| **ParameterManager** | [查看文档](ParameterManager-API.md) | 参数管理系统,配置文件管理、热更新、多配置文件隔离 |
| **ParameterAccessor** | [查看文档](ParameterManager-API.md#parameteraccessor---静态访问层) | 静态访问层,零配置使用 |
| **ParameterSection** | [查看文档](ParameterManager-API.md#parametersection---分组视图) | 分组视图,简化访问 |

### 1. 报警与状态监控

| 控件 | 文档链接 | 说明 |
|------|---------|------|
| **AlarmDisplay** | [查看文档](AlarmDisplay-API.md) | 报警显示控件,支持分级报警、自动排序、确认清除 |
| **StatusIndicator** | [查看文档](StatusIndicator-API.md) | 状态指示器,支持多种颜色状态、闪烁效果和历史记录 |

### 2. 通讯与数据交换

| 控件 | 文档链接 | 说明 |
|------|---------|------|
| **CommunicationControl** | [查看文档](CommunicationControl-API.md) | 工业通讯控件,支持TCP客户端/服务端/串口三种模式 |

### 3. 数据输入与验证

| 控件 | 文档链接 | 说明 |
|------|---------|------|
| **NumericInputBox** | [查看文档](DataInput-API.md#numericinputbox---数值输入框) | 数值输入框,带上下限验证、步进按钮、单位显示 |
| **ValidatedTextBox** | [查看文档](DataInput-API.md#validatedtextbox---验证文本框) | 验证文本框,支持多种预定义验证规则 |
| **DataInputPanel** | [查看文档](DataInput-API.md#datainputpanel---数据输入面板) | 数据输入面板,集成多个输入字段的卡片式面板 |

### 4. 数据可视化

| 控件 | 文档链接 | 说明 |
|------|---------|------|
| **TrendChart** | [查看文档](TrendChart-API.md) | 实时趋势图,支持多通道数据曲线和自动缩放 |

### 5. 设备控制

| 控件 | 文档链接 | 说明 |
|------|---------|------|
| **DeviceControlButton** | [查看文档](DeviceControlButton-API.md) | 设备控制按钮,支持多状态切换、长按确认 |

### 6. 用户认证

| 控件 | 文档链接 | 说明 |
|------|---------|------|
| **FlatLoginControl** | [查看文档](FlatLoginControl-API.md) | 登录控件,支持用户认证和参数持久化 |
| **LoginUserManagementForm** | [查看文档](FlatLoginControl-API.md) | 用户管理窗体,用户增删改查 |

### 7. 虚拟键盘

| 控件 | 文档链接 | 说明 |
|------|---------|------|
| **VirtualKeyboardPanel** | [查看文档](VirtualKeyboard-API.md#virtualkeyboardpanel---虚拟键盘面板) | 虚拟键盘面板,可嵌入窗体 |
| **VirtualKeyboardForm** | [查看文档](VirtualKeyboard-API.md#virtualkeyboardform---虚拟键盘窗体) | 虚拟键盘窗体,独立浮动显示 |
| **VirtualKeyboardManager** | [查看文档](VirtualKeyboard-API.md#virtualkeyboardmanager---全局键盘管理器) | 全局键盘管理器,跨窗体焦点管理 |

---

## 快速入门

### 1. 安装与配置

控件库已通过项目引用方式集成,无需额外安装。

```csharp
// 在代码中引用命名空间
using IndustrialControls.Controls.Alarm;
using IndustrialControls.Controls.Communication;
using IndustrialControls.Controls.DataInput;
using IndustrialControls.Controls.DataVisualization;
using IndustrialControls.Controls.DeviceButton;
using IndustrialControls.Controls.Login;
using IndustrialControls.Controls.StatusIndicator;
using IndustrialControls.Controls.VirtualKeyboard;
```

### 2. 设计器使用

所有控件均支持Visual Studio设计器拖放:

1. 打开Visual Studio设计器
2. 从工具箱拖放控件到窗体
3. 在属性窗口配置控件属性
4. 双击控件添加事件处理

### 3. 代码使用

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
using IndustrialControls.Controls.Alarm;
using IndustrialControls.Controls.Communication;
using IndustrialControls.Controls.StatusIndicator;

namespace QuickStart
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // 1. 创建通讯控件
            var commControl = new CommunicationControl
            {
                Size = new Size(400, 350),
                Location = new Point(10, 10),
                Mode = CommunicationMode.Tcp,
                TcpIp = "192.168.1.100",
                TcpPort = 502,
                AutoConnect = true
            };
            this.Controls.Add(commControl);

            // 2. 创建状态指示器
            var indicator = new StatusIndicator
            {
                Size = new Size(80, 80),
                Location = new Point(420, 10),
                State = IndicatorState.Stopped,
                Label = "通讯状态"
            };
            this.Controls.Add(indicator);

            // 监听通讯状态
            commControl.StateChanged += (sender, state) =>
            {
                indicator.State = state == CommunicationState.Connected
                    ? IndicatorState.Running
                    : IndicatorState.Stopped;
            };

            // 3. 创建报警显示
            var alarmDisplay = new AlarmDisplay
            {
                Size = new Size(400, 300),
                Location = new Point(10, 370),
                MaxAlarms = 500
            };
            this.Controls.Add(alarmDisplay);

            // 接收数据时添加报警
            commControl.StringReceived += (sender, data) =>
            {
                if (data.Contains("ALARM"))
                {
                    alarmDisplay.AddAlarm(
                        AlarmLevel.Important,
                        "设备报警",
                        "通讯数据"
                    );
                }
            };
        }
    }
}
```

---

## 核心特性

### 主题系统

所有控件自动适配主题系统:

- **FlatDark**: 深色主题
- **FlatLight**: 浅色主题

```csharp
using IndustrialControls.Theme;

// 切换主题
ThemeManager.Instance.SetTheme(new FlatDarkTheme());
ThemeManager.Instance.SetTheme(new FlatLightTheme());
```

### 参数管理

控件支持自动参数持久化:

```csharp
using IndustrialControls.Core;

// 通讯控件自动保存配置
commControl.AutoPersist = true;
commControl.PersistKey = "MainComm";

// 登录控件使用独立配置
loginControl.ParameterManager = new ParameterManager("config/auth.json");
loginControl.ParameterGroupName = "SystemUsers";
```

### 设计器支持

所有控件完全支持Visual Studio设计器:

- 拖放放置
- 属性配置
- 事件绑定
- 实时预览

---

## 控件关系图

```
┌─────────────────────────────────────────────────────────┐
│                   工业自动化系统                          │
├─────────────────────────────────────────────────────────┤
│                                                         │
│  ┌──────────────┐      ┌──────────────┐                │
│  │ Communication │─────>│  TrendChart  │                │
│  │   Control     │      │              │                │
│  └──────┬───────┘      └──────────────┘                │
│         │                                                │
│         ├──> ┌──────────────┐                           │
│         │    │     Alarm    │                           │
│         │    │   Display    │                           │
│         │    └──────────────┘                           │
│         │                                                │
│         ├──> ┌──────────────┐                           │
│         │    │   Status     │                           │
│         │    │  Indicator   │                           │
│         │    └──────────────┘                           │
│         │                                                │
│         └──> ┌──────────────┐                           │
│              │   Device     │                           │
│              │ ControlButton│                           │
│              └──────────────┘                           │
│                                                         │
│  ┌──────────────┐      ┌──────────────┐                │
│  │    Flat      │─────>│  Virtual     │                │
│  │  LoginControl│      │  Keyboard    │                │
│  └──────────────┘      └──────────────┘                │
│                                                         │
│  ┌──────────────────────────────────┐                  │
│  │     DataInput Panel              │                  │
│  │  ┌─────────┐ ┌───────────────┐  │                  │
│  │  │ Numeric │ │ ValidatedText │  │                  │
│  │  │  Input  │ │     Box       │  │                  │
│  │  └─────────┘ └───────────────┘  │                  │
│  └──────────────────────────────────┘                  │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

---

## 典型应用场景

### 1. 设备监控系统

```csharp
// 通讯控件 + 状态指示器 + 报警显示
var commControl = new CommunicationControl();
var indicator = new StatusIndicator();
var alarmDisplay = new AlarmDisplay();

// 连接状态 -> 指示器
commControl.StateChanged += (s, state) => {
    indicator.State = state == CommunicationState.Connected 
        ? IndicatorState.Running 
        : IndicatorState.Fault;
};

// 接收数据 -> 报警判断
commControl.StringReceived += (s, data) => {
    if (IsAlarm(data))
        alarmDisplay.AddAlarm(AlarmLevel.Emergency, data);
};
```

### 2. 参数配置系统

```csharp
// 数据输入面板 + 虚拟键盘
var configPanel = new DataInputPanel();
var keyboard = VirtualKeyboardManager.Instance;

// 自动弹出虚拟键盘
configPanel.Enter += (s, e) => keyboard.ShowForControl(ActiveControl);

// 验证并提交
if (configPanel.ValidateAll())
{
    SaveConfiguration(configPanel.GetValues());
}
```

### 3. 操作员登录系统

```csharp
// 登录控件 + 用户管理
var loginControl = new FlatLoginControl();

loginControl.LoginSucceeded += (s, user) => {
    CurrentUser.Set(user);
    ShowMainForm();
};

// 用户管理按钮
btnManageUsers.Click += (s, e) => {
    var store = GetUserStore();
    LoginUserManagementForm.ShowForStore(store, this);
};
```

### 4. 实时数据监控

```csharp
// 通讯控件 + 趋势图
var commControl = new CommunicationControl();
var trendChart = new TrendChart();

trendChart.AddChannel("温度", Color.Red);
trendChart.AddChannel("压力", Color.Blue);

commControl.StringReceived += (s, data) => {
    var values = ParseData(data);
    trendChart.AddDataPoint("温度", values.Temperature);
    trendChart.AddDataPoint("压力", values.Pressure);
};
```

---

## 性能建议

### 1. 高频数据更新

```csharp
// 趋势图优化
trendChart.BufferCapacity = 10000;  // 增大缓冲区
trendChart.VisiblePoints = 500;     // 限制可见点数
trendChart.TimeInterval = 0.05;     // 匹配采样频率

// 使用节流刷新
private void ThrottledUpdate(double value)
{
    if (Environment.TickCount - lastUpdateTick > 50)
    {
        trendChart.AddDataPoint("通道", value);
        lastUpdateTick = Environment.TickCount;
    }
}
```

### 2. 大量报警管理

```csharp
// 报警控件优化
alarmDisplay.MaxAlarms = 2000;      // 合理设置上限
alarmDisplay.AutoScroll = true;     // 启用自动滚动

// 定期清理已确认报警
var cleanupTimer = new Timer { Interval = 60000 };
cleanupTimer.Tick += (s, e) => {
    alarmDisplay.ClearAcknowledged();
};
cleanupTimer.Start();
```

### 3. 通讯性能

```csharp
// 使用字节收发减少转换开销
commControl.DataReceived += (s, data) => {
    ProcessBinaryData(data); // 直接处理字节
};

// 而不是
commControl.StringReceived += (s, data) => {
    ProcessStringData(data); // 需要编码转换
};
```

---

## 常见问题

### Q1: 控件在设计器中显示异常?

**A**: 确保已正确引用IndustrialControls项目,重新生成解决方案后重启设计器。

### Q2: 如何自定义控件颜色?

**A**: 控件自动跟随主题系统,修改主题即可:

```csharp
ThemeManager.Instance.SetTheme(new FlatDarkTheme());
```

### Q3: 虚拟键盘不显示?

**A**: 确保在Program.Main()中初始化:

```csharp
VirtualKeyboardManager.Initialize();
```

### Q4: 通讯控件自动连接失败?

**A**: 检查配置文件是否存在,或手动设置参数:

```csharp
commControl.AutoConnect = false;
commControl.TcpIp = "192.168.1.100";
await commControl.ConnectAsync();
```

### Q5: 趋势图性能问题?

**A**: 调整缓冲区和可见点数:

```csharp
trendChart.BufferCapacity = 5000;
trendChart.VisiblePoints = 200;
```

---

## 版本信息

- **控件库版本**: 1.0.0
- **框架要求**: .NET Framework 4.7.2+
- **IDE支持**: Visual Studio 2019+
- **设计器**: 完全支持WinForms设计器

---

## 更新日志

### v1.0.0 (2024)

- 初始版本发布
- 包含8个核心控件组
- 完整的设计器支持
- 主题系统集成
- 参数管理系统

---

## 技术支持

- **项目路径**: `c:\Users\13626\Desktop\Winform上位机控件库`
- **源码位置**: `src\IndustrialControls\`
- **示例项目**: `samples\IndustrialControls.Demo\`
- **模板项目**: `samples\IndustrialControls.Template\`

---

## 许可证

Copyright © 2024 IndustrialControls Team

本文档涵盖了IndustrialControls控件库的所有公开API,建议配合示例项目一起学习使用。
