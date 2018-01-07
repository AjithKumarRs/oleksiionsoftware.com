namespace OleksiiOnSoftware.Services.Blog.Import.Services.Impl.ContentTransformers
{
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using Microsoft.Extensions.Logging;
    using Model;

    public class TransformRelativeToAbsoluteLinksContentTransformer : IContentTransformer
    {
        private const string LinkRegExp = @"!\[.*?\]\((.*?)\)";
        private readonly ILogger<TransformRelativeToAbsoluteLinksContentTransformer> _logger;

        public TransformRelativeToAbsoluteLinksContentTransformer(ILogger<TransformRelativeToAbsoluteLinksContentTransformer> logger)
        {
            _logger = logger;
        }

        public string RawContentTemplate { get; set; } = "https://raw.githubusercontent.com/{User}/{Repo}/{Branch}/{Path}";

        public string User { get; set; } = "boades";

        public string Repo { get; set; } = "boades-blog-content";

        public string Branch { get; set; } = "master";

        public Blog Transform(Blog blog)
        {
            blog.Avatar = TransformLink(string.Empty, blog.Avatar);
            foreach (var post in blog.Posts)
            {
                post.Content = TransformLinks(post.RelativePath, post.Content);
            }

            return blog;
        }

        private string TransformLinks(string path, string body)
        {
            var matches = new Regex(LinkRegExp).Matches(body);
            if (matches.Count == 0)
            {
                return body;
            }

            var buffer = new StringBuilder(body);
            foreach (Match match in matches)
            {
                var token = match.Groups[0].Value;
                var relativeUrl = match.Groups[1].Value;

                if (IsAbsoluteUrl(relativeUrl))
                {
                    _logger.LogInformation($"Skiping transformation of {relativeUrl}, it is already absolute");
                }

                _logger.LogInformation($"Transforming relative {relativeUrl} to absolute url");

                var absolutelUrl = TransformLink(path, relativeUrl);
                var newFull = token.Replace(relativeUrl, absolutelUrl);
                buffer = buffer.Replace(token, newFull);
            }

            return buffer.ToString();
        }

        private string TransformLink(string path, string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
            {
                return string.Empty;
            }

            var absoluteUrl = RawContentTemplate
                     .Replace("{User}", User)
                     .Replace("{Repo}", Repo)
                     .Replace("{Branch}", Branch)
                     .Replace("{Path}", path);

            absoluteUrl = Path.Combine(absoluteUrl, relativeUrl).Replace("\\", "/");
            return absoluteUrl;
        }

        private bool IsAbsoluteUrl(string url)
        {
            return url.Contains("http://") || url.Contains("https://");
        }
    }
}
