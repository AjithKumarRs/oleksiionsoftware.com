namespace OleksiiOnSoftware.Services.Common.Redis
{
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System.Threading.Tasks;

    public class RedisEventBus : IEventBus
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        protected string Channel = "Blog.Events";

        public RedisEventBus(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public void Publish<T>(T evnt) where T : Event
        {
            var sub = _connectionMultiplexer.GetSubscriber();
            sub.Publish(Channel, JsonConvert.SerializeObject(evnt, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }));
        }

        public Task PublishAsync<T>(T evnt) where T : Event
        {
            var sub = _connectionMultiplexer.GetSubscriber();
            return sub.PublishAsync(Channel, JsonConvert.SerializeObject(evnt, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }));
        }
    }
}
