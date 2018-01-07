using OleksiiOnSoftware.Services.Blog.Domain.Commands;
using OleksiiOnSoftware.Services.Blog.Domain.Events;
using OleksiiOnSoftware.Services.Blog.Domain.Exceptions;
using OleksiiOnSoftware.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OleksiiOnSoftware.Services.Blog.Domain.Model
{
    public class Blog : Aggregate,
        IHandleCommand<StartBlogCommand>,
        IHandleCommand<ChangeBlogCommand>,
        IHandleCommand<CloseBlogCommand>,
        IApplyEvent<BlogStartedEvent>,
        IApplyEvent<BlogChangedEvent>,
        IApplyEvent<BlogClosedEvent>,

        IHandleCommand<StartCategoryCommand>,
        IHandleCommand<ChangeCategoryCommand>,
        IHandleCommand<CloseCategoryCommand>,
        IApplyEvent<CategoryStartedEvent>,
        IApplyEvent<CategoryChangedEvent>,
        IApplyEvent<CategoryClosedEvent>,

        IHandleCommand<StartPostCommand>,
        IHandleCommand<ChangePostCommand>,
        IApplyEvent<PostStartedEvent>,
        IApplyEvent<PostChangedEvent>,

        IHandleCommand<StartTagCommand>,
        IHandleCommand<ChangeTagCommand>,
        IHandleCommand<CloseTagCommand>,
        IApplyEvent<TagStartedEvent>,
        IApplyEvent<TagChangedEvent>,
        IApplyEvent<TagClosedEvent>,

        IHandleCommand<StartLinkCommand>,
        IHandleCommand<CloseLinkCommand>,
        IApplyEvent<LinkStartedEvent>,
        IApplyEvent<LinkClosedEvent>
    {
        private string _brand;
        private string _copyright;
        private string _avatar;
        private string _github;
        private string _linkedin;
        private string _twitter;
        private bool _isClosed;

        private readonly List<Link> _links;
        private readonly List<Tag> _tags;
        private readonly Categories _categories;

        public Blog()
        {
            _links = new List<Link>();
            _tags = new List<Tag>();
            _categories = new Categories();
        }

        #region Blog

        public IEnumerable<Event> Handle(StartBlogCommand message)
        {
            if (string.IsNullOrEmpty(message.AggregateId))
            {
                throw new InvalidBlogHostException("Host can't be null or empty string. Specify a valid host.");
            }

            if (string.IsNullOrEmpty(message.Brand))
            {
                throw new InvalidBlogBrandException("Brand can't be null or empty string. Specify a valid brand.");
            }

            if (string.IsNullOrEmpty(message.Copyright))
            {
                throw new InvalidBlogCopyrightException("Copyright can't be null or empty string. Specify a valid brand.");
            }

            if (message.AggregateId != null && message.AggregateId.Equals(AggregateId)
                && message.Brand != null && message.Brand.Equals(_brand)
                && message.Copyright != null && message.Copyright.Equals(_copyright)
                && message.Avatar != null && message.Avatar.Equals(_avatar)
                && message.Github != null && message.Github.Equals(_github)
                && message.Linkedin != null && message.Linkedin.Equals(_linkedin)
                && message.Twitter != null && message.Twitter.Equals(_twitter))
            {
                yield break;
            }

            yield return new BlogStartedEvent(message.AggregateId,
                message.Brand,
                message.Copyright,
                message.Avatar,
                message.Github,
                message.Linkedin,
                message.Twitter);
        }

        public void Apply(BlogStartedEvent evnt)
        {
            _brand = evnt.Brand;
            _copyright = evnt.Copyright;
            _avatar = evnt.Avatar;
            _github = evnt.Github;
            _linkedin = evnt.Linkedin;
            _twitter = evnt.Twitter;
        }

        public IEnumerable<Event> Handle(ChangeBlogCommand message)
        {
            if (string.IsNullOrEmpty(message.AggregateId))
            {
                throw new InvalidBlogHostException("Host can't be null or empty string. Specify a valid host.");
            }

            if (string.IsNullOrEmpty(message.Brand))
            {
                throw new InvalidBlogBrandException("Brand can't be null or empty string. Specify a valid brand.");
            }

            if (string.IsNullOrEmpty(message.Copyright))
            {
                throw new InvalidBlogCopyrightException("Copyright can't be null or empty string. Specify a valid brand.");
            }

            if (message.AggregateId.Equals(AggregateId)
               && message.Brand.Equals(_brand)
               && message.Copyright.Equals(_copyright)
               && message.Avatar.Equals(_avatar)
               && message.Github.Equals(_github)
               && message.Linkedin.Equals(_linkedin)
               && message.Twitter.Equals(_twitter))
            {
                yield break;
            }

            yield return new BlogChangedEvent(message.AggregateId, message.Brand, message.Copyright, message.Avatar, message.Github, message.Linkedin, message.Twitter);
        }

        public void Apply(BlogChangedEvent evnt)
        {
            _brand = evnt.Brand;
            _copyright = evnt.Copyright;
            _avatar = evnt.Avatar;
            _github = evnt.Github;
            _linkedin = evnt.Linkedin;
            _twitter = evnt.Twitter;
        }

        public IEnumerable<Event> Handle(CloseBlogCommand message)
        {
            if (_links.Any())
            {
                throw new InvalidBlogLinksStateException($"There are still some links in {AggregateId}, blog has to be empty before it can be closed.");
            }

            if (_tags.Any())
            {
                throw new InvalidBlogTagsStateException($"There are still some tags in {AggregateId}, blog has to be empty before it can be closed.");
            }

            if (_categories.Any())
            {
                throw new InvalidBlogCategoriesStateException($"There are still some categories in {AggregateId}, blog has to be empty before it can be closed.");
            }

            yield return new BlogClosedEvent(message.AggregateId);
        }

        public void Apply(BlogClosedEvent evnt)
        {
            _links.Clear();
            _tags.Clear();
            _categories.Clear();
            _isClosed = true;
        }

        #endregion // Blog

        #region Category

        public IEnumerable<Event> Handle(StartCategoryCommand message)
        {
            var category = _categories.GetCategory(message.Url);
            if (category != null)
            {
                throw new CategoryAlreadyExistsException($"The category '{message.Url}' you are trying to create already exists.");
            }

            var categories = _categories.GetCategories(message.Url);
            if (categories == null)
            {
                throw new CategoryParentDoesNotExistException($"One of the '{message.Url}' category parrents doesn't exist.");
            }

            yield return new CategoryStartedEvent(message.AggregateId, message.Url, message.Title);
        }

        public void Apply(CategoryStartedEvent evnt)
        {
            _categories.AddCategory(evnt.Url, evnt.Title);
        }

        public IEnumerable<Event> Handle(ChangeCategoryCommand message)
        {
            var category = _categories.GetCategory(message.Url);
            if (category == null)
            {
                throw new CategoryDoesNotExistException($"The category '{message.Url}' you are trying to update does not exist.");
            }

            yield return new CategoryChangedEvent(message.AggregateId, message.Url, message.Title);
        }

        public void Apply(CategoryChangedEvent evnt)
        {
            var category = _categories.GetCategory(evnt.Url);
            category.Title = evnt.Title;
        }

        public IEnumerable<Event> Handle(CloseCategoryCommand message)
        {
            var category = _categories.GetCategory(message.Url);
            if (category == null)
            {
                throw new CategoryDoesNotExistException($"The category '{message.Url}' you are trying to close does not exist.");
            }

            if (category.Categories.Any())
            {
                throw new InvalidCategoryCategoriesStateException($"The category '{message.Url}' you are trying to close should not contain open categories.");
            }

            if (category.Posts.Any())
            {
                throw new InvalidCategoryPostsStateException($"The category '{message.Url}' you are trying to close should not contain open posts.");
            }

            yield return new CategoryClosedEvent(message.AggregateId, message.Url);
        }

        public void Apply(CategoryClosedEvent evnt)
        {
            var categories = _categories.GetCategories(evnt.Url);
            categories.Remove(new Category(evnt.Url));
        }

        #endregion // Category

        #region Post 

        public IEnumerable<Event> Handle(StartPostCommand message)
        {
            if (string.IsNullOrEmpty(message.Url))
            {
                throw new InvalidPostUrlException($"The post's '{nameof(Post.Url)}' can not be null or empty string.");
            }

            if (string.IsNullOrEmpty(message.Title))
            {
                throw new InvalidPostTitleException($"The post's '{nameof(Post.Title)}' can not be null or empty string.");
            }

            var category = _categories.GetCategory(message.CategoryUrl);
            if (category == null)
            {
                throw new CategoryDoesNotExistException($"Category '{message.CategoryUrl}' doesn't exist");
            }

            if (category.Posts.Any(_ => string.Equals(_.Url, message.Url)))
            {
                throw new PostAlreadyExistsException($"The post '{message.Url}' you are trying to create already exists.");
            }

            Func<Tag, bool> tagsComparator = tag => message.TagUrls.Any(url => string.Equals(url, tag.Url));
            var tags = _tags
                    .Where(tagsComparator)
                    .Select(_ => new PostStartedEvent.PostStartedEventTag(_.Url, _.Title))
                    .ToList();

            yield return new PostStartedEvent(message.AggregateId,
                message.Url,
                message.Title,
                message.Body,
                message.PublishAt,
                category.Title,
                message.CategoryUrl,
                tags,
                message.Infobar,
                message.Hidden,
                message.Comments);
        }

        public void Apply(PostStartedEvent evnt)
        {
            var category = _categories.GetCategory(evnt.CategoryUrl);
            category.Posts.Add(new Post(evnt.Url, evnt.Title, evnt.Body));
        }

        public IEnumerable<Event> Handle(ChangePostCommand message)
        {
            if (string.IsNullOrEmpty(message.Url))
            {
                throw new InvalidPostUrlException($"The post's '{nameof(Post.Url)}' can not be null or empty string.");
            }

            if (string.IsNullOrEmpty(message.Title))
            {
                throw new InvalidPostTitleException($"The post's '{nameof(Post.Title)}' can not be null or empty string.");
            }

            var category = _categories.GetCategory(message.CategoryUrl);
            if (category == null)
            {
                throw new CategoryDoesNotExistException($"Category '{message.CategoryUrl}' doesn't exist");
            }

            if (!category.Posts.Any(_ => string.Equals(_.Url, message.Url)))
            {
                throw new PostDoesNotExistException($"The post '{message.Url}' you are trying to update does not exist.");
            }

            Func<Tag, bool> tagsComparator = tag => message.TagUrls.Any(url => string.Equals(url, tag.Url));
            var tags = _tags
                    .Where(tagsComparator)
                    .Select(_ => new PostStartedEvent.PostStartedEventTag(_.Url, _.Title))
                    .ToList();

            yield return new PostChangedEvent(message.AggregateId,
                message.Url,
                message.Title,
                message.Body,
                message.PublishAt,
                category.Title,
                category.Url,
                tags,
                message.Infobar,
                message.Hidden,
                message.Comments);
        }

        public void Apply(PostChangedEvent evnt)
        {
            var category = _categories.GetCategory(evnt.CategoryUrl);
            var post = category.Posts.FirstOrDefault(_ => string.Equals(_.Url, evnt.Url));
            post.Title = evnt.Title;
            post.Body = evnt.Body;
            post.PublishAt = evnt.PublishAt;
            post.TagUrls = new HashSet<string>(evnt.Tags.Select(_ => _.Url).ToList());
            post.Infobar = evnt.Infobar;
            post.Hidden = evnt.Hidden;
            post.Comments = evnt.Comments;
        }

        #endregion // Post

        #region Tag

        public IEnumerable<Event> Handle(StartTagCommand message)
        {
            if (_tags.Any(_ => string.Equals(_.Url, message.Url)))
            {
                throw new TagAlreadyExistsException($"The tag (${message.Url}) you are trying to create already exists.");
            }

            yield return new TagStartedEvent(message.AggregateId, message.Url, message.Title);
        }

        public void Apply(TagStartedEvent evnt)
        {
            _tags.Add(new Tag(evnt.Url, evnt.Title));
        }

        public IEnumerable<Event> Handle(ChangeTagCommand message)
        {
            if (!_tags.Any(_ => string.Equals(_.Url, message.Url)))
            {
                throw new TagDoesNotExistException($"The tag '{message.Url}' you are trying to update does not exist.");
            }

            yield return new TagChangedEvent(message.AggregateId, message.Url, message.Title);
        }

        public void Apply(TagChangedEvent evnt)
        {
            var tag = _tags.FirstOrDefault(_ => string.Equals(_.Url, evnt.Url));
            tag.Title = evnt.Title;
        }

        public IEnumerable<Event> Handle(CloseTagCommand message)
        {
            if (!_tags.Any(_ => string.Equals(_.Url, message.Url)))
            {
                throw new TagDoesNotExistException($"The tag '{message.Url}' you are trying to update does not exist.");
            }

            yield return new TagClosedEvent(message.AggregateId, message.Url);
        }

        public void Apply(TagClosedEvent evnt)
        {
            var tag = _tags.FirstOrDefault(_ => string.Equals(_.Url, evnt.Url));
            _tags.Remove(tag);
        }

        #endregion // Tag

        #region Links

        public IEnumerable<Event> Handle(StartLinkCommand message)
        {
            if (_links.Any(_ => string.Equals(_.Url, message.Url)))
            {
                throw new LinkAlreadyExistsException($"The navigation item with the same title '{message.Title}' or path '{message.Url}' already exists.");
            }

            yield return new LinkStartedEvent(message.AggregateId, message.Url, message.Title, message.Order);
        }

        public void Apply(LinkStartedEvent evnt)
        {
            _links.Add(new Link(evnt.Url, evnt.Title, evnt.Order));
        }

        public IEnumerable<Event> Handle(CloseLinkCommand message)
        {
            if (!_links.Any(_ => string.Equals(_.Url, message.Url)))
            {
                throw new LinkDoesNotExistException($"The link to '{message.Url}' doesn't exist exception.");
            }

            yield return new LinkClosedEvent(message.AggregateId, message.Url);
        }

        public void Apply(LinkClosedEvent evnt)
        {
            var link = _links.FirstOrDefault(_ => string.Equals(_.Url, evnt.Url));
            _links.Remove(link);
        }

        #endregion // Links
    }
}

