# 数据输入控件 (DataInput) API 使用手册

## 概述

数据输入控件组包含三个专业控件:`NumericInputBox`(数值输入框)、`ValidatedTextBox`(验证文本框)和`DataInputPanel`(数据输入面板),提供完整的工业参数输入解决方案。

## 命名空间

```csharp
IndustrialControls.Controls.DataInput
```

---

## NumericInputBox - 数值输入框

### 概述

带上下限验证、步进按钮、单位显示的数值输入控件。

### 快速开始

```csharp
using IndustrialControls.Controls.DataInput;

// 创建数值输入框
var numInput = new NumericInputBox
{
    Size = new Size(200, 36),
    Location = new Point(10, 10),
    Value = 25.5,
    Minimum = 0,
    Maximum = 100,
    Step = 0.5,
    DecimalPlaces = 2,
    Unit = "°C",
    ShowButtons = true
};

// 监听值变化
numInput.ValueChanged += (sender, newValue) =>
{
    Console.WriteLine($"温度设置为: {newValue} °C");
};

// 监听验证失败
numInput.ValidationFailed += (sender, errorMsg) =>
{
    MessageBox.Show(errorMsg, "输入错误", 
        MessageBoxButtons.OK, MessageBoxIcon.Warning);
};

this.Controls.Add(numInput);
```

### 属性

#### Value

- **类型**: `double`
- **默认值**: `0.0`
- **说明**: 当前数值(自动钳位到Minimum-Maximum范围)
- **示例**:
  ```csharp
  numInput.Value = 50.5;
  double currentValue = numInput.Value;
  ```

#### Minimum

- **类型**: `double`
- **默认值**: `0.0`
- **说明**: 最小值
- **示例**:
  ```csharp
  numInput.Minimum = -100; // 允许负值
  ```

#### Maximum

- **类型**: `double`
- **默认值**: `100.0`
- **说明**: 最大值
- **示例**:
  ```csharp
  numInput.Maximum = 500;
  ```

#### Step

- **类型**: `double`
- **默认值**: `1.0`
- **说明**: 步进值(点击+/-按钮时的增量)
- **示例**:
  ```csharp
  numInput.Step = 0.1; // 精细调节
  ```

#### DecimalPlaces

- **类型**: `int`
- **默认值**: `2`
- **说明**: 小数位数
- **示例**:
  ```csharp
  numInput.DecimalPlaces = 3; // 显示3位小数
  ```

#### Unit

- **类型**: `string`
- **默认值**: `""`
- **说明**: 数值单位文本
- **示例**:
  ```csharp
  numInput.Unit = "MPa";
  ```

#### ShowButtons

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否显示步进按钮(+/-)
- **示例**:
  ```csharp
  numInput.ShowButtons = false; // 隐藏按钮
  ```

#### IsValid

- **类型**: `bool` (只读)
- **说明**: 当前输入是否有效
- **示例**:
  ```csharp
  if (numInput.IsValid)
  {
      SaveValue(numInput.Value);
  }
  ```

### 事件

#### ValueChanged

值变更时触发。

**事件参数**: `EventHandler<double>`

**示例**:
```csharp
numInput.ValueChanged += (sender, newValue) =>
{
    // 实时应用参数
    Device.SetParameter("temperature", newValue);
};
```

#### ValidationFailed

验证失败时触发。

**事件参数**: `EventHandler<string>`

**示例**:
```csharp
numInput.ValidationFailed += (sender, errorMsg) =>
{
    MessageBox.Show(errorMsg, "验证失败");
};
```

---

## ValidatedTextBox - 验证文本框

### 概述

带实时验证的文本输入框,支持多种预定义验证规则。

### 快速开始

```csharp
using IndustrialControls.Controls.DataInput;

// 创建验证文本框
var validText = new ValidatedTextBox
{
    Size = new Size(200, 32),
    Location = new Point(10, 10),
    ValidationPreset = ValidationPreset.Email,
    AllowEmpty = false
};

// 监听验证结果
validText.ValidationChanged += (sender, isValid) =>
{
    if (isValid)
    {
        validText.BackColor = Color.LightGreen;
    }
    else
    {
        validText.BackColor = Color.LightCoral;
    }
};

this.Controls.Add(validText);
```

### 属性

#### ValidationPreset

- **类型**: `ValidationPreset`
- **默认值**: `ValidationPreset.None`
- **说明**: 预定义验证规则
- **示例**:
  ```csharp
  validText.ValidationPreset = ValidationPreset.Integer;
  ```

#### AllowEmpty

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否允许空值
- **示例**:
  ```csharp
  validText.AllowEmpty = false; // 必填项
  ```

#### IsValid

- **类型**: `bool` (只读)
- **说明**: 当前输入是否通过验证
- **示例**:
  ```csharp
  if (validText.IsValid)
  {
      SubmitForm(validText.Text);
  }
  ```

#### ErrorMessage

- **类型**: `string` (只读)
- **说明**: 验证失败时的错误消息
- **示例**:
  ```csharp
  if (!validText.IsValid)
  {
      errorProvider.SetError(validText, validText.ErrorMessage);
  }
  ```

### 验证预设

```csharp
public enum ValidationPreset
{
    None,       // 无验证
    Email,      // 电子邮件
    Integer,    // 整数
    Double,     // 浮点数
    PhoneNumber,// 电话号码
    IPAddress,  // IP地址
    Url,        // URL
    Custom      // 自定义
}
```

### 自定义验证

```csharp
// 使用自定义验证
validText.ValidationPreset = ValidationPreset.Custom;
validText.CustomValidator = (text, out string error) =>
{
    if (text.Length < 6)
    {
        error = "长度至少为6个字符";
        return false;
    }
    
    if (!text.Any(char.IsDigit))
    {
        error = "必须包含至少一个数字";
        return false;
    }
    
    error = "";
    return true;
};
```

### 事件

#### ValidationChanged

验证状态变更时触发。

**事件参数**: `EventHandler<bool>`

**示例**:
```csharp
validText.ValidationChanged += (sender, isValid) =>
{
    btnSubmit.Enabled = isValid;
};
```

---

## DataInputPanel - 数据输入面板

### 概述

集成多个输入字段的卡片式面板,支持标题、验证汇总和统一提交。

### 快速开始

```csharp
using IndustrialControls.Controls.DataInput;

// 创建输入面板
var inputPanel = new DataInputPanel
{
    Size = new Size(400, 300),
    Location = new Point(10, 10),
    PanelTitle = "设备参数设置",
    ShowValidationSummary = true
};

// 添加数值输入字段
var tempField = inputPanel.AddNumericField(
    "温度", 
    25.0,   // 默认值
    0,      // 最小值
    100,    // 最大值
    0.5,    // 步进
    1,      // 小数位数
    "°C"    // 单位
);

var pressureField = inputPanel.AddNumericField(
    "压力",
    1.0,
    0,
    10,
    0.1,
    2,
    "MPa"
);

// 添加文本输入字段
var nameField = inputPanel.AddTextField(
    "设备名称",
    ValidationPreset.None,
    allowEmpty: false
);

// 监听提交
inputPanel.SubmitRequested += (sender, e) =>
{
    if (inputPanel.ValidateAll())
    {
        // 获取所有值
        double temp = tempField.Value;
        double pressure = pressureField.Value;
        string name = nameField.Text;
        
        SaveDeviceParameters(name, temp, pressure);
        MessageBox.Show("参数保存成功!");
    }
};

this.Controls.Add(inputPanel);
```

### 属性

#### PanelTitle

- **类型**: `string`
- **说明**: 面板标题
- **示例**:
  ```csharp
  inputPanel.PanelTitle = "工艺参数配置";
  ```

#### ShowValidationSummary

- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否显示验证汇总信息
- **示例**:
  ```csharp
  inputPanel.ShowValidationSummary = true;
  ```

### 方法

#### AddNumericField(...)

添加数值输入字段。

**参数**:
- `label`: 字段标签
- `defaultValue`: 默认值
- `minimum`: 最小值
- `maximum`: 最大值
- `step`: 步进值
- `decimalPlaces`: 小数位数
- `unit`: 单位

**返回**: `NumericInputBox`

**示例**:
```csharp
var field = inputPanel.AddNumericField(
    "速度", 0, 0, 3000, 10, 0, "rpm"
);
```

#### AddTextField(...)

添加文本输入字段。

**参数**:
- `label`: 字段标签
- `validation`: 验证预设
- `allowEmpty`: 是否允许空

**返回**: `ValidatedTextBox`

**示例**:
```csharp
var field = inputPanel.AddTextField(
    "操作员", ValidationPreset.None, false
);
```

#### ValidateAll()

验证所有字段。

**返回**: `bool` - 是否全部通过验证

**示例**:
```csharp
if (inputPanel.ValidateAll())
{
    SubmitData();
}
else
{
    MessageBox.Show("请检查输入字段");
}
```

#### ResetAll()

重置所有字段到默认值。

**示例**:
```csharp
inputPanel.ResetAll();
```

### 事件

#### SubmitRequested

提交请求时触发(通常由提交按钮触发)。

**事件参数**: `EventHandler`

**示例**:
```csharp
inputPanel.SubmitRequested += (sender, e) =>
{
    if (inputPanel.ValidateAll())
    {
        SaveData();
    }
};
```

## 完整示例

### 设备参数配置表单

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
using IndustrialControls.Controls.DataInput;

namespace DeviceConfig
{
    public partial class ConfigForm : Form
    {
        private DataInputPanel configPanel;
        private NumericInputBox temperatureField;
        private NumericInputBox pressureField;
        private NumericInputBox speedField;
        private ValidatedTextBox operatorField;

        public ConfigForm()
        {
            InitializeComponent();
            InitializeConfigPanel();
        }

        private void InitializeConfigPanel()
        {
            configPanel = new DataInputPanel
            {
                Dock = DockStyle.Fill,
                PanelTitle = "设备工艺参数配置",
                ShowValidationSummary = true
            };

            // 温度字段
            temperatureField = configPanel.AddNumericField(
                "加热温度",
                150.0,  // 默认
                50,     // 最小
                300,    // 最大
                1,      // 步进
                1,      // 小数位
                "°C"    // 单位
            );

            // 压力字段
            pressureField = configPanel.AddNumericField(
                "工作压力",
                0.5,
                0.1,
                2.0,
                0.1,
                2,
                "MPa"
            );

            // 速度字段
            speedField = configPanel.AddNumericField(
                "运行速度",
                1000,
                100,
                5000,
                50,
                0,
                "rpm"
            );

            // 操作员字段
            operatorField = configPanel.AddTextField(
                "操作员",
                ValidationPreset.None,
                allowEmpty: false
            );

            // 订阅值变化
            temperatureField.ValueChanged += (s, v) => 
                LogChange("温度", v);
            
            pressureField.ValueChanged += (s, v) => 
                LogChange("压力", v);

            // 提交事件
            configPanel.SubmitRequested += OnSubmit;

            this.Controls.Add(configPanel);
        }

        private void LogChange(string paramName, double newValue)
        {
            Console.WriteLine($"[{DateTime.Now}] {paramName} = {newValue}");
        }

        private void OnSubmit(object sender, EventArgs e)
        {
            if (!configPanel.ValidateAll())
            {
                MessageBox.Show(
                    "请检查输入参数!\n" +
                    "确保所有必填项已填写且数值在有效范围内。",
                    "验证失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                // 收集参数
                var parameters = new
                {
                    Temperature = temperatureField.Value,
                    Pressure = pressureField.Value,
                    Speed = speedField.Value,
                    Operator = operatorField.Text,
                    Timestamp = DateTime.Now
                };

                // 保存到数据库或配置文件
                SaveParameters(parameters);

                MessageBox.Show(
                    "参数配置已保存!",
                    "成功",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"保存失败: {ex.Message}",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void SaveParameters(object parameters)
        {
            // 实现保存逻辑
            Console.WriteLine($"保存参数: {parameters}");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 重置所有字段
            configPanel.ResetAll();
        }
    }
}
```

## 注意事项

1. **数值钳位**: `NumericInputBox` 的 `Value` 自动钳位到 `Minimum-Maximum` 范围
2. **实时验证**: `ValidatedTextBox` 在每次文本变化时实时验证
3. **线程安全**: 可在后台线程修改控件属性
4. **设计器支持**: 所有控件支持Visual Studio设计器
5. **主题适配**: 自动跟随系统主题

## 最佳实践

1. **合理设置范围**: 为数值字段设置合理的 `Minimum` 和 `Maximum`
2. **验证反馈**: 在 `ValidationFailed` 事件中提供清晰的错误提示
3. **单位标注**: 为数值字段添加 `Unit` 提高可读性
4. **步进优化**: 根据精度需求设置合适的 `Step` 值
5. **必填标注**: 对必填字段设置 `AllowEmpty = false`

## 相关控件

- **CommunicationControl**: 通讯控件,发送配置参数到设备
- **DeviceControlButton**: 设备控制按钮,应用新配置
- **StatusIndicator**: 状态指示器,显示设备运行状态
- **TrendChart**: 趋势图,可视化参数变化
