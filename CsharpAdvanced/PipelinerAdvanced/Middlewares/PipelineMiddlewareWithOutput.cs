using CsharpAdvanced.PipelinerStateful.Interfaces;

namespace CsharpAdvanced.PipelinerStateful.Middlewares;

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
