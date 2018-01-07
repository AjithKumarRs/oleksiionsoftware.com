namespace OleksiiOnSoftware.Services.Blog.Query.Views
{
    using System.Collections.Generic;

    public class HomeView
    {
        public string Id { get; set; }

        public string Brand { get; set; }

        public string Copyright { get; set; }

        public string Avatar { get; set; }

        public string Github { get; set; }

        public string Linkedin { get; set; }

        public string Twitter { get; set; }

        public List<LinkHomeView> Links { get; set; }

        public List<PostHomeView> Posts { get; set; }

        public FilterHomeView Filter { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int PagesCount { get; set; }

        public HomeView()
        {
            Links = new List<LinkHomeView>();
            Posts = new List<PostHomeView>();
        }
    }

    public class FilterHomeView
    {
        public string By { get; set; }

        public string Title { get; set; }
    }

    public class LinkHomeView
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }
    }

    public class TagHomeView
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }

    public class CategoryHomeView
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }

    public class PostHomeView
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Short { get; set; }

        public string Date { get; set; }

        public CategoryHomeView Category { get; set; }

        public List<TagHomeView> Tags { get; set; }

        public PostHomeView()
        {
            Tags = new List<TagHomeView>();
        }
    }
}
