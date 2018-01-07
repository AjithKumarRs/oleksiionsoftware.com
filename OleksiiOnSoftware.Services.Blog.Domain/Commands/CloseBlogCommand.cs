namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using Common;

    public class CloseBlogCommand : Command
    {
        public CloseBlogCommand(string aggregateId) : base(aggregateId)
        {
        }
    }
}
