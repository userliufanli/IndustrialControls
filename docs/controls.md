# 控件参考总览

以下为 **IndustrialControls** 主要公开类型所在命名空间及用途摘要；属性、事件与枚举以源码与设计器为准，**IndustrialControls.Demo** 为权威交互示例。

## 按命名空间

| 命名空间 / 区域 | 主要内容 |
|-----------------|----------|
| `IndustrialControls.Controls.StatusIndicator` | 状态指示器（状态、形状、闪烁、历史等） |
| `IndustrialControls.Controls.DeviceButton` | 设备多状态按钮、长按确认等 |
| `IndustrialControls.Controls.Alarm` | 报警列表、等级、确认与清除 |
| `IndustrialControls.Controls.DataInput` | `NumericInputBox`、`ValidatedTextBox`、`DataInputPanel`（单行标签+数值+单位）等 |
| `IndustrialControls.Controls.DataVisualization` | `TrendChart`、`GaugeControl`、`IndustrialProgressBar` 等 |
| `IndustrialControls.Controls.Communication` | `CommunicationControl`（复合 UI）、`CommunicationManager`（无 UI 组件） |
| `IndustrialControls.Controls.VirtualKeyboard` | 虚拟键盘面板、窗体、`VirtualKeyboardManager` |
| `IndustrialControls.Controls.Login` | `FlatLoginControl`、`LoginUserManagementForm`、用户存储相关类型 |
| `IndustrialControls.Core` | `ParameterManager`、`ParameterAccessor`、`ParameterSection`、`BaseControl` 等 |
| `IndustrialControls.Theme` | `ThemeManager`、`FlatLightTheme`、`FlatDarkTheme`、`ThemeColors` 等 |
| `IndustrialControls.Automation` | 产线侧协议/会话等类型（以当前源码为准） |

## 通信模式（`CommunicationMode`）

当前源码中的枚举以 **`Tcp`**（TCP 客户端）、**`TcpServer`**、**`Serial`** 为主；具体属性名（如 `TcpIp`、`TcpPort`）与异步方法（如 `ConnectAsync`、`SendAsync`）请对照 **`CommunicationControl`** / **`CommunicationManager`** 源码与 Demo 页面。

## 如何深入

1. 在 Demo 中搜索控件类名，打开对应 **Page / Form**。  
2. 阅读 `src/IndustrialControls` 下同名目录中的 `.cs` 与 `///` 注释。  
3. 需要参数与文件落盘行为时，结合 [parameters.md](parameters.md)。
