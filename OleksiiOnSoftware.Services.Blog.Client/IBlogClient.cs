namespace OleksiiOnSoftware.Services.Blog.Client
{
    using Dto;
    using OleksiiOnSoftware.Services.Blog.Query.Views;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlogClient
    {
        Task<IEnumerable<string>> GetBlogs();

        Task<HomeView> GetBlog(string blogId);

        Task CreateBlog(BlogDto blog);

        Task CreateLink(LinkDto link);

        Task CreatePost(PostDto post);

        Task CreateCategory(CategoryDto category);

        Task CreateTag(TagDto tag);

        Task DeleteBlog(string blogId);
    }
}
