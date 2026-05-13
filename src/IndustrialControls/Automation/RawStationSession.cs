using System;
using System.Threading;
using System.Threading.Tasks;
using IndustrialControls.Controls.Communication;

namespace IndustrialControls.Automation
{
    public sealed class RawStationSession : IStationCommSession
    {
        private readonly StationCommProfile _profile;
        private readonly CommunicationManager _manager;
        private readonly bool _ownsManager;
        private volatile bool _disposed;

        public RawStationSession(StationCommProfile profile, CommunicationManager existingManager = null, SynchronizationContext uiSync = null)
        {
            _profile = profile ?? throw new ArgumentNullException(nameof(profile));
            if (profile.Protocol != IndustrialProtocolKind.Raw && profile.Protocol != IndustrialProtocolKind.ModbusRtu)
                throw new ArgumentException("仅支持 Raw 或 ModbusRtu。", nameof(profile));

            if (existingManager != null)
            {
                _manager = existingManager;
                _ownsManager = false;
            }
            else
            {
                _manager = new CommunicationManager();
                _ownsManager = true;
            }

            profile.ApplyToCommunicationManager(_manager, uiSync);
        }

        public StationCommProfile Profile => _profile;

        public bool IsConnected => !_disposed && _manager.State == CommunicationState.Connected;

        public CommunicationManager TryGetRawManager() => _manager;

        public ModbusTcpMaster TryGetModbusTcpMaster() => null;

        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();
            await _manager.ConnectAsync().ConfigureAwait(false);
        }

        public Task DisconnectAsync()
        {
            ThrowIfDisposed();
            return _manager.DisconnectAsync();
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            if (_ownsManager)
                _manager.Dispose();
        }

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(RawStationSession));
        }
    }
}
