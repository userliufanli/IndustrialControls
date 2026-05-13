using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace IndustrialControls.Automation
{
    public sealed class LineStationConfiguration
    {
        public List<StationCommProfile> Stations { get; set; } = new List<StationCommProfile>();

        public bool IsMultiStation => Stations != null && Stations.Count > 1;

        public static LineStationConfiguration Single(StationCommProfile station)
        {
            if (station == null) throw new ArgumentNullException(nameof(station));
            return new LineStationConfiguration { Stations = new List<StationCommProfile> { station } };
        }

        public void Validate()
        {
            if (Stations == null || Stations.Count == 0)
                throw new InvalidOperationException("至少需要配置一个工位。");

            var dup = Stations.GroupBy(s => s.StationId).FirstOrDefault(g => g.Count() > 1);
            if (dup != null)
                throw new InvalidOperationException($"重复的 StationId：{dup.Key}。");

            foreach (var s in Stations)
                s.Validate();
        }

        public string ToJson()
        {
            return new JavaScriptSerializer().Serialize(this);
        }

        public static LineStationConfiguration FromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("json 为空。", nameof(json));
            var cfg = new JavaScriptSerializer().Deserialize<LineStationConfiguration>(json);
            if (cfg?.Stations == null)
                cfg = new LineStationConfiguration();
            return cfg;
        }

        public string Summarize()
        {
            var sb = new StringBuilder();
            sb.Append(IsMultiStation ? "多工位" : "单工位").Append("：");
            foreach (var s in Stations)
                sb.Append('[').Append(s.StationId).Append(' ').Append(s.Name).Append(' ').Append(s.Protocol).Append(']');
            return sb.ToString();
        }
    }
}
