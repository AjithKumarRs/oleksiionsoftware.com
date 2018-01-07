namespace OleksiiOnSoftware.Services.Blog.Domain.Events
{
    using Common;

    public class BlogClosedEvent : Event
    {
        public BlogClosedEvent(string aggregateId) : base(aggregateId)
        {
        }
    }
}
