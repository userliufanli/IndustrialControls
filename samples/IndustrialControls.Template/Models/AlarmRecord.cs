using System;
using IndustrialControls.Controls.Alarm;

namespace IndustrialControls.Template.Models
{
    /// <summary>报警管理器中的逻辑记录（可与 <see cref="AlarmDisplay"/> 同步）。</summary>
    public sealed class AlarmRecord
    {
        public string Id { get; set; }

        public AlarmLevel Level { get; set; }

        public string Message { get; set; }

        public string Source { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
