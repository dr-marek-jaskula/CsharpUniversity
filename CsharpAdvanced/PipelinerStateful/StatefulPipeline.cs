namespace CsharpAdvanced.PipelinerStateful;

public sealed class StatefulPipeline
{
    private int? _pipelineStopIndex;
    private object? _fallback;
    private IPipelineMiddleware _currentPipelineMiddleware;
    private readonly List<IPipelineMiddleware> _pipelineMiddlewares = [];

    private StatefulPipeline(IPipelineMiddleware pipelineMiddleware)
    {
        _pipelineMiddlewares.Add(pipelineMiddleware);
        _currentPipelineMiddleware = pipelineMiddleware;
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
        return Initializate(new PipelineMiddleware(func));
    }

    public StatefulPipeline ContinueWith<TInput>(Action<TInput> func)
    {
        var lastMiddlewareWithMatchingInput = GetLastMiddleware<TInput>();
        return Continue(new PipelineMiddlewareWithInput<TInput>(func, lastMiddlewareWithMatchingInput));
    }

    public StatefulPipeline ContinueWith<TInput, TOutput>(Func<TInput, TOutput> func)
    {
        var lastMiddlewareWithMatchingInput = GetLastMiddleware<TInput>();
        return Continue(new PipelineMiddlewareWithInputAndOutput<TInput, TOutput>(func, lastMiddlewareWithMatchingInput));
    }
    
    public StatefulPipeline ContinueWith<TInput, TOutput>(Func<TInput, Task<TOutput>> func)
    {
        var lastMiddlewareWithMatchingInput = GetLastMiddleware<TInput>();
        return Continue(new PipelineMiddlewareWithInputAndOutput<TInput, TOutput>(func, lastMiddlewareWithMatchingInput));
    }

    public StatefulPipeline ContinueWith<TOutput>(Func<TOutput> func)
    {
        return Continue(new PipelineMiddlewareWithOutput<TOutput>(func));
    }

    public StatefulPipeline ContinueWith<TOutput>(Func<Task<TOutput>> func)
    {
        return Continue(new PipelineMiddlewareWithOutput<TOutput>(func));
    }

    public StatefulPipeline ContinueWith(Action func)
    {
        return Continue(new PipelineMiddleware(func));
    }

    public StatefulPipeline EndIf<TFallback>(bool condition, TFallback? fallback = default)
    {
        if (condition)
        {
            SetEndConfiguration(fallback);
        }

        return this;
    }

    public StatefulPipeline EndIf<TFallback>(Func<bool> predicate, TFallback? fallback = default)
    {
        if (predicate())
        {
            SetEndConfiguration(fallback);
        }

        return this;
    }

    public async Task<StatefulPipeline> EndIfAsync<TFallback>(Func<Task<bool>> predicate, TFallback? fallback = default)
    {
        if (await predicate())
        {
            SetEndConfiguration(fallback);
        }

        return this;
    }

    public async Task MoveNextAsync()
    {
        var indexOfCurrentPipeline = _pipelineMiddlewares.IndexOf(_currentPipelineMiddleware);

        if (CanMoveNext(indexOfCurrentPipeline) is false)
        {
            return;
        }

        await _currentPipelineMiddleware!.ExecuteAsync();
        _currentPipelineMiddleware = _pipelineMiddlewares[indexOfCurrentPipeline + 1];
    }

    public bool CanMoveNext(int indexOfCurrentPipeline)
    {
        return indexOfCurrentPipeline < _pipelineMiddlewares.Count - 1 
            && (_pipelineStopIndex is null || _pipelineStopIndex > indexOfCurrentPipeline);
    }

    internal IPipelineMiddleware GetCurrentMiddleware()
    {
        return _currentPipelineMiddleware!;
    }

    public async Task EndAsync()
    {
        var startIndex = _pipelineMiddlewares.IndexOf(_currentPipelineMiddleware);

        for (int i = startIndex; i < _pipelineMiddlewares.Count - 1; i++)
        {
            await MoveNextAsync();
        }

        await _currentPipelineMiddleware.ExecuteAsync();
    }

    public async Task<TOutput> EndWithAsync<TOutput>()
    {
        await EndAsync();
        return GetCurrentMiddlewareOutputOrFallback<TOutput>();
    }

    private static StatefulPipeline Initializate(IPipelineMiddleware pipelineMiddleware)
    {
        return new StatefulPipeline(pipelineMiddleware);
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

    private IHasOutput<TOutput> GetLastMiddleware<TOutput>()
    {
        return ((IHasOutput<TOutput>)_pipelineMiddlewares
            .Where(middleware => middleware is IHasOutput<TOutput> hasOutput)
            .Last());
    }

    private TOutput GetCurrentMiddlewareOutputOrFallback<TOutput>()
    {
        if (_fallback is not null)
        {
            return (TOutput)_fallback;
        }

        return ((IHasOutput<TOutput>)_currentPipelineMiddleware)
            .Output!;
    }

    private void SetEndConfiguration<TFallback>(TFallback? fallback)
    {
        _pipelineStopIndex = _pipelineMiddlewares.Count - 1;
        _fallback = fallback;
    }
}