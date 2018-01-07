namespace OleksiiOnSoftware.Services.Blog.Import.Services
{
    using Model;

    public interface IContentSource
    {
        Blog GetContent();
    }
}
