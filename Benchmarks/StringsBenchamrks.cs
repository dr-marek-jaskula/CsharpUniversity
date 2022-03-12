using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace Benchmarks;

//Png graphic file called "StringsBenchmarkResults.png" has the results of this benchmark
//This pgn file is in the same folder

[MemoryDiagnoser]
public class StringBenchmarks
{
    public static void RunBenchmarks()
    {
        BenchmarkRunner.Run<StringBenchmarks>();
    }

    /// <summary>
    /// Least efficient, the lowered code use an array of objects (all inputs are getting boxed. They go to the heap, not a stack)
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public string BuildDateWithStringConcatenationMethod()
    {
        return string.Concat(1993, "/", 6, "/", 9);
    }
    //Above string.Concat method lowered is:
    /*
    object[] array = new object[5];
    array[0] = 1993;
    array[1] = "/";
    array[2] = 6;
    array[3] = "/";
    array[4] = 9;
    return string.Concat(array);
    */

    /// <summary>
    /// A bit more efficient. The compiler detects that all imputs are string, so the lowered code uses array of strings, so stack is used here.
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public string BuildDateWithStringConcatenationMethodToString()
    {
        return string.Concat(1993.ToString(), "/", 6.ToString(), "/", 9.ToString());
    }
    //Above string.Concat method lowered is:
    /*
    object[] array = new object[5];
    array[0] = 1993.ToString();
    array[1] = "/";
    array[2] = 6.ToString();
    array[3] = "/";
    array[4] = 9.ToString();
    return string.Concat(array);
    */

    /// <summary>
    /// In sense of lowered code, this apporach is the same are the previous one
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public string BuildDateWithStringConcatenation()
    {
        return 1993 + "/" + 6 + "/" + 9;
    }
    //Above method lowered is:
    /*
    object[] array = new object[5];
    array[0] = 1993.ToString();
    array[1] = "/";
    array[2] = 6.ToString();
    array[3] = "/";
    array[4] = 9.ToString();
    return string.Concat(array);
    */

    /// <summary>
    /// The fastest apporach, however is uses more memory that string interpolation
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public string BuildDateWithStringBuilder()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.Append(1993);
        stringBuilder.Append('/');
        stringBuilder.Append(6);
        stringBuilder.Append('/');
        stringBuilder.Append(9);
        return stringBuilder.ToString();
    }

    /// <summary>
    /// This old way of interpolating string (used in c# 9.0, .net5.0, even for $"some string {date}" but behind the scenes).
    /// Its slow because the parameters of the Format method are treated as 'objects' (type 'object') so the are boxed, so they wont go to the stack, but to the heap.
    /// This results in more memory allocation. 
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public string BuildDateWithOldStringInterpolation()
    {
        return string.Format("{0}/{1}/{2}", 1993, 6, 9);
    }


    /// <summary>
    /// Since c# 10, this way of interpolating string does not use the Format method
    /// Therefore, the interpolated string does not go to the heap, but to the stack
    /// Least memosry allocation. Best performace (since c# 10!)
    /// From c# 10, this way uses "DeafultInterpolatedStringHandler", which is more efficient then Format method
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public string BuildDateWithNewStringInterpolation()
    {
        return $"{1993}/{6}/{9}";
    }
    //The lowered code is the code connected to the DeafultInterpolatedStringHandler
}

