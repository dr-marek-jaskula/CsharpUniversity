namespace CsharpAdvanced.PipelinerStateful;

internal interface IHasOutput
{
}

internal interface IHasOutput<TOutput> : IHasOutput
{
    public TOutput? Output { get; set; }
}

internal interface IHasInput
{
}

internal interface IHasInput<TInput> : IHasInput
{
    public TInput? Input { get; set; }
}

internal interface IPipelineMiddleware
{
    Task ExecuteAsync();
}

internal class PipelineMiddleware(Action action) : IPipelineMiddleware
{
    private readonly Action _action = action;

    public Task ExecuteAsync()
    {
        _action();
        return Task.CompletedTask;
    }
}

internal class PipelineMiddlewareWithInput<TInput>(Action<TInput> action, IHasOutput<TInput> hasOutput) 
    : IPipelineMiddleware, IHasInput<TInput>
{
    private readonly IHasOutput<TInput> _hasOutput = hasOutput;
    private readonly Action<TInput> _action = action;

    public TInput? Input { get; set; }

    public Task ExecuteAsync()
    {
        Input = _hasOutput.Output!;
        _action(Input);
        return Task.CompletedTask;
    }

    public override string ToString()
    {
        return $"Input: '{Input}'";
    }
}

internal class PipelineMiddlewareWithOutput<TOutput> : IPipelineMiddleware, IHasOutput<TOutput>
{
    private readonly TOutput? _output;
    private readonly Func<TOutput>? _func;
    private readonly Func<Task<TOutput>>? _asyncFunc;

    public PipelineMiddlewareWithOutput(TOutput output)
    {
        _output = output;
    }

    public PipelineMiddlewareWithOutput(Func<TOutput> func)
    {
        _func = func;
    }

    public PipelineMiddlewareWithOutput(Func<Task<TOutput>> asyncFunc)
    {
        _asyncFunc = asyncFunc;
    }

    public TOutput? Output { get; set; }

    public async Task ExecuteAsync()
    {
        if (_output is not null)
        {
            Output = _output;
            return;
        }

        if (_func is not null)
        {
            Output = _func!();
            return;
        }

        Output = await _asyncFunc!();
    }

    public override string ToString()
    {
        return $"Output: '{Output}'";
    }
}

internal sealed class PipelineMiddlewareWithInputAndOutput<TInput, TOutput>
    : IPipelineMiddleware, IHasInput<TInput>, IHasOutput<TOutput>
{
    private readonly Func<TInput, TOutput>? _func;
    private readonly Func<TInput, Task<TOutput>>? _asyncFunc;
    private readonly IHasOutput<TInput> _hasOutput;

    public TInput? Input { get; set; }
    public TOutput? Output { get; set; }

    public PipelineMiddlewareWithInputAndOutput(Func<TInput, TOutput> func, IHasOutput<TInput> hasOutput)
    {
        _func = func;
        _hasOutput = hasOutput;
    }

    public PipelineMiddlewareWithInputAndOutput(Func<TInput, Task<TOutput>>? asyncFunc, IHasOutput<TInput> hasOutput)
    {
        _asyncFunc = asyncFunc;
        _hasOutput = hasOutput;
    }

    public async Task ExecuteAsync()
    {
        Input = _hasOutput.Output!;

        if (_func is not null)
        {
            Output = _func!(Input);
            return;
        }

        Output = await _asyncFunc!(Input);
    }

    public override string ToString()
    {
        return $"Input: '{Input}'. Output: '{Output}'";
    }
}