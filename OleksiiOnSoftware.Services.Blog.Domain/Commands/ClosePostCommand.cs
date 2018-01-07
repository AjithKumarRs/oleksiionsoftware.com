namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using Common;

    public class ClosePostCommand : Command
    {
        public string Url { get; }

        public ClosePostCommand(string aggregateId, string url) : base(aggregateId)
        {
            Url = url;
        }
    }
}
