namespace OleksiiOnSoftware.Services.Common
{
    using System.Collections.Generic;

    public interface IHandleCommand<in T> where T : Command
    {
        IEnumerable<Event> Handle(T message);
    }
}
