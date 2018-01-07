namespace OleksiiOnSoftware.Services.Blog.Query.Model
{
    using System.Collections.Generic;

    public class BlogListState
    {
        public HashSet<string> Blogs { get; set; }

        public BlogListState()
        {
            Blogs = new HashSet<string>();
        }
    }
}
