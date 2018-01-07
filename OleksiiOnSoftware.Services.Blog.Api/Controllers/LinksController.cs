namespace OleksiiOnSoftware.Services.Blog.Api.Controllers
{
    using Dto;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;

    [Produces("application/json")]
    [Route("api/blogs/{blogId}/links")]
    public class LinksController : Controller
    {
        private readonly IBlogsService _blogsService;

        public LinksController(IBlogsService blogsService)
        {
            _blogsService = blogsService;
        }

        [HttpPost]
        public async Task Post([FromBody] LinkDto link)
        {
            await _blogsService.CreateLinkAsync(link);
        }
    }
}