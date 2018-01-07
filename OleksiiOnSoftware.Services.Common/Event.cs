namespace OleksiiOnSoftware.Services.Common
{
    public class Event : Message
    {
        public Event(string aggregateId) : base(aggregateId)
        {
        }
    }
}
