namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using Common;

    public class ChangeCategoryCommand : Command
    {
        public string Url { get; }
        public string Title { get; }

        public ChangeCategoryCommand(string aggregateId, string url, string title) : base(aggregateId)
        {
            Url = url;
            Title = title;
        }
    }
}
