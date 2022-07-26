using BenchmarkDotNet.Running;
using Benchmarks.DateParser;
using Benchmarks.Log;
using Benchmarks.Reflections;
using Benchmarks.Span;
using Benchmarks.Strings;
using Serilog;
using Serilog.Events;

//In this project we test the performance of some approaches described in other projects in current solution.

//StringBenchmarks.RunBenchmarks();
//ReflectionBenchmarks.RunBenchmarks();
//SpanBenchmarks.RunBenchmarks();
//BenchmarkRunner.Run<DateParserBenchmarks>();

LogBenchmarks.RunBenchmarks();