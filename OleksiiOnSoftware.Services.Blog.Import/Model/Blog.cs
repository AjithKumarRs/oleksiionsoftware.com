using System.Collections.Generic;

namespace OleksiiOnSoftware.Services.Blog.Import.Model
{
    public class Blog
    {
        public Blog()
        {
            Links = new List<Link>();
            Posts = new List<Post>();
            Categories = new List<Category>();
            Tags = new List<Tag>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Copyright { get; set; }

        public string Avatar { get; set; }

        public string Github { get; set; }

        public string Linkedin { get; set; }

        public string Twitter { get; set; }

        public List<Link> Links { get; set; }

        public List<Post> Posts { get; set; }

        public List<Category> Categories { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
