using System;

namespace IndustrialControls.Controls.StatusIndicator
{
    /// <summary>
    /// 状态历史记录
    /// </summary>
    public class StatusHistoryRecord
    {
        /// <summary>
        /// 状态
        /// </summary>
        public IndicatorState State { get; set; }

        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 持续时长（到下一次变更）
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public StatusHistoryRecord(IndicatorState state, DateTime timestamp, string remark = "")
        {
            State = state;
            Timestamp = timestamp;
            Remark = remark ?? "";
            Duration = TimeSpan.Zero;
        }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss}] {State} - {Remark}";
        }
    }
}
