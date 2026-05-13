using System;
using System.Threading;
using System.Threading.Tasks;
using IndustrialControls.Controls.Communication;

namespace IndustrialControls.Automation
{
    public sealed class ModbusTcpStationSession : IStationCommSession
    {
        private readonly StationCommProfile _profile;
        private readonly ModbusTcpMaster _master;
        private volatile bool _disposed;

        public ModbusTcpStationSession(StationCommProfile profile)
        {
            _profile = profile ?? throw new ArgumentNullException(nameof(profile));
            if (profile.Protocol != IndustrialProtocolKind.ModbusTcp)
                throw new ArgumentException("仅支持 ModbusTcp。", nameof(profile));
            _master = new ModbusTcpMaster(
                profile.TcpRemoteHost,
                profile.TcpRemotePort,
                profile.TcpConnectTimeoutMs,
                profile.IoTimeoutMs);
        }

        public StationCommProfile Profile => _profile;

        public ModbusTcpMaster Modbus => _master;

        public bool IsConnected => !_disposed && _master.IsConnected;

        public CommunicationManager TryGetRawManager() => null;

        public ModbusTcpMaster TryGetModbusTcpMaster() => _master;

        public Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            return _master.ConnectAsync(cancellationToken);
        }

        public Task DisconnectAsync()
        {
            ThrowIfDisposed();
            _master.Disconnect();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _master.Dispose();
        }

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ModbusTcpStationSession));
        }
    }
}
