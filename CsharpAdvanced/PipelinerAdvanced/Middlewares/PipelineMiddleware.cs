using CsharpAdvanced.PipelinerStateful.Interfaces;

namespace CsharpAdvanced.PipelinerStateful.Middlewares;

internal class PipelineMiddleware(Action action) : IPipelineMiddleware
{
    private readonly Action _action = action;

    public Task ExecuteAsync()
    {
        _action();
        return Task.CompletedTask;
    }
}
