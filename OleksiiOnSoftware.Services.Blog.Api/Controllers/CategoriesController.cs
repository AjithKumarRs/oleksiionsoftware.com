using Microsoft.AspNetCore.Mvc;
using OleksiiOnSoftware.Services.Blog.Api.Services;
using OleksiiOnSoftware.Services.Blog.Dto;
using System.Threading.Tasks;

namespace OleksiiOnSoftware.Services.Blog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/blogs/{blogId}/categories")]
    public class CategoriesController : Controller
    {
        private readonly IBlogsService _blogsService;

        public CategoriesController(IBlogsService blogsService)
        {
            _blogsService = blogsService;
        }

        public async Task Post([FromBody] CategoryDto category)
        {
            await _blogsService.CreateCategoryAsync(category);
        }
    }
}
