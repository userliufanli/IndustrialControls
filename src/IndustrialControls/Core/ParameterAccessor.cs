using System;
using System.Collections.Generic;

namespace IndustrialControls.Core
{
    /// <summary>
    /// 参数访问器 - 提供简洁的参数管理 API
    /// 
    /// 简化用法：
    /// 1. 默认配置：<see cref="Default"/> 或 <see cref="Section(string)"/> 按分组读写（只需键名）
    /// 2. 多文件：<see cref="Register"/> 后使用返回的 <see cref="ParameterManager.Section"/>
    /// </summary>
    public static class ParameterAccessor
    {
        /// <summary>
        /// 默认配置名称
        /// </summary>
        public const string DefaultConfigName = "Default";

        /// <summary>
        /// 命名参数管理器缓存（支持多配置文件）
        /// </summary>
        private static readonly Dictionary<string, Lazy<ParameterManager>> _namedInstances = 
            new Dictionary<string, Lazy<ParameterManager>>();

        private static readonly object _lock = new object();

        #region 默认实例 - 零配置使用

        /// <summary>
        /// 默认配置管理器（自动在 %AppData% 创建配置）
        /// </summary>
        public static ParameterManager Default => GetOrCreate(DefaultConfigName, () =>
        {
            string path = GetDefaultConfigPath();
            return new ParameterManager(path);
        });

        /// <summary>
        /// 获取默认配置路径（%AppData%\IndustrialControls\parameters.json）
        /// </summary>
        public static string GetDefaultConfigPath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string configDir = System.IO.Path.Combine(appData, "IndustrialControls");
            string configPath = System.IO.Path.Combine(configDir, "parameters.json");
            
            if (!System.IO.Directory.Exists(configDir))
            {
                System.IO.Directory.CreateDirectory(configDir);
            }
            
            return configPath;
        }

        #endregion

        #region 分组视图

        /// <summary>
        /// 默认配置文件下指定分组的参数视图（读写只需键名，无需重复写分组字符串）。
        /// </summary>
        public static ParameterSection Section(string groupName)
        {
            return Default.Section(groupName);
        }

        #endregion

        #region 实例管理

        /// <summary>
        /// 获取或创建参数管理器
        /// </summary>
        private static ParameterManager GetOrCreate(string name, Func<ParameterManager> factory)
        {
            lock (_lock)
            {
                if (!_namedInstances.TryGetValue(name, out var lazy))
                {
                    lazy = new Lazy<ParameterManager>(factory);
                    _namedInstances[name] = lazy;
                }
                return lazy.Value;
            }
        }

        /// <summary>
        /// 注册或获取命名配置
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="configFilePath">配置文件路径</param>
        /// <returns>参数管理器实例</returns>
        public static ParameterManager Register(string name, string configFilePath)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(configFilePath))
                throw new ArgumentNullException(nameof(configFilePath));

            string fullPath = System.IO.Path.GetFullPath(configFilePath);
            return GetOrCreate(name, () => new ParameterManager(fullPath));
        }

        /// <summary>
        /// 获取默认配置（等同于 Register(DefaultConfigName, GetDefaultConfigPath())）
        /// </summary>
        public static ParameterManager GetDefault()
        {
            return Default;
        }

        /// <summary>
        /// 获取指定名称的参数管理器
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns>参数管理器实例</returns>
        /// <exception cref="KeyNotFoundException">未找到指定配置</exception>
        public static ParameterManager Resolve(string name)
        {
            lock (_lock)
            {
                if (_namedInstances.TryGetValue(name, out var lazy) && lazy.IsValueCreated)
                    return lazy.Value;
            }
            throw new KeyNotFoundException($"未找到名为 '{name}' 的参数管理器，请先调用 Register 注册。");
        }

        /// <summary>
        /// 尝试获取指定名称的参数管理器
        /// </summary>
        public static bool TryResolve(string name, out ParameterManager manager)
        {
            lock (_lock)
            {
                if (_namedInstances.TryGetValue(name, out var lazy) && lazy.IsValueCreated)
                {
                    manager = lazy.Value;
                    return true;
                }
            }
            manager = null;
            return false;
        }

        /// <summary>
        /// 检查指定名称的配置是否已注册
        /// </summary>
        public static bool IsRegistered(string name)
        {
            lock (_lock)
            {
                return _namedInstances.TryGetValue(name, out var lazy) && lazy.IsValueCreated;
            }
        }

        /// <summary>
        /// 获取所有已注册的配置名称
        /// </summary>
        public static IEnumerable<string> GetRegisteredNames()
        {
            lock (_lock)
            {
                foreach (var kvp in _namedInstances)
                {
                    if (kvp.Value.IsValueCreated)
                        yield return kvp.Key;
                }
            }
        }

        /// <summary>
        /// 移除指定名称的配置
        /// </summary>
        public static bool Remove(string name)
        {
            lock (_lock)
            {
                if (_namedInstances.TryGetValue(name, out var lazy) && lazy.IsValueCreated)
                {
                    lazy.Value.Dispose();
                    _namedInstances.Remove(name);
                    return true;
                }
                return false;
            }
        }

        #endregion

        #region 便捷静态方法

        /// <summary>
        /// 设置参数值（默认配置）
        /// </summary>
        public static void Set<T>(string groupName, string key, T value)
        {
            Default.SetValue(groupName, key, value);
        }

        /// <summary>
        /// 获取参数值（默认配置）
        /// </summary>
        public static T Get<T>(string groupName, string key, T defaultValue = default)
        {
            return Default.GetValue(groupName, key, defaultValue);
        }

        /// <summary>
        /// 批量设置参数（默认配置）
        /// </summary>
        public static void SetBatch(string groupName, Dictionary<string, object> parameters)
        {
            Default.SetBatch(groupName, parameters);
        }

        #endregion
    }
}
