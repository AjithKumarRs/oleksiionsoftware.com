using OleksiiOnSoftware.Services.Blog.Domain.Commands;
using OleksiiOnSoftware.Services.Blog.Domain.Events;
using OleksiiOnSoftware.Services.Blog.Domain.Exceptions;
using OleksiiOnSoftware.Services.Blog.Domain.Tests.Definitions;
using Xunit;

namespace OleksiiOnSoftware.Services.Blog.Domain.Tests.Commands
{
    public class StartLinkCommandTests
    {
        [Theory]
        [InlineData(Consts.Host, Consts.LinkUrl, "external link", 0)]
        public void Given_DefaultBlog_When_StartLinkCommand_With_ValidParams_Then_LinkStartedEvent(string host, string url, string title, int order) =>
            Given.DefaultBlog()
                .Begin()
                    .When(new StartLinkCommand(host, url, title, order))
                    .Then(new LinkStartedEvent(host, url, title, order))
                .End();

        [Theory]
        [InlineData(Consts.Host, Consts.LinkUrl, "external link", 0)]
        public void Given_DefaultBlog_When_StartLinkCommand_WithValidParams_ExecuteTwice_Then_LinkAlreadyExistsException(string host, string url, string title, int order) =>
            Given.DefaultBlog()
                .Begin()
                    .When(new StartLinkCommand(host, url, title, order))
                    .Then(new LinkStartedEvent(host, url, title, order))
                .End()
                .Begin()
                    .When(new StartLinkCommand(host, url, title, order))
                    .ThenException<LinkAlreadyExistsException>()
                .End();
    }
}
