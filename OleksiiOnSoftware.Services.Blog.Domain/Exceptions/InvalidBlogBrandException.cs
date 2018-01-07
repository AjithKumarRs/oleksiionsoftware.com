using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class InvalidBlogBrandException : DomainException
    {
        public InvalidBlogBrandException() { }
        public InvalidBlogBrandException(string message) : base(message) { }
        public InvalidBlogBrandException(string message, Exception inner) : base(message, inner) { }
    }
}
