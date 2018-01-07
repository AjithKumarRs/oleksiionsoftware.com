namespace OleksiiOnSoftware.Services.Blog.Domain.Commands
{
    using Common;

    public class StartBlogCommand : Command
    {
        public string Brand { get; }
        public string Copyright { get; }
        public string Avatar { get; }
        public string Github { get; }
        public string Linkedin { get; }
        public string Twitter { get; }

        public StartBlogCommand(string aggregateId, string brand, string copyright, string avatar, string github, string linkedin, string twitter) : base(aggregateId)
        {
            Brand = brand;
            Copyright = copyright;
            Avatar = avatar;
            Github = github;
            Linkedin = linkedin;
            Twitter = twitter;
        }
    }
}
