namespace OleksiiOnSoftware.Services.Blog.Domain.Model
{
    class Link
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }

        public Link(string url, string title, int order)
        {
            Url = url;
            Title = title;
            Order = order;
        }
    }
}
