namespace OleksiiOnSoftware.Services.Blog.Domain.Events
{
    using Common;

    public class TagClosedEvent : Event
    {
        public string Url { get; }

        public TagClosedEvent(string aggregateId, string url) : base(aggregateId)
        {
            Url = url;
        }
    }
}
