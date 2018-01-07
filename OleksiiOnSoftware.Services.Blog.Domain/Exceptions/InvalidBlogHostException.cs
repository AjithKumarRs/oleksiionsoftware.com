using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class InvalidBlogHostException : DomainException
    {
        public InvalidBlogHostException() { }
        public InvalidBlogHostException(string message) : base(message) { }
        public InvalidBlogHostException(string message, Exception inner) : base(message, inner) { }
    }
}