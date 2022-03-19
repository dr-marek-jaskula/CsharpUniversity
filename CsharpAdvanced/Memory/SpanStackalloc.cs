using System.Diagnostics;

namespace CsharpAdvanced.Memory;

public class SpanStackalloc
{
    //Span was created to avoid heap allocation
    //It is a best for string manipulation, while string in c# are immutable, and they are allocated in the heap

    //Benchmarks are done in the benchmark project
    //Span is faster
    //However, Span is ALL about memory allocation -> span is always allocated on the stack [therefore it is faster]

    //Span is iterable
    //Span is a ref struct -> it can be allocated on the heap
    //proof:
    //ReadOnlySpan<char> dateAsSpan = _dateAsText; // cant be done, cant be boxed, can implement interfaces, can used in yield and async

    //Recommend using Span<T> or ReadOnlySpan<T> types to work with stack allocated memory whenever possible.

    //The way how span works:
    //String is allocated somewhere in the heap
    //Span points to the beginning (or some offset) of the string and store the length of the string, so it gets the substring from a string, and not create new one
    //So it store at the stack: the origin address + offset and the length in next block

    private static readonly string _dateAsText = "08 07 2022";

    public static void InvokeSpanStackalllocExamples()
    {
        //Examine the benchmarks
        var date = DateWithStringAndSubstring();
        var date2 = DateWithStringAndSpan();

        //iterable (as is was an array)
        ReadOnlySpan<char> someSpan = "This is a span";
        foreach (var character in someSpan)
            Debug.WriteLine(character);
    }

    //Standard way with substring
    public static (int day, int month, int year) DateWithStringAndSubstring()
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

    //ReadOnlySpan version
    public static (int day, int month, int year) DateWithStringAndSpan()
    {
        ReadOnlySpan<char> dateAsSpan = _dateAsText;
        //These 4 string allocated in the heap (expensive)  
        var dayAsText = dateAsSpan.Slice(0, 2);
        var monthAsText = dateAsSpan.Slice(3, 2);
        var yearAsText = dateAsSpan.Slice(6);

        var day = int.Parse(dayAsText);
        var month = int.Parse(monthAsText);
        var year = int.Parse(yearAsText);

        return (day, month, year);
    }

    public static void LerningSpan()
    {
        #region Inefficient way, enforce to use more memory

        string s = "TyType 1n";
        var first = s.Substring(startIndex: 0, length: 6);
        Debug.WriteLine(first);
        var second = s.Substring(7);
        Debug.WriteLine(second);

        //no we will see how it uses more memory
        s = s.Substring(3); 

        #endregion

        #region Efficient way to use memory. Slow Span

        ReadOnlySpan<char> s2 = "YGG871".AsSpan(); 

        var first2 = s2.Slice(start: 0, length: 2); 
        foreach (var item in first2)
            Debug.WriteLine(item);

        var second2 = s2.Slice(start: 3);
        foreach (var item in second2)
            Debug.WriteLine(item);

        s2 = s2.Slice(3); 
        int num = int.Parse(s2); 

        #endregion

        #region Efficient way to allocate memory. Fast Span

        //tutaj Span ma 2 fields: Lenght i Pointer



        #endregion

        Random random = new();
        var listOfStringWithSpan = string.Create(length: 10, random, (Span<char> chars, Random r) =>
        {
            for (int i = 0; i < chars.Length; i++)
                chars[i] = (char)(r.Next(0, 10) + '0'); 
        });


        //let us check if this is faster indeed
        string longString = "FDFDfesdzcbrbkrkvksdkcdfsdfRRRVSadseQQQQczxcsEAEDCAEDAEdfdfdDFDSFFFefsedSFDSFSDFDSFDSFDfdsfsefEFEFSEFSDFSDVSVSDFdsfsefbgbhjnyBFGBFGBFTbtdgdvdVRVFDVFVFVfdbdbgBVBVBDFVFDVFDVDFvfdvfdVDFVFDVDFVDF";
        Debug.WriteLine(longString.Length);

        //Benchmarks would be better but...
        Stopwatch stopwatch1 = Stopwatch.StartNew();
        ConstainsCapitalLetter(longString);
        stopwatch1.Stop();

        Stopwatch stopwatch2 = Stopwatch.StartNew();
        Sum(new int[] { 3, 4, 5, 9, 11, 42, 55, 22, 99, 3232,542, 121,4232,4232,555,22,31,1,3, 4, 5, 9, 11, 42, 55, 22, 99, 3232, 542, 121, 4232, 4232, 555, 22, 31, 1, 3, 4, 5, 9, 11, 42, 55, 22, 99, 3232, 542, 121, 4232, 4232, 555, 22, 31, 1, 3, 4, 5, 9, 11, 42, 55, 22, 99, 3232, 542, 121, 4232, 4232, 555, 22, 31, 1, 3, 4, 5, 9, 11, 42, 55, 22, 99, 3232, 542, 121, 4232, 4232, 555, 22, 31, 1, 3, 4, 5, 9, 11, 42, 55, 22, 99, 3232, 542, 121, 4232, 4232, 555, 22, 31, 1, 3 });
        stopwatch2.Stop();


        Stopwatch stopwatch3 = Stopwatch.StartNew();
        ConstainsCapitalLetter2(longString);
        stopwatch3.Stop();
        
        Stopwatch stopwatch4 = Stopwatch.StartNew();
        Sum2(new int[] { 3, 4, 5, 9, 11, 42, 55, 22, 99, 3232, 542, 121, 4232, 4232, 555, 22, 31, 1, 3, 4, 5, 9, 11, 42, 55, 22, 99, 3232, 542, 121, 4232, 4232, 555, 22, 31, 1, 3, 4, 5, 9, 11, 42, 55, 22, 99, 3232, 542, 121, 4232, 4232, 555, 22, 31, 1, 3, 4, 5, 9, 11, 42, 55, 22, 99, 3232, 542, 121, 4232, 4232, 555, 22, 31, 1, 3, 4, 5, 9, 11, 42, 55, 22, 99, 3232, 542, 121, 4232, 4232, 555, 22, 31, 1, 3, 4, 5, 9, 11, 42, 55, 22, 99, 3232, 542, 121, 4232, 4232, 555, 22, 31, 1, 3 });
        stopwatch4.Stop();

        Debug.WriteLine(stopwatch1.Elapsed);
        Debug.WriteLine(stopwatch2.Elapsed);
        Debug.WriteLine(stopwatch3.Elapsed);
        Debug.WriteLine(stopwatch4.Elapsed);
    }

    #region Where to use span

    //Slow functions
    public static bool ConstainsCapitalLetter(string s)
    {
        for (int i = 0; i < s.Length; i++)
            if (char.IsUpper(s[i]))
                return true;
        return false;
    }
    
    public static int Sum(int[] a)
    {
        int sum = 0;
        for (int i = 0; i < a.Length; i++)
            sum += a[i];
        return sum;
    }

    //Fast functions
    public static bool ConstainsCapitalLetter2(ReadOnlySpan<char> s)
    {
        for (int i = 0; i < s.Length; i++)
            if (char.IsUpper(s[i]))
                return true;
        return false;
    }

    public static int Sum2(ReadOnlySpan<int> a)
    {
        int sum = 0;
        for (int i = 0; i < a.Length; i++)
            sum += a[i];
        return sum;
    }

    #endregion
}
