namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using Common;

    public class StartLinkCommand : Command
    {
        public string Url { get; }
        public string Title { get; }
        public int Order { get; }

        public StartLinkCommand(string aggregateId, string url, string title, int order) : base(aggregateId)
        {
            Url = url;
            Title = title;
            Order = order;
        }
    }
}
