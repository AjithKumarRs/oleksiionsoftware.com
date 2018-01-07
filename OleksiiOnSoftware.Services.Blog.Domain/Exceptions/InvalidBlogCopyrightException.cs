using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class InvalidBlogCopyrightException : DomainException
    {
        public InvalidBlogCopyrightException() { }
        public InvalidBlogCopyrightException(string message) : base(message) { }
        public InvalidBlogCopyrightException(string message, Exception inner) : base(message, inner) { }
    }
}
