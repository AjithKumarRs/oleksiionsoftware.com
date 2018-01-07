namespace OleksiiOnSoftware.Services.Blog.Domain.Events
{
    using Common;
    using System;
    using System.Collections.Generic;

    public class PostStartedEvent : Event
    {
        public string Url { get; }
        public string Title { get; }
        public string Body { get; }
        public DateTime PublishAt { get; }
        public string CategoryTitle { get; }
        public string CategoryUrl { get; }
        public List<PostStartedEventTag> Tags { get; }
        public bool Infobar { get; }
        public bool Hidden { get; }
        public bool Comments { get; }

        public PostStartedEvent(string aggregateId,
            string url,
            string title,
            string body,
            DateTime publishAt,
            string categoryTitle,
            string categoryUrl,
            List<PostStartedEventTag> tags,
            bool infobar,
            bool hidden,
            bool comments) : base(aggregateId)
        {
            Url = url;
            Title = title;
            Body = body;
            PublishAt = publishAt;
            CategoryTitle = categoryTitle;
            CategoryUrl = categoryUrl;
            Tags = tags;
            Infobar = infobar;
            Hidden = hidden;
            Comments = comments;
        }

        public class PostStartedEventTag
        {
            public string Url { get; }

            public string Title { get; }

            public PostStartedEventTag(string url, string title)
            {
                Url = url;
                Title = title;
            }
        }
    }
}
