namespace OleksiiOnSoftware.Services.Blog.Domain.Events
{
    using Common;

    public class CategoryClosedEvent : Event
    {
        public string Url { get; }

        public CategoryClosedEvent(string aggregateId, string url) : base(aggregateId)
        {
            Url = url;
        }
    }
}
