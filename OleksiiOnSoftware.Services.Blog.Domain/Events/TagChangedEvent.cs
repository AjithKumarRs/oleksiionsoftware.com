namespace OleksiiOnSoftware.Services.Blog.Domain.Events
{
    using Common;

    public class TagChangedEvent : Event
    {
        public string Url { get; }
        public string Title { get; }

        public TagChangedEvent(string aggregateId, string url, string title) : base(aggregateId)
        {
            Url = url;
            Title = title;
        }

        protected bool Equals(TagChangedEvent other)
        {
            return base.Equals(other) && string.Equals(Url, other.Url) && string.Equals(Title, other.Title);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TagChangedEvent) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Title != null ? Title.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
