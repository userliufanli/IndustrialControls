# 设计约定：WinForms 与设计器

本仓库中的 **IndustrialControls.Demo**、**IndustrialControls.Template** 及新增业务窗体，建议统一遵守下列约定，以便设计器合并与代码审阅。

## 窗体与控件

- 使用 Visual Studio **设计器**维护窗体：主类为 **`partial`**。  
- 所有可见控件的字段声明、`new`、属性赋值、`Controls.Add` / `Controls.AddRange`、事件 `+=` 均放在 **`*.Designer.cs`** 的 `InitializeComponent()` 中（可先 `SuspendLayout()`，结束后再 `ResumeLayout` / `PerformLayout`）。  
- **不要**在运行时代码（如 `Load`、普通业务方法、构造函数中除 `InitializeComponent()` 以外的部分）里 `new` 可见控件并挂到窗体上。

## 业务代码位置

- 业务逻辑、数据加载、与控件库 API 的交互，写在**非 Designer** 的 `partial` 类文件（如 `MainForm.cs`）中。

## 与文档的关系

更细的参数、通讯与控件用法见 [parameters.md](parameters.md) 与 [controls.md](controls.md)。
