using Microsoft.Extensions.Caching.Memory;

namespace WebApiTemplate.Providers
{
    public static class AppSettingsExtensions
    {
        private const string appsettingjson = "appsettings.json";

        static MemoryCache _memCache = new MemoryCache(new MemoryCacheOptions { });

        public static IConfiguration? Configuration { get; private set; }

        public static void SetConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static string GetConnectionString(string name)
        {
            TryReadConfigFile();

            if (Configuration == null) return String.Empty;

            var connectionString = Configuration[$"ConnectionStrings:{name}"];
            return connectionString;
        }

        public static void TryReadConfigFile()
        {
            if (Configuration == null)
            {
                var consoleFileApp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appsettingjson);

                Console.WriteLine(consoleFileApp);

                var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

                if (File.Exists(consoleFileApp))
                {
                    builder.AddJsonFile(consoleFileApp, false, true);
                }
                else
                {
                    throw new FileNotFoundException("Not found: appsettings.json");
                }

                Configuration = builder.Build();
            }
        }
        /// <summary>
        /// cache by key 120 seconds
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValueByKey(string key, string? defaultVal = null)
        {
            if (_memCache.TryGetValue(key, out string val) && val != null)
            {
                return val;
            }

            TryReadConfigFile();

            if (Configuration == null) return String.Empty;

            if (defaultVal == null)
            {
                val = Configuration[key];
            }
            else
            {
                try
                {
                    val = Configuration[key];

                    if (val == null) val = defaultVal;

                }
                catch
                {
                    val = defaultVal;
                }
            }

            _memCache.Set(key, val, DateTimeOffset.Now.AddSeconds(120));

            return val;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiredAt">null is cache by 120 seconds</param>
        /// <returns></returns>
        public static string GetValueByKey(string key, DateTimeOffset? expiredAt)
        {
            if (_memCache.TryGetValue(key, out string val) && val != null)
            {
                return val;
            }

            TryReadConfigFile();

            if (Configuration == null) return String.Empty;

            val = Configuration[key];

            if (expiredAt == null)
            {
                _memCache.Set(key, val, DateTimeOffset.Now.AddSeconds(120));
            }
            else
            {
                _memCache.Set(key, val, expiredAt.Value);
            }

            return val;
        }

    }

}
