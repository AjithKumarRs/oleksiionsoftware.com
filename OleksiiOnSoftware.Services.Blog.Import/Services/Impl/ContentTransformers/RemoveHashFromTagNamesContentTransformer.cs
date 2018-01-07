namespace OleksiiOnSoftware.Services.Blog.Import.Services.Impl.ContentTransformers
{
    using Microsoft.Extensions.Logging;
    using Model;

    public class RemoveHashFromTagNamesContentTransformer : IContentTransformer
    {
        private readonly ILogger<RemoveHashFromTagNamesContentTransformer> _logger;

        public RemoveHashFromTagNamesContentTransformer(ILogger<RemoveHashFromTagNamesContentTransformer> logger)
        {
            _logger = logger;
        }

        public Blog Transform(Blog blog)
        {
            _logger.LogInformation("Transforming tags, removing hashes from tag internal names...");
            foreach (var blogTag in blog.Tags)
            {
                blogTag.Url = blogTag.Url.Replace("#", "");
            }

            _logger.LogInformation("Tags transformed");
            return blog;
        }
    }
}
