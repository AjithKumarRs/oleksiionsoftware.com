namespace OleksiiOnSoftware.Services.Common
{
    using System.Threading.Tasks;

    public interface ICommandBus
    {
        Task SendAsync<T>(T cmd) where T : Command;
    }
}