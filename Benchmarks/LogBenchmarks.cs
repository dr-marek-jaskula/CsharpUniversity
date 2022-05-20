using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CsharpAdvanced.Logs;

namespace Benchmarks;

[MemoryDiagnoser]
public class LogBenchmarks
{
    private static readonly LogsPerformance _logsPerformance = new();

    //The problem of Logger is that the last parameter is "params object?[]? propertyValues"
    //Therefore, we have:
    //1) using array that is allocated in the heap (use of memory)
    //2) boxing (due to the array is an array of "objects")

    public static void RunBenchmarks()
    {
        BenchmarkRunner.Run<LogBenchmarks>();
    }

    [Benchmark]
    public void Log_WithoutIf_WithParams()
    {
        _logsPerformance.WithoutIfWithParams();
    }

    [Benchmark]
    public void Log_WithtIf_WithParams()
    {
        _logsPerformance.WithIfWithParams();
    }
}