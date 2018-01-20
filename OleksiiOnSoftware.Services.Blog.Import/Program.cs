namespace OleksiiOnSoftware.Services.Blog.Import
{
    using Client;
    using Client.Impl;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Services;
    using Services.Impl;
    using Services.Impl.ContentTransformers;
    using System;
    using System.IO;

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

            services.AddSingleton<IBlogClient>(new BlogApiClient("http://api:5001"));
            services.AddSingleton<IBlogMetadataParser, BlogMetadataParser>();
            services.AddSingleton<IContentSource, GitHubContentSource>();
            services.AddSingleton<IContentDestination, BlogServiceContentDestination>();
            services.AddSingleton<IContentTransformer, TransformRelativeToAbsoluteLinksContentTransformer>();
            services.AddSingleton<IContentTransformer, AdjustNavigationLinksContentTransofrmer>();
            services.AddSingleton<IContentTransformer, RemoveHashFromTagNamesContentTransformer>();
            services.AddSingleton<IContentTransformer, RemoveNumbersFromCategoryNamesContentTransformer>();
            var serviceProvider = services.BuildServiceProvider();

            var contentSource = serviceProvider.GetService<IContentSource>();
            var content = contentSource.GetContent();

            var contentTransfromers = serviceProvider.GetServices<IContentTransformer>();
            foreach (var contentTransfromer in contentTransfromers)
            {
                content = contentTransfromer.Transform(content);
            }

            var contentDestination = serviceProvider.GetService<IContentDestination>();
            contentDestination.SetContent(content);

            Console.ReadLine();
            return 0;
        }
    }
}
