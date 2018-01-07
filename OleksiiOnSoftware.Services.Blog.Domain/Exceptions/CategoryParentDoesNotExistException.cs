using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class CategoryParentDoesNotExistException : DomainException
    {
        public CategoryParentDoesNotExistException()
        {
        }

        public CategoryParentDoesNotExistException(string message) : base(message)
        {
        }

        public CategoryParentDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
