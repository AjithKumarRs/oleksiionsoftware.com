using System;
using System.Collections.Generic;
using OleksiiOnSoftware.Services.Blog.Domain.Commands;
using OleksiiOnSoftware.Services.Blog.Domain.Events;
using OleksiiOnSoftware.Services.Blog.Domain.Exceptions;
using OleksiiOnSoftware.Services.Blog.Domain.Tests.Definitions;
using Xunit;

namespace OleksiiOnSoftware.Services.Blog.Domain.Tests.Commands
{
    public class StartPostCommandTests
    {
        [Theory]
        [InlineData(Consts.Host, Consts.PostUrl, Consts.PostTitle, Consts.PostBody, Consts.PostPublishAt, Consts.CategoryUrl, Consts.CategoryTitle, Consts.TagUrl, Consts.TagTitle, true, false, true)]
        public void Given_BlogWithCategoriesWithTags_When_StartPostCommand_With_ValidParams_Then_PostStartedEvent(string host, string url, string title, string body, string publishAt, string categoryUrl, string categoryTitle, string tagUrl, string tagTitle, bool inforbar, bool hidden, bool comments)
        {
            var command = new StartPostCommand(host, url, title, body, DateTime.Parse(publishAt), categoryUrl, new HashSet<string> { tagUrl }, inforbar, hidden, comments);
            var expectedEvent = new PostStartedEvent(host, url, title, body, DateTime.Parse(publishAt), categoryTitle, categoryUrl, new List<PostStartedEvent.PostStartedEventTag> { new PostStartedEvent.PostStartedEventTag(tagUrl, tagTitle) }, inforbar, hidden, comments);

            Given.DefaultBlog().WithCategories().WithTags()
                .Begin()
                    .When(command)
                    .Then(expectedEvent)
                .End();
        }

        [Theory]
        [InlineData(Consts.Host, Consts.PostUrl, Consts.PostTitle, Consts.PostBody, Consts.PostPublishAt, Consts.CategoryUrl, Consts.CategoryTitle, Consts.TagUrl, Consts.TagTitle, true, false, true)]
        public void Given_BlogWithCategoriesWithTags_When_StartPostCommand_With_ValidParamsTwice_Then_PostAlreadyExistsException(string host, string url, string title, string body, string publishAt, string categoryUrl, string categoryTitle, string tagUrl, string tagTitle, bool inforbar, bool hidden, bool comments)
        {
            var command = new StartPostCommand(host, url, title, body, DateTime.Parse(publishAt), categoryUrl, new HashSet<string> { tagUrl }, inforbar, hidden, comments);
            var expectedEvent = new PostStartedEvent(host, url, title, body, DateTime.Parse(publishAt), categoryTitle, categoryUrl, new List<PostStartedEvent.PostStartedEventTag> { new PostStartedEvent.PostStartedEventTag(tagUrl, tagTitle) }, inforbar, hidden, comments);

            Given.DefaultBlog().WithCategories().WithTags()
                .Begin()
                    .When(command)
                    .Then(expectedEvent)
                .End()
                .Begin()
                    .When(command)
                    .ThenException<PostAlreadyExistsException>()
                .End();
        }

        [Theory]
        [InlineData(Consts.Host, "", Consts.PostTitle, Consts.PostBody, Consts.PostPublishAt, Consts.CategoryUrl, Consts.CategoryTitle, Consts.TagUrl, Consts.TagTitle, true, false, true)]
        [InlineData(Consts.Host, null, Consts.PostTitle, Consts.PostBody, Consts.PostPublishAt, Consts.CategoryUrl, Consts.CategoryTitle, Consts.TagUrl, Consts.TagTitle, true, false, true)]
        public void Given_BlogWithCategoriesWithTags_When_StartPostCommand_With_InvalidUrl_Then_InvalidPostUrlException(string host, string url, string title, string body, string publishAt, string categoryUrl, string categoryTitle, string tagUrl, string tagTitle, bool infobar, bool hidden, bool comments)
        {
            var command = new StartPostCommand(host, url, title, body, DateTime.Parse(publishAt), categoryUrl, new HashSet<string> { tagUrl }, infobar, hidden, comments);
            
            Given.DefaultBlog().WithCategories().WithTags()
                .Begin()
                    .When(command)
                    .ThenException<InvalidPostUrlException>()
                .End();
        }

        [Theory]
        [InlineData(Consts.Host, Consts.PostUrl, "", Consts.PostBody, Consts.PostPublishAt, Consts.CategoryUrl, Consts.CategoryTitle, Consts.TagUrl, Consts.TagTitle, true, false, true)]
        [InlineData(Consts.Host, Consts.PostUrl, null, Consts.PostBody, Consts.PostPublishAt, Consts.CategoryUrl, Consts.CategoryTitle, Consts.TagUrl, Consts.TagTitle, true, false, true)]
        public void Given_BlogWithCategoriesWithTags_When_StartPostCommand_With_InvalidPostTitle_Then_InvalidPostTitleException(string host, string url, string title, string body, string publishAt, string categoryUrl, string categoryTitle, string tagUrl, string tagTitle, bool infobar, bool hidden, bool comments)
        {
            var command = new StartPostCommand(host, url, title, body, DateTime.Parse(publishAt), categoryUrl, new HashSet<string> { tagUrl }, infobar, hidden, comments);

            Given.DefaultBlog().WithCategories().WithTags()
                .Begin()
                    .When(command)
                    .ThenException<InvalidPostTitleException>()
                .End();
        }

        [Theory]
        [InlineData(Consts.Host, Consts.PostUrl, Consts.PostTitle, Consts.PostBody, Consts.PostPublishAt, "dummy", Consts.CategoryTitle, Consts.TagUrl, Consts.TagTitle, true, false, true)]
        [InlineData(Consts.Host, Consts.PostUrl, Consts.PostTitle, Consts.PostBody, Consts.PostPublishAt, "category1/dummy", Consts.CategoryTitle, Consts.TagUrl, Consts.TagTitle, true, false, true)]
        public void Given_BlogWithCategoriesWithTags_When_StartPostCommand_With_InvalidCategoryUrl_Then_CategoryDoesNotExistException(string host, string url, string title, string body, string publishAt, string categoryUrl, string categoryTitle, string tagUrl, string tagTitle, bool infobar, bool hidden, bool comments)
        {
            var command = new StartPostCommand(host, url, title, body, DateTime.Parse(publishAt), categoryUrl, new HashSet<string> { tagUrl }, infobar, hidden, comments);

            Given.DefaultBlog().WithCategories().WithTags()
                .Begin()
                    .When(command)
                    .ThenException<CategoryDoesNotExistException>()
                .End();
        }
    }
}
