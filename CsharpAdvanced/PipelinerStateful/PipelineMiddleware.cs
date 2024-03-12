using Microsoft.AspNetCore.SignalR.Protocol;

namespace CsharpAdvanced.PipelinerStateful;

internal interface HasResult
{
}

internal interface HasResult<TOutput> : HasResult
{
    public TOutput Result { get; set; }
}

internal class PipelineMiddleware
{
    public void Execute(Action func)
    {
        func();
    }

    public void Execute<TInput>(TInput input, Action<TInput> func)
    {
        func(input);
    }
}

internal class PipelineMiddleware<TOutput> : PipelineMiddleware, HasResult<TOutput>
{
    public PipelineMiddleware(TOutput result)
    {
        Result = result;
    }

    public PipelineMiddleware(Func<TOutput> func)
    {
        Result = func();
    }

    public TOutput Result { get; set; }

    public override string ToString()
    {
        return Result!.ToString()!;
    }
}

internal sealed class PipelineMiddleware<TInput, TOutput> : PipelineMiddleware, HasResult<TOutput>
{
    public PipelineMiddleware(TOutput result)
    {
        Result = result;
    }

    public PipelineMiddleware(TInput input, Func<TInput, TOutput> func)
    {
        Result = func(input);
    }

    public TOutput Result { get; set; }

    public override string ToString()
    {
        return Result!.ToString()!;
    }
}