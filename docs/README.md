# 文档

本目录为 **IndustrialControls** 的开发说明，与源码同步维护；与实现不一致时以 `src/IndustrialControls` 为准。

## 建议阅读顺序

1. [入门：环境与引用](getting-started.md) — 运行 Demo、在宿主项目中引用类库。  
2. [设计约定：WinForms 与设计器](design-conventions.md) — 维护 Demo/Template 时的窗体规范。  
3. [参数与配置](parameters.md) — `ParameterManager`、`ParameterAccessor`、推荐封装方式。  
4. [控件参考总览](controls.md) — 各控件命名空间与能力索引。

## 文档一览

| 文档 | 说明 |
|------|------|
| [getting-started.md](getting-started.md) | 环境、打开解决方案、引用、目录说明 |
| [design-conventions.md](design-conventions.md) | `partial`、Designer、禁止运行时挂载可见控件等 |
| [parameters.md](parameters.md) | JSON 持久化、分组、`AppParameters` 模式、事件与热重载要点 |
| [controls.md](controls.md) | 控件与命名空间、通信模式说明、与其它文档的交叉引用 |

仓库根目录 [README.md](../README.md) 提供项目一句话介绍与最快运行步骤。
