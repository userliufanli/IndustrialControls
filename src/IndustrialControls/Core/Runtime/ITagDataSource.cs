using System;
using System.Collections.Generic;

namespace IndustrialControls.Core.Runtime
{
    /// <summary>
    /// 实时过程数据源抽象。由通信层/OPC/模拟器等实现；UI 控件只依赖此接口。
    /// </summary>
    public interface ITagDataSource
    {
        /// <summary>
        /// 同步读取当前快照（用于首次绘制或轮询兜底）。
        /// </summary>
        bool TryGetSnapshot(string tagId, out TagSnapshot snapshot);

        /// <summary>
        /// 任一订阅点位变化时触发；须在 UI 线程或自行 <see cref="System.Windows.Forms.Control.BeginInvoke"/>。
        /// </summary>
        event EventHandler<TagChangedEventArgs> TagChanged;

        /// <summary>
        /// 声明当前画面关心的点位集合，便于数据源合并订阅与退订。实现可为空操作。
        /// </summary>
        void SetActiveTags(IEnumerable<string> tagIds);
    }
}
