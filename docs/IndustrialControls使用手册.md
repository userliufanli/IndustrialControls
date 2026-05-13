# IndustrialControls 使用手册

> 本文档面向在 WinForms 工业上位机项目中引用 **IndustrialControls** 控件库的开发者，说明环境要求、项目结构、参数持久化、常用控件与演示工程用法。  
> **技术栈**：.NET Framework 4.8、Windows Forms。

---

## 目录

1. [概述](#1-概述)  
2. [环境与引用](#2-环境与引用)  
3. [项目与程序集结构](#3-项目与程序集结构)  
4. [参数与配置（推荐用法）](#4-参数与配置推荐用法)  
5. [参数系统 API 参考](#5-参数系统-api-参考)  
6. [JSON 配置文件说明](#6-json-配置文件说明)  
7. [事件、热更新与线程安全](#7-事件热更新与线程安全)  
8. [WinForms 窗体开发约定](#8-winforms-窗体开发约定)  
9. [演示工程 IndustrialControls.Demo](#9-演示工程-industrialcontrolsdemo)  
10. [控件与主题一览](#10-控件与主题一览)  
11. [常见问题](#11-常见问题)  
12. [相关文档索引](#12-相关文档索引)

---

## 1. 概述

**IndustrialControls** 是一套面向工业上位机的 WinForms 控件与基础设施库，提供：

- 工业场景常用 UI（状态指示、设备按钮、报警、数据输入、虚拟键盘等）
- 通信相关控件与能力（TCP/UDP/串口等，与参数持久化配合）
- **主题系统**（亮色/暗色等）
- **参数管理**（JSON 持久化、分组、类型转换、防抖保存、可选文件热重载）

本手册中与「配置」相关的推荐实践是：在应用程序中定义静态类（如示例中的 `AppParameters`），统一暴露强类型属性，底层使用 `ParameterManager` / `ParameterAccessor` / `ParameterSection`。

---

## 2. 环境与引用

### 2.1 系统与工具

| 项目 | 要求 |
|------|------|
| 操作系统 | Windows 10 / 11 |
| 目标框架 | **.NET Framework 4.8** |
| 推荐 IDE | **Visual Studio 2022**（带 .NET 桌面开发工作负载） |

### 2.2 引用控件库

任选其一：

1. **项目引用**：将解决方案中的 `IndustrialControls` 项目添加为引用。  
2. **程序集引用**：引用生成的 `IndustrialControls.dll`（需同时满足其依赖，见控件库 `.csproj` 中的 `Reference`）。

引用后，在代码中使用各命名空间，例如：

```csharp
using IndustrialControls.Core;
// using IndustrialControls.Controls.StatusIndicator;
// using IndustrialControls.Theme;
```

---

## 3. 项目与程序集结构

典型仓库布局如下（与仓库实际目录一致）：

| 路径 | 说明 |
|------|------|
| `src/IndustrialControls/` | 控件库主工程（生成 `IndustrialControls.dll`） |
| `src/IndustrialControls/Controls/` | 各功能控件子目录 |
| `src/IndustrialControls/Core/` | 核心类型：`ParameterManager`、`ParameterAccessor`、`ParameterSection`、`BaseControl` 等 |
| `src/IndustrialControls/Theme/` | 主题接口与实现 |
| `src/IndustrialControls/Utilities/` | 通用工具类 |
| `samples/IndustrialControls.Demo/` | 官方演示程序 |
| `docs/` | 文档（本手册、`README.md`、API 说明等） |

---

## 4. 参数与配置（推荐用法）

### 4.1 设计目标

- 读写接口简单、类型明确（`int`、`string`、`bool` 等）
- 自动持久化到 JSON，支持多文件（例如：全局参数 + 通讯参数分文件）
- 多线程场景下由管理器内部加锁，调用方按普通属性读写即可（仍建议在 UI 线程更新界面）

### 4.2 推荐：应用内静态 `AppParameters`

在演示项目中，`AppParameters` 展示了推荐模式：

1. 使用 **`ParameterAccessor.Default`** 对应 `%AppData%\IndustrialControls\parameters.json`（路径可通过 `ParameterAccessor.GetDefaultConfigPath()` 查询）。  
2. 使用 **`ParameterAccessor.Section("分组名")`** 得到 **`ParameterSection`**，在静态嵌套类中用 `Get` / `Set` 封装每个业务字段。  
3. 其他配置文件使用 **`ParameterAccessor.Register("逻辑名", 文件完整或相对路径)`**，再对该 **`ParameterManager`** 调用 **`.Section("分组名")`**。

示例（与 Demo 中思路一致，节选）：

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

窗体或业务代码中直接：

```csharp
AppParameters.App.Language = "zh-CN";
int w = AppParameters.UI.WindowWidth;
```

**优点**：分组名与键名集中管理；调用处可读性强；便于搜索与重构。

### 4.3 何时直接使用 `ParameterManager`

以下情况可 `new ParameterManager(path)` 或通过 `Register` 取得实例后调用 `GetValue` / `SetValue`：

- 与控件库内置控件（如通信控件）共享同一文件路径的单例行为
- 临时工具、迁移脚本等不希望通过 `AppParameters` 封装

注意：对同一路径应复用同一实例，避免多实例互相覆盖（Demo 中通信控件采用静态字典等方式共享实例，可参考其实现）。

---

## 5. 参数系统 API 参考

### 5.1 `ParameterAccessor`（静态入口）

| 成员 | 说明 |
|------|------|
| `Default` | 默认文件的 `ParameterManager` 单例 |
| `GetDefaultConfigPath()` | 默认 JSON 的绝对路径 |
| `Section(string groupName)` | 等价于 `Default.Section(groupName)` |
| `Register(string name, string configFilePath)` | 按逻辑名注册/获取第二套及多套配置 |
| `Resolve` / `TryResolve` / `IsRegistered` / `GetRegisteredNames` / `Remove` | 多配置生命周期管理 |
| `Get<T>` / `Set<T>` / `SetBatch` | 针对默认文件、需写全分组名的快捷方法 |

### 5.2 `ParameterSection`

绑定 **一个** `ParameterManager` 与 **一个分组名**，之后只操作键：

| 方法 | 说明 |
|------|------|
| `Get<T>(key, defaultValue)` | 读取 |
| `Set<T>(key, value)` | 写入并参与自动保存 |
| `Contains(key)` | 是否包含键 |
| `TryGet<T>(key, out value)` | 尝试读取 |
| `GetOrSet<T>(key, seed)` | 不存在或无法转换时用种子写入并返回 |
| `Delete(key)` | 删除键 |
| `SetBatch` / `GetBatch` | 当前分组批量写/读 |

### 5.3 `ParameterManager`（完整能力）

| 类别 | 代表成员 |
|------|-----------|
| 读写 | `GetValue<T>(group, key, default)`、`SetValue<T>(group, key, value)` |
| 结构 | `Contains`、`Delete`、`DeleteGroup`、`GetGroupNames`、`GetKeys` |
| 批量 | `SetBatch`、`GetBatch` |
| 文件 | `LoadFromFile()`、`SaveToFile()`（立即落盘，并取消挂起的防抖保存） |
| 自动保存 | `SuspendAutoSave()` / `ResumeAutoSave()` 嵌套暂停防抖写入 |
| 其他 | `Section(group)`、`ConfigFilePath`、`EnableHotReload`、`EnableLogging` |
| 事件 | `ParameterChanged`、`SaveFailed`、`LoadFailed` |
| 释放 | `Dispose()` |

设计器模式下构造函数会跳过文件与监视器逻辑，避免设计期异常。

---

## 6. JSON 配置文件说明

- 根对象包含版本、最后修改时间、**分组列表**。  
- 每个分组下有若干 **键值** 项，内部带类型元数据等，由 `JavaScriptSerializer` 序列化。  
- 保存时采用 **临时文件 + 复制替换**，并尝试生成 `.bak` 备份。  
- 数值从 JSON 读入时可能为 `long`/`double`，管理器内会做 **到目标类型的转换**（如 `int`、`float`）。

不要在生产环境中手改 JSON 破坏结构；若外部编辑器保存文件，可依赖 **热重载**（见下节）触发 `Reloaded` 事件。

---

## 7. 事件、热更新与线程安全

### 7.1 `ParameterChanged`

参数增删改、分组删除、以及从磁盘 **热重载** 后会触发。事件参数类型为 **`ParameterChangedEventArgs`**，含 `GroupName`、`Key`、`Value`、`ChangeType`、`Timestamp`。

在 WinForms 中更新 UI 时，若订阅者可能运行在非 UI 线程，请使用 `Control.Invoke` / `BeginInvoke`（Demo 中 `ParameterManagerTestPage` 有示例）。

### 7.2 热更新

当 `EnableHotReload == true`（默认）时，管理器会监视配置文件目录的变更，经防抖后调用 `LoadFromFile()`，并发出 **`ParameterChangeType.Reloaded`**。若不希望监视，可将 `EnableHotReload` 设为 `false`（需在构造完成监视器之前设置，或自行扩展初始化顺序）。

### 7.3 自动保存与防抖

`SetValue` 等修改内存后，会通过 **短时防抖** 合并多次写入，减少磁盘抖动。`SaveToFile()` 会立即保存。`SuspendAutoSave` / `ResumeAutoSave` 用于批量修改时避免中间状态频繁写盘。

### 7.4 线程安全

`ParameterManager` 内部使用读写锁；一般可在多线程调用 `GetValue`/`SetValue`。**仍建议**：界面绑定与控件赋值在 UI 线程执行，避免跨线程访问控件。

---

## 8. WinForms 窗体开发约定

仓库内对 **IndustrialControls.Demo** 的维护约定（摘要，完整条文见 `docs/.cursorrules`）：

- 使用 Visual Studio 设计器维护窗体：主类为 **`partial`**，控件声明与 **`InitializeComponent()`** 放在 **`*.Designer.cs`**。  
- **禁止**在运行时代码中 `new` 控件并 `Controls.Add` 挂载到窗体（设计器生成的 `InitializeComponent` 内的 `new` 除外）。  
- 业务逻辑、数据加载放在非 Designer 的 `partial` 类文件中。

新建业务窗体时建议遵循相同约定，便于团队协作与设计器合并。

---

## 9. 演示工程 IndustrialControls.Demo

### 9.1 作用

- 展示主要控件的用法与交互  
- 提供 **`AppParameters`** 完整示例（默认配置 + `Config/communication.json`）  
- **`ParameterManagerTestPage`**：通过 `AppParameters` 编辑语言/主题/自动保存/窗口尺寸，并显示参数变更日志

### 9.2 打开与编译

在 Visual Studio 中打开解决方案，将 **IndustrialControls.Demo** 设为启动项目后生成并运行。若使用命令行 `dotnet msbuild` 构建带 `.resx` 的 WinForms 项目，可能需要按本机 SDK 要求开启资源相关选项；**以 VS 内生成结果为推荐**。

---

## 10. 控件与主题一览

具体属性、事件、枚举与截图级说明见 **[API使用说明书](./API使用说明书.md)**。本库主要控件目录包括（以仓库为准）：

| 区域 | 典型控件 |
|------|-----------|
| `Controls/StatusIndicator` | 状态指示器 |
| `Controls/DeviceButton` | 设备控制按钮 |
| `Controls/Alarm` | 报警显示 |
| `Controls/Communication` | 通信控制 |
| `Controls/DataInput` | 数据输入、校验 |
| `Controls/DataVisualization` | 数据可视化 |
| `Controls/VirtualKeyboard` | 虚拟键盘 |
| `Theme` | `ThemeManager`、亮色/暗色主题实现 |

主题切换与配色请阅读 API 文档中主题章节。

---

## 11. 常见问题

### 11.1 默认配置文件在哪里？

调用 `ParameterAccessor.GetDefaultConfigPath()`。通常为当前用户 **应用程序数据** 目录下的 `IndustrialControls\parameters.json`。

### 11.2 修改了参数但别的窗体没刷新？

请订阅对应 **`ParameterManager`** 的 **`ParameterChanged`**，或在 `Reloaded` 时重新绑定界面。不同逻辑名对应不同 `ParameterManager` 实例时，需订阅各自实例。

### 11.3 `Register` 第二次传不同路径会怎样？

同一逻辑名在进程内对应 **一个** 懒加载单例；首次注册时的工厂生效，后续路径参数应保持一致，避免歧义。

### 11.4 设计器打开窗体报错

确认目标框架为 **.NET Framework 4.8**，且已正确引用控件库及其依赖（如 `System.Web.Extensions` 等与控件库一致）。

### 11.5 与旧文档不一致

若 **[ParameterManager使用指南](./ParameterManager使用指南.md)** 等仍提及已移除的类型，以 **本手册与当前源码** 为准；核心类型为：`ParameterManager`、`ParameterAccessor`、`ParameterSection`。

---

## 12. 相关文档索引

| 文档 | 内容 |
|------|------|
| [docs/README.md](./README.md) | 文档中心总览与目录 |
| [快速入门指南](./快速入门指南.md) | 最短路径上手示例 |
| [API使用说明书](./API使用说明书.md) | 控件与 API 细则 |
| [ParameterManager使用指南](./ParameterManager使用指南.md) | 参数管理器进阶说明（若与源码冲突以源码为准） |
| `docs/.cursorrules` | 含 WinForms 设计器协作规则 |

---

*文档版本与仓库源码同步维护；如有遗漏，请以 `src/IndustrialControls` 下公开 API 为准。*
