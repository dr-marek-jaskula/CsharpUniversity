using CsharpAdvanced.Pipeliner;
using CsharpAdvanced.PipelinerStateful;
using Xunit;
using Xunit.Abstractions;

namespace UniversityEntrypoints.CsharpAdvancedProject;

public sealed class PipelinerEntrypoint
{
    private readonly ITestOutputHelper _testOutputHelper;

    public PipelinerEntrypoint(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void StatelessEntrypoint()
    {
        ExecutePipeliner.Invoke(_testOutputHelper.WriteLine);
    }

    [Fact]
    public void StatefulEntrypoint()
    {
        ExecutePipelinerStateful.Invoke(_testOutputHelper.WriteLine);
    }
}