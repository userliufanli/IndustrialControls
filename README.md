# IndustrialControls

面向 **.NET Framework 4.8** 的 **WinForms** 工业上位机控件与配套能力：状态与报警、数据录入与可视化、通信、虚拟键盘、主题与参数持久化等。

| 内容 | 路径 |
|------|------|
| 类库 | `src/IndustrialControls/` |
| 演示 | `samples/IndustrialControls.Demo/` |
| 模板工程 | `samples/IndustrialControls.Template/` |
| **文档** | [`docs/README.md`](docs/README.md) |

## 本地运行

1. 用 Visual Studio 打开 `IndustrialControls.sln`。  
2. 将 **IndustrialControls.Demo** 设为启动项目，生成并运行。  
3. 需要多页面骨架时，改用 **IndustrialControls.Template**。

## 在你自己的工程中使用

在目标 WinForms（.NET Framework 4.8）项目中添加对 `src/IndustrialControls/IndustrialControls.csproj` 的**项目引用**（或引用编译产物 `IndustrialControls.dll` 及相同依赖），详见 [入门：环境与引用](docs/getting-started.md)。

## 更多信息

完整文档目录、阅读顺序与专题说明见 **[文档首页](docs/README.md)**。
