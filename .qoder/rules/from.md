---
trigger: always_on
---
1. 使用Visual Studio设计器的拖放方式创建窗体，生成标准的Designer代码
2. 将所有控件（TextBox、NumericUpDown、ComboBox等）通过设计器添加到窗体上
3. 生成对应的partial类文件和Designer文件
4. 保持原有的功能不变：包含文本框、多行文本框、数字输入框、下拉框等控件
5. 确保控件的属性（位置、大小、字体、名称等）与当前功能需求匹配
6. 生成标准的InitializeComponent方法和控件声明
7.禁止动态添加控件