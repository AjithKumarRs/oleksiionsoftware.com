namespace OleksiiOnSoftware.Services.Blog.Query.Handlers
{
    using OleksiiOnSoftware.Services.Blog.Domain.Events;
    using OleksiiOnSoftware.Services.Blog.Query.Model;
    using OleksiiOnSoftware.Services.Blog.Query.Utils;
    using OleksiiOnSoftware.Services.Common;
    using StackExchange.Redis;
    using System;

    public class BlogListStateHandler :
        IHandleEvent<BlogStartedEvent>,
        IHandleEvent<BlogClosedEvent>
    {
        private readonly IDatabase _db;

        public BlogListStateHandler(IDatabase db)
        {
            _db = db;
        }

        public void Handle(BlogStartedEvent evnt)
        {
            var key = KeyUtils.GetBlogListKey();

            var state = _db.GetObject<BlogListState>(key);
            if (state != null)
            {
                throw new Exception("There is a blog with the same host name in the store.");
            }

            state = new BlogListState();
            state.Blogs.Add(evnt.AggregateId);

            _db.SetObject(key, state);
        }

        public void Handle(BlogClosedEvent evnt)
        {
            var key = KeyUtils.GetBlogListKey();

            var state = _db.GetObject<BlogListState>(key);
            if (state != null)
            {
                throw new Exception("There is a blog with the same host name in the store.");
            }

            state.Blogs.Remove(evnt.AggregateId);

            _db.SetObject(key, state);
        }
    }
}
