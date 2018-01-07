namespace OleksiiOnSoftware.Services.Blog.Api.Services.Impl
{
    using Common;
    using Domain.Commands;
    using Dto;
    using OleksiiOnSoftware.Services.Blog.Query.Views;
    using Query.Queries;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BlogsService : IBlogsService
    {
        private readonly ICommandBus _bus;

        private readonly GetBlogListQuery _getBlogListQuery;
        private readonly GetHomeViewQuery _getHomeViewQuery;
        private readonly GetPostViewQuery _getPostViewQuery;

        public BlogsService(ICommandBus bus,
            GetHomeViewQuery getHomeViewQuery,
            GetPostViewQuery getPostViewQuery,
            GetBlogListQuery getBlogListQuery)
        {
            _bus = bus;
            _getBlogListQuery = getBlogListQuery;
            _getHomeViewQuery = getHomeViewQuery;
            _getPostViewQuery = getPostViewQuery;
        }

        public async Task<IEnumerable<string>> GetAllAsync()
        {
            return await _getBlogListQuery.ExecuteAsync();
        }

        public async Task<HomeView> GetHomeViewAsync(string id,
            string filterByDate,
            string filterByCategory,
            string filterByTag,
            int pageIndex = 0,
            int pageSize = 10)
        {
            var result = await _getHomeViewQuery
                .SetBlogId("oleksiionsoftware.com")
                .SetFilterByDate(filterByDate)
                .SetFilterByCategory(filterByCategory)
                .SetFilterByTag(filterByTag)
                .SetPageIndex(pageIndex)
                .SetPageSize(pageSize)
                .ExecuteAsync();

            return result;
        }

        public async Task<PostView> GetPostViewAsync(string id, string postId)
        {
            var result = await _getPostViewQuery
                .SetBlogId("oleksiionsoftware.com")
                .SetPostId(postId)
                .ExecuteAsync();

            return result;
        }

        public async Task CreateBlogAsync(BlogDto blog)
        {
            await _bus.SendAsync(new StartBlogCommand(blog.Id, blog.Brand, blog.Copyright, blog.Avatar, blog.Github, blog.Linkedin, blog.Twitter));
        }

        public async Task CreateLinkAsync(LinkDto link)
        {
            await _bus.SendAsync(new StartLinkCommand(link.BlogId, link.Url, link.Title, link.Order));
        }

        public async Task CreateCategoryAsync(CategoryDto category)
        {
            await _bus.SendAsync(new StartCategoryCommand(category.BlogId, category.Url, category.Title));
        }

        public async Task CreateTagAsync(TagDto tag)
        {
            await _bus.SendAsync(new StartTagCommand(tag.BlogId, tag.Url, tag.Title));
        }

        public async Task CreatePostAsync(PostDto post)
        {
            var cmd = new StartPostCommand(post.BlogId,
                post.Url,
                post.Title,
                post.Content,
                post.Publish,
                post.Category,
                new HashSet<string>(post.Tags),
                post.Infobar,
                post.Hidden,
                true);

            await _bus.SendAsync(cmd);
        }

        public async Task DeleteAsync(string id)
        {
            await _bus.SendAsync(new CloseBlogCommand(id));
        }
    }
}
