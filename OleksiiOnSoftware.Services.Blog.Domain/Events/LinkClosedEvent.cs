namespace OleksiiOnSoftware.Services.Blog.Domain.Events
{
    using Common;

    public class LinkClosedEvent : Event
    {
        public string Url { get; }

        public LinkClosedEvent(string aggregateId, string url) : base(aggregateId)
        {
            Url = url;
        }
    }
}
