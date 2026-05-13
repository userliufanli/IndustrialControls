# DeviceControlButton（设备控制按钮）

**命名空间**：`IndustrialControls.Controls.DeviceButton`  
**类型**：`DeviceControlButton`（多状态、可选长按确认的工业按钮）

## 用途

启停、模式切换、带确认的危险操作等；支持多套 `DeviceButtonState`（文案与颜色），可在「动作模式」与「状态模式」间切换展示逻辑。

## 枚举 `DisplayMode`

| 值 | 说明 |
|----|------|
| `ActionMode` | 强调将要执行的动作 |
| `StatusMode` | 强调设备当前状态 |

## 常用属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Mode` | `DisplayMode` | 显示模式 |
| `DefaultStateIndex` | `int` | 初始状态索引 |
| `ButtonText` | `string` | 按钮主文案 |
| `CurrentStateIndex` | `int` | 当前状态索引 |
| `CurrentState` | `DeviceButtonState` | 当前状态对象（只读语义） |
| `EnableLongPress` | `bool` | 是否长按确认 |
| `LongPressTime` | `int` | 长按时间（毫秒） |
| `Icon` | `Image` | 图标 |

## 事件

| 事件 | 说明 |
|------|------|
| `StateChanged` | `EventHandler<DeviceButtonState>` |
| `ButtonActivated` | `EventHandler`，点击或长按完成 |

## 方法

| 方法 | 说明 |
|------|------|
| `SetStates(params DeviceButtonState[] states)` | 配置全部状态 |
| `NextState()` | 切换到下一状态 |

## 代码示例

```csharp
using IndustrialControls.Controls.DeviceButton;

var btn = new DeviceControlButton
{
    Size = new Size(120, 40),
    EnableLongPress = true,
    LongPressTime = 1000
};

btn.SetStates(
    new DeviceButtonState { DisplayText = "启动", Color = Color.Green },
    new DeviceButtonState { DisplayText = "停止", Color = Color.Red }
);

btn.StateChanged += (s, state) => { /* */ };
```

## 另见

- [文档索引](./README.md)  
