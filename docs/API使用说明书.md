# API 使用说明书

> 本页为 **索引**：各控件与核心模块的说明已拆分为独立文档，便于维护与检索。  
> 若与源码不一致，**以源码为准**。

---

## 控件与模块文档（推荐）

完整列表与命名空间对照见：**[控件与核心模块文档索引](./controls/README.md)**。

| 主题 | 独立文档 |
|------|----------|
| 状态指示器 | [StatusIndicator.md](./controls/StatusIndicator.md) |
| 设备控制按钮 | [DeviceControlButton.md](./controls/DeviceControlButton.md) |
| 报警显示 | [AlarmDisplay.md](./controls/AlarmDisplay.md) |
| 数值输入框 | [NumericInputBox.md](./controls/NumericInputBox.md) |
| 验证文本框 | [ValidatedTextBox.md](./controls/ValidatedTextBox.md) |
| 标签+数值行 | [DataInputPanel.md](./controls/DataInputPanel.md) |
| 仪表盘 | [GaugeControl.md](./controls/GaugeControl.md) |
| 工业进度条 | [IndustrialProgressBar.md](./controls/IndustrialProgressBar.md) |
| 趋势图 | [TrendChart.md](./controls/TrendChart.md) |
| 通信控件与管理器 | [CommunicationControl.md](./controls/CommunicationControl.md) |
| 虚拟键盘 | [VirtualKeyboard.md](./controls/VirtualKeyboard.md) |
| 登录与用户管理 | [FlatLoginControl.md](./controls/FlatLoginControl.md) |
| 参数管理器 | [ParameterManager.md](./controls/ParameterManager.md) |
| 主题管理器 | [ThemeManager.md](./controls/ThemeManager.md) |

---

## 命名空间速查

| 文档主题 | 命名空间 |
|----------|----------|
| StatusIndicator | `IndustrialControls.Controls.StatusIndicator` |
| DeviceControlButton | `IndustrialControls.Controls.DeviceButton` |
| AlarmDisplay | `IndustrialControls.Controls.Alarm` |
| NumericInputBox / ValidatedTextBox / DataInputPanel | `IndustrialControls.Controls.DataInput` |
| GaugeControl / IndustrialProgressBar / TrendChart | `IndustrialControls.Controls.DataVisualization` |
| CommunicationControl / CommunicationManager | `IndustrialControls.Controls.Communication` |
| VirtualKeyboard 系列 | `IndustrialControls.Controls.VirtualKeyboard` |
| FlatLoginControl / LoginUserManagementForm | `IndustrialControls.Controls.Login` |
| ParameterManager | `IndustrialControls.Core` |
| ThemeManager | `IndustrialControls.Theme` |

---

## 说明

历史上本文件曾为「单文件合订 API」。现已按控件拆分至 `docs/controls/`，避免与源码演进重复脱节。若你本地书签仍指向旧版锚点，请改用上表对应独立文档。
