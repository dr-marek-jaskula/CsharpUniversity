namespace CsharpAdvanced.PipelinerStateful.Interfaces;

internal interface IPipelineMiddleware
{
    Task ExecuteAsync();
}
