namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using Common;
    using System;
    using System.Collections.Generic;

    public class StartPostCommand : Command
    {
        public string Url { get; }
        public string Title { get; }
        public string Body { get; }
        public DateTime PublishAt { get; }
        public string CategoryUrl { get; }
        public HashSet<string> TagUrls { get; }
        public bool Infobar { get; }
        public bool Hidden { get; }
        public bool Comments { get; }

        public StartPostCommand(string aggregateId,
            string url,
            string title,
            string body,
            DateTime publishAt,
            string categoryUrl,
            HashSet<string> tagUrls,
            bool infobar,
            bool hidden,
            bool comments) : base(aggregateId)
        {
            Url = url;
            Title = title;
            Body = body;
            PublishAt = publishAt;
            CategoryUrl = categoryUrl;
            TagUrls = tagUrls;
            Infobar = infobar;
            Hidden = hidden;
            Comments = comments;
        }
    }
}