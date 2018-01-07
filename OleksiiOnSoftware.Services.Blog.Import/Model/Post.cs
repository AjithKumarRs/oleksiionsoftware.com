using System;

namespace OleksiiOnSoftware.Services.Blog.Import.Model
{
    public class Post
    {
        public Post()
        {
            Infobar = true;
            Hidden = false;
            Comments = true;
        }

        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime Publish { get; set; }

        public string Category { get; set; }

        public string[] Tags { get; set; }

        public bool Infobar { get; set; }

        public bool Hidden { get; set; }

        public bool Comments { get; set; }

        public string Content { get; set; }

        public string RelativePath { get; set; }
    }
}
