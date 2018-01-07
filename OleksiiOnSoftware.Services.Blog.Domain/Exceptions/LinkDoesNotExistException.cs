using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class LinkDoesNotExistException : Exception
    {
        public LinkDoesNotExistException()
        {
        }

        public LinkDoesNotExistException(string message) : base(message)
        {
        }

        public LinkDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
