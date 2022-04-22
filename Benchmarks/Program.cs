using BenchmarkDotNet.Running;
using Benchmarks;

//StringBenchmarks.RunBenchmarks();
//ReflectionBenchmarks.RunBenchmarks();
//SpanBenchmarks.RunBenchmarks();

BenchmarkRunner.Run<DateParserBenchmarks>();