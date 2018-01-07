using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class TagAlreadyExistsException : Exception
    {
        public TagAlreadyExistsException()
        {
        }

        public TagAlreadyExistsException(string message) : base(message)
        {
        }

        public TagAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
