namespace OleksiiOnSoftware.Services.Blog.Api
{
    using Common;
    using Common.Redis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Query.Queries;
    using Services;
    using Services.Impl;
    using StackExchange.Redis;

    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add Redis
            var redisConfig = new ConfigurationOptions { ClientName = "CommandHandler", AbortOnConnectFail = false };
            redisConfig.EndPoints.Add("redis", 6379);

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConfig));
            services.AddSingleton<IDatabase>(c => c.GetService<IConnectionMultiplexer>().GetDatabase());

            // Add Framework
            services.AddMvc();

            // Add Services
            services.AddSingleton<IBlogsService, BlogsService>();
            services.AddSingleton<ICommandBus, RedisCommandBus>();

            // Add Queries 
            services.AddSingleton<GetBlogListQuery>();
            services.AddSingleton<GetHomeViewQuery>();
            services.AddSingleton<GetPostViewQuery>();
            services.AddOptions();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IConfiguration>(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseMvc();
        }
    }
}
