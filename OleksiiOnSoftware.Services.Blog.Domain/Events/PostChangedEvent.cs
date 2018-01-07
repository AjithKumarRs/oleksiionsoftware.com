namespace OleksiiOnSoftware.Services.Blog.Domain.Events
{
    using System;
    using System.Collections.Generic;

    public class PostChangedEvent : PostStartedEvent
    {
        public PostChangedEvent(string aggregateId, string url, string title, string body, DateTime publishAt, string categoryTitle, string categoryUrl, List<PostStartedEventTag> tags, bool infobar, bool hidden, bool comments) : base(aggregateId, url, title, body, publishAt, categoryTitle, categoryUrl, tags, infobar, hidden, comments)
        {
        }
    }
}
