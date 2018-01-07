namespace OleksiiOnSoftware.Services.Blog.Query.Handlers
{
    using OleksiiOnSoftware.Services.Blog.Domain.Events;
    using OleksiiOnSoftware.Services.Blog.Query.Model;
    using OleksiiOnSoftware.Services.Blog.Query.Utils;
    using OleksiiOnSoftware.Services.Common;
    using StackExchange.Redis;
    using System;
    using System.Linq;

    public class BlogStateHandler :
        IHandleEvent<LinkStartedEvent>,
        IHandleEvent<LinkClosedEvent>,
        IHandleEvent<BlogStartedEvent>,
        IHandleEvent<BlogClosedEvent>,
        IHandleEvent<PostStartedEvent>,
        IHandleEvent<PostChangedEvent>,
        IHandleEvent<CategoryStartedEvent>,
        IHandleEvent<CategoryChangedEvent>,
        IHandleEvent<TagStartedEvent>,
        IHandleEvent<TagChangedEvent>
    {
        private readonly IDatabase _db;

        public BlogStateHandler(IDatabase db)
        {
            _db = db;
        }

        public void Handle(BlogStartedEvent evnt)
        {
            var key = KeyUtils.GetStateKey(evnt.AggregateId);

            var state = _db.GetObject<BlogState>(key);
            if (state != null)
            {
                throw new Exception("There is a blog with the same host name in the store.");
            }

            state = new BlogState
            {
                Id = evnt.AggregateId,
                Brand = evnt.Brand,
                Copyright = evnt.Copyright,
                Avatar = evnt.Avatar,
                Github = evnt.Github,
                Twitter = evnt.Twitter,
                Linkedin = evnt.Linkedin
            };

            _db.SetObject(key, state);
        }

        public void Handle(BlogClosedEvent evnt)
        {
            var key = KeyUtils.GetStateKey(evnt.AggregateId);
            _db.KeyDelete(key);
        }

        public void Handle(LinkStartedEvent evnt)
        {
            var key = KeyUtils.GetStateKey(evnt.AggregateId);

            var state = _db.GetObject<BlogState>(key);
            if (state == null)
            {
                throw new Exception("Blog has to be started first.");
            }

            var item = new LinkState
            {
                Id = evnt.Url,
                Title = evnt.Title,
                Order = evnt.Order
            };

            state.Links.Add(item);

            _db.SetObject(key, state);
        }

        public void Handle(LinkClosedEvent evnt)
        {
            var key = KeyUtils.GetStateKey(evnt.AggregateId);

            var state = _db.GetObject<BlogState>(key);
            if (state == null)
            {
                throw new Exception("Blog has to be started first.");
            }

            state.Links.RemoveAll(_ => _.Id == evnt.Url);

            _db.SetObject(key, state);
        }

        public void Handle(PostStartedEvent evnt)
        {
            var key = KeyUtils.GetStateKey(evnt.AggregateId);

            var state = _db.GetObject<BlogState>(key);
            if (state == null)
            {
                throw new Exception("Blog has to be started first.");
            }

            var bodyHtml = TextUtils.GetHtmlFromMarkdown(evnt.Body);
            var bodyShortHtml = TextUtils.GetHtmlFromMarkdown(TextUtils.GetBodyShort(evnt.Body));

            var tags = evnt.Tags.Select(_ => new TagState
            {
                TagUrl = _.Url,
                TagTitle = _.Title
            }).ToList();

            var post = new PostState
            {
                Url = evnt.Url,
                Title = evnt.Title,
                Body = bodyHtml,
                BodyShort = bodyShortHtml,
                PublishAt = evnt.PublishAt,
                CategoryTitle = evnt.CategoryTitle,
                CategoryUrl = evnt.CategoryUrl,
                Infobar = evnt.Infobar,
                IsHidden = evnt.Hidden,
                Tags = tags
            };

            state.Posts.Add(post);

            _db.SetObject(key, state);
        }

        public void Handle(PostChangedEvent evnt)
        {
            var key = KeyUtils.GetStateKey(evnt.AggregateId);

            var state = _db.GetObject<BlogState>(key);
            if (state == null)
            {
                throw new Exception("Blog has to be started first.");
            }

            var post = state.Posts.FirstOrDefault(_ => _.Url == evnt.Url);
            if (post == null)
            {
                throw new Exception("Post should be created first.");
            }

            var tags = evnt.Tags.Select(_ => new TagState
            {
                TagUrl = _.Url,
                TagTitle = _.Title
            }).ToList();

            post.Url = evnt.Url;
            post.Title = evnt.Title;
            post.BodyShort = TextUtils.GetHtmlFromMarkdown(TextUtils.GetBodyShort(evnt.Body));
            post.Body = TextUtils.GetHtmlFromMarkdown(evnt.Body);
            post.PublishAt = evnt.PublishAt;
            post.CategoryTitle = evnt.CategoryTitle;
            post.CategoryUrl = evnt.CategoryUrl;
            post.Infobar = evnt.Infobar;
            post.IsHidden = evnt.Hidden;
            post.Tags = tags;

            _db.SetObject(key, state);
        }

        public void Handle(CategoryStartedEvent evnt)
        {
            var key = KeyUtils.GetStateKey(evnt.AggregateId);

            var state = _db.GetObject<BlogState>(key);
            if (state == null)
            {
                throw new Exception("Blog has to be started first.");
            }

            var category = new CategoryState
            {
                Url = evnt.Url,
                Title = evnt.Title
            };

            state.Categories.Add(category);

            _db.SetObject(key, state);
        }

        public void Handle(CategoryChangedEvent evnt)
        {
            var key = KeyUtils.GetStateKey(evnt.AggregateId);

            var state = _db.GetObject<BlogState>(key);
            if (state == null)
            {
                throw new Exception("Blog has to be started first.");
            }

            var posts = state.Posts.Where(_ => _.CategoryUrl == evnt.Url).ToList();
            foreach (var post in posts)
            {
                post.CategoryTitle = evnt.Title;
                post.CategoryUrl = evnt.Url;
            }

            _db.SetObject(key, state);
        }

        public void Handle(TagStartedEvent evnt)
        {
            var key = KeyUtils.GetStateKey(evnt.AggregateId);

            var state = _db.GetObject<BlogState>(key);
            if (state == null)
            {
                throw new Exception("Blog has to be started first.");
            }

            var tag = new TagState
            {
                TagUrl = evnt.Url,
                TagTitle = evnt.Title
            };

            state.Tags.Add(tag);

            _db.SetObject(key, state);
        }

        public void Handle(TagChangedEvent evnt)
        {
            var key = KeyUtils.GetStateKey(evnt.AggregateId);

            var state = _db.GetObject<BlogState>(key);
            if (state == null)
            {
                throw new Exception("Blog has to be started first.");
            }

            var tags = state
                .Posts
                .SelectMany(_ => _.Tags)
                .Where(_ => _.TagUrl == evnt.Url)
                .ToList();

            foreach (var tag in tags)
            {
                tag.TagTitle = evnt.Title;
                tag.TagUrl = evnt.Url;
            }

            _db.SetObject(key, state);
        }
    }
}
