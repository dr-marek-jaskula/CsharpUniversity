using Xunit;
using CsharpBasics.Keywords;

namespace UniversityEntrypoints.CsharpBasicsProject;

public class BasicKeywordsEntrypoint
{
    [Fact]
    public void IsEntrypoint()
    {
        Is.InvokeIsExamples();
    }

    [Fact]
    public void AsEntrypoint()
    {
        As.InvokeAsExamples();
    }

    [Fact]
    public void UsingEntrypoint()
    {
        Using.InvokeUsingExamples();
    }
}