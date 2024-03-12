namespace CsharpAdvanced.PipelinerStateful;

public sealed class StatefulPipeline
{
    private bool _properlyEnded = false;
    private readonly List<PipelineMiddleware> _pipelineMiddlewares = [];

    private StatefulPipeline()
    {
    }

    public static StatefulPipeline StartFrom<TInput>(TInput input)
    {
        return Initializate(new PipelineMiddleware<TInput>(input));
    }

    public static StatefulPipeline StartFrom<TOutput>(Func<TOutput> func)
    {
        return Initializate(new PipelineMiddleware<TOutput>(func));
    }

    public static StatefulPipeline StartFrom(Action func)
    {
        var initialMiddleware = new PipelineMiddleware();
        initialMiddleware.Execute(func);
        return Initializate(initialMiddleware);
    }

    public StatefulPipeline ContinueWith<TOutput>(Action<TOutput> func)
    {
        var nextMiddleware = new PipelineMiddleware();
        var previousResult = GetPreviousResult<TOutput>();
        return Continue(nextMiddleware, () => nextMiddleware.Execute(previousResult!, func));
    }

    public StatefulPipeline ContinueWith<TInput, TOutput>(Func<TInput, TOutput> func)
    {
        var previousResult = GetPreviousResult<TInput>();
        return Continue(new PipelineMiddleware<TInput, TOutput>(previousResult!, func));
    }

    public StatefulPipeline ContinueWith<TOutput>(Func<TOutput> func)
    {
        return Continue(new PipelineMiddleware<TOutput>(func));
    }

    public StatefulPipeline ContinueWith(Action func)
    {
        return Continue(new PipelineMiddleware(), func);
    }

    public void End()
    {
        _properlyEnded = true;
    }

    public TOutput EndWith<TOutput>()
    {
        End();
        return GetPreviousResult<TOutput>();
    }

    public TOutput EndWith<TInput, TOutput>(Func<TInput, TOutput> mapping)
    {
        End();
        return mapping(GetPreviousResult<TInput>());
    }

    private static StatefulPipeline Initializate(PipelineMiddleware pipelineMiddleware)
    {
        var newPipeline = new StatefulPipeline();
        newPipeline._pipelineMiddlewares.Add(pipelineMiddleware);
        return newPipeline;
    }

    private StatefulPipeline Continue(PipelineMiddleware nextMiddleware, Action? func = null)
    {
        if (func is not null)
        {
            nextMiddleware.Execute(func);
        }

        _pipelineMiddlewares.Add(nextMiddleware);

        return this;
    }

    private TInput GetPreviousResult<TInput>()
    {
        return ((HasResult<TInput>)_pipelineMiddlewares
            .Where(x => x is HasResult)
            .Last())
            .Result;
    }
}