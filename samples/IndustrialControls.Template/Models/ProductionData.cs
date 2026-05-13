using System;

namespace IndustrialControls.Template.Models
{
    /// <summary>生产/过程数据快照（模板占位）。</summary>
    public sealed class ProductionData
    {
        public DateTime Timestamp { get; set; }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public int GoodCount { get; set; }

        public int NgCount { get; set; }
    }
}
