namespace OleksiiOnSoftware.Services.Blog.Import.Services.Impl
{
    using Microsoft.Extensions.Logging;
    using Model;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public class GitHubContentSource : IContentSource
    {
        private const string ReadmeFile = "readme.md";

        private readonly IBlogMetadataParser _blogMetadataParser;
        private readonly ILogger<GitHubContentSource> _logger;

        public GitHubContentSource(IBlogMetadataParser blogMetadataParser, ILogger<GitHubContentSource> logger)
        {
            _blogMetadataParser = blogMetadataParser;
            _logger = logger;
        }

        public string BaseUrl { get; set; } = "https://github.com/";

        public string RepoUrlTemplate { get; set; } = "https://github.com/{User}/{Repo}.git";

        public string RawBaseUrl { get; set; } = "https://raw.githubusercontent.com/";

        public string RawContentTemplate { get; set; } = "https://raw.githubusercontent.com/{User}/{Repo}/{Branch}/{Path}";

        public string User { get; set; } = "boades";

        public string Repo { get; set; } = "boades-blog-content";

        public string Branch { get; set; } = "master";

        public Blog GetContent()
        {
            if (!Directory.Exists(Repo))
            {
                Clone();
            }

            GetLatestChanges(Repo);

            var metadataFile = Path.Combine(Repo, ReadmeFile).ToLowerInvariant();
            var blog = _blogMetadataParser.GetBlogDto(Path.Combine(Repo, ReadmeFile));

            var postFiles = Directory.GetFiles(Repo, ReadmeFile, SearchOption.AllDirectories)
                .Except(new[] { metadataFile })
                .ToList();

            _logger.LogInformation($"Discovered ${postFiles.Count()} files.");
            foreach (var postFile in postFiles)
            {
                var post = GetPost(postFile);

                var dir = Path.GetDirectoryName(postFile);
                var repl = Repo;
                post.RelativePath = dir.Replace(repl, string.Empty).Trim(Path.PathSeparator);

                if (string.IsNullOrEmpty(post.Category))
                {
                    post.Category = Path.GetDirectoryName(post.RelativePath);
                }

                blog.Posts.Add(post);
            }

            return blog;
        }

        private Post GetPost(string file)
        {
            _logger.LogInformation($"Processing {file}...");
            var text = File.ReadAllText(file);

            var headingStartIndex = text.IndexOf('#');
            var headingEndIndex = text.IndexOfAny(new[] { '\r', '\n' }, headingStartIndex);
            var meta = text.Substring(0, headingStartIndex);
            var title = text.Substring(headingStartIndex, headingEndIndex - headingStartIndex);
            var content = text.Substring(headingEndIndex, text.Length - headingEndIndex);

            var postDto = JsonConvert.DeserializeObject<Post>(
                meta.Replace("<!--", string.Empty)
                    .Replace("-->", string.Empty),
                new IsoDateTimeConverter
                {
                    DateTimeFormat = "dd/MM/yyyy"
                });

            postDto.Title = title.Replace("# ", string.Empty);
            postDto.Content = content;
            return postDto;
        }

        private void Clone()
        {
            var repoUrl = RepoUrlTemplate
               .Replace("{User}", User)
               .Replace("{Repo}", Repo);

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = $"clone -b {Branch} {repoUrl}",
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            var process = Process.Start(processStartInfo);
            var stdErr = process.StandardError.ReadToEnd();
            var stdOut = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            _logger.LogInformation(stdErr);
            _logger.LogInformation(stdOut);
        }

        private void GetLatestChanges(string repoPath)
        {
            var processStartInfo = new ProcessStartInfo
            {
                WorkingDirectory = repoPath,
                FileName = "git",
                Arguments = $"pull origin {Branch}",
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            var process = Process.Start(processStartInfo);
            var stdErr = process.StandardError.ReadToEnd();
            var stdOut = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            _logger.LogInformation(stdErr);
            _logger.LogInformation(stdOut);
        }
    }
}
