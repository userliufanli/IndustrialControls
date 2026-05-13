using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IndustrialControls.Automation;

namespace IndustrialControls.Template.Core
{
    /// <summary>工位连接与 <see cref="LineStationConfiguration"/> 加载。</summary>
    public sealed class CommunicationService : IDisposable
    {
        private StationLineHost _host;
        private bool _disposed;

        public string LastSummary { get; private set; }

        public bool HasHost => _host != null;

        public void ReloadFromRelativePath(string relativeToStartup)
        {
            DisposeHost();

            string fullPath = Path.GetFullPath(Path.Combine(Application.StartupPath, relativeToStartup));
            if (!File.Exists(fullPath))
            {
                LastSummary = "未找到: " + fullPath;
                return;
            }

            string json = File.ReadAllText(fullPath);
            var cfg = LineStationConfiguration.FromJson(json);
            _host = StationLineHost.FromConfiguration(cfg, SynchronizationContext.Current);
            LastSummary = cfg.Summarize();
        }

        public async Task ConnectAllAsync()
        {
            if (_host == null)
                throw new InvalidOperationException("未加载 line 配置。");
            await _host.ConnectAllAsync().ConfigureAwait(true);
        }

        public async Task DisconnectAllAsync()
        {
            if (_host == null)
                return;
            await _host.DisconnectAllAsync().ConfigureAwait(true);
        }

        private void DisposeHost()
        {
            if (_host == null)
                return;
            try
            {
                _host.Dispose();
            }
            catch
            {
                // ignore
            }

            _host = null;
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            DisposeHost();
        }
    }
}
