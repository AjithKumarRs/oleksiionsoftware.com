namespace OleksiiOnSoftware.Services.Common.Redis
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class RedisEventProcessor : IEventProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly ILogger<RedisEventProcessor> _logger;
        private readonly Dictionary<Type, List<Type>> _eventHandlers;

        protected string Channel = "Blog.Events";

        public RedisEventProcessor(IServiceProvider serviceProvider, IConnectionMultiplexer connectionMultiplexer, ILogger<RedisEventProcessor> logger)
        {
            _serviceProvider = serviceProvider;
            _connectionMultiplexer = connectionMultiplexer;
            _logger = logger;
            _eventHandlers = new Dictionary<Type, List<Type>>();
        }

        public void Start()
        {
            _logger.LogInformation($"Event processor started. Listening to {Channel} channel.");

            var sub = _connectionMultiplexer.GetSubscriber();
            sub.Subscribe(Channel, (ch, val) =>
            {
                try
                {
                    var evnt = JsonConvert.DeserializeObject<Event>(val, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                    _logger.LogInformation($"Processing event: {evnt.GetType().Name}");

                    Process(evnt);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                _logger.LogInformation($"Waiting for the next event.");
            });
        }

        private void Process(Event evnt)
        {
            var evntType = evnt.GetType();
            if (!_eventHandlers.ContainsKey(evntType))
            {
                return;
            }

            foreach (var handlerType in _eventHandlers[evntType])
            {
                var handler = _serviceProvider.GetService(handlerType);
                ((dynamic)handler).Handle((dynamic)evnt);
            }
        }

        public void RegisterEventHandler<TEvent, TEventHandler>()
            where TEvent : Event
            where TEventHandler : IHandleEvent<TEvent>
        {
            RegisterEventHandler(typeof(TEvent), typeof(TEventHandler));
        }

        public void RegisterEventHandler(Type evnt, Type evntHandler)
        {
            if (!_eventHandlers.ContainsKey(evnt))
            {
                _eventHandlers.Add(evnt, new List<Type>());
            }

            var eventHandlers = _eventHandlers[evnt];
            if (eventHandlers.Contains(evntHandler))
            {
                throw new Exception("Event handler already added to the list of event handlers.");
            }

            eventHandlers.Add(evntHandler);
        }

        public void RegisterEventHandler(Type evntHandler)
        {
            var interfaces = evntHandler.GetInterfaces();
            foreach (var interf in interfaces)
            {
                if (interf.GetTypeInfo().IsGenericType && interf.GetGenericTypeDefinition() == typeof(IHandleEvent<>))
                {
                    var eventType = interf.GetGenericArguments()[0];
                    RegisterEventHandler(eventType, evntHandler);
                }
            }
        }

        public void RegisterEventHandler(Assembly assembly)
        {
            var types = assembly.GetExportedTypes();
            foreach (var type in types)
            {
                RegisterEventHandler(type);
            }
        }
    }
}