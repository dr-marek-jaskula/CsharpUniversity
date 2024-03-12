using CsharpAdvanced.PipelinerStateful.Interfaces;

namespace CsharpAdvanced.PipelinerStateful.Middlewares;

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