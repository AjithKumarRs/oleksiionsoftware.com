namespace OleksiiOnSoftware.Services.Common
{
    using System;
    using System.Reflection;

    public interface IEventProcessor
    {
        void Start();

        void RegisterEventHandler<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IHandleEvent<TEvent>;

        void RegisterEventHandler(Type evnt, Type evntHandler);

        void RegisterEventHandler(Type evntHandler);

        void RegisterEventHandler(Assembly assembly);
    }
}
