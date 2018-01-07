namespace OleksiiOnSoftware.Services.Common
{
    public abstract class Message
    {
        public string AggregateId { get; }

        protected Message(string aggregateId)
        {
            AggregateId = aggregateId;
        }

        protected bool Equals(Message other)
        {
            return string.Equals(AggregateId, other.AggregateId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Message)obj);
        }

        public override int GetHashCode()
        {
            return AggregateId?.GetHashCode() ?? 0;
        }
    }
}
