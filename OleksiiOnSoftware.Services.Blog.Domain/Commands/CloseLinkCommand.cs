namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using Common;

    public class CloseLinkCommand : Command
    {
        public string Url { get; }

        public CloseLinkCommand(string aggregateId, string url) : base(aggregateId)
        {
            Url = url;
        }
    }
}
