namespace OleksiiOnSoftware.Services.Blog.Query.Utils
{
    using Markdig;
    using System;

    public static class TextUtils
    {
        public static string GetHtmlFromMarkdown(string markdown)
        {
            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseBootstrap()
                .Build();

            var html = Markdown.ToHtml(markdown, pipeline);
            return html;
        }

        public static string GetBodyShort(string body)
        {
            var startIndex = body.IndexOf("####", StringComparison.OrdinalIgnoreCase);
            if (startIndex == -1)
            {
                return body;
            }

            startIndex = body.IndexOf('\n', startIndex);

            var endIndex = body.IndexOf("####", startIndex + 1, StringComparison.OrdinalIgnoreCase);
            var result = body.Substring(startIndex, endIndex - startIndex);
            return result;
        }
    }
}
