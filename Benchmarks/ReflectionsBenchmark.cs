using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CsharpAdvanced.Reflections;

namespace Benchmarks;

[MemoryDiagnoser]
public class ReflectionBenchmarks
{
    public static void RunBenchmarks()
    {
        BenchmarkRunner.Run<ReflectionBenchmarks>();
    }

    [Benchmark]
    public string SimpleGet() => ReflectionPerformance.SimpleGet();

    [Benchmark]
    public string TraditionalReflection() => ReflectionPerformance.TraditionalReflection();

    [Benchmark]
    public string OptimizedTraditionalReflection() => ReflectionPerformance.OptimizedTraditionalReflection();

    [Benchmark]
    public string CompiledDelegateOptimizedTraditionalReflection() => ReflectionPerformance.CompiledDelegateOptimizedTraditionalReflection();

    //[Benchmark]
    //public string EmittedOptimizedTraditionalReflection() => ReflectionPerformance.EmittedOptimizedTraditionalReflection();
}




