namespace CsharpAdvanced.PipelinerStateful;

public sealed class StatefulPipeline
{
    private readonly List<IPipelineMiddleware> _pipelineMiddlewares = [];

    private StatefulPipeline()
    {
    }

    public static StatefulPipeline StartFrom<TInput>(TInput input)
    {
        return Initializate(new PipelineMiddlewareWithOutput<TInput>(input));
    }

    public static StatefulPipeline StartFrom<TOutput>(Func<TOutput> func)
    {
        return Initializate(new PipelineMiddlewareWithOutput<TOutput>(func));
    }

    public static StatefulPipeline StartFrom(Action func)
    {
        var initialMiddleware = new PipelineMiddleware(func);
        return Initializate(initialMiddleware);
    }

    public StatefulPipeline ContinueWith<TInput>(Action<TInput> func)
    {
        var lastMiddlewareWithMatchingInput = GetLastMiddlewareWithOutputType<TInput>();
        return Continue(new PipelineMiddlewareWithInput<TInput>(func, lastMiddlewareWithMatchingInput));
    }

    public StatefulPipeline ContinueWith<TInput, TOutput>(Func<TInput, TOutput> func)
    {
        var lastMiddlewareWithMatchingInput = GetLastMiddlewareWithOutputType<TInput>();
        return Continue(new PipelineMiddlewareWithInputAndOutput<TInput, TOutput>(func, lastMiddlewareWithMatchingInput));
    }

    public StatefulPipeline ContinueWith<TOutput>(Func<TOutput> func)
    {
        return Continue(new PipelineMiddlewareWithOutput<TOutput>(func));
    }

    public StatefulPipeline ContinueWith(Action func)
    {
        return Continue(new PipelineMiddleware(func));
    }

    public void End()
    {
        foreach (var middleware in _pipelineMiddlewares)
        {
            middleware.Execute();
        }
    }

    public TOutput EndWith<TOutput>()
    {
        End();
        return GetLastOutputOfType<TOutput>();
    }

    public TOutput EndWith<TInput, TOutput>(Func<TInput, TOutput> mapping)
    {
        End();
        return mapping(GetLastOutputOfType<TInput>());
    }

    private static StatefulPipeline Initializate(IPipelineMiddleware pipelineMiddleware)
    {
        var newPipeline = new StatefulPipeline();
        newPipeline._pipelineMiddlewares.Add(pipelineMiddleware);
        return newPipeline;
    }

    private StatefulPipeline Continue(IPipelineMiddleware nextMiddleware, Action? func = null)
    {
        if (func is not null)
        {
            func();
        }

        _pipelineMiddlewares.Add(nextMiddleware);

        return this;
    }

    private HasOutput<TOutput> GetLastMiddlewareWithOutputType<TOutput>()
    {
        return ((HasOutput<TOutput>)_pipelineMiddlewares
            .Where(x => x is HasOutput)
            .Last());
    }

    private TOutput GetLastOutputOfType<TOutput>()
    {
        return ((HasOutput<TOutput>)_pipelineMiddlewares
            .Where(x => x is HasOutput<TOutput>)
            .Last())
            .Output!;
    }
}