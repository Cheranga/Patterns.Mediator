using System;
using MediatR;

namespace Patterns.Mediator.ConsoleApp.Messages
{
    public class NotificationHandlerException: Exception
    {
        public NotificationHandlerException(INotification notification, string errorCode, string message) : base(message)
        {
            Notification = notification;
            ErrorCode = errorCode;
        }

        public INotification Notification { get; }

        public string ErrorCode { get; }
    }
}