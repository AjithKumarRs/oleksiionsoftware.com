namespace OleksiiOnSoftware.Services.Blog.Import.Services
{
    using Model;

    public interface IContentTransformer
    {
        Blog Transform(Blog blogDto);
    }
}
