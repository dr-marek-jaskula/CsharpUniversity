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
    void Execute();
}

internal class PipelineMiddleware(Action action) : IPipelineMiddleware
{
    private readonly Action _action = action;

    public void Execute()
    {
        _action();
    }
}

internal class PipelineMiddlewareWithInput<TInput>(Action<TInput> action, IHasOutput<TInput> hasOutput) 
    : IPipelineMiddleware, IHasInput<TInput>
{
    private readonly IHasOutput<TInput> _hasOutput = hasOutput;
    private readonly Action<TInput> _action = action;

    public TInput? Input { get; set; }

    public void Execute()
    {
        Input = _hasOutput.Output!;
        _action(Input);
    }

    public override string ToString()
    {
        return $"Input: {Input}";
    }
}

internal class PipelineMiddlewareWithOutput<TOutput> : IPipelineMiddleware, IHasOutput<TOutput>
{
    private readonly TOutput? _output;
    private readonly Func<TOutput>? _func;

    public PipelineMiddlewareWithOutput(TOutput output)
    {
        _output = output;
    }

    public PipelineMiddlewareWithOutput(Func<TOutput> func)
    {
        _func = func;
    }

    public TOutput? Output { get; set; }

    public void Execute()
    {
        if (_output is not null)
        {
            Output = _output;
            return;
        }

        Output = _func!();
    }

    public override string ToString()
    {
        return $"Output: {Output}";
    }
}

internal sealed class PipelineMiddlewareWithInputAndOutput<TInput, TOutput>(Func<TInput, TOutput> func, IHasOutput<TInput> hasOutput) 
    : IPipelineMiddleware, IHasInput<TInput>, IHasOutput<TOutput>
{
    private readonly Func<TInput, TOutput> _func = func;
    private readonly IHasOutput<TInput> _hasOutput = hasOutput;

    public TInput? Input { get; set; }
    public TOutput? Output { get; set; }

    public void Execute()
    {
        Input = _hasOutput.Output!;
        Output = _func(Input);
    }

    public override string ToString()
    {
        return $"Input: {Input}. Output: {Output}";
    }
}