namespace OleksiiOnSoftware.Services.Common.Redis
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Aggregate = Aggregate;

    public class RedisCommandProcessor : ICommandProcessor
    {
        private const string Channel = "Blog.Commands";

        private readonly Dictionary<Type, Type> _commandHandlers;

        private readonly IRepository _repository;
        private readonly IEventBus _eventBus;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly ILogger<RedisCommandProcessor> _logger;

        public RedisCommandProcessor(IRepository repository, IEventBus eventBus, IConnectionMultiplexer connectionMultiplexer, ILogger<RedisCommandProcessor> logger)
        {
            _commandHandlers = new Dictionary<Type, Type>();

            _repository = repository;
            _eventBus = eventBus;
            _connectionMultiplexer = connectionMultiplexer;
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInformation($"Command processor started. Listening to {Channel} channel.");

            var sub = _connectionMultiplexer.GetSubscriber();
            sub.Subscribe(Channel, (ch, val) =>
            {
                _logger.LogInformation($"Received command.");
                try
                {
                    var cmd = JsonConvert.DeserializeObject<Command>(val, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                    _logger.LogInformation($"Processing command: {cmd.GetType().Name}");

                    var events = Process(cmd);
                    foreach (var evnt in events)
                    {
                        _logger.LogInformation($"Publishing event: {evnt.GetType().Name}");
                        _eventBus.Publish(evnt);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                _logger.LogInformation($"Waiting for the next command.");
            });
        }

        public void RegisterCommandHandler<TCommand, TAggregate>()
            where TCommand : Command
            where TAggregate : Aggregate, IHandleCommand<TCommand>, new()
        {
            RegisterCommandHandler(typeof(TCommand), typeof(TAggregate));
        }

        public void RegisterCommandHandler(Type command, Type aggregate)
        {
            if (_commandHandlers.ContainsKey(command))
            {
                throw new Exception("It is not allowed to have more than one command handler for specific command type.");
            }

            _commandHandlers.Add(command, aggregate);
        }

        public void RegisterCommandHandler(Type aggregate)
        {
            var interfaces = aggregate.GetInterfaces();
            foreach (var interf in interfaces)
            {
                if (interf.GetTypeInfo().IsGenericType && interf.GetGenericTypeDefinition() == typeof(IHandleCommand<>))
                {
                    var commandType = interf.GetGenericArguments()[0];
                    RegisterCommandHandler(commandType, aggregate);
                }
            }
        }

        private IEnumerable<Event> Process<T>(T cmd) where T : Command
        {
            var evnts = new List<Event>();

            var cmdType = cmd.GetType();
            if (!_commandHandlers.ContainsKey(cmdType))
            {
                return evnts;
            }

            var aggregateType = _commandHandlers[cmdType];
            var aggregate = _repository.Get(aggregateType, cmd.AggregateId);
            foreach (var evnt in ((dynamic)aggregate).Handle((dynamic)cmd))
            {
                evnts.Add(evnt);
            }

            _repository.Set(cmd.AggregateId, aggregate, evnts);
            return evnts;
        }
    }
}
