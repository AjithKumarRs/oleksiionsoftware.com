namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    public class ChangeBlogCommand : StartBlogCommand
    {
        public ChangeBlogCommand(string aggregateId, string brand, string copyright, string avatar, string github, string linkedin, string twitter) : base(aggregateId, brand, copyright, avatar, github, linkedin, twitter)
        {
        }
    }
}
