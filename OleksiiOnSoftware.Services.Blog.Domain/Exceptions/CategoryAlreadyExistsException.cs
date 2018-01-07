using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class CategoryAlreadyExistsException : DomainException
    {
        public CategoryAlreadyExistsException()
        {
        }

        public CategoryAlreadyExistsException(string message) : base(message)
        {
        }

        public CategoryAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
