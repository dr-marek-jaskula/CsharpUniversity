using CsharpAdvanced.AsyncProgramming;
using Xunit;

namespace UniversityEntrypoints.CsharpAdvancedProject;

public class AdvanceAsyncProgrammingEntrypoint
{
    [Fact]
    public void ThreadsEntrypoint()
    {
        Threads.InvokeThreadsExamples();
    }

    [Fact]
    public void TasksEntrypoint()
    {
        Tasks.InvokeTasksExamples();
    }
}