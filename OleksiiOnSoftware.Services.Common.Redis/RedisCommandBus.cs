namespace OleksiiOnSoftware.Services.Common.Redis
{
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System.Threading.Tasks;

    public class RedisCommandBus : ICommandBus
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        protected string Channel = "Blog.Commands";

        public RedisCommandBus(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task SendAsync<T>(T cmd) where T : Command
        {
            var sub = _connectionMultiplexer.GetSubscriber();
            await sub.PublishAsync(Channel, JsonConvert.SerializeObject(cmd, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }));
        }
    }
}
