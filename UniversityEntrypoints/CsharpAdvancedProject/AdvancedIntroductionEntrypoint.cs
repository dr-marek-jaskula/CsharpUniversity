using CsharpAdvanced.Introduction;
using Xunit;

namespace UniversityEntrypoints.CsharpAdvancedProject;

public class AdvancedIntroductionEntrypoint
{
    [Fact]
    public void IteratorsEntrypoint()
    {
        Interators.InvokeIteratorExamples();
    }

    [Fact]
    public void SwtichExpressionEntrypoint()
    {
        Switch.InvokeSwitchExamples();
    }

    [Fact]
    public void CustomComparersEntrypoint()
    {
        CustomComparers.InvokeCustomComparers();
    }
}

