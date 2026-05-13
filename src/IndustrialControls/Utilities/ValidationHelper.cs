using System;
using System.Text.RegularExpressions;

namespace IndustrialControls.Utilities
{
    /// <summary>
    /// 数据验证辅助类
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// 验证数值是否在范围内
        /// </summary>
        public static bool ValidateRange(double value, double min, double max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// 验证整数是否在范围内
        /// </summary>
        public static bool ValidateRange(int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// 正则表达式验证
        /// </summary>
        public static bool ValidateRegex(string input, string pattern)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern))
                return false;
            try
            {
                return Regex.IsMatch(input, pattern);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 验证非空
        /// </summary>
        public static bool ValidateRequired(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// 验证数值字符串
        /// </summary>
        public static bool ValidateNumeric(string input)
        {
            return double.TryParse(input, out _);
        }

        /// <summary>
        /// 验证整数字符串
        /// </summary>
        public static bool ValidateInteger(string input)
        {
            return int.TryParse(input, out _);
        }

        /// <summary>
        /// 格式化数值
        /// </summary>
        public static string FormatNumber(double value, int decimalPlaces)
        {
            return value.ToString($"F{decimalPlaces}");
        }

        /// <summary>
        /// 钳制数值到范围内
        /// </summary>
        public static double Clamp(double value, double min, double max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        /// <summary>
        /// 钳制整数到范围内
        /// </summary>
        public static int Clamp(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        /// <summary>
        /// IP地址验证
        /// </summary>
        public static bool ValidateIPAddress(string input)
        {
            return ValidateRegex(input, @"^(\d{1,3}\.){3}\d{1,3}$");
        }

        /// <summary>
        /// 端口号验证
        /// </summary>
        public static bool ValidatePort(string input)
        {
            if (int.TryParse(input, out int port))
                return port >= 0 && port <= 65535;
            return false;
        }
    }
}
