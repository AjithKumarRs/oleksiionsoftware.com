namespace OleksiiOnSoftware.Services.Common
{
    using System;
    using System.Collections.Generic;

    public interface IRepository
    {
        Aggregate Get(Type aggregateType, string aggregateId);
        void Set(string aggregateId, Aggregate aggregate, IEnumerable<Event> events);
    }
}
