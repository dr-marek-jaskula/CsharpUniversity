namespace CsharpAdvanced.PipelinerStateful;

public sealed class StatefulPipeline
{
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
        var lastMiddlewareWithMatchingInput = GetLastMiddlewareWithOutputType<TInput>();
        return Continue(new PipelineMiddlewareWithInput<TInput>(func, lastMiddlewareWithMatchingInput));
    }

    public StatefulPipeline ContinueWith<TInput, TOutput>(Func<TInput, TOutput> func)
    {
        var lastMiddlewareWithMatchingInput = GetLastMiddlewareWithOutputType<TInput>();
        return Continue(new PipelineMiddlewareWithInputAndOutput<TInput, TOutput>(func, lastMiddlewareWithMatchingInput));
    }
    
    public StatefulPipeline ContinueWith<TInput, TOutput>(Func<TInput, Task<TOutput>> func)
    {
        var lastMiddlewareWithMatchingInput = GetLastMiddlewareWithOutputType<TInput>();
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

    //public StatefulPipeline EndIf(Func<bool> func)
    //{
    //    return Continue(new PipelineMiddlewareWithOutput<TOutput>(func));
    //}

    //public StatefulPipeline EndIf(Func<Task<bool>> func)
    //{
    //    return Continue(new PipelineMiddlewareWithOutput<TOutput>(func));
    //}

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
        return indexOfCurrentPipeline < _pipelineMiddlewares.Count - 1;
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
        return GetLastOutputOfType<TOutput>();
    }

    public async Task<TOutput> EndWithAsync<TInput, TOutput>(Func<TInput, TOutput> mapping)
    {
        await EndAsync();
        return mapping(GetLastOutputOfType<TInput>());
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

    private IHasOutput<TOutput> GetLastMiddlewareWithOutputType<TOutput>()
    {
        return ((IHasOutput<TOutput>)_pipelineMiddlewares
            .Where(x => x is IHasOutput<TOutput>)
            .Last());
    }

    private TOutput GetLastOutputOfType<TOutput>()
    {
        return GetLastMiddlewareWithOutputType<TOutput>()
            .Output!;
    }
}