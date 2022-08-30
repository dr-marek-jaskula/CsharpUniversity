namespace CustomTools.DomainDrivenDesign.NoThrowByResult;

public class Result
{
    public bool IsSuccess { get; }

    public Error Error { get; }

    public bool IsFailure => !IsSuccess;

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    //protected internal used due to the domain reasons
    protected internal Result(bool isSucess, Error error)
    {
        if (isSucess && error != Error.None)
            throw new InvalidOperationException();

        if (!isSucess && error == Error.None)
            throw new InvalidOperationException();

        IsSuccess = isSucess;
        Error = error;
    }

    protected static Result<TValue> Create<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, Error.None);
    }
}

