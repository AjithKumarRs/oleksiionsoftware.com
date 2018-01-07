using System;

namespace OleksiiOnSoftware.Services.Blog.Domain.Exceptions
{
    public class InvalidPostTitleException : Exception
    {
        public InvalidPostTitleException()
        {
        }

        public InvalidPostTitleException(string message) : base(message)
        {
        }

        public InvalidPostTitleException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
