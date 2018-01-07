namespace OleksiiOnSoftware.Services.Blog.Query.Queries
{
    using OleksiiOnSoftware.Services.Blog.Query.Model;
    using OleksiiOnSoftware.Services.Blog.Query.Utils;
    using OleksiiOnSoftware.Services.Common;
    using StackExchange.Redis;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GetBlogListQuery : IAsyncQuery<List<string>>
    {
        private readonly IDatabase _db;

        public GetBlogListQuery(IDatabase db)
        {
            _db = db;
        }

        public async Task<List<string>> ExecuteAsync()
        {
            var key = KeyUtils.GetBlogListKey();

            var state = await _db.GetObjectAsync<BlogListState>(key);
            return state.Blogs.ToList();
        }
    }
}
