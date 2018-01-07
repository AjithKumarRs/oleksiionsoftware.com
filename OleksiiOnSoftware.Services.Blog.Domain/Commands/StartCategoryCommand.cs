namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using Common;

    public class StartCategoryCommand : Command
    {
        public string Url { get; }
        public string Title { get; }

        public StartCategoryCommand(string aggregateId, string url, string title) : base(aggregateId)
        {
            Url = url;
            Title = title;
        }
    }
}
