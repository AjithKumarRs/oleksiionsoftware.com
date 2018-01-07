namespace OleksiiOnSoftware.Services.Common
{
    using System.Threading.Tasks;

    public interface IEventBus
    {
        void Publish<T>(T evnt) where T : Event;

        Task PublishAsync<T>(T evnt) where T : Event;
    }
}
