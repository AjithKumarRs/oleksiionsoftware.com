using OleksiiOnSoftware.Services.Blog.Domain.Commands;
using OleksiiOnSoftware.Services.Blog.Domain.Events;
using OleksiiOnSoftware.Services.Blog.Domain.Exceptions;
using OleksiiOnSoftware.Services.Blog.Domain.Tests.Definitions;
using Xunit;

namespace OleksiiOnSoftware.Services.Blog.Domain.Tests.Commands
{
    public class StartCategoryCommandTests
    {
        [Theory]
        [InlineData(Consts.Host, Consts.CategoryUrl, Consts.CategoryTitle)]
        public void Given_DefaultBlog_When_StartCategoryCommand_WithValidParams_Then_CategoryStartedEvent(string aggregateId, string url, string title) =>
            Given.DefaultBlog()
                .Begin()
                    .When(new StartCategoryCommand(aggregateId, url, title))
                    .Then(new CategoryStartedEvent(aggregateId, url, title))
                .End();


        [Theory]
        [InlineData(Consts.Host, Consts.CategoryUrl, Consts.CategoryTitle)]
        public void Given_DefaultBlogWithCategories_When_StartCategory_Command_WithInvalidParams_Then_CategoryAlreadyExistsException(string aggregateId, string url, string title) =>
            Given.DefaultBlog().WithCategories()
                .Begin()
                    .When(new StartCategoryCommand(aggregateId, url, title))
                    .ThenException<CategoryAlreadyExistsException>()
                .End();

        [Theory]
        [InlineData(Consts.Host, Consts.CategoryUrl + "/sub-category", "Sub Category")]
        public void Given_DefaultBlogWithCategories_When_StartCategoryCommand_WithValidParams_Then_CategoryStartedEvent(string aggregateId, string url, string title) =>
            Given.DefaultBlog().WithCategories()
                .Begin()
                    .When(new StartCategoryCommand(aggregateId, url, title))
                    .Then(new CategoryStartedEvent(aggregateId, url, title))
                .End();

        [Theory]
        [InlineData(Consts.Host, Consts.CategoryUrl + "/sub-category", "Sub Category")]
        public void Given_DefaultBlogWithCategories_When_StartCategoryCommand_TwiceForSubCategory_Then_CategoryAlreadyExistsException(string aggregateId, string url, string title) =>
            Given.DefaultBlog().WithCategories()
                .Begin()
                    .When(new StartCategoryCommand(aggregateId, url, title))
                    .Then(new CategoryStartedEvent(aggregateId, url, title))
                .End()
                .Begin()
                    .When(new StartCategoryCommand(aggregateId, url, title))
                    .ThenException<CategoryAlreadyExistsException>()
                .End();
    }
}
