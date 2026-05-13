using System;

namespace IndustrialControls.Core.Runtime
{
    /// <summary>
    /// 与画面无关的报警快照，用于历史表、条带等绑定；可与 <c>AlarmItem</c> 互转（见文档）。
    /// </summary>
    public sealed class ProcessAlarmSnapshot
    {
        public string Id { get; set; }

        public RuntimeAlarmLevel Level { get; set; }

        public string Message { get; set; }

        public string Source { get; set; }

        public DateTime Timestamp { get; set; }

        public bool IsAcknowledged { get; set; }

        public DateTime? AcknowledgedTime { get; set; }

        public string AcknowledgedBy { get; set; }

        public bool IsNew { get; set; }
    }
}
