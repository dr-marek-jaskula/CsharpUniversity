using CsharpAdvanced.PipelinerStateful.Interfaces;
using CsharpAdvanced.PipelinerStateful.MiddlewareConditons;
using CsharpAdvanced.PipelinerStateful.Middlewares;

namespace CsharpAdvanced.PipelinerStateful;

public sealed class Pipeline<TPipelineOutput>
{
    private bool _stopped = false;
    private TPipelineOutput? _fallback = default;
    private IPipelineMiddleware _currentPipelineMiddleware;
    private readonly List<IPipelineMiddleware> _pipelineMiddlewares = [];
    private readonly List<IMiddlewareCondition<TPipelineOutput>> _middlewareConditions = [];

    private Pipeline(IPipelineMiddleware pipelineMiddleware)
    {
        _pipelineMiddlewares.Add(pipelineMiddleware);
        _currentPipelineMiddleware = pipelineMiddleware;
    }

    public static Pipeline<TPipelineOutput> StartFrom<TInput>(TInput input)
    {
        return Initializate(new PipelineMiddlewareWithOutput<TInput>(input));
    }

    public static Pipeline<TPipelineOutput> StartFrom<TOutput>(Func<TOutput> func)
    {
        return Initializate(new PipelineMiddlewareWithOutput<TOutput>(func));
    }

    public static Pipeline<TPipelineOutput> StartFrom(Action func)
    {
        return Initializate(new PipelineMiddleware(func));
    }

    public Pipeline<TPipelineOutput> ContinueWith<TInput>(Action<TInput> func)
    {
        var lastMiddlewareWithMatchingInput = GetLastMiddleware<TInput>();
        return Continue(new PipelineMiddlewareWithInput<TInput>(func, lastMiddlewareWithMatchingInput));
    }

    public Pipeline<TPipelineOutput> ContinueWith<TInput, TOutput>(Func<TInput, TOutput> func)
    {
        var lastMiddlewareWithMatchingInput = GetLastMiddleware<TInput>();
        return Continue(new PipelineMiddlewareWithInputAndOutput<TInput, TOutput>(func, lastMiddlewareWithMatchingInput));
    }
    
    public Pipeline<TPipelineOutput> ContinueWith<TInput, TOutput>(Func<TInput, Task<TOutput>> func)
    {
        var lastMiddlewareWithMatchingInput = GetLastMiddleware<TInput>();
        return Continue(new PipelineMiddlewareWithInputAndOutput<TInput, TOutput>(func, lastMiddlewareWithMatchingInput));
    }

    public Pipeline<TPipelineOutput> ContinueWith<TOutput>(Func<TOutput> func)
    {
        return Continue(new PipelineMiddlewareWithOutput<TOutput>(func));
    }

    public Pipeline<TPipelineOutput> ContinueWith<TOutput>(Func<Task<TOutput>> func)
    {
        return Continue(new PipelineMiddlewareWithOutput<TOutput>(func));
    }

    public Pipeline<TPipelineOutput> ContinueWith(Action func)
    {
        return Continue(new PipelineMiddleware(func));
    }

    public Pipeline<TPipelineOutput> EndIf(Func<bool> predicate, TPipelineOutput fallback)
    {
        var newMiddlewareCondition = new MiddlewareCondition<TPipelineOutput>
        (
            _pipelineMiddlewares.Count - 1,
            fallback,
            predicate
        );

        _middlewareConditions.Add(newMiddlewareCondition);

        return this;
    }

    public Pipeline<TPipelineOutput> EndIfAsync(Func<Task<bool>> predicate, TPipelineOutput fallback)
    {
        var newMiddlewareCondition = new MiddlewareConditionAsync<TPipelineOutput>
        (
            _pipelineMiddlewares.Count - 1,
            fallback,
            predicate
        );

        _middlewareConditions.Add(newMiddlewareCondition);

        return this;
    }

    public Pipeline<TPipelineOutput> EndIf<TInput>(Func<TInput, bool> predicate, TPipelineOutput fallback)
    {
        var lastMiddlewareWithMatchingInput = GetLastMiddleware<TInput>();

        var newMiddlewareCondition = new MiddlewareCondition<TInput, TPipelineOutput>
        (
            _pipelineMiddlewares.Count - 1,
            fallback,
            predicate,
            lastMiddlewareWithMatchingInput
        );

        _middlewareConditions.Add(newMiddlewareCondition);

        return this;
    }

    public Pipeline<TPipelineOutput> EndIfAsync<TInput>(Func<TInput, Task<bool>> predicate, TPipelineOutput fallback)
    {
        var lastMiddlewareWithMatchingInput = GetLastMiddleware<TInput>();

        var newMiddlewareCondition = new MiddlewareConditionAsync<TInput, TPipelineOutput>
        (
            _pipelineMiddlewares.Count - 1,
            fallback,
            predicate,
            lastMiddlewareWithMatchingInput
        );

        _middlewareConditions.Add(newMiddlewareCondition);

        return this;
    }

    public async Task MoveNextAsync()
    {
        var indexOfCurrentMiddleware = GetIndexOfCurrentMiddleware();

        if (CanMoveNext(indexOfCurrentMiddleware) is false)
        {
            return;
        }

        await _currentPipelineMiddleware!.ExecuteAsync();

        var currentCondition = _middlewareConditions.FirstOrDefault(x => x.AfterMiddlewareIndex == indexOfCurrentMiddleware);

        if (currentCondition is not null && await currentCondition.UseFallback())
        {
            _stopped = true;
            _fallback = currentCondition.Fallback;
            return;
        }

        _currentPipelineMiddleware = _pipelineMiddlewares[indexOfCurrentMiddleware + 1];
    }

    public bool CanMoveNext(int indexOfCurrentPipeline)
    {
        return indexOfCurrentPipeline < _pipelineMiddlewares.Count - 1
            && _stopped is false;
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

            if (_stopped)
            {
                break;
            }
        }

        await _currentPipelineMiddleware.ExecuteAsync();
    }

    public async Task<TPipelineOutput> EndWithAsync()
    {
        await EndAsync();

        if (_stopped)
        {
            return _fallback;
        }

        return GetLastMiddleware<TPipelineOutput>()
            .Output!;
    }

    private static Pipeline<TPipelineOutput> Initializate(IPipelineMiddleware pipelineMiddleware)
    {
        return new Pipeline<TPipelineOutput>(pipelineMiddleware);
    }

    private Pipeline<TPipelineOutput> Continue(IPipelineMiddleware nextMiddleware, Action? func = null)
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

    private int GetIndexOfCurrentMiddleware()
    {
        return _pipelineMiddlewares.IndexOf(_currentPipelineMiddleware);
    }
}