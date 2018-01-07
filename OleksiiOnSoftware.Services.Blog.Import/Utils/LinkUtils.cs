namespace OleksiiOnSoftware.Services.Blog.Import.Utils
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class LinkUtils
    {
        /// <summary>
        /// Match patterns like 00-not-set, 55-custom-category-name
        /// </summary>
        private static string LinkRegExp = @"[0-9]*-(.*)";

        public static string MakeFriendly(string url)
        {
            var segments = url.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var segment in segments)
            {
                var matches = new Regex(LinkRegExp).Matches(segment);
                if (matches.Count == 0)
                {
                    return null;
                }

                sb.AppendFormat("{0}_", matches[0].Groups[1].Value);
            }

            var newUrl = sb.ToString().Trim('_');
            return newUrl;
        }
    }
}
