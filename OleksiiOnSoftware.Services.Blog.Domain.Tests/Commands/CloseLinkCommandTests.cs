using OleksiiOnSoftware.Services.Blog.Domain.Commands;
using OleksiiOnSoftware.Services.Blog.Domain.Events;
using OleksiiOnSoftware.Services.Blog.Domain.Exceptions;
using OleksiiOnSoftware.Services.Blog.Domain.Tests.Definitions;
using Xunit;

namespace OleksiiOnSoftware.Services.Blog.Domain.Tests.Commands
{
    public class CloseLinkCommandTests
    {
        [Theory]
        [InlineData(Consts.Host, Consts.LinkUrl)]
        public void Given_DefaultBlogWithLinks_When_CloseLinkCommand_With_ValidParams_Then_LinkClosedEvent(string aggregateId, string url) =>
            Given.DefaultBlog().WithLinks()    
                .Begin()
                    .When(new CloseLinkCommand(aggregateId, url))
                    .Then(new LinkClosedEvent(aggregateId, url))
                .End();

        [Theory]
        [InlineData(Consts.Host, "http://dummy")]
        public void Given_DefaultBlog_When_CloseLinkCommand_With_InvalidUrl_Then_LinkDoesNotExistException(string aggregateId, string url) =>
            Given.DefaultBlog()
                .Begin()
                    .When(new CloseLinkCommand(aggregateId, url))
                    .ThenException<LinkDoesNotExistException>()
                .End();
    }
}
