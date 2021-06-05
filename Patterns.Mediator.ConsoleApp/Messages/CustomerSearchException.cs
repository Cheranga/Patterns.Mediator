using System;

namespace Patterns.Mediator.ConsoleApp.Messages
{
    public class CustomerSearchException : Exception
    {
        public string ErrorCode { get; }

        public CustomerSearchException(string errorCode, string errorMessage): base(errorMessage)
        {
            ErrorCode = errorCode;
        }
    }
}