using System;

namespace OleksiiOnSoftware.Services.Blog.Dto
{
    public class PostDto
    {
        public string BlogId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime Publish { get; set; }

        public string Category { get; set; }

        public string[] Tags { get; set; }

        public bool Infobar { get; set; }

        public bool Hidden { get; set; }

        public bool Comments { get; set; }

        public string Content { get; set; }
    }
}
