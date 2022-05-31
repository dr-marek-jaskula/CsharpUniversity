using CsharpAdvanced.AsyncProgramming;
using CsharpAdvanced.ThrowNuGet;
using Xunit;

namespace UniversityEntrypoints.CsharpAdvancedProject;

public class AdvanceThrowEntrypoint
{
    [Fact]
    public void ThrowEntrypoint()
    {
        ThrowNuGet.InvokeThrowNuGetExample();
    }
}