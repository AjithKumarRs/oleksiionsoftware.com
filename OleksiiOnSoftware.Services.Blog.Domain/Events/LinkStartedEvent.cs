namespace OleksiiOnSoftware.Services.Blog.Domain.Events
{
    using Common;

    public class LinkStartedEvent : Event
    {
        public string Title { get; }
        public string Url { get; }
        public int Order { get; }

        public LinkStartedEvent(string aggregateId, string url, string title, int order) : base(aggregateId)
        {
            Title = title;
            Url = url;
            Order = order;
        }
    }
}
