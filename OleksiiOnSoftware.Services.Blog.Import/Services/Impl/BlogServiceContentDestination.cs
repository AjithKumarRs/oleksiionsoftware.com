namespace OleksiiOnSoftware.Services.Blog.Import.Services.Impl
{
    using Client;
    using Dto;
    using Microsoft.Extensions.Logging;
    using Model;

    public class BlogServiceContentDestination : IContentDestination
    {
        private readonly ILogger<BlogServiceContentDestination> _logger;
        private readonly IBlogClient _blogClient;

        public BlogServiceContentDestination(ILogger<BlogServiceContentDestination> logger, IBlogClient blogClient)
        {
            _logger = logger;
            _blogClient = blogClient;
        }

        public void SetContent(Blog blog)
        {
            _logger.LogInformation($"Starting blog {blog.Title} on {blog.Id}");

            _blogClient.CreateBlog(new BlogDto
            {
                Id = blog.Id,
                Brand = blog.Title,
                Twitter = blog.Twitter,
                Avatar = blog.Avatar,
                Copyright = blog.Copyright,
                Github = blog.Github,
                Linkedin = blog.Linkedin
            }).Wait();

            foreach (var link in blog.Links)
            {
                _logger.LogInformation($"Adding link {link.Title}:{link.Url}");
                _blogClient.CreateLink(new LinkDto
                {
                    BlogId = blog.Id,
                    Url = link.Url,
                    Title = link.Title,
                    Order = link.Order
                }).Wait();
            }

            foreach (var category in blog.Categories)
            {
                _logger.LogInformation($"Adding category {category.Title}:{category.Url}");
                _blogClient.CreateCategory(new CategoryDto
                {
                    BlogId = blog.Id,
                    Title = category.Title,
                    Description = category.Description,
                    Url = category.Url
                }).Wait();
            }

            foreach (var tag in blog.Tags)
            {
                _logger.LogInformation($"Adding tag {tag.Title}:{tag.Url}");
                _blogClient.CreateTag(new TagDto
                {
                    BlogId = blog.Id,
                    Url = tag.Url,
                    Title = tag.Title,
                    Description = tag.Description
                }).Wait();
            }

            foreach (var post in blog.Posts)
            {
                _logger.LogInformation($"Adding post {post.Title}:{post.Url}");
                _blogClient.CreatePost(new PostDto
                {
                    BlogId = blog.Id,
                    Title = post.Title,
                    Url = post.Url,
                    Category = post.Category,
                    Comments = post.Comments,
                    Content = post.Content,
                    Hidden = post.Hidden,
                    Infobar = post.Infobar,
                    Publish = post.Publish,
                    Tags = post.Tags
                }).Wait();
            }

            _logger.LogInformation("Finished!");
        }
    }
}
