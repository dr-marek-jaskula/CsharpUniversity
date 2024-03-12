using CsharpAdvanced.Pipeliner;
using CsharpAdvanced.PipelinerStateful;
using System.Threading.Tasks;
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
    public async Task StatefulEntrypoint()
    {
        await ExecutePipeline.Invoke(_testOutputHelper.WriteLine);
    }
}