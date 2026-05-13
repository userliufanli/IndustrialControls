using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using IndustrialControls.Template.Models;

namespace IndustrialControls.Template.Core
{
    /// <summary>读取 <c>Config/devices</c> 下的设备描述 JSON。</summary>
    public sealed class DeviceManager
    {
        private static readonly JavaScriptSerializer Json = new JavaScriptSerializer();

        public string DevicesDirectory =>
            Path.Combine(Application.StartupPath, "Config", "devices");

        public IReadOnlyList<DeviceConfig> LoadAll()
        {
            var list = new List<DeviceConfig>();
            if (!Directory.Exists(DevicesDirectory))
                return list;

            foreach (var file in Directory.GetFiles(DevicesDirectory, "*.json"))
            {
                try
                {
                    string text = File.ReadAllText(file).Trim();
                    if (text.StartsWith("[", StringComparison.Ordinal))
                    {
                        var arr = Json.Deserialize<List<DeviceConfig>>(text);
                        if (arr != null)
                            list.AddRange(arr.Where(x => x != null));
                    }
                    else
                    {
                        var one = Json.Deserialize<DeviceConfig>(text);
                        if (one != null)
                            list.Add(one);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn("读取设备配置失败 " + file + ": " + ex.Message);
                }
            }

            return list;
        }
    }
}
