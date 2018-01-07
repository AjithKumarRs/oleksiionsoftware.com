namespace OleksiiOnSoftware.Services.Blog.Import.Services
{
    using Model;

    public interface IBlogMetadataParser
    {
        Blog GetBlogDto(string fileName);
    }
}
