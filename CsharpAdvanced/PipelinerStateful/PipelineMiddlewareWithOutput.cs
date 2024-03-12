namespace CsharpAdvanced.PipelinerStateful;

internal interface HasOutput
{
}

internal interface HasOutput<TOutput> : HasOutput
{
    public TOutput? Output { get; set; }
}

internal interface HasInput
{
}

internal interface HasInput<TInput> : HasInput
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

internal class PipelineMiddlewareWithInput<TInput>(Action<TInput> action, HasOutput<TInput> hasOutput) 
    : IPipelineMiddleware, HasInput<TInput>
{
    private readonly HasOutput<TInput> _hasOutput = hasOutput;
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

internal class PipelineMiddlewareWithOutput<TOutput> : IPipelineMiddleware, HasOutput<TOutput>
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

internal sealed class PipelineMiddlewareWithInputAndOutput<TInput, TOutput>(Func<TInput, TOutput> func, HasOutput<TInput> hasOutput) 
    : IPipelineMiddleware, HasInput<TInput>, HasOutput<TOutput>
{
    private readonly Func<TInput, TOutput> _func = func;
    private readonly HasOutput<TInput> _hasOutput = hasOutput;

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