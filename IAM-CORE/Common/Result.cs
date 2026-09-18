using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM_CORE.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string ErrorCode { get; }
        public string ErrorMessage { get; }

        protected Result(bool isSuccess, string errorCode, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public static Result Success() => new(true, string.Empty, string.Empty);

        public static Result Failure(string errorCode, string errorMessage) =>
            new(false, errorCode, errorMessage);
        public static Result<T> Failure<T>(string errorCode, string errorMessage) => new(default, false, errorCode, errorMessage);

        // Sukces dla typu generycznego
        public static Result<T> Success<T>(T value) => new(value, true, string.Empty, string.Empty);

    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        internal Result(T? value, bool isSuccess, string errorCode, string errorMessage)
            : base(isSuccess, errorCode, errorMessage)
        {
            _value = value;
        }

        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Nie można odczytać wartości z nieudanego rezultatu.");
    }

}
