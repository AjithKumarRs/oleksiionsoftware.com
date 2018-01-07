namespace OleksiiOnSoftware.Services.Blog.Query
{
    using OleksiiOnSoftware.Services.Blog.Query.Model;

    public static class KeyUtils
    {
        public static string GetStateKey(string host) => $"{host}:{nameof(BlogState)}";

        public static string GetBlogListKey() => nameof(BlogListState);
    }
}