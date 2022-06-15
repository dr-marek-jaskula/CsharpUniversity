using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Benchmarks;

//MemoryDiagnoser attribute used to display the memory consumption
[MemoryDiagnoser]
public class ExampleBenchmarks
{
    private int[]? _myArray;

    //Each benchmark will be called three times, for three different _number values
    [Params(100, 1000, 10000)]
    public int Size { get; set; }

    //Initial setup. This method will run once to initialize variables
    [GlobalSetup]
    public void Setup()
    {
        _myArray = new int[Size];

        for (int i = 0; i < Size; i++)
            _myArray[i] = i;
    }

    public static void RunBenchmarks()
    {
        BenchmarkRunner.Run<ExampleBenchmarks>();
    }

    /// <summary>
    /// Benchmark description
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public void FirstBenchmark()
    {
    }
}

