using System;
using IndustrialControls.Template.Models;

namespace IndustrialControls.Template.Core
{
    /// <summary>当前登录用户上下文。</summary>
    public static class CurrentUser
    {
        public static string Name { get; set; }

        public static UserRole Role { get; set; }

        public static DateTime LoginTime { get; set; }

        public static bool IsLoggedIn => !string.IsNullOrEmpty(Name);

        public static void Set(string name, UserRole role)
        {
            Name = name;
            Role = role;
            LoginTime = DateTime.Now;
        }

        public static void Clear()
        {
            Name = null;
            Role = UserRole.Operator;
            LoginTime = default;
        }
    }
}
