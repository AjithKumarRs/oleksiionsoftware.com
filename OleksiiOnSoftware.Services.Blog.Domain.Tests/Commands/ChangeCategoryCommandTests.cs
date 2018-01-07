using OleksiiOnSoftware.Services.Blog.Domain.Commands;
using OleksiiOnSoftware.Services.Blog.Domain.Events;
using OleksiiOnSoftware.Services.Blog.Domain.Tests.Definitions;
using Xunit;

namespace OleksiiOnSoftware.Services.Blog.Domain.Tests.Commands
{
    public class ChangeCategoryCommandTests
    {
        [Theory]
        [InlineData(Consts.Host, Consts.CategoryUrl, "New Title")]
        public void Given_DefaultBlogWithCategories_WhenChangeCategoryCommand_WithValidParams_ThenCategoryChangedEvent(string host, string url, string title) =>
            Given.DefaultBlog().WithCategories()
                .Begin()
                    .When(new ChangeCategoryCommand(host, url, title))
                    .Then(new CategoryChangedEvent(host, url, title))
                .End();
    }
}
