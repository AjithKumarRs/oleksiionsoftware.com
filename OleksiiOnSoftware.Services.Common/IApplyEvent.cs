namespace OleksiiOnSoftware.Services.Common
{
    public interface IApplyEvent<in T> where T : Event
    {
        void Apply(T evnt);
    }
}
