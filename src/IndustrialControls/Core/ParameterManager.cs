using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;

namespace IndustrialControls.Core
{
    /// <summary>
    /// 本地参数管理器 - 统一管理应用程序的持久化参数
    /// 特性：JSON存储、分组管理、类型安全、线程安全、热更新
    /// </summary>
    public class ParameterManager : IDisposable
    {
        #region 字段与属性

        private readonly string _configFilePath;
        private readonly Dictionary<string, ParameterGroup> _parameterGroups;
        private readonly ReaderWriterLockSlim _lock;
        private FileSystemWatcher _fileWatcher;
        private bool _disposed;
        private DateTime _lastLoadTime;
        private readonly object _logLock = new object(); // 日志线程安全锁

        private System.Threading.Timer _saveDebounceTimer;
        private const int SaveDebounceMs = 300; // 300ms 合并保存
        private volatile int _savePending; // 0=未挂起, 1=挂起（用 int 以支持 Interlocked）
        private int _saveSuspendCount; // > 0 时暂停自动保存

        private System.Threading.Timer _hotReloadDebounceTimer;
        private const int HotReloadDebounceMs = 1000;

        /// <summary>
        /// 参数变更事件
        /// </summary>
        public event EventHandler<ParameterChangedEventArgs> ParameterChanged;

        /// <summary>
        /// 保存失败事件
        /// </summary>
        public event EventHandler<Exception> SaveFailed;

        /// <summary>
        /// 加载失败事件
        /// </summary>
        public event EventHandler<Exception> LoadFailed;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string ConfigFilePath => _configFilePath;

        /// <summary>
        /// 是否启用热更新监控
        /// </summary>
        public bool EnableHotReload { get; set; } = true;

        /// <summary>
        /// 是否启用详细日志记录
        /// </summary>
        public bool EnableLogging { get; set; } = true;

        #endregion

        #region 构造函数

        /// <summary>
        /// 创建参数管理器实例
        /// </summary>
        /// <param name="configFilePath">配置文件路径（JSON格式）</param>
        /// <exception cref="ArgumentNullException">configFilePath 为 null 时抛出</exception>
        /// <exception cref="SecurityException">路径包含非法字符或路径遍历攻击时抛出</exception>
        public ParameterManager(string configFilePath)
        {
            if (configFilePath == null)
                throw new ArgumentNullException(nameof(configFilePath));

            // 安全路径验证：防止路径遍历攻击
            ValidateFilePath(configFilePath);

            // 规范化路径
            _configFilePath = Path.GetFullPath(configFilePath);
            _parameterGroups = new Dictionary<string, ParameterGroup>();
            _lock = new ReaderWriterLockSlim();
            _lastLoadTime = DateTime.MinValue;

            // 设计器模式下跳过文件操作
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            // 确保目录存在
            string directory = Path.GetDirectoryName(_configFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                try
                {
                    Directory.CreateDirectory(directory);
                    LogInfo($"创建配置目录: {directory}");
                }
                catch (UnauthorizedAccessException ex)
                {
                    LogError($"权限不足，无法创建目录: {ex.Message}");
                    throw new InvalidOperationException($"无法创建配置目录，请检查权限设置。", ex);
                }
                catch (Exception ex)
                {
                    LogError($"创建目录失败: {ex.Message}");
                    throw new InvalidOperationException($"创建配置目录失败: {ex.Message}", ex);
                }
            }

            // 加载现有配置
            LoadFromFile();

            // 初始化文件监控（热更新）
            if (EnableHotReload)
            {
                InitializeFileWatcher();
            }
        }

        /// <summary>
        /// 获取固定分组的参数视图，读写时只需提供键名。
        /// </summary>
        public ParameterSection Section(string groupName)
        {
            return new ParameterSection(this, groupName);
        }

        #endregion

        #region 参数操作

        /// <summary>
        /// 获取参数值（带默认值）
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="groupName">分组名称</param>
        /// <param name="key">参数键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        public T GetValue<T>(string groupName, string key, T defaultValue = default)
        {
            _lock.EnterReadLock();
            try
            {
                if (_parameterGroups.TryGetValue(groupName, out ParameterGroup group))
                {
                    if (group.Parameters.TryGetValue(key, out ParameterItem item))
                    {
                        try
                        {
                            // 类型安全转换
                            return ConvertToType<T>(item.Value);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"[ParameterManager] 类型转换失败: {ex.Message}");
                            return defaultValue;
                        }
                    }
                }

                // 参数不存在，返回默认值（不在读取锁内调用 SetValue）
                return defaultValue;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 设置参数值（自动保存）
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="groupName">分组名称</param>
        /// <param name="key">参数键</param>
        /// <param name="value">参数值</param>
        public void SetValue<T>(string groupName, string key, T value)
        {
            bool changed = false;
            _lock.EnterWriteLock();
            try
            {
                // 获取或创建分组
                if (!_parameterGroups.TryGetValue(groupName, out ParameterGroup group))
                {
                    group = new ParameterGroup(groupName);
                    _parameterGroups[groupName] = group;
                }

                // 检查值是否真的变化了（优化：避免无意义的保存和事件触发）
                if (group.Parameters.TryGetValue(key, out ParameterItem existingItem))
                {
                    // 如果值相同，直接返回，不触发事件也不保存
                    if (Equals(existingItem.Value, value))
                    {
                        return;
                    }
                }

                // 更新或添加参数
                bool isUpdate = group.Parameters.ContainsKey(key);
                group.Parameters[key] = new ParameterItem
                {
                    Key = key,
                    Value = value,
                    Type = typeof(T).FullName,
                    Group = groupName,
                    LastModified = DateTime.Now
                };

                // 触发事件（只有值真正变化时才会到这里）
                if (isUpdate)
                {
                    OnParameterChanged(groupName, key, value, ParameterChangeType.Modified);
                }
                else
                {
                    OnParameterChanged(groupName, key, value, ParameterChangeType.Added);
                }

                changed = true;
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            // 锁外调度保存
            if (changed)
            {
                ScheduleSave();
            }
        }

        /// <summary>
        /// 删除参数
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <param name="key">参数键</param>
        /// <returns>是否删除成功</returns>
        public bool Delete(string groupName, string key)
        {
            bool removed = false;
            _lock.EnterWriteLock();
            try
            {
                if (_parameterGroups.TryGetValue(groupName, out ParameterGroup group))
                {
                    if (group.Parameters.Remove(key))
                    {
                        OnParameterChanged(groupName, key, null, ParameterChangeType.Deleted);
                        removed = true;
                    }
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            if (removed)
                ScheduleSave();
            return removed;
        }

        /// <summary>
        /// 删除整个分组
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteGroup(string groupName)
        {
            bool removed = false;
            _lock.EnterWriteLock();
            try
            {
                if (_parameterGroups.TryGetValue(groupName, out var group))
                {
                    _parameterGroups.Remove(groupName);
                    OnParameterChanged(groupName, null, null, ParameterChangeType.GroupDeleted);
                    removed = true;
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            if (removed)
                ScheduleSave();
            return removed;
        }

        /// <summary>
        /// 检查参数是否存在
        /// </summary>
        public bool Contains(string groupName, string key)
        {
            _lock.EnterReadLock();
            try
            {
                return _parameterGroups.TryGetValue(groupName, out ParameterGroup group) &&
                       group.Parameters.ContainsKey(key);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 获取所有分组名称
        /// </summary>
        public IEnumerable<string> GetGroupNames()
        {
            _lock.EnterReadLock();
            try
            {
                return _parameterGroups.Keys.ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 获取分组下的所有参数键
        /// </summary>
        public IEnumerable<string> GetKeys(string groupName)
        {
            _lock.EnterReadLock();
            try
            {
                if (_parameterGroups.TryGetValue(groupName, out ParameterGroup group))
                {
                    return group.Parameters.Keys.ToList();
                }
                return Enumerable.Empty<string>();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 批量设置参数
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <param name="parameters">参数字典</param>
        public void SetBatch(string groupName, Dictionary<string, object> parameters)
        {
            if (parameters == null) return;

            _lock.EnterWriteLock();
            try
            {
                // 获取或创建分组
                if (!_parameterGroups.TryGetValue(groupName, out ParameterGroup group))
                {
                    group = new ParameterGroup(groupName);
                    _parameterGroups[groupName] = group;
                }

                foreach (var kvp in parameters)
                {
                    group.Parameters[kvp.Key] = new ParameterItem
                    {
                        Key = kvp.Key,
                        Value = kvp.Value,
                        Type = kvp.Value?.GetType().FullName,
                        Group = groupName,
                        LastModified = DateTime.Now
                    };
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            ScheduleSave();
        }

        /// <summary>
        /// 批量获取参数
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <returns>参数字典</returns>
        public Dictionary<string, object> GetBatch(string groupName)
        {
            _lock.EnterReadLock();
            try
            {
                if (_parameterGroups.TryGetValue(groupName, out ParameterGroup group))
                {
                    return new Dictionary<string, object>(group.Parameters
                        .ToDictionary(k => k.Key, v => v.Value.Value));
                }
                return new Dictionary<string, object>();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 获取或设置参数（不存在时设置默认值）
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="groupName">分组名称</param>
        /// <param name="key">参数键</param>
        /// <param name="seedValue">参数不存在（或无法转换）时写入的值</param>
        /// <returns>已存在的转换后的值，或刚写入的 <paramref name="seedValue"/></returns>
        public T GetOrSet<T>(string groupName, string key, T seedValue)
        {
            bool needSave = false;
            T result;
            _lock.EnterWriteLock();
            try
            {
                if (!_parameterGroups.TryGetValue(groupName, out ParameterGroup group))
                {
                    group = new ParameterGroup(groupName);
                    _parameterGroups[groupName] = group;
                }

                if (group.Parameters.TryGetValue(key, out ParameterItem existingItem))
                {
                    try
                    {
                        return ConvertToType<T>(existingItem.Value);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[ParameterManager] GetOrSet 类型转换失败，将写入初始值: {ex.Message}");
                    }
                }

                bool existed = group.Parameters.ContainsKey(key);
                group.Parameters[key] = new ParameterItem
                {
                    Key = key,
                    Value = seedValue,
                    Type = typeof(T).FullName,
                    Group = groupName,
                    LastModified = DateTime.Now
                };

                if (existed)
                    OnParameterChanged(groupName, key, seedValue, ParameterChangeType.Modified);
                else
                    OnParameterChanged(groupName, key, seedValue, ParameterChangeType.Added);

                needSave = true;
                result = seedValue;
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            if (needSave)
                ScheduleSave();
            return result;
        }

        /// <summary>
        /// 尝试获取参数值
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="groupName">分组名称</param>
        /// <param name="key">参数键</param>
        /// <param name="value">输出参数值</param>
        /// <returns>是否获取成功</returns>
        public bool TryGetValue<T>(string groupName, string key, out T value)
        {
            value = default;
            _lock.EnterReadLock();
            try
            {
                if (!_parameterGroups.TryGetValue(groupName, out ParameterGroup group))
                    return false;
                if (!group.Parameters.TryGetValue(key, out ParameterItem item))
                    return false;
                try
                {
                    value = ConvertToType<T>(item.Value);
                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[ParameterManager] TryGetValue 类型转换失败: {ex.Message}");
                    return false;
                }
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        #endregion

        #region 文件操作

        private static readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();

        /// <summary>
        /// 从文件加载配置
        /// </summary>
        public void LoadFromFile()
        {
            _lock.EnterWriteLock();
            try
            {
                if (!File.Exists(_configFilePath))
                {
                    // 文件不存在，创建新文件
                    SaveToFile();
                    return;
                }

                string json = File.ReadAllText(_configFilePath);
                var config = _serializer.Deserialize<ParameterConfig>(json);

                if (config?.Groups != null)
                {
                    _parameterGroups.Clear();
                    foreach (var group in config.Groups)
                    {
                        _parameterGroups[group.Name] = group;
                    }
                }

                _lastLoadTime = File.GetLastWriteTime(_configFilePath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ParameterManager] 加载配置失败: {ex.Message}");
                OnLoadFailed(ex);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 保存配置到文件（公共方法，立即保存）
        /// </summary>
        public void SaveToFile()
        {
            _saveDebounceTimer?.Dispose();
            _saveDebounceTimer = null;
            _savePending = 0;
            SaveToFileInternal();
        }

        /// <summary>
        /// 保存配置到文件（内部方法，使用原子写入防止文件损坏）
        /// </summary>
        private void SaveToFileInternal()
        {
            string tempPath = null;
            try
            {
                List<ParameterGroup> groupsCopy;
                _lock.EnterReadLock();
                try
                {
                    groupsCopy = _parameterGroups.Values.ToList();
                }
                finally
                {
                    _lock.ExitReadLock();
                }

                var config = new ParameterConfig
                {
                    Version = "1.0",
                    LastModified = DateTime.Now,
                    Groups = groupsCopy
                };

                string json = _serializer.Serialize(config);

                // 原子写入：先写临时文件，再替换目标文件
                tempPath = _configFilePath + ".tmp";
                File.WriteAllText(tempPath, json);

                // 备份旧文件（如存在）
                if (File.Exists(_configFilePath))
                {
                    string backupPath = _configFilePath + ".bak";
                    File.Copy(_configFilePath, backupPath, true);
                }

                File.Copy(tempPath, _configFilePath, true);

                _lastLoadTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ParameterManager] 保存配置失败: {ex.Message}");
                OnSaveFailed(ex);
            }
            finally
            {
                // 确保临时文件被清理
                if (tempPath != null && File.Exists(tempPath))
                {
                    try { File.Delete(tempPath); } catch { }
                }
            }
        }

        /// <summary>
        /// 暂停自动保存（可嵌套调用）
        /// </summary>
        public void SuspendAutoSave()
        {
            System.Threading.Interlocked.Increment(ref _saveSuspendCount);
        }

        /// <summary>
        /// 恢复自动保存（匹配 SuspendAutoSave 调用）
        /// </summary>
        public void ResumeAutoSave()
        {
            if (System.Threading.Interlocked.Decrement(ref _saveSuspendCount) == 0)
            {
                // 使用原子操作确保不会和 Timer 回调产生竞态
                if (System.Threading.Interlocked.Exchange(ref _savePending, 0) == 1)
                {
                    SaveToFileInternal();
                }
            }
        }

        /// <summary>
        /// 检查是否允许自动保存
        /// </summary>
        private bool CanAutoSave => _saveSuspendCount == 0 && !_disposed;

        /// <summary>
        /// 调度延迟保存（防抖：300ms 内的多次修改合并为一次写入）
        /// </summary>
        private void ScheduleSave()
        {
            if (!CanAutoSave) return;

            _savePending = 1;
            _saveDebounceTimer?.Dispose();
            _saveDebounceTimer = new System.Threading.Timer(_ =>
            {
                // 使用原子操作：防止和 ResumeAutoSave / SaveToFile 产生竞态
                if (System.Threading.Interlocked.Exchange(ref _savePending, 0) == 1)
                {
                    if (!_disposed)
                    {
                        SaveToFileInternal();
                    }
                }
            }, null, SaveDebounceMs, Timeout.Infinite);
        }

        #endregion

        #region 热更新

        /// <summary>
        /// 初始化文件监控
        /// </summary>
        private void InitializeFileWatcher()
        {
            string directory = Path.GetDirectoryName(_configFilePath);
            string fileName = Path.GetFileName(_configFilePath);

            if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
                return;

            _fileWatcher = new FileSystemWatcher(directory, fileName)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
            };

            _fileWatcher.Changed += OnFileChanged;
            _fileWatcher.EnableRaisingEvents = true;
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            // 使用 Timer 防抖，避免 FileSystemWatcher 的重复触发
            _hotReloadDebounceTimer?.Dispose();
            _hotReloadDebounceTimer = new System.Threading.Timer(_ =>
            {
                if (_disposed) return;

                try
                {
                    DateTime fileTime = File.GetLastWriteTime(_configFilePath);
                    if (fileTime > _lastLoadTime.AddSeconds(1))
                    {
                        System.Diagnostics.Debug.WriteLine("[ParameterManager] 检测到配置文件变更，重新加载...");
                        LoadFromFile();
                        OnParameterChanged(null, null, null, ParameterChangeType.Reloaded);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[ParameterManager] 热更新失败: {ex.Message}");
                }
            }, null, HotReloadDebounceMs, Timeout.Infinite);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 类型安全转换
        /// </summary>
        private static T ConvertToType<T>(object value)
        {
            if (value == null)
                return default;

            if (value is T typedValue)
                return typedValue;

            // 处理可空类型
            Type targetType = typeof(T);
            if (Nullable.GetUnderlyingType(targetType) is Type underlyingType)
            {
                return (T)Convert.ChangeType(value, underlyingType);
            }

            // 特殊处理数字类型转换（JavaScriptSerializer 会将数字转为 long/double）
            if (targetType == typeof(int))
            {
                return (T)(object)Convert.ToInt32(value);
            }
            if (targetType == typeof(long))
            {
                return (T)(object)Convert.ToInt64(value);
            }
            if (targetType == typeof(double))
            {
                return (T)(object)Convert.ToDouble(value);
            }
            if (targetType == typeof(float))
            {
                return (T)(object)Convert.ToSingle(value);
            }

            return (T)Convert.ChangeType(value, targetType);
        }

        /// <summary>
        /// 触发参数变更事件
        /// </summary>
        private void OnParameterChanged(string groupName, string key, object value, ParameterChangeType changeType)
        {
            ParameterChanged?.Invoke(this, new ParameterChangedEventArgs
            {
                GroupName = groupName,
                Key = key,
                Value = value,
                ChangeType = changeType,
                Timestamp = DateTime.Now
            });
        }

        private void OnSaveFailed(Exception ex)
        {
            LogError($"保存配置失败: {ex.Message}");
            SaveFailed?.Invoke(this, ex);
        }

        private void OnLoadFailed(Exception ex)
        {
            LogError($"加载配置失败: {ex.Message}");
            LoadFailed?.Invoke(this, ex);
        }

        #endregion

        #region 日志辅助方法

        private void LogInfo(string message) => TraceLog("INFO", message);

        private void LogError(string message) => TraceLog("ERROR", message);

        private void TraceLog(string category, string message)
        {
            if (!EnableLogging)
                return;
            lock (_logLock)
            {
                System.Diagnostics.Debug.WriteLine($"[ParameterManager][{category}] {DateTime.Now:HH:mm:ss.fff} - {message}");
            }
        }

        #endregion

        #region 安全辅助方法

        /// <summary>
        /// 验证文件路径安全性，防止路径遍历攻击
        /// </summary>
        /// <param name="filePath">要验证的文件路径</param>
        /// <exception cref="SecurityException">路径包含非法字符或路径遍历攻击时抛出</exception>
        private void ValidateFilePath(string filePath)
        {
            // 检查路径遍历攻击
            if (filePath.Contains(".."))
            {
                throw new System.Security.SecurityException("路径包含非法的路径遍历字符");
            }

            // 检查非法字符
            char[] invalidChars = Path.GetInvalidPathChars();
            foreach (char c in invalidChars)
            {
                if (filePath.Contains(c))
                {
                    throw new System.Security.SecurityException("路径包含非法字符");
                }
            }

            // 检查空路径段
            string[] parts = filePath.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                if (string.IsNullOrWhiteSpace(part))
                {
                    throw new System.Security.SecurityException("路径包含空路径段");
                }
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _fileWatcher?.Dispose();
                _saveDebounceTimer?.Dispose();
                _hotReloadDebounceTimer?.Dispose();

                // 如果有待保存的数据，立即保存（忽略暂停标记）
                if (_savePending == 1)
                {
                    _savePending = 0;
                    SaveToFileInternal();
                }

                _lock?.Dispose();
            }
        }

        #endregion
    }

    #region 数据模型

    /// <summary>
    /// 参数配置（根对象）
    /// </summary>
    public class ParameterConfig
    {
        public string Version { get; set; } = "1.0";
        public DateTime LastModified { get; set; }
        public List<ParameterGroup> Groups { get; set; } = new List<ParameterGroup>();
    }

    /// <summary>
    /// 参数分组
    /// </summary>
    public class ParameterGroup
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<string, ParameterItem> Parameters { get; set; } = new Dictionary<string, ParameterItem>();

        public ParameterGroup() { }

        public ParameterGroup(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// 参数项
    /// </summary>
    public class ParameterItem
    {
        public string Key { get; set; }

        public object Value { get; set; }

        public string Type { get; set; }

        public string Group { get; set; }

        public string Description { get; set; }

        public DateTime LastModified { get; set; }
    }

    /// <summary>
    /// 参数变更事件参数
    /// </summary>
    public class ParameterChangedEventArgs : EventArgs
    {
        public string GroupName { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
        public ParameterChangeType ChangeType { get; set; }
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// 参数变更类型
    /// </summary>
    public enum ParameterChangeType
    {
        Added,
        Modified,
        Deleted,
        GroupDeleted,
        Reloaded
    }

    #endregion
}
