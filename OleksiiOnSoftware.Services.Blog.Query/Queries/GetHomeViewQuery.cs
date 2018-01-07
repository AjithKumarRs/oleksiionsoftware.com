namespace OleksiiOnSoftware.Services.Blog.Query.Queries
{
    using Common;
    using OleksiiOnSoftware.Services.Blog.Query.Model;
    using OleksiiOnSoftware.Services.Blog.Query.Utils;
    using OleksiiOnSoftware.Services.Blog.Query.Views;
    using StackExchange.Redis;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using static System.Math;

    public class GetHomeViewQuery : IAsyncQuery<HomeView>
    {
        private readonly IDatabase _db;
        private string _blogId;
        private int _pageIndex;
        private int _pageSize;

        private string _filterByDate;
        private string _filterByCategory;
        private string _filterByTag;

        public GetHomeViewQuery(IDatabase db)
        {
            _db = db;
        }

        public GetHomeViewQuery SetBlogId(string blogId)
        {
            _blogId = blogId;
            return this;
        }

        public GetHomeViewQuery SetPageIndex(int pageIndex)
        {
            _pageIndex = pageIndex;
            return this;
        }

        public GetHomeViewQuery SetPageSize(int pageSize)
        {
            _pageSize = pageSize;
            return this;
        }

        public GetHomeViewQuery SetFilterByDate(string date)
        {
            _filterByDate = date;
            return this;
        }

        public GetHomeViewQuery SetFilterByCategory(string categoryId)
        {
            _filterByCategory = categoryId;
            return this;
        }

        public GetHomeViewQuery SetFilterByTag(string tagId)
        {
            _filterByTag = tagId;
            return this;
        }

        public async Task<HomeView> ExecuteAsync()
        {
            var key = KeyUtils.GetStateKey(_blogId);

            var filter = new FilterHomeView { By = "none" };

            var state = await _db.GetObjectAsync<BlogState>(key);
            var query = state.Posts.Where(_ => !_.IsHidden);

            if (!string.IsNullOrEmpty(_filterByDate))
            {
                var date = DateTime.Parse(_filterByDate);
                query = query.Where(_ => _.PublishAt == date);
                filter = new FilterHomeView { By = "date", Title = _filterByDate };
            }

            if (!string.IsNullOrEmpty(_filterByCategory))
            {
                query = query.Where(_ => _.CategoryUrl == _filterByCategory);

                var category = state.Categories.FirstOrDefault(_ => _.Url == _filterByCategory);
                filter = new FilterHomeView { By = "category", Title = category.Title };
            }

            if (!string.IsNullOrEmpty(_filterByTag))
            {
                query = query.Where(_ => _.Tags.Any(t => t.TagUrl == _filterByTag));

                var tag = state.Tags.FirstOrDefault(_ => _.TagUrl == _filterByTag);
                filter = new FilterHomeView { By = "tag", Title = tag.TagTitle };
            }

            var posts = query
                .OrderByDescending(_ => _.PublishAt)
                .ToList();

            var links = state.Links.Select(_ => new LinkHomeView
            {
                Id = _.Id,
                Title = _.Title,
                Order = _.Order
            });

            var result = new HomeView
            {
                Id = _blogId,
                Avatar = state.Avatar,
                Brand = state.Brand,
                Copyright = state.Copyright,
                Github = state.Github,
                Linkedin = state.Linkedin,
                Twitter = state.Twitter,
                Links = links.ToList(),
                Filter = filter,
                Posts = posts
                    .Skip(_pageIndex * _pageSize)
                    .Take(_pageSize)
                    .Select(_ => new PostHomeView
                    {
                        Id = _.Url,
                        Url = _.Url,
                        Title = _.Title,
                        Short = _.BodyShort,
                        Date = _.PublishAt.ToString("dd-MM-yyyy"),
                        Category = new CategoryHomeView { Id = _.CategoryUrl, Title = _.CategoryTitle },
                        Tags = _.Tags.Select(tag => new TagHomeView { Id = tag.TagUrl, Title = tag.TagTitle }).ToList()
                    })
                    .ToList(),
                PageIndex = _pageIndex,
                PageSize = _pageSize,
                PagesCount = (int)Ceiling(posts.Count / (double)_pageSize)
            };

            return result;
        }
    }
}
