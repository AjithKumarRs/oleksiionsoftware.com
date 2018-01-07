namespace OleksiiOnSoftware.Services.Common.Redis
{
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Aggregate = Aggregate;

    public class RedisRepository : IRepository
    {
        private readonly IDatabase _db;

        public RedisRepository(IDatabase db)
        {
            _db = db;
        }

        public Aggregate Get(Type aggregateType, string aggregateId)
        {
            //TODO: Check for snapshoot
            var values = _db.ListRange(GetKey(aggregateId));
            var events = values
                .Select(_ => JsonConvert.DeserializeObject<Event>(_, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }))
                .ToList();

            var aggregate = (Aggregate)Activator.CreateInstance(aggregateType);
            aggregate.AggregateId = aggregateId;
            foreach (var evnt in events)
            {
                aggregate.ApplyEvent(evnt);
            }

            return aggregate;
        }

        public void Set(string aggregateId, Aggregate aggregate, IEnumerable<Event> events)
        {
            //TODO: Check for snapshoot and version
            foreach (var evnt in events)
            {
                _db.ListRightPush(GetKey(aggregateId), JsonConvert.SerializeObject(evnt, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }));
            }
        }

        private static string GetKey(string aggregateId) => $"{aggregateId}:Events";
    }
}
