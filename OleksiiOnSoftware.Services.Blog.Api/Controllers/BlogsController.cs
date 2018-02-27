namespace OleksiiOnSoftware.Services.Blog.Api.Controllers
{
    using Dto;
    using Microsoft.AspNetCore.Mvc;
    using OleksiiOnSoftware.Services.Blog.Query.Views;
    using Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BlogsController : Controller
    {
        private readonly IBlogsService _blogsService;

        public BlogsController(IBlogsService blogsService)
        {
            _blogsService = blogsService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return await _blogsService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<HomeView> Get(string id, 
            string filterByDate = null, 
            string filterByCategory = null,
            string filterByTag = null,
            int pageIndex = 0, 
            int pageSize = 100)
        {
            return await _blogsService.GetHomeViewAsync(id, 
                filterByDate, 
                filterByCategory, 
                filterByTag, 
                pageIndex, 
                pageSize);
        }

        [HttpPost]
        public async Task Post([FromBody] BlogDto blog)
        {
            await _blogsService.CreateBlogAsync(blog);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _blogsService.DeleteAsync(id);
        }
    }
}
