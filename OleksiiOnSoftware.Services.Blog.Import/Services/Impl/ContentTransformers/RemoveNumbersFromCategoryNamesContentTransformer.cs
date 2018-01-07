namespace OleksiiOnSoftware.Services.Blog.Import.Services.Impl.ContentTransformers
{
    using Microsoft.Extensions.Logging;
    using Model;
    using Utils;

    public class RemoveNumbersFromCategoryNamesContentTransformer : IContentTransformer
    {
        private readonly ILogger<RemoveNumbersFromCategoryNamesContentTransformer> _logger;

        public RemoveNumbersFromCategoryNamesContentTransformer(ILogger<RemoveNumbersFromCategoryNamesContentTransformer> logger)
        {
            _logger = logger;
        }

        public Blog Transform(Blog blog)
        {
            _logger.LogInformation("Removing numbers from category urls...");
            foreach (var blogCategory in blog.Categories)
            {
                _logger.LogInformation($"Processing {blogCategory.Url}...");

                var newUrl = LinkUtils.MakeFriendly(blogCategory.Url);
                blogCategory.Url = newUrl;

                _logger.LogInformation($"Processed: {newUrl}");
            }

            foreach (var blogPost in blog.Posts)
            {
                _logger.LogInformation($"Processing {blogPost.Url}...");

                var newUrl = LinkUtils.MakeFriendly(blogPost.Category);
                blogPost.Category = newUrl;

                _logger.LogInformation($"Processed: {newUrl}");
            }

            _logger.LogInformation("Numbers have been removed from category urls.");
            return blog;
        }
    }
}

