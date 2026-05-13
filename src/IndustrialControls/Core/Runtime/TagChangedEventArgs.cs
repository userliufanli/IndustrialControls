using System;

namespace IndustrialControls.Core.Runtime
{
    /// <summary>
    /// 单个标签变更通知。
    /// </summary>
    public sealed class TagChangedEventArgs : EventArgs
    {
        public TagChangedEventArgs(string tagId, TagSnapshot snapshot)
        {
            TagId = tagId ?? throw new ArgumentNullException(nameof(tagId));
            Snapshot = snapshot;
        }

        public string TagId { get; }

        public TagSnapshot Snapshot { get; }
    }
}
