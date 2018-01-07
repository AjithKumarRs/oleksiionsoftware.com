using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class InvalidBlogCategoriesStateException : Exception
    {
        public InvalidBlogCategoriesStateException()
        {
        }

        public InvalidBlogCategoriesStateException(string message) : base(message)
        {
        }

        public InvalidBlogCategoriesStateException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
