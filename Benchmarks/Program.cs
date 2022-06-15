using BenchmarkDotNet.Running;
using Benchmarks.DateParser;
using Benchmarks.Log;
using Benchmarks.Reflections;
using Benchmarks.Span;
using Benchmarks.Strings;
using Serilog;
using Serilog.Events;

//StringBenchmarks.RunBenchmarks();
//ReflectionBenchmarks.RunBenchmarks();
//SpanBenchmarks.RunBenchmarks();
//BenchmarkRunner.Run<DateParserBenchmarks>();

LogBenchmarks.RunBenchmarks();