namespace CsharpAdvanced.PipelinerStateful.Interfaces;

public interface IMiddlewareCondition<TFallback>
{
    int AfterMiddlewareIndex { get; set; }
    TFallback Fallback { get; set; }

    Task<bool> UseFallback();
}
