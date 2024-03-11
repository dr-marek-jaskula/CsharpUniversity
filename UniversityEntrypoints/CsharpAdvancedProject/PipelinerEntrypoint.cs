using CsharpAdvanced.Pipeliner;
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
    public void Entrypoint()
    {
        ExecutePipeliner.Invoke(_testOutputHelper.WriteLine);
    }
}