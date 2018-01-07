namespace OleksiiOnSoftware.Services.Common
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IStorageConfigurator
    {
        void Configure(IServiceCollection services, dynamic configuration);
    }
}
