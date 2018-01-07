namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using Common;

    public class StartTagCommand : Command
    {
        public string Url { get; }
        public string Title { get; }

        public StartTagCommand(string aggregateId, string url, string title) : base(aggregateId)
        {
            Url = url;
            Title = title;
        }
    }
}
