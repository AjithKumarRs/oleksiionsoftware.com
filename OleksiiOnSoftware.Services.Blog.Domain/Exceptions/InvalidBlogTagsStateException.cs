using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{ 
    public class InvalidBlogTagsStateException : Exception
    {
        public InvalidBlogTagsStateException()
        {
        }

        public InvalidBlogTagsStateException(string message) : base(message)
        {
        }

        public InvalidBlogTagsStateException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
