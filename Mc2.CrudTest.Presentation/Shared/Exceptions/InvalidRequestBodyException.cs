using System;

namespace Mc2.CrudTest.Shared.Exceptions
{
    public class InvalidRequestBodyException : Exception
    {
        public InvalidRequestBodyException() { }

        public InvalidRequestBodyException(string[] errors) { Errors = errors; }

        public InvalidRequestBodyException(string message)
            : base(message) { }

        public InvalidRequestBodyException(string message, Exception innerException)
            : base(message, innerException) { }

        public string[] Errors { get; set; }
    }
}