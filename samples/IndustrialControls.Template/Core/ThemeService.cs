using IndustrialControls.Theme;

namespace IndustrialControls.Template.Core
{
    /// <summary>对 <see cref="ThemeManager"/> 的薄封装，便于测试与替换。</summary>
    public sealed class ThemeService
    {
        public void ApplyRegisteredTheme(string themeName)
        {
            ThemeManager.Instance.SetTheme(themeName);
        }

        public void Toggle()
        {
            ThemeManager.Instance.ToggleTheme();
        }

        public string CurrentName => ThemeManager.Instance.CurrentTheme?.Name ?? "FlatLight";
    }
}
