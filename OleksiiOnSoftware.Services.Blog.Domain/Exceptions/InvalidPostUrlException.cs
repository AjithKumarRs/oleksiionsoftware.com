using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class InvalidPostUrlException : Exception
    {
        public InvalidPostUrlException()
        {
        }

        public InvalidPostUrlException(string message) : base(message)
        {
        }

        public InvalidPostUrlException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
