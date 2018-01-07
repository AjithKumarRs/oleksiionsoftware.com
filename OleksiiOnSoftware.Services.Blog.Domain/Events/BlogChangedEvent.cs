namespace OleksiiOnSoftware.Services.Blog.Domain.Events
{
    public class BlogChangedEvent : BlogStartedEvent
    {
        public BlogChangedEvent(string aggregateId, string brand, string copyright, string avatar, string github, string linkedin, string twitter) : base(aggregateId, brand, copyright, avatar, github, linkedin, twitter)
        {
        }
    }
}
