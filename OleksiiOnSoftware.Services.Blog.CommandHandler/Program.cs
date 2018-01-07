namespace OleksiiOnSoftware.Services.Blog.CommandHandler
{
    using Common;
    using Common.Redis;
    using Domain.Model;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.IO;
    using StackExchange.Redis;

    public class Program
    {
        public static int Main(string[] args)
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddLogging();

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            services.AddSingleton<ILoggerFactory>(loggerFactory);
            
            // Add Redis
            var redisConfig = new ConfigurationOptions { ClientName = "CommandHandler", AbortOnConnectFail = false };
            redisConfig.EndPoints.Add("redis", 6379);

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConfig));
            services.AddSingleton<IDatabase>(c => c.GetService<IConnectionMultiplexer>().GetDatabase());

            // Add Custom Services
            services.AddSingleton<IRepository, RedisRepository>();
            services.AddSingleton<IEventBus, RedisEventBus>();
            services.AddSingleton<ICommandProcessor, RedisCommandProcessor>();

            var serviceProvider = services.BuildServiceProvider();

            // Start
            var processor = serviceProvider.GetService<ICommandProcessor>();
            processor.RegisterCommandHandler(typeof(Blog));
            processor.Start();

            Console.ReadLine();
            return 0;
        }
    }
}
