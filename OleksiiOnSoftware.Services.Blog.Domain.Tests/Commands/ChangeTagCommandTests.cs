using OleksiiOnSoftware.Services.Blog.Domain.Commands;
using OleksiiOnSoftware.Services.Blog.Domain.Events;
using OleksiiOnSoftware.Services.Blog.Domain.Exceptions;
using OleksiiOnSoftware.Services.Blog.Domain.Tests.Definitions;
using Xunit;

namespace OleksiiOnSoftware.Services.Blog.Domain.Tests.Commands
{
    public class ChangeTagCommandTests
    {
        [Theory]
        [InlineData(Consts.Host, Consts.TagUrl, "new title")]
        public void Given_DefaultBlogWithTags_When_ChangeTagCommand_WithValidParams_Then_BlogChangedEvent(string aggregateId, string url, string title) =>
            Given.DefaultBlog().WithTags()
                .Begin()
                    .When(new ChangeTagCommand(aggregateId, url, title))
                    .Then(new TagChangedEvent(aggregateId, url, title))
                .End();

        [Theory]
        [InlineData(Consts.Host, "dummy", "new title")]
        public void Given_DefaultBlogWithTags_When_ChangeTagCommand_WithInvalidUrl_Then_TagDoesNotExistException(string aggregateId, string url, string title) =>
            Given.DefaultBlog().WithTags()
                .Begin()
                    .When(new ChangeTagCommand(aggregateId, url, title))
                    .ThenException<TagDoesNotExistException>()
                .End();
    }
}
