using OleksiiOnSoftware.Services.Blog.Domain.Commands;
using OleksiiOnSoftware.Services.Blog.Domain.Events;
using OleksiiOnSoftware.Services.Blog.Domain.Exceptions;
using OleksiiOnSoftware.Services.Blog.Domain.Tests.Definitions;
using Xunit;

namespace OleksiiOnSoftware.Services.Blog.Domain.Tests.Commands
{
    public class CloseTagCommandTests
    {
        [Theory]
        [InlineData(Consts.Host, Consts.TagUrl)]
        public void Given_DefaultBlogWithTags_When_CloseTagCommand_WithValidParams_Then_TagClosedEvent(string aggregateId, string url) =>
            Given.DefaultBlog().WithTags()
                .Begin()
                    .When(new CloseTagCommand(aggregateId, url))
                    .Then(new TagClosedEvent(aggregateId, url))
                .End();

        [Theory]
        [InlineData(Consts.Host, "invalid url")]
        public void Given_DefaultBlogWithTags_When_CloseTagCommand_WithInvalidUrl_Then_TagDoesNotExistException(string aggregateId, string url) => 
            Given.DefaultBlog().WithTags()
                .Begin()
                    .When(new CloseTagCommand(aggregateId, url))
                    .ThenException<TagDoesNotExistException>()
                .End();
    }
}
