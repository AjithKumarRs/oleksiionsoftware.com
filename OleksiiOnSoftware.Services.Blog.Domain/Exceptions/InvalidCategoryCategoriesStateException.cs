using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class InvalidCategoryCategoriesStateException : Exception
    {
        public InvalidCategoryCategoriesStateException()
        {
        }

        public InvalidCategoryCategoriesStateException(string message) : base(message)
        {
        }

        public InvalidCategoryCategoriesStateException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
