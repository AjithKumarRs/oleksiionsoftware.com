using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class PostDoesNotExistException : Exception
    {
        public PostDoesNotExistException()
        {
        }

        public PostDoesNotExistException(string message) : base(message)
        {
        }

        public PostDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
