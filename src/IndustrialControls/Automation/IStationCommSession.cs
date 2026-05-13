using System;
using System.Threading;
using System.Threading.Tasks;
using IndustrialControls.Controls.Communication;

namespace IndustrialControls.Automation
{
    public interface IStationCommSession : IDisposable
    {
        StationCommProfile Profile { get; }
        Task ConnectAsync(CancellationToken cancellationToken = default);
        Task DisconnectAsync();
        bool IsConnected { get; }
        CommunicationManager TryGetRawManager();
        ModbusTcpMaster TryGetModbusTcpMaster();
    }
}
