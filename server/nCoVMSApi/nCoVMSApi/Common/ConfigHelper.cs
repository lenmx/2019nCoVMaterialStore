using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Common
{
    public class ConfigHelper
    {
        private static ConfigHelper _instance;
        private static readonly object Lockobj = new object();
        private readonly IConfigurationRoot _config;
        private ConfigHelper()
        {
            _config = new ConfigurationBuilder()
            .AddInMemoryCollection()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        }

        private static ConfigHelper Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (Lockobj)
                {
                    _instance = new ConfigHelper();
                }

                return _instance;
            }
        }

        public static string Get(string keys, string defValue = "") => Get<string>(keys, defValue);
        public static T Get<T>(string keys, T defValue = default(T))
        {
            if (!keys.Contains(":") && !keys.Contains("."))
                throw new ArgumentException($"参数错误：{nameof(keys)}");

            var _keys = keys.Split(new char[] { ':', '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (_keys == null || _keys.Length < 2)
                throw new ArgumentException($"参数错误：{nameof(keys)}");

            return Get<T>(defValue, _keys);
        }

        public static string Get(string defValue = "", params string[] keys) => Get<string>(defValue, keys);
        public static T Get<T>(T defValue = default(T), params string[] keys)
        {
            try
            {
                var str = Instance._config[$"{string.Join(":", keys)}"];
                if (string.IsNullOrEmpty(str)) return default(T);

                return (T)Convert.ChangeType(str, typeof(T));
            }
            catch
            {
                return defValue;
            }
        }
    }
}
