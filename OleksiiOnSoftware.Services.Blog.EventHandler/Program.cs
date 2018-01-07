namespace OleksiiOnSoftware.Services.Blog.EventHandler
{
    using Common;
    using Common.Redis;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using StackExchange.Redis;

    class Program
    {
        static int Main(string[] args)
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

            // Add Event Handlers to ServiceCollection
            var assemblies = GetAssemblies();
            var handlers = assemblies
                .SelectMany(_ => _.GetTypes())
                .Where(_ => _.Namespace != null)
                .Where(_ => !_.Name.Contains("<") && !_.Name.Contains(">"))
                .Where(_ => _.Name.EndsWith("Handler"))
                .Where(_ => !_.GetTypeInfo().IsInterface)
                .ToList();

            foreach (var handler in handlers)
            {
                services.AddTransient(handler);

                foreach (var interf in handler.GetInterfaces())
                {
                    services.AddTransient(interf, handler);
                }
            }

            // Add Redis
            var redisConfig = new ConfigurationOptions { ClientName = "CommandHandler", AbortOnConnectFail = false };
            redisConfig.EndPoints.Add("redis", 6379);

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConfig));
            services.AddSingleton<IDatabase>(c => c.GetService<IConnectionMultiplexer>().GetDatabase());

            services.AddSingleton<IRepository, RedisRepository>();
            services.AddSingleton<IEventProcessor, RedisEventProcessor>();

            var serviceProvider = services.BuildServiceProvider();

            var processor = serviceProvider.GetService<IEventProcessor>();
            processor.RegisterEventHandler(Assembly.Load(new AssemblyName("OleksiiOnSoftware.Services.Blog.Query")));
            processor.Start();

            Console.ReadLine();
            return 0;
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            var dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var dlls = Directory.GetFiles(dir, "OleksiiOnSoftware.*.dll");

            foreach (var dll in dlls)
            {
                yield return Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(dll)));
            }
        }
    }
}
