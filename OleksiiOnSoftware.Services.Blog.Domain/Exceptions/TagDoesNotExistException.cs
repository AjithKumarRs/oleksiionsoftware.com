using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class TagDoesNotExistException : Exception
    {
        public TagDoesNotExistException()
        {
        }

        public TagDoesNotExistException(string message) : base(message)
        {
        }

        public TagDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
