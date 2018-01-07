namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using Common;

    public class CloseCategoryCommand : Command
    {
        public string Url { get; }

        public CloseCategoryCommand(string aggregateId, string url) : base(aggregateId)
        {
            Url = url;
        }
    }
}
