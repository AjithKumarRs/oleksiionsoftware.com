namespace OleksiiOnSoftware.Services.Common
{
    public class Command : Message
    {
        public Command(string aggregateId) : base(aggregateId)
        {
        }
    }
}
