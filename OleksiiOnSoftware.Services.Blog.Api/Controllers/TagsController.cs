using Microsoft.AspNetCore.Mvc;
using OleksiiOnSoftware.Services.Blog.Api.Services;
using OleksiiOnSoftware.Services.Blog.Dto;
using System.Threading.Tasks;

namespace OleksiiOnSoftware.Services.Blog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/blogs/{blogId}/tags")]
    public class TagsController : Controller
    {
        private readonly IBlogsService _blogsService;

        public TagsController(IBlogsService blogsService)
        {
            _blogsService = blogsService;
        }

        [HttpPost]
        public async Task Post([FromBody] TagDto tag)
        {
            await _blogsService.CreateTagAsync(tag);
        }
    }
}
