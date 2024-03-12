using CsharpAdvanced.PipelinerStateful.Interfaces;

namespace CsharpAdvanced.PipelinerStateful.Middlewares;

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
