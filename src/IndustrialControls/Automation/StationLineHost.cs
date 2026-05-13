using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IndustrialControls.Automation
{
    public sealed class StationLineHost : IDisposable
    {
        private readonly List<IStationCommSession> _sessions;
        private volatile bool _disposed;

        public StationLineHost(IEnumerable<IStationCommSession> sessions)
        {
            if (sessions == null) throw new ArgumentNullException(nameof(sessions));
            _sessions = sessions.ToList();
            if (_sessions.Count == 0)
                throw new ArgumentException("至少需要一个工位会话。", nameof(sessions));
        }

        public IReadOnlyList<IStationCommSession> Sessions => _sessions;

        public static StationLineHost FromConfiguration(LineStationConfiguration configuration, SynchronizationContext uiSync = null)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            configuration.Validate();
            var list = new List<IStationCommSession>();
            foreach (var p in configuration.Stations)
            {
                switch (p.Protocol)
                {
                    case IndustrialProtocolKind.Raw:
                    case IndustrialProtocolKind.ModbusRtu:
                        list.Add(new RawStationSession(p, null, uiSync));
                        break;
                    case IndustrialProtocolKind.ModbusTcp:
                        list.Add(new ModbusTcpStationSession(p));
                        break;
                    default:
                        throw new NotSupportedException(
                            $"工位 {p.StationId}：协议 {p.Protocol} 未内置会话，请实现 IStationCommSession。");
                }
            }
            return new StationLineHost(list);
        }

        public async Task ConnectAllAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            foreach (var s in _sessions)
                cancellationToken.ThrowIfCancellationRequested();
            await Task.WhenAll(_sessions.Select(s => s.ConnectAsync(cancellationToken))).ConfigureAwait(false);
        }

        public Task DisconnectAllAsync()
        {
            ThrowIfDisposed();
            return Task.WhenAll(_sessions.Select(s => s.DisconnectAsync()));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            foreach (var s in _sessions)
            {
                try { s.Dispose(); } catch { }
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(StationLineHost));
        }
    }
}
