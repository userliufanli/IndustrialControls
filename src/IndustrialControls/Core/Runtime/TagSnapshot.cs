using System;

namespace IndustrialControls.Core.Runtime
{
    /// <summary>
    /// 单个点位的瞬时快照，供绑定型控件与表格列使用。
    /// </summary>
    public readonly struct TagSnapshot
    {
        public TagSnapshot(object value, TagQuality quality, DateTime timestampUtc)
        {
            Value = value;
            Quality = quality;
            TimestampUtc = timestampUtc;
        }

        /// <summary>工程值（数值、布尔或字符串等，由数据源约定）。</summary>
        public object Value { get; }

        public TagQuality Quality { get; }

        /// <summary>UTC 时间戳。</summary>
        public DateTime TimestampUtc { get; }
    }
}
