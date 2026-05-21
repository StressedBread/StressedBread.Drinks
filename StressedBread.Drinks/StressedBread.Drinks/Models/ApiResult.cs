using static StressedBread.Drinks.Enums;

namespace StressedBread.Drinks.Models;
internal class ApiResult<T>
{
    internal bool IsSuccess { get; set; }
    internal T? Data { get; set; } = default;
    internal string ErrorMessage { get; set; } = string.Empty;
    internal ErrorType ErrorType { get; set; } = ErrorType.None;

    internal static ApiResult<T> Success(T data)
    {
        return new ApiResult<T>
        {
            IsSuccess = true,
            Data = data
        };
    }

    internal static ApiResult<T> Failure(string errorMessage, ErrorType error)
    {
        return new ApiResult<T>
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            ErrorType = error
        };
    }
}
