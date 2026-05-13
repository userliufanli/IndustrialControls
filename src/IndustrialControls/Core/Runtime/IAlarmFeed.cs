using System;
using System.Collections.Generic;

namespace IndustrialControls.Core.Runtime
{
    /// <summary>
    /// 报警列表与确认能力抽象，供报警历史表、顶部报警条订阅。
    /// </summary>
    public interface IAlarmFeed
    {
        IReadOnlyList<ProcessAlarmSnapshot> GetActiveAlarms();

        event EventHandler AlarmListChanged;

        bool TryAcknowledge(string alarmId, string user, out string errorMessage);
    }
}
