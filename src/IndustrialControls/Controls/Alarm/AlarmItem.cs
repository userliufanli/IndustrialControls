using System;

namespace IndustrialControls.Controls.Alarm
{
    /// <summary>
    /// 报警条目
    /// </summary>
    public class AlarmItem
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 报警等级
        /// </summary>
        public AlarmLevel Level { get; set; }

        /// <summary>
        /// 报警消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 报警来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 触发时间
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 是否已确认
        /// </summary>
        public bool IsAcknowledged { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? AcknowledgedTime { get; set; }

        /// <summary>
        /// 确认人
        /// </summary>
        public string AcknowledgedBy { get; set; }

        /// <summary>
        /// 是否为新报警（用于闪烁提示）
        /// </summary>
        public bool IsNew { get; set; }

        public AlarmItem()
        {
            Id = Guid.NewGuid().ToString("N").Substring(0, 8);
            Timestamp = DateTime.Now;
            IsNew = true;
        }

        public AlarmItem(AlarmLevel level, string message, string source = "")
            : this()
        {
            Level = level;
            Message = message;
            Source = source;
        }

        /// <summary>
        /// 确认报警
        /// </summary>
        public void Acknowledge(string user = "")
        {
            IsAcknowledged = true;
            AcknowledgedTime = DateTime.Now;
            AcknowledgedBy = user;
            IsNew = false;
        }

        public override string ToString()
        {
            return $"[{Level}] {Timestamp:HH:mm:ss} - {Message}";
        }
    }
}
