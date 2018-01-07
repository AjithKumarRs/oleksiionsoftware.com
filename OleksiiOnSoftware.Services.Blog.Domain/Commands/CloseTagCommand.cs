namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using Common;

    public class CloseTagCommand : Command
    {
        public string Url { get; }

        public CloseTagCommand(string aggregateId, string url) : base(aggregateId)
        {
            Url = url;
        }
    }
}
