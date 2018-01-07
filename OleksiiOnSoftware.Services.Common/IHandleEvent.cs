namespace OleksiiOnSoftware.Services.Common
{
    public interface IHandleEvent<in T> where T : Event
    {
        void Handle(T evnt);
    }
}
