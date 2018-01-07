namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using System;
    using System.Collections.Generic;

    public class ChangePostCommand : StartPostCommand
    {
        public ChangePostCommand(string aggregateId, string url, string title, string body, DateTime publishAt, string categoryUrl, HashSet<string> tagUrls, bool infobar, bool hidden, bool comments) : base(aggregateId, url, title, body, publishAt, categoryUrl, tagUrls, infobar, hidden, comments)
        {
        }
    }
}
