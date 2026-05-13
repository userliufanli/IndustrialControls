using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using IndustrialControls.Core;

namespace IndustrialControls.Controls.Login
{
    /// <summary>
    /// 用于 JSON 序列化的单条用户记录（公开属性供 JavaScriptSerializer 使用）。
    /// </summary>
    public sealed class SerializedLoginUser
    {
        public string UserName { get; set; }
        public string SaltBase64 { get; set; }
        public string HashBase64 { get; set; }
    }

    /// <summary>
    /// 基于 <see cref="ParameterSection"/> 的本地登录用户存储（盐 + SHA256）。
    /// </summary>
    public sealed class LoginUserStore
    {
        public const string DefaultCredentialKey = "CredentialList";

        private static readonly JavaScriptSerializer Json = new JavaScriptSerializer();

        private readonly ParameterSection _section;
        private readonly string _credentialKey;
        private readonly object _sync = new object();

        /// <summary>
        /// 使用指定参数分组存储凭据。
        /// </summary>
        /// <param name="section">参数分组视图</param>
        /// <param name="credentialKey">存储 JSON 列表的参数键</param>
        public LoginUserStore(ParameterSection section, string credentialKey = DefaultCredentialKey)
        {
            _section = section ?? throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(credentialKey))
                throw new ArgumentException("凭据键不能为空。", nameof(credentialKey));
            _credentialKey = credentialKey;
        }

        /// <summary>
        /// 当前分组名（便于界面提示）。
        /// </summary>
        public string GroupName => _section.GroupName;

        public IReadOnlyList<string> ListUserNames()
        {
            lock (_sync)
            {
                return LoadUsers().Select(u => u.UserName).Where(n => !string.IsNullOrEmpty(n)).OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToList();
            }
        }

        public bool TryAddUser(string userName, string password, out string error)
        {
            error = null;
            if (!ValidateUserName(userName, out error))
                return false;
            if (string.IsNullOrEmpty(password))
            {
                error = "密码不能为空。";
                return false;
            }

            lock (_sync)
            {
                var list = LoadUsers();
                if (list.Any(u => string.Equals(u.UserName, userName, StringComparison.OrdinalIgnoreCase)))
                {
                    error = "该用户名已存在。";
                    return false;
                }

                byte[] salt = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                    rng.GetBytes(salt);

                list.Add(new SerializedLoginUser
                {
                    UserName = userName.Trim(),
                    SaltBase64 = Convert.ToBase64String(salt),
                    HashBase64 = Convert.ToBase64String(ComputeHash(salt, password))
                });
                SaveUsers(list);
            }

            return true;
        }

        public bool TryRemoveUser(string userName, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(userName))
            {
                error = "请指定用户名。";
                return false;
            }

            lock (_sync)
            {
                var list = LoadUsers();
                int removed = list.RemoveAll(u => string.Equals(u.UserName, userName, StringComparison.OrdinalIgnoreCase));
                if (removed == 0)
                {
                    error = "未找到该用户。";
                    return false;
                }

                SaveUsers(list);
            }

            return true;
        }

        public bool TryChangePassword(string userName, string oldPassword, string newPassword, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(userName))
            {
                error = "请指定用户名。";
                return false;
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                error = "新密码不能为空。";
                return false;
            }

            lock (_sync)
            {
                var list = LoadUsers();
                var idx = list.FindIndex(u => string.Equals(u.UserName, userName, StringComparison.OrdinalIgnoreCase));
                if (idx < 0)
                {
                    error = "未找到该用户。";
                    return false;
                }

                var user = list[idx];
                byte[] salt = TryParseBase64(user.SaltBase64);
                byte[] hash = TryParseBase64(user.HashBase64);
                if (salt == null || hash == null)
                {
                    error = "用户数据损坏。";
                    return false;
                }

                if (!SlowEquals(hash, ComputeHash(salt, oldPassword)))
                {
                    error = "原密码不正确。";
                    return false;
                }

                byte[] newSalt = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                    rng.GetBytes(newSalt);

                user.SaltBase64 = Convert.ToBase64String(newSalt);
                user.HashBase64 = Convert.ToBase64String(ComputeHash(newSalt, newPassword));
                list[idx] = user;
                SaveUsers(list);
            }

            return true;
        }

        public bool TryVerify(string userName, string password, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(userName))
            {
                error = "请输入用户名。";
                return false;
            }

            if (password == null)
            {
                error = "请输入密码。";
                return false;
            }

            lock (_sync)
            {
                var list = LoadUsers();
                var user = list.FirstOrDefault(u => string.Equals(u.UserName, userName, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    error = "用户名或密码错误。";
                    return false;
                }

                byte[] salt = TryParseBase64(user.SaltBase64);
                byte[] hash = TryParseBase64(user.HashBase64);
                if (salt == null || hash == null)
                {
                    error = "用户数据损坏。";
                    return false;
                }

                if (!SlowEquals(hash, ComputeHash(salt, password)))
                {
                    error = "用户名或密码错误。";
                    return false;
                }
            }

            return true;
        }

        private static bool ValidateUserName(string userName, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(userName))
            {
                error = "用户名不能为空。";
                return false;
            }

            string t = userName.Trim();
            if (t.Length > 64)
            {
                error = "用户名过长（最多 64 字符）。";
                return false;
            }

            return true;
        }

        private List<SerializedLoginUser> LoadUsers()
        {
            string json = _section.Get<string>(_credentialKey, null);
            if (string.IsNullOrWhiteSpace(json))
                return new List<SerializedLoginUser>();

            try
            {
                var arr = Json.Deserialize<SerializedLoginUser[]>(json);
                if (arr == null)
                    return new List<SerializedLoginUser>();
                return arr.Where(u => u != null && !string.IsNullOrWhiteSpace(u.UserName)).ToList();
            }
            catch
            {
                return new List<SerializedLoginUser>();
            }
        }

        private void SaveUsers(List<SerializedLoginUser> users)
        {
            string json = Json.Serialize(users ?? new List<SerializedLoginUser>());
            _section.Set(_credentialKey, json);
        }

        private static byte[] ComputeHash(byte[] salt, string password)
        {
            byte[] pw = Encoding.UTF8.GetBytes(password ?? string.Empty);
            var buf = new byte[salt.Length + pw.Length];
            Buffer.BlockCopy(salt, 0, buf, 0, salt.Length);
            Buffer.BlockCopy(pw, 0, buf, salt.Length, pw.Length);
            using (var sha = SHA256.Create())
                return sha.ComputeHash(buf);
        }

        private static byte[] TryParseBase64(string b64)
        {
            if (string.IsNullOrWhiteSpace(b64))
                return null;
            try
            {
                return Convert.FromBase64String(b64);
            }
            catch
            {
                return null;
            }
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}
