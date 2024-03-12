using CsharpAdvanced.PipelinerStateful.Interfaces;

namespace CsharpAdvanced.PipelinerStateful.MiddlewareConditons;

internal class MiddlewareCondition<TFallback>
(
    int afterMiddlewareIndex,
    TFallback fallback,
    Func<bool> condition
)
    : IMiddlewareCondition<TFallback>
{
    public int AfterMiddlewareIndex { get; set; } = afterMiddlewareIndex;
    public TFallback Fallback { get; set; } = fallback;
    public Func<bool> Condition { get; set; } = condition;

    public Task<bool> UseFallback()
    {
        return Task.FromResult(Condition());
    }
}

internal class MiddlewareCondition<TInput, TFallback>
(
    int afterMiddlewareIndex,
    TFallback fallback,
    Func<TInput, bool> conditionWithInput,
    IHasOutput<TInput> hasOutput
)
    : IMiddlewareCondition<TFallback>
{
    public IHasOutput<TInput> HasOutput { get; set; } = hasOutput;
    public int AfterMiddlewareIndex { get; set; } = afterMiddlewareIndex;
    public TFallback Fallback { get; set; } = fallback;
    public Func<TInput, bool> ConditionWithInput { get; set; } = conditionWithInput;

    public Task<bool> UseFallback()
    {
        return Task.FromResult(ConditionWithInput(HasOutput.Output!));
    }
}