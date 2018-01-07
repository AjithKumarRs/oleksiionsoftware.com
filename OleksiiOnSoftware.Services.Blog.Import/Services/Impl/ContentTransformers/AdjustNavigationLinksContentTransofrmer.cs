namespace OleksiiOnSoftware.Services.Blog.Import.Services.Impl.ContentTransformers
{
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using Model;
    using Utils;

    public class AdjustNavigationLinksContentTransofrmer : IContentTransformer
    {
        private readonly ILogger<AdjustNavigationLinksContentTransofrmer> _logger;

        public AdjustNavigationLinksContentTransofrmer(ILogger<AdjustNavigationLinksContentTransofrmer> logger)
        {
            _logger = logger;
        }

        public string CategoryPrefix { get; set; } = "/category/";

        public Blog Transform(Blog blogDto)
        {
            _logger.LogInformation("Transforming links...");
            foreach (var blogDtoLink in blogDto.Links)
            {
                var linkedCategory = blogDto.Categories.FirstOrDefault(_ => _.Url == blogDtoLink.Url);
                if (linkedCategory != null)
                {
                    blogDtoLink.Url = CategoryPrefix + LinkUtils.MakeFriendly(blogDtoLink.Url);
                    continue;
                }

                if (string.IsNullOrEmpty(blogDtoLink.Url))
                {
                    blogDtoLink.Url = "/";
                    continue;
                }
            }

            _logger.LogInformation("Links transformed.");

            return blogDto;
        }
    }
}
