using OleksiiOnSoftware.Services.Blog.Domain.Commands;
using OleksiiOnSoftware.Services.Blog.Domain.Events;
using OleksiiOnSoftware.Services.Blog.Domain.Exceptions;
using OleksiiOnSoftware.Services.Blog.Domain.Tests.Definitions;
using Xunit;

namespace OleksiiOnSoftware.Services.Blog.Domain.Tests.Commands
{
    public class CloseBlogCommandTests
    {
        [Theory]
        [InlineData(Consts.Host)]
        public void Given_DefaultBlog_When_CloseBlogCommand_With_ValidParams_Then_BlogClosedEvent(string host) =>
            Given.DefaultBlog()
                .Begin()
                    .When(new CloseBlogCommand(host))
                    .Then(new BlogClosedEvent(host))
                .End();

        [Theory]
        [InlineData(Consts.Host)]
        public void Given_DefaultBlogWithLinks_When_CloseBlogCommand_With_ValidParams_Then_InvalidLinksStateException(string host) =>
            Given.DefaultBlog().WithLinks()
                .Begin()
                    .When(new CloseBlogCommand(host))
                    .ThenException<InvalidBlogLinksStateException>()
                .End();

        [Theory]
        [InlineData(Consts.Host)]
        public void Given_DefaultBlogWithTags_When_CloseBlogCommand_With_ValidParams_Then_InvalidTagsStateException(string host) =>
            Given.DefaultBlog().WithTags()
                .Begin()
                    .When(new CloseBlogCommand(host))
                    .ThenException<InvalidBlogTagsStateException>()
                .End();

        [Theory]
        [InlineData(Consts.Host)]
        public void Given_DefaultBlogWithCategories_When_CloseBlogCommand_With_ValidParams_Then_InvalidCategoriesStateException(string host) =>
            Given.DefaultBlog().WithCategories()
                .Begin()
                    .When(new CloseBlogCommand(host))
                    .ThenException<InvalidBlogCategoriesStateException>()
                .End();
    }
}
