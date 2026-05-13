using System;
using System.Collections.Generic;

namespace IndustrialControls.Core
{
    /// <summary>
    /// 绑定到固定分组的参数视图，避免每次读写都传入分组名。
    /// 通过 <see cref="ParameterManager.Section"/> 或 <see cref="ParameterAccessor.Section"/> 获取。
    /// </summary>
    public sealed class ParameterSection
    {
        /// <summary>
        /// 所属参数管理器
        /// </summary>
        public ParameterManager Manager { get; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; }

        /// <summary>
        /// 创建分组视图
        /// </summary>
        public ParameterSection(ParameterManager manager, string groupName)
        {
            Manager = manager ?? throw new ArgumentNullException(nameof(manager));
            GroupName = groupName ?? throw new ArgumentNullException(nameof(groupName));
        }

        /// <inheritdoc cref="ParameterManager.GetValue{T}(string, string, T)" />
        public T Get<T>(string key, T defaultValue = default)
        {
            return Manager.GetValue(GroupName, key, defaultValue);
        }

        /// <inheritdoc cref="ParameterManager.SetValue{T}(string, string, T)" />
        public void Set<T>(string key, T value)
        {
            Manager.SetValue(GroupName, key, value);
        }

        /// <inheritdoc cref="ParameterManager.Contains(string, string)" />
        public bool Contains(string key)
        {
            return Manager.Contains(GroupName, key);
        }

        /// <inheritdoc cref="ParameterManager.TryGetValue{T}(string, string, out T)" />
        public bool TryGet<T>(string key, out T value)
        {
            return Manager.TryGetValue(GroupName, key, out value);
        }

        /// <inheritdoc cref="ParameterManager.GetOrSet{T}(string, string, T)" />
        public T GetOrSet<T>(string key, T value)
        {
            return Manager.GetOrSet(GroupName, key, value);
        }

        /// <inheritdoc cref="ParameterManager.Delete(string, string)" />
        public bool Delete(string key)
        {
            return Manager.Delete(GroupName, key);
        }

        /// <summary>
        /// 批量写入当前分组参数（等同于 <see cref="ParameterManager.SetBatch"/>）。
        /// </summary>
        public void SetBatch(Dictionary<string, object> parameters)
        {
            Manager.SetBatch(GroupName, parameters);
        }

        /// <summary>
        /// 读取当前分组全部参数（等同于 <see cref="ParameterManager.GetBatch"/>）。
        /// </summary>
        public Dictionary<string, object> GetBatch()
        {
            return Manager.GetBatch(GroupName);
        }
    }
}
