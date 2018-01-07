namespace OleksiiOnSoftware.Services.Blog.Domain.Tests.Definitions
{
    using OleksiiOnSoftware.Services.Blog.Domain.Model;
    using OleksiiOnSoftware.Services.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class BlogStory
    {
        private readonly Blog _blog;

        public BlogStory()
        {
            _blog = new Blog();
        }

        private BlogStory(Blog blog)
        {
            _blog = blog;
        }

        public BlogStoryBeginContinuation Begin()
        {
            return new BlogStoryBeginContinuation(_blog);
        }

        public class BlogStoryBeginContinuation
        {
            private readonly Blog _blog;

            public BlogStoryBeginContinuation(Blog blog)
            {
                _blog = blog;
            }

            public BlogStoryWhenContinuation When(Command cmd)
            {
                return new BlogStoryWhenContinuation(_blog, () => ((dynamic)_blog).Handle((dynamic)cmd));
            }
        }

        public class BlogStoryWhenContinuation
        {
            private readonly Blog _blog;
            private readonly Func<IEnumerable<Event>> _func;

            public BlogStoryWhenContinuation(Blog blog, Func<IEnumerable<Event>> func)
            {
                _blog = blog;
                _func = func;
            }

            public BlogStoryThenContinuation Then(params Event[] events)
            {
                return new BlogStoryThenContinuation(_blog, () =>
                {
                    var result = _func().ToList();
                    result.ForEach(_blog.ApplyEvent);
                    Assert.Equal(result, events);
                });
            }

            public BlogStoryThenContinuation ThenException<TException>() where TException : Exception
            {
                return new BlogStoryThenContinuation(_blog, () =>
                {
                    Assert.Throws<TException>(() => _func().ToList());
                });
            }
        }

        public class BlogStoryThenContinuation
        {
            private readonly Blog _blog;
            private readonly Action _action;

            public BlogStoryThenContinuation(Blog blog, Action action)
            {
                _blog = blog;
                _action = action;
            }

            public BlogStory End()
            {
                _action();
                return new BlogStory(_blog);
            }
        }
    }
}
