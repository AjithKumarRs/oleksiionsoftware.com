namespace OleksiiOnSoftware.Services.Blog.Api.Services
{
    using Dto;
    using OleksiiOnSoftware.Services.Blog.Query.Views;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogsService
    {
        Task<IEnumerable<string>> GetAllAsync();

        Task<HomeView> GetHomeViewAsync(string id,
            string filterByDate,
            string filterByCategory,
            string filterByTag,
            int pageIndex,
            int pageSize);

        Task<PostView> GetPostViewAsync(string id, string postId);

        Task CreateBlogAsync(BlogDto blog);

        Task CreateLinkAsync(LinkDto link);

        Task CreatePostAsync(PostDto post);

        Task CreateCategoryAsync(CategoryDto category);

        Task CreateTagAsync(TagDto tag);

        Task DeleteAsync(string id);
    }
}
