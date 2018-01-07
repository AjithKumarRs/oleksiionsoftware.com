namespace OleksiiOnSoftware.Services.Common.Redis
{
    using Microsoft.Extensions.DependencyInjection;
    using StackExchange.Redis;

    public class RedisConfigurator : IStorageConfigurator
    {
        public void Configure(IServiceCollection services, dynamic configuration)
        {
            //var dnsTask = Dns.GetHostAddressesAsync(configuration);
            //var connect = string.Join(",", dnsTask.Result.Select(x => x.MapToIPv4().ToString()));

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect((string)configuration));
            services.AddSingleton<IDatabase>(c => c.GetService<IConnectionMultiplexer>().GetDatabase());
        }
    }
}
