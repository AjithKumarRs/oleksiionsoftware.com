using OleksiiOnSoftware.Services.Blog.Domain.Commands;
using OleksiiOnSoftware.Services.Blog.Domain.Events;
using OleksiiOnSoftware.Services.Blog.Domain.Exceptions;
using OleksiiOnSoftware.Services.Blog.Domain.Tests.Definitions;
using Xunit;

namespace OleksiiOnSoftware.Services.Blog.Domain.Tests.Commands
{
    public class ChangeBlogCommandTests
    {
        [Theory]
        [InlineData("host", "brand", "copyright", "avatar", "github", "linkedin", "twitter")]
        [InlineData("host", "brand", "copyright", "", "", "", "")]
        [InlineData("host", "brand", "copyright", null, null, null, null)]
        public void Given_DefaultBlog_When_ChangeBlogCommand_With_ValidParams_Then_BlogStartedEvent(string aggregateId, string brand, string copyright, string avatar, string github, string linkedin, string twitter) =>
            Given.DefaultBlog()
                .Begin()
                    .When(new ChangeBlogCommand(aggregateId, brand, copyright, avatar, github, linkedin, twitter))
                    .Then(new BlogChangedEvent(aggregateId, brand, copyright, avatar, github, linkedin, twitter))
                .End();


        [Theory]
        [InlineData("", "brand", "copyright", "avatar", "github", "linkedin", "twitter")]
        [InlineData(null, "brand", "copyright", "avatar", "github", "linkedin", "twitter")]
        public void Given_DefaultBlog_When_ChangeBlogCommand_With_InvalidHost_Then_InvalidHostException(string aggregateId, string brand, string copyright, string avatar, string github, string linkedin, string twitter) =>
            Given.DefaultBlog()
                .Begin()
                    .When(new ChangeBlogCommand(aggregateId, brand, copyright, avatar, github, linkedin, twitter))
                    .ThenException<InvalidBlogHostException>()
                .End();

        [Theory]
        [InlineData("host", "", "copyright", "avatar", "github", "linkedin", "twitter")]
        [InlineData("host", null, "copyright", "avatar", "github", "linkedin", "twitter")]
        public void Given_DefaultBlog_When_ChangeBlogCommand_With_InvalidBrand_Then_InvalidBrandException(string aggregateId, string brand, string copyright, string avatar, string github, string linkedin, string twitter) =>
            Given.DefaultBlog()
                .Begin()
                   .When(new ChangeBlogCommand(aggregateId, brand, copyright, avatar, github, linkedin, twitter))
                   .ThenException<InvalidBlogBrandException>()
                .End();

        [Theory]
        [InlineData("host", "brand", "", "avatar", "github", "linkedin", "twitter")]
        [InlineData("host", "brand", null, "avatar", "github", "linkedin", "twitter")]
        public void Given_DefaultBlog_When_ChangeBlogCommand_With_InvalidCopyright_Then_InvalidCopyrightException(string aggregateId, string brand, string copyright, string avatar, string github, string linkedin, string twitter) =>
            Given.DefaultBlog()
                .Begin()
                    .When(new ChangeBlogCommand(aggregateId, brand, copyright, avatar, github, linkedin, twitter))
                    .ThenException<InvalidBlogCopyrightException>()
                .End();
    }
}
