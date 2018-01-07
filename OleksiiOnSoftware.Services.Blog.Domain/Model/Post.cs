using System;
using System.Collections.Generic;

namespace OleksiiOnSoftware.Services.Blog.Domain.Model
{
    class Post
    {
        public string Url { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime PublishAt { get; set; }

        public HashSet<string> TagUrls { get; set; }

        public bool Infobar { get; set; }

        public bool Hidden { get; set; }

        public bool Comments { get; set; }

        public Post(string url, string title, string body)
        {
            Url = url;
            Body = body;
            Title = title;
        }
    }
}
