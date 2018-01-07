namespace OleksiiOnSoftware.Services.Blog.Query.Queries
{
    using OleksiiOnSoftware.Services.Blog.Query.Model;
    using OleksiiOnSoftware.Services.Blog.Query.Utils;
    using OleksiiOnSoftware.Services.Blog.Query.Views;
    using OleksiiOnSoftware.Services.Common;
    using StackExchange.Redis;
    using System.Linq;
    using System.Threading.Tasks;

    public class GetPostViewQuery : IAsyncQuery<PostView>
    {
        private readonly IDatabase _db;

        private string _blogId;
        private string _postId;

        public GetPostViewQuery(IDatabase db)
        {
            _db = db;
        }

        public GetPostViewQuery SetBlogId(string blogId)
        {
            _blogId = blogId;
            return this;
        }

        public GetPostViewQuery SetPostId(string postId)
        {
            _postId = postId;
            return this;
        }

        public async Task<PostView> ExecuteAsync()
        {
            var key = KeyUtils.GetStateKey(_blogId);

            var state = await _db.GetObjectAsync<BlogState>(key);

            var post = state.Posts.FirstOrDefault(_ => _.Url == _postId);

            var links = state.Links.Select(_ => new LinkHomeView
            {
                Id = _.Id,
                Title = _.Title,
                Order = _.Order
            });

            var result = new PostView
            {
                Id = _blogId,
                Url = _postId,
                Avatar = state.Avatar,
                Brand = state.Brand,
                Copyright = state.Copyright,
                Github = state.Github,
                Linkedin = state.Linkedin,
                Twitter = state.Twitter,
                Title = post.Title,
                Comments = post.Comments,
                Infobar = post.Infobar,
                Links = links.ToList(),
                Body = post.Body,
                Date = post.PublishAt.ToShortDateString(),
                Category = new CategoryHomeView { Id = post.CategoryUrl, Title = post.CategoryTitle },
                Tags = post.Tags.Select(tag => new TagHomeView { Id = tag.TagUrl, Title = tag.TagTitle }).ToList()
            };

            return result;
        }
    }
}
