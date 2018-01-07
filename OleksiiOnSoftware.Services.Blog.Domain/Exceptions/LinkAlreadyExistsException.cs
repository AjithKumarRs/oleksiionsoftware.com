using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class LinkAlreadyExistsException : Exception
    {
        public LinkAlreadyExistsException()
        {
        }

        public LinkAlreadyExistsException(string message) : base(message)
        {
        }

        public LinkAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
