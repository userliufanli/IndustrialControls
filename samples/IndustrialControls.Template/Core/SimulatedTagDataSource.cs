using System;
using System.Collections.Generic;
using System.Threading;
using IndustrialControls.Controls.StatusIndicator;
using IndustrialControls.Core.Runtime;

namespace IndustrialControls.Template.Core
{
    /// <summary>离线演示用内存标签源。</summary>
    public sealed class SimulatedTagDataSource : ITagDataSource, IDisposable
    {
        public const string MotorRunningTagId = "Motor.Running";

        private readonly object _sync = new object();
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        private HashSet<string> _active = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly SynchronizationContext _ui;
        private bool _disposed;

        public SimulatedTagDataSource(SynchronizationContext uiContext)
        {
            _ui = uiContext;
            _values[MotorRunningTagId] = false;
        }

        public event EventHandler<TagChangedEventArgs> TagChanged;

        public bool TryGetSnapshot(string tagId, out TagSnapshot snapshot)
        {
            lock (_sync)
            {
                if (_values.TryGetValue(tagId, out var v))
                {
                    snapshot = new TagSnapshot(v, TagQuality.Good, DateTime.UtcNow);
                    return true;
                }
            }

            snapshot = default;
            return false;
        }

        public void SetActiveTags(IEnumerable<string> tagIds)
        {
            lock (_sync)
            {
                _active = tagIds == null
                    ? new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                    : new HashSet<string>(tagIds, StringComparer.OrdinalIgnoreCase);
            }
        }

        public void SetMotorRunning(bool running)
        {
            TagSnapshot snap;
            lock (_sync)
            {
                _values[MotorRunningTagId] = running;
                snap = new TagSnapshot(running, TagQuality.Good, DateTime.UtcNow);
            }

            Raise(MotorRunningTagId, snap);
        }

        public static IndicatorState ToIndicatorState(bool running)
        {
            return running ? IndicatorState.Running : IndicatorState.Stopped;
        }

        private void Raise(string tagId, TagSnapshot snapshot)
        {
            var handler = TagChanged;
            if (handler == null)
                return;

            void Invoke()
            {
                handler(this, new TagChangedEventArgs(tagId, snapshot));
            }

            if (_ui != null)
                _ui.Post(_ => Invoke(), null);
            else
                Invoke();
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
        }
    }
}
