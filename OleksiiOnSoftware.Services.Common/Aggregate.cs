namespace OleksiiOnSoftware.Services.Common
{
    public abstract class Aggregate
    {
        public string AggregateId { get; set; }

        public int Version { get; set; }
        
        public void ApplyEvent(Event evnt)
        {
            (this as dynamic).Apply((dynamic)evnt);
            Version++;
        }
    }
}
