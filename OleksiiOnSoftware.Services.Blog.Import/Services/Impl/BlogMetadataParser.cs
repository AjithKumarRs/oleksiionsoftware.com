namespace OleksiiOnSoftware.Services.Blog.Import.Services.Impl
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    public class BlogMetadataParser : IBlogMetadataParser
    {
        private const string LinkRegEx = @"\[(.*?)\]\((.*?)\)";

        public Blog GetBlogDto(string fileName)
        {
            var content = File.ReadAllLines(fileName);
            var dto = new Blog();
            dto.Copyright = "Oleksii Udovychenko";

            for (var i = 0; i < content.Length; i++)
            {
                var line = content[i].Trim();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.Equals("#### Heading", StringComparison.CurrentCultureIgnoreCase))
                {
                    do
                    {
                        line = content[++i].Trim();
                        var matches = new Regex(LinkRegEx).Matches(line);
                        if (matches.Count == 0)
                        {
                            continue;
                        }

                        dto.Title = matches[0].Groups[1].Value;
                        dto.Id = matches[0].Groups[2].Value
                            .Replace("http://", string.Empty)
                            .Replace("https://", string.Empty);

                    } while (!line.StartsWith("####"));
                }

                if (line.Equals("#### Navigation", StringComparison.CurrentCultureIgnoreCase))
                {
                    var order = 0;
                    do
                    {
                        line = content[++i].Trim();
                        var matches = new Regex(LinkRegEx).Matches(line);
                        if (matches.Count == 0)
                        {
                            continue;
                        }

                        var link = new Link();
                        link.Title = matches[0].Groups[1].Value;
                        link.Url = matches[0].Groups[2].Value;
                        link.Order = order++;
                        dto.Links.Add(link);

                    } while (!line.StartsWith("####"));
                }

                if (line.Equals("#### Categories", StringComparison.CurrentCultureIgnoreCase))
                {
                    do
                    {
                        line = content[++i].Trim();
                        var matches = new Regex(LinkRegEx).Matches(line);
                        if (matches.Count == 0)
                        {
                            continue;
                        }

                        var categoryDto = new Category
                        {
                            Title = matches[0].Groups[1].Value,
                            Url = matches[0].Groups[2].Value
                        };

                        dto.Categories.Add(categoryDto);
                    } while (!line.StartsWith("####"));
                }

                if (line.Equals("#### Tags", StringComparison.CurrentCultureIgnoreCase))
                {
                    do
                    {
                        line = content[++i].Trim();
                        var matches = new Regex(LinkRegEx).Matches(line);
                        if (matches.Count == 0)
                        {
                            continue;
                        }

                        if (line.StartsWith("*"))
                        {
                            var tagDto = new Tag
                            {
                                Title = matches[0].Groups[1].Value,
                                Url = matches[0].Groups[2].Value
                            };

                            dto.Tags.Add(tagDto);
                        }

                    } while (!line.StartsWith("####"));
                }

                if (line.Equals("#### Links", StringComparison.CurrentCultureIgnoreCase))
                {
                    var links = new Dictionary<string, string>();
                    do
                    {
                        line = content[++i].Trim();
                        var matches = new Regex(LinkRegEx).Matches(line);
                        if (matches.Count == 0)
                        {
                            continue;
                        }

                        if (line.StartsWith("*"))
                        {
                            links.Add(matches[0].Groups[1].Value, matches[0].Groups[2].Value);
                        }

                    } while (!line.StartsWith("####") && i < content.Length - 1);

                    dto.Github = links["GitHub"];
                    dto.Linkedin = links["LinkedIn"];
                    dto.Twitter = links["Twitter"];
                }
            }

            return dto;
        }
    }
}
