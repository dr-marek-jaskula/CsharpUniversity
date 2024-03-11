namespace CsharpAdvanced.Pipeliner;

public record PipelineResult
{
    public static readonly PipelineResult Instance = new();

    public PipelineResult<TOutput> ContinueWith<TOutput>(Func<TOutput> func)
    {
        return func();
    }

    public PipelineResult ContinueWith(Action func)
    {
        func();
        return Instance;
    }

    public override string ToString()
    {
        return string.Empty;
    }
}

public sealed record PipelineResult<TInput>(TInput Input) : PipelineResult
{
    public static implicit operator PipelineResult<TInput>(TInput value)
    {
        return new PipelineResult<TInput>(value);
    }

    public PipelineResult<TOutput> ContinueWith<TOutput>(Func<TInput, TOutput> func)
    {
        return func(Input);
    }

    public PipelineResult ContinueWith(Action<TInput> func)
    {
        func(Input);
        return Instance;
    }

    public override string ToString()
    {
        return Input!.ToString()!;
    }
}