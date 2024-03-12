using CsharpAdvanced.PipelinerStateful.Interfaces;

namespace CsharpAdvanced.PipelinerStateful.MiddlewareConditons;

internal class MiddlewareConditionAsync<TFallback>
(
    int afterMiddlewareIndex, 
    TFallback fallback, 
    Func<Task<bool>> conditionAsync
) 
    : IMiddlewareCondition<TFallback>
{
    public int AfterMiddlewareIndex { get; set; } = afterMiddlewareIndex;
    public TFallback Fallback { get; set; } = fallback;
    public Func<Task<bool>> ConditionAsync { get; set; } = conditionAsync;

    public async Task<bool> StopAndUseFallback()
    {
        return await ConditionAsync();
    }
}

internal class MiddlewareConditionAsync<TInput, TFallback>
(
    int afterMiddlewareIndex,
    TFallback fallback,
    Func<TInput, Task<bool>> conditionWithInputAsync,
    IHasOutput<TInput> hasOutput
)
    : IMiddlewareCondition<TFallback>
{
    public IHasOutput<TInput> HasOutput { get; set; } = hasOutput;
    public int AfterMiddlewareIndex { get; set; } = afterMiddlewareIndex;
    public TFallback Fallback { get; set; } = fallback;
    public Func<TInput, Task<bool>> ConditionWithInputAsync { get; set; } = conditionWithInputAsync;

    public async Task<bool> StopAndUseFallback()
    {
        return await ConditionWithInputAsync(HasOutput.Output!);
    }
}