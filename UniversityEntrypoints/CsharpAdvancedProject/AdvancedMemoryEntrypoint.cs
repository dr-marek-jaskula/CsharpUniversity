using CsharpAdvanced.Memory;
using Xunit;

namespace UniversityEntrypoints.CsharpAdvancedProject;

public class AdvancedMemoryEntrypoint
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

    [Fact]
    public void SpanStackallocEntrypoint()
    {
        SpanStackalloc.InvokeSpanStackalllocExamples();
    }
}

