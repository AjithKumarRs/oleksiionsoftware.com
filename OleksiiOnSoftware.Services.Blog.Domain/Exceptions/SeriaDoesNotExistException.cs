using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class SeriaDoesNotExistException : Exception
    {
        public SeriaDoesNotExistException()
        {
        }

        public SeriaDoesNotExistException(string message) : base(message)
        {
        }

        public SeriaDoesNotExistException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
