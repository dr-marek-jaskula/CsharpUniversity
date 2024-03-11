namespace CsharpAdvanced.Pipeliner;

public static class Pipeline
{
    public static PipelineResult<TInput> StartFrom<TInput>(TInput input)
    {
        return new PipelineResult<TInput>(input);
    }

    public static PipelineResult<TOutput> StartFrom<TOutput>(Func<TOutput> func)
    {
        return func();
    }

    public static PipelineResult StartFrom(Action func)
    {
        func();
        return PipelineResult.Instance;
    }
}