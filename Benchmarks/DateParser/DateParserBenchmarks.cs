using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

namespace Benchmarks.DateParser;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class DateParserBenchmarks
{
    private const string _dateTime = "2019-12-13T16:33:06Z";
    private static readonly DateParser _parser = new();

    //This attribute informs that we will compare results to this method
    [Benchmark(Baseline = true)]
    public void GetYearFromDateTime()
    {
        _parser.GetYearFromDateTime(_dateTime);
    }

    [Benchmark]
    public void GetYearFromSubstring()
    {
        _parser.GetYearFromSubstring(_dateTime);
    }

    [Benchmark]
    public void GetYearFromSplit()
    {
        _parser.GetYearFromSplit(_dateTime);
    }

    [Benchmark]
    public void GetYearFromSpan()
    {
        _parser.GetYearFromSpan(_dateTime);
    }

    [Benchmark]
    public void GetYearFromSpanWithMaualConversion()
    {
        _parser.GetYearFromSpanWithMaualConversion(_dateTime);
    }
}