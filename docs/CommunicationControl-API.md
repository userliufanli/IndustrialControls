# 通讯控件 (CommunicationControl) API 使用手册

## 概述

`CommunicationControl` 是一个功能完整的工业通讯复合控件,集成TCP客户端、TCP服务端、串口通信三种模式,支持参数自动持久化、自动重连、数据帧解析等企业级特性。

## 命名空间

```csharp
IndustrialControls.Controls.Communication
```

## 继承关系

```
System.Object
  └─ System.MarshalByRefObject
     └─ System.ComponentModel.Component
        └─ System.Windows.Forms.Control
           └─ System.Windows.Forms.ScrollableControl
              └─ System.Windows.Forms.ContainerControl
                 └─ System.Windows.Forms.UserControl
                    └─ CommunicationControl
```

## 快速开始

### 1. 基本使用(TCP客户端模式)

```csharp
using IndustrialControls.Controls.Communication;

// 创建通讯控件
var commControl = new CommunicationControl
{
    Size = new Size(400, 350),
    Location = new Point(10, 10),
    Mode = CommunicationMode.Tcp,
    TcpIp = "192.168.1.100",
    TcpPort = 502,
    AutoConnect = true,  // 自动连接
    AutoReconnect = true // 自动重连
};

// 订阅数据接收事件
commControl.StringReceived += (sender, data) =>
{
    Console.WriteLine($"收到数据: {data}");
};

// 添加到窗体
this.Controls.Add(commControl);
```

### 2. TCP服务端模式

```csharp
var serverControl = new CommunicationControl
{
    Mode = CommunicationMode.TcpServer,
    TcpServerIp = "0.0.0.0",
    TcpServerPort = 8080,
    AutoConnect = true
};

// 客户端连接事件
serverControl.ClientConnected += (sender, e) =>
{
    Console.WriteLine($"客户端连接: {e.ClientId}");
};

// 客户端断开事件
serverControl.ClientDisconnected += (sender, e) =>
{
    Console.WriteLine($"客户端断开: {e.ClientId}");
};

// 接收客户端数据
serverControl.ClientStringReceived += (sender, e) =>
{
    Console.WriteLine($"来自 {e.ClientId}: {e.Data}");
    
    // 回复客户端
    serverControl.SendToClientAsync(e.ClientId, "收到你的消息");
};

this.Controls.Add(serverControl);
```

### 3. 串口模式

```csharp
var serialControl = new CommunicationControl
{
    Mode = CommunicationMode.Serial,
    SerialPortName = "COM1",
    SerialBaudRate = 9600,
    SerialParity = Parity.None,
    SerialDataBits = 8,
    SerialStopBits = StopBits.One
};

serialControl.StringReceived += (sender, data) =>
{
    Console.WriteLine($"串口收到: {data}");
};

this.Controls.Add(serialControl);
```

### 4. 数据收发

```csharp
// 发送字符串
await commControl.SendAsync("Hello Device");

// 发送字节
byte[] data = new byte[] { 0x01, 0x03, 0x00, 0x00, 0x00, 0x0A, 0xC4, 0x0B };
await commControl.SendAsync(data);

// 同步发送
commControl.Send("同步发送");

// TCP服务端广播
serverControl.SendToAllAsync("广播消息");

// TCP服务端定向发送
serverControl.SendToClientAsync(clientId, "定向消息");
```

## 属性

### 通讯模式

#### Mode

- **类型**: `CommunicationMode`
- **默认值**: `CommunicationMode.Tcp`
- **说明**: 通讯模式(TCP客户端/TCP服务端/串口)
- **示例**:
  ```csharp
  commControl.Mode = CommunicationMode.TcpServer;
  ```

### TCP客户端属性

#### TcpIp

- **类型**: `string`
- **默认值**: `"127.0.0.1"`
- **说明**: TCP目标IP地址

#### TcpPort

- **类型**: `int`
- **默认值**: `502`
- **说明**: TCP目标端口号

#### TcpTimeout

- **类型**: `int`
- **默认值**: `5000`
- **说明**: TCP连接超时时间(毫秒)

### TCP服务端属性

#### TcpServerIp

- **类型**: `string`
- **默认值**: `"0.0.0.0"`
- **说明**: TCP服务端监听IP地址

#### TcpServerPort

- **类型**: `int`
- **默认值**: `502`
- **说明**: TCP服务端监听端口号

### 串口属性

#### SerialPortName

- **类型**: `string`
- **默认值**: `"COM1"`
- **说明**: 串口名称

#### SerialBaudRate

- **类型**: `int`
- **默认值**: `9600`
- **说明**: 串口波特率

#### SerialParity

- **类型**: `Parity`
- **默认值**: `Parity.None`
- **说明**: 串口校验位

#### SerialDataBits

- **类型**: `int`
- **默认值**: `8`
- **说明**: 串口数据位

#### SerialStopBits

- **类型**: `StopBits`
- **默认值**: `StopBits.One`
- **说明**: 串口停止位

### 状态属性

#### State

- **类型**: `CommunicationState` (只读)
- **说明**: 当前通讯状态(未连接/连接中/已连接/错误)

#### BytesSent

- **类型**: `long` (只读)
- **说明**: 已发送字节数

#### BytesReceived

- **类型**: `long` (只读)
- **说明**: 已接收字节数

#### FramesSent

- **类型**: `long` (只读)
- **说明**: 已发送帧数

#### FramesReceived

- **类型**: `long` (只读)
- **说明**: 已接收帧数

#### ErrorCount

- **类型**: `long` (只读)
- **说明**: 错误计数

#### ClientCount

- **类型**: `int` (只读,TCP服务端模式)
- **说明**: 当前连接的客户端数量

### 自动重连属性

#### AutoConnect

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 宿主窗体打开后自动连接

#### AutoReconnect

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否启用自动重连机制

#### MaxReconnectAttempts

- **类型**: `int`
- **默认值**: `5`
- **说明**: 最大自动重连尝试次数

#### ReconnectDelayMs

- **类型**: `int`
- **默认值**: `3000`
- **说明**: 重连间隔时间(毫秒)

### 参数持久化属性

#### AutoPersist

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否启用自动参数持久化

#### PersistKey

- **类型**: `string`
- **默认值**: `""`
- **说明**: 持久化键名(为空时使用控件Name)

#### ConfigFilePath

- **类型**: `string`
- **默认值**: `""`
- **说明**: 参数配置文件路径(为空时使用默认路径)

### 帧解析属性

#### FrameDelimiter

- **类型**: `byte[]`
- **默认值**: `[0x0D, 0x0A]` (即 \r\n)
- **说明**: 帧分隔符字节序列
- **示例**:
  ```csharp
  // 使用单个字节作为分隔符
  commControl.FrameDelimiter = new byte[] { 0x0A }; // \n
  
  // 使用自定义分隔符
  commControl.FrameDelimiter = new byte[] { 0xFF, 0x00, 0xFF };
  ```

#### DataEncoding

- **类型**: `Encoding`
- **默认值**: `Encoding.UTF8`
- **说明**: 字符串编码
- **示例**:
  ```csharp
  commControl.DataEncoding = Encoding.ASCII;
  commControl.DataEncoding = Encoding.GetEncoding("GB2312");
  ```

## 方法

### 连接管理

#### ConnectAsync()

异步连接到目标设备。

**返回**: `Task`

**示例**:
```csharp
try
{
    await commControl.ConnectAsync();
    Console.WriteLine("连接成功");
}
catch (Exception ex)
{
    Console.WriteLine($"连接失败: {ex.Message}");
}
```

#### DisconnectAsync()

异步断开连接。

**返回**: `Task`

**示例**:
```csharp
await commControl.DisconnectAsync();
```

#### Disconnect()

同步断开连接。

**示例**:
```csharp
commControl.Disconnect();
```

### 数据发送(TCP客户端/串口模式)

#### SendAsync(byte[] data)

异步发送字节数据。

**参数**:
- `data`: `byte[]` 要发送的字节数组

**返回**: `Task`

**示例**:
```csharp
byte[] data = new byte[] { 0x01, 0x03, 0x00, 0x00 };
await commControl.SendAsync(data);
```

#### SendAsync(string text)

异步发送字符串数据。

**参数**:
- `text`: `string` 要发送的字符串

**返回**: `Task`

**示例**:
```csharp
await commControl.SendAsync("Hello Device");
```

#### Send(byte[] data)

同步发送字节数据。

**参数**:
- `data`: `byte[]` 要发送的字节数组

**示例**:
```csharp
commControl.Send(new byte[] { 0x01, 0x02, 0x03 });
```

#### Send(string text)

同步发送字符串数据。

**参数**:
- `text`: `string` 要发送的字符串

**示例**:
```csharp
commControl.Send("同步发送");
```

### 数据发送(TCP服务端模式)

#### SendToClientAsync(string clientId, byte[] data)

向指定客户端异步发送字节数据。

**参数**:
- `clientId`: `string` 客户端ID
- `data`: `byte[]` 要发送的字节数组

**返回**: `Task`

**示例**:
```csharp
await serverControl.SendToClientAsync(clientId, new byte[] { 0x01, 0x02 });
```

#### SendToClientAsync(string clientId, string text)

向指定客户端异步发送字符串。

**参数**:
- `clientId`: `string` 客户端ID
- `text`: `string` 要发送的字符串

**返回**: `Task`

**示例**:
```csharp
await serverControl.SendToClientAsync(clientId, "回复消息");
```

#### SendToAllAsync(byte[] data)

广播字节数据到所有客户端。

**参数**:
- `data`: `byte[]` 要广播的字节数组

**返回**: `Task`

**示例**:
```csharp
await serverControl.SendToAllAsync(new byte[] { 0xFF });
```

#### SendToAllAsync(string text)

广播字符串到所有客户端。

**参数**:
- `text`: `string` 要广播的字符串

**返回**: `Task`

**示例**:
```csharp
await serverControl.SendToAllAsync("广播通知");
```

#### SendToClient(string clientId, byte[] data)

同步向指定客户端发送字节数据。

**示例**:
```csharp
serverControl.SendToClient(clientId, new byte[] { 0x01 });
```

#### SendToClient(string clientId, string text)

同步向指定客户端发送字符串。

**示例**:
```csharp
serverControl.SendToClient(clientId, "同步消息");
```

#### SendToAll(byte[] data)

同步广播字节数据。

**示例**:
```csharp
serverControl.SendToAll(new byte[] { 0xFF });
```

#### SendToAll(string text)

同步广播字符串。

**示例**:
```csharp
serverControl.SendToAll("广播消息");
```

### 工具方法

#### ResetStatistics()

重置所有收发统计。

**示例**:
```csharp
commControl.ResetStatistics();
```

#### GetConnectedClients()

获取所有已连接客户端ID列表(TCP服务端模式)。

**返回**: `string[]`

**示例**:
```csharp
string[] clients = serverControl.GetConnectedClients();
foreach (var client in clients)
{
    Console.WriteLine($"客户端: {client}");
}
```

#### ResetReconnectCounter()

重置重连计数器。

**示例**:
```csharp
commControl.ResetReconnectCounter();
```

### 参数加载/保存

#### LoadParameters(...)

从外部参数加载到控件UI。

**参数**:
- `tcpIp`, `tcpPort`, `tcpTimeout`: TCP客户端参数
- `serverIp`, `serverPort`: TCP服务端参数
- `serialPort`, `baudRate`, `parity`, `dataBits`, `stopBits`: 串口参数
- `mode`: 通讯模式("Tcp"/"TcpServer"/"Serial")

**示例**:
```csharp
commControl.LoadParameters(
    "192.168.1.100", 502, 5000,  // TCP
    "0.0.0.0", 8080,             // TCP Server
    "COM1", 9600, "N", 8, "1",   // Serial
    "Tcp"                         // Mode
);
```

#### GetCurrentParameters(...)

从控件UI读取当前参数。

**示例**:
```csharp
commControl.GetCurrentParameters(
    out string tcpIp, out int tcpPort, out int tcpTimeout,
    out string serverIp, out int serverPort,
    out string serialPort, out int baudRate, 
    out string parity, out int dataBits, out string stopBits,
    out string mode
);
```

## 事件

### StateChanged

通讯状态变更时触发。

**事件参数**: `EventHandler<CommunicationState>`

**示例**:
```csharp
commControl.StateChanged += (sender, state) =>
{
    switch (state)
    {
        case CommunicationState.Connected:
            Console.WriteLine("已连接");
            break;
        case CommunicationState.Disconnected:
            Console.WriteLine("已断开");
            break;
        case CommunicationState.Error:
            Console.WriteLine("连接错误");
            break;
    }
};
```

### DataReceived

收到原始字节数据时触发(TCP客户端/串口模式)。

**事件参数**: `EventHandler<byte[]>`

**示例**:
```csharp
commControl.DataReceived += (sender, data) =>
{
    Console.WriteLine($"收到 {data.Length} 字节");
    Console.WriteLine(BitConverter.ToString(data));
};
```

### StringReceived

收到字符串数据时触发(自动使用DataEncoding解码)。

**事件参数**: `EventHandler<string>`

**示例**:
```csharp
commControl.StringReceived += (sender, text) =>
{
    Console.WriteLine($"收到字符串: {text}");
};
```

### ErrorOccurred

发生错误时触发。

**事件参数**: `EventHandler<Exception>`

**示例**:
```csharp
commControl.ErrorOccurred += (sender, ex) =>
{
    Console.WriteLine($"通讯错误: {ex.Message}");
};
```

### ClientConnected (TCP服务端)

客户端连接时触发。

**事件参数**: `EventHandler<ClientEventArgs>`

**示例**:
```csharp
serverControl.ClientConnected += (sender, e) =>
{
    Console.WriteLine($"客户端连接: {e.ClientId}");
    Console.WriteLine($"当前客户端数: {serverControl.ClientCount}");
};
```

### ClientDisconnected (TCP服务端)

客户端断开时触发。

**事件参数**: `EventHandler<ClientEventArgs>`

**示例**:
```csharp
serverControl.ClientDisconnected += (sender, e) =>
{
    Console.WriteLine($"客户端断开: {e.ClientId}");
};
```

### ClientDataReceived (TCP服务端)

收到客户端数据时触发(原始字节)。

**事件参数**: `EventHandler<ClientDataEventArgs>`

**示例**:
```csharp
serverControl.ClientDataReceived += (sender, e) =>
{
    Console.WriteLine($"来自 {e.ClientId}: {BitConverter.ToString(e.Data)}");
};
```

### ClientStringReceived (TCP服务端)

收到客户端字符串时触发(自动解码)。

**事件参数**: `EventHandler<ClientStringEventArgs>`

**示例**:
```csharp
serverControl.ClientStringReceived += (sender, e) =>
{
    Console.WriteLine($"来自 {e.ClientId}: {e.Data}");
    
    // 回复
    serverControl.SendToClientAsync(e.ClientId, "收到");
};
```

### FrameReceived

收到完整数据帧时触发(按FrameDelimiter分隔)。

**事件参数**: `EventHandler<FrameReceivedEventArgs>`

**示例**:
```csharp
commControl.FrameReceived += (sender, e) =>
{
    Console.WriteLine($"收到完整帧: {BitConverter.ToString(e.Frame)}");
};
```

### ClientFrameReceived (TCP服务端)

收到客户端完整数据帧时触发。

**事件参数**: `EventHandler<ClientFrameReceivedEventArgs>`

**示例**:
```csharp
serverControl.ClientFrameReceived += (sender, e) =>
{
    Console.WriteLine($"来自 {e.ClientId} 的帧: {BitConverter.ToString(e.Frame)}");
};
```

### ParametersSaveRequested

参数保存请求时触发。

**事件参数**: `EventHandler`

**示例**:
```csharp
commControl.ParametersSaveRequested += (sender, e) =>
{
    Console.WriteLine("参数已保存");
};
```

## 枚举类型

### CommunicationMode

通讯模式枚举。

```csharp
public enum CommunicationMode
{
    Tcp,        // TCP客户端
    TcpServer,  // TCP服务端
    Serial      // 串口
}
```

### CommunicationState

通讯状态枚举。

```csharp
public enum CommunicationState
{
    Disconnected,  // 未连接
    Connecting,    // 连接中
    Connected,     // 已连接
    Error          // 错误
}
```

## 完整示例

### TCP客户端完整示例

```csharp
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IndustrialControls.Controls.Communication;

namespace TcpClientDemo
{
    public partial class MainForm : Form
    {
        private CommunicationControl commControl;

        public MainForm()
        {
            InitializeComponent();
            InitializeCommControl();
        }

        private void InitializeCommControl()
        {
            commControl = new CommunicationControl
            {
                Dock = DockStyle.Fill,
                Mode = CommunicationMode.Tcp,
                TcpIp = "192.168.1.100",
                TcpPort = 502,
                AutoConnect = false,
                AutoReconnect = true,
                MaxReconnectAttempts = 10,
                ReconnectDelayMs = 3000
            };

            // 订阅事件
            commControl.StateChanged += OnStateChanged;
            commControl.StringReceived += OnStringReceived;
            commControl.ErrorOccurred += OnErrorOccurred;
            commControl.FrameReceived += OnFrameReceived;

            // 配置帧解析
            commControl.FrameDelimiter = new byte[] { 0x0D, 0x0A };
            commControl.DataEncoding = Encoding.UTF8;

            this.Controls.Add(commControl);
        }

        private void OnStateChanged(object sender, CommunicationState state)
        {
            Console.WriteLine($"状态变更: {state}");
            
            if (state == CommunicationState.Connected)
            {
                // 连接成功后发送查询命令
                commControl.SendAsync("QUERY_STATUS\r\n");
            }
        }

        private void OnStringReceived(object sender, string data)
        {
            Console.WriteLine($"收到: {data}");
            
            // 解析响应数据
            ParseResponse(data);
        }

        private void OnFrameReceived(object sender, FrameReceivedEventArgs e)
        {
            Console.WriteLine($"收到完整帧: {Encoding.UTF8.GetString(e.Frame)}");
        }

        private void OnErrorOccurred(object sender, Exception ex)
        {
            Console.WriteLine($"错误: {ex.Message}");
        }

        private void ParseResponse(string response)
        {
            // 解析设备响应
            // ...
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                await commControl.SendAsync(txtSend.Text + "\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发送失败: {ex.Message}");
            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                await commControl.ConnectAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"连接失败: {ex.Message}");
            }
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            await commControl.DisconnectAsync();
        }
    }
}
```

### TCP服务端完整示例

```csharp
using System;
using System.Windows.Forms;
using IndustrialControls.Controls.Communication;

namespace TcpServerDemo
{
    public partial class MainForm : Form
    {
        private CommunicationControl serverControl;

        public MainForm()
        {
            InitializeComponent();
            InitializeServer();
        }

        private void InitializeServer()
        {
            serverControl = new CommunicationControl
            {
                Dock = DockStyle.Fill,
                Mode = CommunicationMode.TcpServer,
                TcpServerIp = "0.0.0.0",
                TcpServerPort = 8080,
                AutoConnect = true
            };

            serverControl.ClientConnected += OnClientConnected;
            serverControl.ClientDisconnected += OnClientDisconnected;
            serverControl.ClientStringReceived += OnClientStringReceived;

            this.Controls.Add(serverControl);
        }

        private void OnClientConnected(object sender, ClientEventArgs e)
        {
            Console.WriteLine($"客户端连接: {e.ClientId}");
            BroadcastMessage($"欢迎 {e.ClientId} 加入");
        }

        private void OnClientDisconnected(object sender, ClientEventArgs e)
        {
            Console.WriteLine($"客户端断开: {e.ClientId}");
            BroadcastMessage($"{e.ClientId} 已离开");
        }

        private void OnClientStringReceived(object sender, ClientStringEventArgs e)
        {
            Console.WriteLine($"[{e.ClientId}] {e.Data}");
            
            // 回复客户端
            serverControl.SendToClientAsync(e.ClientId, $"服务器收到: {e.Data}");
            
            // 如果不是命令,则广播给其他客户端
            if (!e.Data.StartsWith("/"))
            {
                BroadcastMessage($"[{e.ClientId}] {e.Data}");
            }
        }

        private void BroadcastMessage(string message)
        {
            serverControl.SendToAllAsync(message);
        }
    }
}
```

## 注意事项

1. **线程安全**: 所有事件回调在UI线程执行,可直接更新UI控件
2. **自动重连**: 手动调用 `DisconnectAsync()` 会抑制自动重连
3. **参数持久化**: 默认启用,配置文件位于 `Config/communication.json`
4. **帧解析**: 设置 `FrameDelimiter` 后自动按分隔符解析完整帧
5. **资源释放**: 控件自动管理内部资源,无需手动释放
6. **设计器支持**: 完全支持Visual Studio设计器拖放和属性配置

## 最佳实践

1. **错误处理**: 始终使用 try-catch 包裹异步调用
2. **重连策略**: 根据网络环境调整 `MaxReconnectAttempts` 和 `ReconnectDelayMs`
3. **帧协议**: 使用 `FrameDelimiter` 自动解析完整数据帧
4. **编码设置**: 根据设备协议设置正确的 `DataEncoding`
5. **日志记录**: 在事件中记录通讯日志便于故障排查
6. **性能优化**: 高频数据场景使用字节收发,避免字符串转换开销

## 相关控件

- **TrendChart**: 趋势图控件,用于可视化通讯数据
- **DataInputPanel**: 数据输入面板,用于发送命令参数
- **StatusIndicator**: 状态指示器,显示通讯连接状态
