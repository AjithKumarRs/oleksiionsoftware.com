namespace OleksiiOnSoftware.Services.Blog.Query.Utils
{
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System.Threading.Tasks;

    public static class DatabaseUtils
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public static T GetObject<T>(this IDatabase @this, RedisKey key, CommandFlags flags = CommandFlags.None) where T : class, new()
        {
            var json = @this.StringGet(key, flags);
            if (json.IsNullOrEmpty)
            {
                return null;
            }

            var state = JsonConvert.DeserializeObject<T>(json, _settings);
            return state;
        }

        public static async Task<T> GetObjectAsync<T>(this IDatabase @this, RedisKey key, CommandFlags flags = CommandFlags.None) where T : class, new()
        {
            var json = await @this.StringGetAsync(key, flags);
            if (json.IsNullOrEmpty)
            {
                return null;
            }

            var state = JsonConvert.DeserializeObject<T>(json, _settings);
            return state;
        }

        public static bool SetObject<T>(this IDatabase @this, RedisKey key, T obj) where T : class, new()
        {
            var json = JsonConvert.SerializeObject(obj, _settings);
            return @this.StringSet(key, json);
        }

        public static async Task<bool> SetObjectAsync<T>(this IDatabase @this, RedisKey key, T obj) where T : class, new()
        {
            var json = JsonConvert.SerializeObject(obj, _settings);
            return await @this.StringSetAsync(key, json);
        }
    }
}
