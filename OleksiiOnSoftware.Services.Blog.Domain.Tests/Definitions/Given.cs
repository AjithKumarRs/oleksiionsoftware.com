using OleksiiOnSoftware.Services.Blog.Domain.Commands;
using OleksiiOnSoftware.Services.Blog.Domain.Events;

namespace OleksiiOnSoftware.Services.Blog.Domain.Tests.Definitions
{
    public static class Given
    {
        public static BlogStory NewBlog() => new BlogStory();

        public static BlogStory DefaultBlog() => NewBlog()
            .Begin()
                .When(new StartBlogCommand(Consts.Host, Consts.Brand, Consts.Copyright, Consts.Avatar, Consts.Github, Consts.Linkedin, Consts.Twitter))
                .Then(new BlogStartedEvent(Consts.Host, Consts.Brand, Consts.Copyright, Consts.Avatar, Consts.Github, Consts.Linkedin, Consts.Twitter))
            .End();

        public static BlogStory WithLinks(this BlogStory story) => story
            .Begin()
                .When(new StartLinkCommand(Consts.Host, Consts.LinkUrl, Consts.LinkTitle, 0))
                .Then(new LinkStartedEvent(Consts.Host, Consts.LinkUrl, Consts.LinkTitle, 0))
            .End();

        public static BlogStory WithTags(this BlogStory story) => story
            .Begin()
                .When(new StartTagCommand(Consts.Host, Consts.TagUrl, Consts.TagTitle))
                .Then(new TagStartedEvent(Consts.Host, Consts.TagUrl, Consts.TagTitle))
            .End();

        public static BlogStory WithCategories(this BlogStory story) => story
            .Begin()
                .When(new StartCategoryCommand(Consts.Host, Consts.CategoryUrl, Consts.CategoryTitle))
                .Then(new CategoryStartedEvent(Consts.Host, Consts.CategoryUrl, Consts.CategoryTitle))
            .End();
    }
}
