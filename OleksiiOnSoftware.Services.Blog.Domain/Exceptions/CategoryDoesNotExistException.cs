using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class CategoryDoesNotExistException : DomainException
    {
        public CategoryDoesNotExistException()
        {
        }

        public CategoryDoesNotExistException(string message) : base(message)
        {
        }

        public CategoryDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
