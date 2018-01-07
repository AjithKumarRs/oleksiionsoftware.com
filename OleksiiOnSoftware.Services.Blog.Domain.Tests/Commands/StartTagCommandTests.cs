using OleksiiOnSoftware.Services.Blog.Domain.Commands;
using OleksiiOnSoftware.Services.Blog.Domain.Events;
using OleksiiOnSoftware.Services.Blog.Domain.Exceptions;
using OleksiiOnSoftware.Services.Blog.Domain.Tests.Definitions;
using Xunit;

namespace OleksiiOnSoftware.Services.Blog.Domain.Tests.Commands
{
    public class StartTagCommandTests
    {
        [Theory]
        [InlineData(Consts.Host, Consts.TagUrl, Consts.TagTitle)]
        public void Given_DefaultBlog_When_StartTagCommand_With_ValidParams_Then_BlogStartedEvent(string aggregateId, string url, string title) =>
            Given.DefaultBlog()
                .Begin()
                    .When(new StartTagCommand(aggregateId, url, title))
                    .Then(new TagStartedEvent(aggregateId, url, title))
                .End();

        [Theory]
        [InlineData(Consts.Host, Consts.TagUrl, Consts.TagTitle)]
        public void Given_DefaultBlogWithTags_When_StartTagCommand_With_ValidParams_Then_TagAlreadyExistsException(string aggregateId, string url, string title) =>
            Given.DefaultBlog().WithTags()
                .Begin()
                    .When(new StartTagCommand(aggregateId, url, title))
                    .ThenException<TagAlreadyExistsException>()
                .End();
    }
}
