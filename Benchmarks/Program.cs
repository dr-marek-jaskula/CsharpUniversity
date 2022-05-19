using BenchmarkDotNet.Running;
using Benchmarks;
using Serilog;
using Serilog.Events;

//StringBenchmarks.RunBenchmarks();
//ReflectionBenchmarks.RunBenchmarks();
//SpanBenchmarks.RunBenchmarks();
//BenchmarkRunner.Run<DateParserBenchmarks>();

LogBenchmarks.RunBenchmarks();
//LogBenchmarks LogBenchmarks = new();
//LogBenchmarks.Log_WithtIf_WithParams();
//LogBenchmarks.Log_WithoutIf_WithParams();
