namespace OleksiiOnSoftware.Services.Blog.Import.Services
{
    using Model;

    public interface IContentDestination
    {
        void SetContent(Blog blogDto);
    }
}
