using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace Benchmarks.Span;

//Png graphic file called "SpanBenchmarkResults.png" has the results of this benchmark
//This pgn file is in the same folder

[MemoryDiagnoser]
public class SpanBenchmarks
{
    public static void RunBenchmarks()
    {
        BenchmarkRunner.Run<SpanBenchmarks>();
    }

    private static readonly string _dateAsText = "08 07 2022";

    [Benchmark]
    public (int day, int month, int year) DateWithStringAndSubstring()
    {
        //These 4 string allocated in the heap (expensive)  
        var dayAsText = _dateAsText.Substring(0, 2);
        var monthAsText = _dateAsText.Substring(3, 2);
        var yearAsText = _dateAsText.Substring(6);

        var day = int.Parse(dayAsText);
        var month = int.Parse(monthAsText);
        var year = int.Parse(yearAsText);

        return (day, month, year);
    }

    [Benchmark]
    public (int day, int month, int year) DateWithStringAndSpan()
    {
        ReadOnlySpan<char> dateAsSpan = _dateAsText;
        var dayAsText = dateAsSpan.Slice(0, 2);
        var monthAsText = dateAsSpan.Slice(3, 2);
        var yearAsText = dateAsSpan.Slice(6);

        var day = int.Parse(dayAsText);
        var month = int.Parse(monthAsText);
        var year = int.Parse(yearAsText);

        return (day, month, year);
    }
}

