namespace OleksiiOnSoftware.Services.Blog.Domain.Events
{
    using Common;

    public class PostClosedEvent : Event
    {
        public string Url { get; }

        public PostClosedEvent(string aggregateId, string url) : base(aggregateId)
        {
            Url = url;
        }
    }
}
