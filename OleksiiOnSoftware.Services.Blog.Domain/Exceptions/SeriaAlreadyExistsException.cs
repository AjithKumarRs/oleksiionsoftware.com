using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class SeriaAlreadyExistsException : Exception
    {
        public SeriaAlreadyExistsException()
        {
        }

        public SeriaAlreadyExistsException(string message) : base(message)
        {
        }

        public SeriaAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
