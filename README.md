# IndustrialControls（WinForm 上位机控件库）

基于 .NET Framework 4.8 的 WinForms 工业上位机控件库：状态/报警/通信/趋势/虚拟键盘/主题与参数管理等。

WinForm 工业 HMI 控件与示例：可复用 UI + 通信与参数能力，含 Demo 与模板工程。

## 环境要求

- Windows 10 / 11  
- **Visual Studio 2019 或更高版本**（建议 VS 2022）  
- **.NET Framework 4.8** 开发包  

## 仓库结构

| 路径 | 说明 |
|------|------|
| `IndustrialControls.sln` | 解决方案入口 |
| `src/IndustrialControls/` | 类库源码（控件、主题、参数、自动化通信等） |
| `samples/IndustrialControls.Demo/` | 功能演示工程 |
| `samples/IndustrialControls.Template/` | 模板示例工程（多页面示例结构） |
| `docs/` | 使用手册、API 说明与入门文档 |

## 本地编译

1. 用 Visual Studio 打开 `IndustrialControls.sln`。  
2. 生成解决方案（默认 `Debug | Any CPU` 即可）。  
3. 将 `IndustrialControls` 项目以**项目引用**或**程序集引用**方式加入你的 WinForms（.NET Framework 4.8）工程后，即可在设计器与代码中使用库中控件。  

## 主要能力概览

- **控件**：状态指示、设备按钮、报警列表、数据输入与校验、趋势/仪表盘/工业进度条、通信面板、虚拟键盘、扁平登录与用户管理等。  
- **主题**：亮色 / 暗色主题与统一配色扩展。  
- **参数**：`ParameterManager` 等参数持久化与访问（详见文档）。  
- **自动化**：Modbus 等线路/站点会话相关类型（具体能力以源码与文档为准）。  

## 文档

完整目录与阅读顺序见 **[文档中心](docs/README.md)**。常用入口：

- [IndustrialControls 使用手册](docs/IndustrialControls使用手册.md)  
- [快速入门指南](docs/快速入门指南.md)  
- [控件文档索引](docs/controls/README.md)（各控件独立说明）  
- [API 使用说明书（索引）](docs/API使用说明书.md)  
- [ParameterManager 使用指南](docs/ParameterManager使用指南.md)  

## 版本信息

类库程序集版本见 `src/IndustrialControls/Properties/AssemblyInfo.cs`（当前为 **1.0.0.0**）。  

---

如有问题或改进建议，欢迎在仓库中提交 Issue 或 Pull Request。
