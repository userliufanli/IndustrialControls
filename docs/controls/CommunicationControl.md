# CommunicationControl / CommunicationManager（通信）

**命名空间**：`IndustrialControls.Controls.Communication`

## 组件关系

| 类型 | 说明 |
|------|------|
| `CommunicationControl` | **用户控件**：界面集成模式选择、参数、连接按钮、收发区与统计；内部持有 `CommunicationManager`。 |
| `CommunicationManager` | **`Component`**：无 UI，可在代码中单独放置于窗体或容器，负责 TCP 客户端 / TCP 服务端 / 串口的异步连接与收发。 |

当前源码中的通讯模式为 **`Tcp`（客户端）、`TcpServer`、`Serial`**，不包含 UDP（若旧文档提及 UDP，以源码为准）。

## 枚举

### `CommunicationMode`

| 值 | 说明 |
|----|------|
| `Tcp` | TCP 客户端 |
| `TcpServer` | TCP 服务端（多客户端） |
| `Serial` | 串口 |

### `CommunicationState`

| 值 | 说明 |
|----|------|
| `Disconnected` | 未连接 |
| `Connecting` | 连接中 |
| `Connected` | 已连接 |
| `Error` | 错误 |

## CommunicationControl：典型用法

设计器将控件拖到窗体，在属性页配置 IP、端口或串口参数；在代码中订阅事件并调用异步连接/发送。

### 事件（节选）

| 事件 | 说明 |
|------|------|
| `StateChanged` | 连接状态变化 |
| `DataReceived` | 收到原始字节 |
| `StringReceived` | 按 `DataEncoding` 解码后的字符串 |
| `ErrorOccurred` | 异常 |
| `ParametersSaveRequested` | UI 请求将参数写入你的持久化逻辑 |
| `ClientConnected` / `ClientDisconnected` | 服务端模式客户端上下线 |
| `ClientDataReceived` / `ClientStringReceived` | 带客户端标识的接收 |

### 常用异步方法

| 方法 | 说明 |
|------|------|
| `ConnectAsync()` | 连接 |
| `DisconnectAsync()` | 断开 |
| `SendAsync(byte[])` / `SendAsync(string)` | 发送 |
| `SendToClientAsync` / `SendToAllAsync` | 服务端定向或广播 |

## CommunicationManager

与控件相同的事件语义，适合自绘界面或后台服务；需自行处理 UI 线程 marshalling（控件已做部分封装）。

详细参数键名、自动重连、帧解析等见 **[IndustrialControls 使用手册](../IndustrialControls使用手册.md)** 中与通讯相关的章节。

## 代码示例（控件）

```csharp
using IndustrialControls.Controls.Communication;

// 假设已从设计器添加 communicationControl1
communicationControl1.StateChanged += (s, st) => { /* 更新 UI */ };
communicationControl1.StringReceived += (s, text) => { /* 解析协议 */ };

await communicationControl1.ConnectAsync();
await communicationControl1.SendAsync("READ 1\r\n");
```

## 另见

- [ParameterManager](./ParameterManager.md)（通讯参数持久化）  
- [文档索引](./README.md)  
