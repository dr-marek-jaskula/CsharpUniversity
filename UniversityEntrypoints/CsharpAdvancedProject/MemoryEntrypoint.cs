using CsharpBasics.Memory;
using Xunit;

namespace UniversityEntrypoints.CsharpAdvancedProject;

public class MemoryEntrypoint
{
    [Fact]
    public void PointerEntrypoint()
    {
        Pointers.InvokePointersExample();
    }

    [Fact]
    public void ClousersEntrypoint()
    {
        Closures.InvokeClousersExamples();
    }
}

