using IndustrialControls.Template.Models;

namespace IndustrialControls.Template.Services
{
    /// <summary>过程/生产数据读取抽象。</summary>
    public interface IDataService
    {
        ProductionData GetSnapshot();
    }
}
