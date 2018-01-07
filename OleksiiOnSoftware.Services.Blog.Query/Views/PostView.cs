namespace OleksiiOnSoftware.Services.Blog.Query.Views
{
    using System.Collections.Generic;

    public class PostView
    {
        public string Id { get; set; }

        public string Brand { get; set; }

        public string Copyright { get; set; }

        public string Avatar { get; set; }

        public string Github { get; set; }

        public string Linkedin { get; set; }

        public string Twitter { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Date { get; set; }

        public bool Infobar { get; set; }

        public bool Comments { get; set; }

        public CategoryHomeView Category { get; set; }

        public List<TagHomeView> Tags { get; set; }

        public List<LinkHomeView> Links { get; set; }
    }
}
