using System;
using System.Collections.Generic;
using IndustrialControls.Controls.Alarm;
using IndustrialControls.Template.Models;

namespace IndustrialControls.Template.Core
{
    /// <summary>逻辑层报警广播（页面可订阅并写入 <see cref="AlarmDisplay"/>）。</summary>
    public sealed class AlarmManager
    {
        private readonly List<AlarmRecord> _buffer = new List<AlarmRecord>();
        private readonly object _sync = new object();

        public event EventHandler<AlarmRecord> AlarmRaised;

        public void Raise(AlarmLevel level, string message, string source = "")
        {
            var rec = new AlarmRecord
            {
                Id = Guid.NewGuid().ToString("N"),
                Level = level,
                Message = message,
                Source = source,
                Timestamp = DateTime.Now
            };

            lock (_sync)
            {
                _buffer.Add(rec);
                if (_buffer.Count > 500)
                    _buffer.RemoveAt(0);
            }

            AlarmRaised?.Invoke(this, rec);
        }

        public int ActiveCount
        {
            get
            {
                lock (_sync)
                    return _buffer.Count;
            }
        }
    }
}
