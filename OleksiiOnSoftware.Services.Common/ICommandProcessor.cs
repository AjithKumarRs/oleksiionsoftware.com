namespace OleksiiOnSoftware.Services.Common
{
    using System;

    public interface ICommandProcessor
    {
        void Start();

        void RegisterCommandHandler(Type aggregate);
        
        void RegisterCommandHandler(Type command, Type aggregate);

        void RegisterCommandHandler<TCommand, TAggregate>()
            where TCommand : Command
            where TAggregate : Aggregate, IHandleCommand<TCommand>, new();
    }
}
