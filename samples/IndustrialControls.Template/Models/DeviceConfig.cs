namespace IndustrialControls.Template.Models
{
    /// <summary>单台设备或工位在模板中的元数据（与 line.json 可配合使用）。</summary>
    public sealed class DeviceConfig
    {
        public string DeviceId { get; set; }

        public string DisplayName { get; set; }

        /// <summary>可选：指向 <c>Config/Models/...</c> 下的 line 配置相对路径。</summary>
        public string LineConfigRelative { get; set; }

        public string Notes { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(DisplayName))
                return DisplayName;
            return DeviceId ?? "(设备)";
        }
    }
}
