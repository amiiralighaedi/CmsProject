




namespace Cms.Shared.Domain.Result;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Shared.Domain.Errors.Error Error { get; }

    protected Result(bool isSuccess, Shared.Domain.Errors.Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Shared.Domain.Errors.Error.None);

    public static Result Failure(Shared.Domain.Errors.Error error) => new(false, error);
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool isSuccess, T? value, Shared.Domain.Errors.Error error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) =>
        new(true, value, Shared.Domain.Errors.Error.None);

    public static Result<T> Failure(Shared.Domain.Errors.Error error) =>
        new(false, default, error);
}