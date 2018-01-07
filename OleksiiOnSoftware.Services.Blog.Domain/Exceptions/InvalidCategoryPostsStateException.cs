using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class InvalidCategoryPostsStateException : Exception
    {
        public InvalidCategoryPostsStateException()
        {
        }

        public InvalidCategoryPostsStateException(string message) : base(message)
        {
        }

        public InvalidCategoryPostsStateException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
