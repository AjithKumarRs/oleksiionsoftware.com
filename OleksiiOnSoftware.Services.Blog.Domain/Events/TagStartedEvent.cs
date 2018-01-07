namespace OleksiiOnSoftware.Services.Blog.Domain.Events
{
    using Common;

    public class TagStartedEvent : Event
    {
        public string Url { get; }
        public string Title { get; }

        public TagStartedEvent(string aggregateId, string url, string title) : base(aggregateId)
        {
            Url = url;
            Title = title;
        }
    }
}
