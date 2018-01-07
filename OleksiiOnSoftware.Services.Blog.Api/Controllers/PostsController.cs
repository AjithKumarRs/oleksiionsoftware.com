namespace OleksiiOnSoftware.Services.Blog.Api.Controllers
{
    using Dto;
    using Microsoft.AspNetCore.Mvc;
    using OleksiiOnSoftware.Services.Blog.Query.Views;
    using Services;
    using System.Threading.Tasks;

    [Produces("application/json")]
    [Route("api/blogs/{blogId}/posts")]
    public class PostsController : Controller
    {
        private readonly IBlogsService _blogsService;

        public PostsController(IBlogsService blogsService)
        {
            _blogsService = blogsService;
        }

        [HttpGet("{postId}")]
        public async Task<PostView> Get(string blogId, string postId)
        {
            return await _blogsService.GetPostViewAsync(blogId, postId);
        }

        [HttpPost]
        public async Task Post([FromBody] PostDto post)
        {
            await _blogsService.CreatePostAsync(post);
        }
    }
}