namespace OleksiiOnSoftware.Services.Blog.Query.Model
{
    using System.Collections.Generic;

    public class BlogState
    {
        public string Id { get; set; }

        public string Brand { get; set; }

        public string Copyright { get; set; }

        public string Avatar { get; set; }

        public string Github { get; set; }

        public string Linkedin { get; set; }

        public string Twitter { get; set; }

        public List<LinkState> Links { get; set; }

        public List<CategoryState> Categories { get; set; }

        public List<TagState> Tags { get; set; }

        public List<PostState> Posts { get; set; }

        public BlogState()
        {
            Links = new List<LinkState>();
            Categories = new List<CategoryState>();
            Tags = new List<TagState>();
            Posts = new List<PostState>();
        }
    }
}
