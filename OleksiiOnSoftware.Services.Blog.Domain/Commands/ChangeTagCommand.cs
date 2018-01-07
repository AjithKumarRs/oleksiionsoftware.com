namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using Common;

    public class ChangeTagCommand : Command
    {
        public string Url { get; }
        public string Title { get; }

        public ChangeTagCommand(string aggregateId, string url, string title) : base(aggregateId)
        {
            Url = url;
            Title = title;
        }
    }
}
