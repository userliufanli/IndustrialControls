using System;
using System.IO;

namespace IndustrialControls.Template.Core
{
    /// <summary>简单文件日志（模板用，可按项目替换为 NLog 等）。</summary>
    public static class Logger
    {
        private static readonly object Sync = new object();
        private static string _logDirectory;

        public static void Initialize(string logDirectory)
        {
            _logDirectory = logDirectory;
            try
            {
                if (!string.IsNullOrEmpty(_logDirectory) && !Directory.Exists(_logDirectory))
                    Directory.CreateDirectory(_logDirectory);
            }
            catch
            {
                // 忽略初始化目录失败
            }
        }

        public static void Info(string message) => Write("INFO", message);

        public static void Warn(string message) => Write("WARN", message);

        public static void Error(string message) => Write("ERROR", message);

        private static void Write(string level, string message)
        {
            string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}";
            System.Diagnostics.Debug.WriteLine(line);
            if (string.IsNullOrEmpty(_logDirectory))
                return;
            try
            {
                string file = Path.Combine(_logDirectory, $"app-{DateTime.Now:yyyyMMdd}.log");
                lock (Sync)
                {
                    File.AppendAllText(file, line + Environment.NewLine);
                }
            }
            catch
            {
                // 忽略写盘失败
            }
        }
    }
}
