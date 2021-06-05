using FluentValidation.Results;

namespace Patterns.Mediator.ConsoleApp.Core
{
    public class Result<TData>
    {
        public TData Data { get; set; }

        public string ErrorCode { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public bool Status => string.IsNullOrWhiteSpace(ErrorCode);

        private static Result<TData> Failure(string errorCode, params ValidationFailure[] failures)
        {
            return Failure(errorCode, new ValidationResult(failures));
        }

        public static Result<TData> Failure(string errorCode)
        {
            return Failure(errorCode, new ValidationFailure(errorCode, errorCode));
        }

        public static Result<TData> Failure(string errorCode, string errorMessage)
        {
            return Failure(errorCode, new ValidationFailure("", errorMessage)
            {
                ErrorCode = errorCode
            });
        }

        public static Result<TData> Failure(string errorCode, ValidationResult validationResult)
        {
            return new()
            {
                ErrorCode = errorCode,
                ValidationResult = validationResult
            };
        }

        public static Result<TData> Success(TData data)
        {
            return new()
            {
                Data = data
            };
        }
    }
}