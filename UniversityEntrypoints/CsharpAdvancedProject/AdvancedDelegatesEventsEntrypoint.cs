using CsharpAdvanced.DelegatesEvents;
using Xunit;

namespace UniversityEntrypoints.CsharpAdvancedProject;

public class AdvancedDelegatesEventsEntrypoint
{
    [Fact]
    public void DelegatesEntrypoint()
    {
        DelegatesBasics.InvokeDelegatesExamples();
    }

    [Fact]
    public void EventsEntrypoint()
    {
        EventsBasic.InvokeEventsExamples();
    }
}

