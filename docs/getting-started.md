# 入门：环境与引用

## 环境

| 项目 | 要求 |
|------|------|
| 操作系统 | Windows 10 / 11 |
| 框架 | **.NET Framework 4.8** |
| 开发工具 | **Visual Studio**（建议 2022，安装「.NET 桌面开发」工作负载） |

类库为传统 `csproj`（非 SDK 风格），推荐使用 Visual Studio 打开 `IndustrialControls.sln` 进行生成与调试；含 `.resx` 的 WinForms 工程以 VS 内编译结果为推荐。

## 打开解决方案

1. 双击或从 VS 打开仓库根目录下的 **`IndustrialControls.sln`**。  
2. 在解决方案资源管理器中右键 **IndustrialControls.Demo** →「设为启动项目」。  
3. 选择 `Debug | Any CPU`（或 `Release`），按 F5 运行。

**IndustrialControls.Template** 提供多页面、导航与业务分层的示例结构；需要时可将其设为启动项目对照改造。

## 在宿主项目中引用类库

1. 在 Visual Studio 中打开你的 **Windows Forms 应用（.NET Framework 4.8）** 解决方案。  
2. 右键宿主项目 →「添加」→「项目引用」→ 浏览并勾选本仓库中的 **`IndustrialControls`** 项目（`src/IndustrialControls/IndustrialControls.csproj`）。  
   - 若使用已编译的 **`IndustrialControls.dll`**，需同时满足类库 `.csproj` 中列出的程序集引用。  
3. 生成宿主项目。在代码中加入 `using` 相应命名空间；控件可从工具箱添加（视 VS 与控件注册情况而定），或参阅 Demo 中的用法。

## 仓库目录（与文档相关）

| 路径 | 说明 |
|------|------|
| `src/IndustrialControls/` | 控件库源码 |
| `samples/IndustrialControls.Demo/` | 功能演示与推荐用法示例 |
| `samples/IndustrialControls.Template/` | 上位机页面结构模板 |
| `docs/` | 本目录下的 Markdown 文档 |

下一步建议阅读 [设计约定](design-conventions.md) 与 [参数与配置](parameters.md)。
