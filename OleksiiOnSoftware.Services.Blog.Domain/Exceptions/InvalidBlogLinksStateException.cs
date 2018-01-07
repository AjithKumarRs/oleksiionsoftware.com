using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class InvalidBlogLinksStateException : Exception
    {
        public InvalidBlogLinksStateException()
        {
        }

        public InvalidBlogLinksStateException(string message) : base(message)
        {
        }

        public InvalidBlogLinksStateException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
