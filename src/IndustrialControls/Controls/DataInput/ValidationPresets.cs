using System;

namespace IndustrialControls.Controls.DataInput
{
    /// <summary>
    /// 常用验证类型预设
    /// </summary>
    public enum ValidationPreset
    {
        /// <summary>无预设验证</summary>
        None,
        /// <summary>邮箱地址验证</summary>
        Email,
        /// <summary>中国大陆手机号码验证</summary>
        MobilePhone,
        /// <summary>身份证号码验证（18位）</summary>
        IdCard,
        /// <summary>URL地址验证</summary>
        Url,
        /// <summary>纯数字验证</summary>
        Number,
        /// <summary>密码强度验证（至少8位，包含大小写字母和数字）</summary>
        StrongPassword,
        /// <summary>IP地址验证（IPv4）</summary>
        IpAddress,
        /// <summary>中文姓名验证</summary>
        ChineseName,
        /// <summary>邮政编码验证（中国）</summary>
        PostalCode
    }

    /// <summary>
    /// 验证预设配置类，提供常用验证规则的完整配置
    /// </summary>
    public static class ValidationPresets
    {
        /// <summary>
        /// 获取验证预设的配置信息
        /// </summary>
        /// <param name="preset">验证预设类型</param>
        /// <returns>验证配置（正则表达式、错误消息、占位符）</returns>
        public static ValidationConfig GetConfig(ValidationPreset preset)
        {
            switch (preset)
            {
                case ValidationPreset.Email:
                    return new ValidationConfig
                    {
                        Pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
                        ErrorMessage = "请输入有效的邮箱地址",
                        Placeholder = "example@domain.com",
                        Required = true,
                        Description = "邮箱地址验证"
                    };

                case ValidationPreset.MobilePhone:
                    return new ValidationConfig
                    {
                        Pattern = @"^1[3-9]\d{9}$",
                        ErrorMessage = "请输入有效的11位手机号码",
                        Placeholder = "13800138000",
                        Required = true,
                        Description = "中国大陆手机号码验证"
                    };

                case ValidationPreset.IdCard:
                    return new ValidationConfig
                    {
                        Pattern = @"^\d{17}[\dXx]$",
                        ErrorMessage = "请输入有效的18位身份证号码",
                        Placeholder = "110101199001011234",
                        Required = true,
                        Description = "身份证号码验证（18位）"
                    };

                case ValidationPreset.Url:
                    return new ValidationConfig
                    {
                        Pattern = @"^(https?://)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(/.*)?$",
                        ErrorMessage = "请输入有效的URL地址",
                        Placeholder = "https://www.example.com",
                        Required = false,
                        Description = "URL地址验证"
                    };

                case ValidationPreset.Number:
                    return new ValidationConfig
                    {
                        Pattern = @"^-?\d+(\.\d+)?$",
                        ErrorMessage = "请输入有效的数字",
                        Placeholder = "123.45",
                        Required = false,
                        Description = "纯数字验证"
                    };

                case ValidationPreset.StrongPassword:
                    return new ValidationConfig
                    {
                        Pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@$!%*?&]{8,}$",
                        ErrorMessage = "密码至少8位，需包含大小写字母和数字",
                        Placeholder = "请输入强密码",
                        Required = true,
                        Description = "密码强度验证"
                    };

                case ValidationPreset.IpAddress:
                    return new ValidationConfig
                    {
                        Pattern = @"^(\d{1,3}\.){3}\d{1,3}$",
                        ErrorMessage = "请输入有效的IPv4地址",
                        Placeholder = "192.168.1.1",
                        Required = true,
                        Description = "IP地址验证（IPv4）"
                    };

                case ValidationPreset.ChineseName:
                    return new ValidationConfig
                    {
                        Pattern = @"^[\u4e00-\u9fa5]{2,20}$",
                        ErrorMessage = "请输入2-20位中文姓名",
                        Placeholder = "张三",
                        Required = true,
                        Description = "中文姓名验证"
                    };

                case ValidationPreset.PostalCode:
                    return new ValidationConfig
                    {
                        Pattern = @"^\d{6}$",
                        ErrorMessage = "请输入有效的6位邮政编码",
                        Placeholder = "100000",
                        Required = true,
                        Description = "邮政编码验证（中国）"
                    };

                default:
                    return new ValidationConfig
                    {
                        Pattern = "",
                        ErrorMessage = "",
                        Placeholder = "",
                        Required = false,
                        Description = "无预设验证"
                    };
            }
        }
    }

    /// <summary>
    /// 验证配置数据结构
    /// </summary>
    public struct ValidationConfig
    {
        /// <summary>正则表达式</summary>
        public string Pattern { get; set; }
        
        /// <summary>错误消息</summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>占位符文本</summary>
        public string Placeholder { get; set; }
        
        /// <summary>是否必填</summary>
        public bool Required { get; set; }
        
        /// <summary>描述信息</summary>
        public string Description { get; set; }
    }
}
