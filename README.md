# IndustrialControls

基于 **.NET Framework 4.8** 的 **WinForms** 工业上位机控件库：状态指示、报警列表、通信配置与收发、趋势与仪表盘、虚拟键盘、主题与参数持久化等，适合搭建产线监控、设备操作站、轻量 HMI 等桌面应用。

仓库同时提供**可复用的类库**与**示例工程**：Demo 用于逐项看控件效果，Template 展示多页面上位机骨架，便于拷贝改造。

---

## 技术栈与运行环境

| 项目 | 说明 |
|------|------|
| 运行时 / 目标框架 | .NET Framework 4.8 |
| UI | Windows Forms |
| 语言 | C# |
| 推荐开发环境 | Visual Studio 2019 及以上（建议 VS 2022），Windows 10 / 11 |

类库为传统 `csproj`（非 SDK 风格），建议使用 **Visual Studio** 打开解决方案进行生成与设计器开发；含 `.resx` 的 WinForms 项目以 VS 内编译结果为推荐。

---

## 仓库里有什么

| 路径 | 作用 |
|------|------|
| `IndustrialControls.sln` | 解决方案入口，包含类库与示例项目。 |
| `src/IndustrialControls/` | **核心类库**：`Controls`（各工业控件）、`Theme`（主题）、`Core`（如 `ParameterManager`）、`Utilities`、`Automation`（如 Modbus 会话等，随版本演进以源码为准）。 |
| `samples/IndustrialControls.Demo/` | **演示程序**：各控件与能力的集中演示，适合学习 API 与交互。 |
| `samples/IndustrialControls.Template/` | **模板程序**：多页面、导航、业务骨架示例，适合作为新上位机项目的起点。 |
| `docs/` | **文档**：使用手册、快速入门、各控件独立说明（`docs/controls/`）、参数管理进阶等。 |

根目录已包含 `.gitignore`，避免将 `bin`、`obj`、`.vs` 等提交到版本库。

---

## 五分钟上手

1. 克隆本仓库后，用 Visual Studio 打开 **`IndustrialControls.sln`**。  
2. 在解决方案资源管理器中将 **`IndustrialControls.Demo`** 设为启动项目。  
3. 选择 **Debug | Any CPU**（或 Release），生成并运行，即可在界面中逐项查看控件行为。  
4. 若希望从完整页面结构起步，可将启动项目改为 **`IndustrialControls.Template`** 并运行，对照其窗体与页面组织方式改造自己的工程。

---

## 在你自己的 WinForms 工程里引用

1. 在同一解决方案中**添加现有项目**，选择 `src/IndustrialControls/IndustrialControls.csproj`，或在独立解决方案中用「添加 → 项目引用」指向该 `csproj`。  
2. 确保你的应用目标框架为 **.NET Framework 4.8**，且依赖与类库一致（例如类库已引用的程序集在宿主侧无需重复添加时，以实际编译提示为准）。  
3. 生成成功后，在工具箱中浏览或手动添加控件，即可像使用第三方控件一样将本库控件拖到窗体；业务逻辑、数据绑定与事件处理建议写在非 Designer 的 `partial` 类文件中，与设计器生成的布局代码分离。

当前未强制要求通过 NuGet 分发；若你本地有打包流程，可自行生成程序集并在其他解决方案中引用 `IndustrialControls.dll`（需同时满足依赖与许可约定）。

---

## 功能模块一览（按用途）

**界面与状态**

- 状态指示器、设备多状态按钮、报警列表与确认/筛选等，便于做设备一览与操作反馈。  

**数据录入与展示**

- 数值输入框、带正则/预设的验证文本框、标签加数值单行面板；仪表盘、工业进度条、多通道趋势图等，便于参数设定与过程量展示。  

**通信与人机**

- 通信用户控件与无界面管理器，支持 TCP 客户端/服务端、串口等模式（具体枚举与 API 以源码与文档为准）；虚拟键盘面板与浮动窗体，配合焦点管理器接入触摸屏场景。  

**登录与配置**

- 扁平风格登录控件与用户管理窗体，与参数管理器分组配合持久化用户数据；主题管理器提供亮色/暗色等统一配色。  

**参数与自动化**

- `ParameterManager` 等用于 JSON 等本地配置与热重载相关能力（详见文档）；`Automation` 下提供产线通信相关类型（如 Modbus），以实际源码与后续文档为准。

更细的属性、事件与代码片段见下方「文档导航」中的控件专题。

---

## 文档导航

建议新读者按顺序：**使用手册（环境与约定）→ 快速入门 → 按需打开各控件文档**。

| 文档 | 适合谁 |
|------|--------|
| [文档中心 `docs/README.md`](docs/README.md) | 想看完整文档目录与推荐阅读路径。 |
| [IndustrialControls 使用手册](docs/IndustrialControls使用手册.md) | 需要环境、引用、参数体系、Demo 与常见问题总览。 |
| [快速入门指南](docs/快速入门指南.md) | 希望用最短示例跑通第一个窗体与典型控件。 |
| [控件与核心模块文档索引](docs/controls/README.md) | 按控件查阅说明、命名空间与示例。 |
| [ParameterManager 使用指南](docs/ParameterManager使用指南.md) | 参数分组、持久化与热重载等进阶内容。 |

---

## 版本与反馈

- 程序集版本、文件版本等信息见：`src/IndustrialControls/Properties/AssemblyInfo.cs`（当前仓库内为 **1.0.0.0**，若你已本地修改以实际文件为准）。  
- 缺陷、需求或文档笔误，欢迎在仓库中提交 **Issue** 或 **Pull Request**。

---

本 README 随仓库演进更新；与某篇子文档或旧截图不一致时，以 **`src/IndustrialControls`** 中的公开 API 与 **`docs`** 内对应页面为准。
