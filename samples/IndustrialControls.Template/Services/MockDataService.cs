using System;
using IndustrialControls.Template.Core;
using IndustrialControls.Template.Models;

namespace IndustrialControls.Template.Services
{
    /// <summary>演示用模拟数据。</summary>
    public sealed class MockDataService : IDataService, IDeviceService
    {
        private readonly Random _random = new Random();

        public ProductionData GetSnapshot()
        {
            double t = 40 + _random.NextDouble() * 30 + Math.Sin(DateTime.Now.TimeOfDay.TotalSeconds / 5) * 5;
            double p = 0.5 + _random.NextDouble() * 0.3;
            return new ProductionData
            {
                Timestamp = DateTime.Now,
                Temperature = t,
                Pressure = p,
                GoodCount = 1000 + _random.Next(50),
                NgCount = _random.Next(5)
            };
        }

        public void SendCommand(string deviceKey, string commandDisplayText)
        {
            Logger.Info($"[MockDevice] {deviceKey} -> {commandDisplayText}");
        }
    }
}
