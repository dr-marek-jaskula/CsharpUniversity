using System.Diagnostics;

namespace CsharpAdvanced.Introduction;

public class SpanStackalloc
{

    //Span jest strukturą 
    //Provide help with buffer overflow and security
    //avoiding alocations


    //nie można w asyncach uzywać, ani w yieldach i moze gdzies jeszcze 
    //tam można używać Memory<T> zamiast Span<T>, ale jest troche wolniejsze
    //Memory<T> ma dwa więcej fieldy. Memory jest wtedy i tak duzo szybsze

    public static void LerningSpan()
    {
        #region Uefficient way, enfoce to use more memory
        string s = "TyType 1n";
        var first = s.Substring( startIndex: 0, length: 6);
        Console.WriteLine(first);
        var second = s.Substring(7);
        Console.WriteLine(second);

        //no we will se how it uses more memory
        s = s.Substring(3); // w tym momencie w pamieci pojawiają się dwie komórki pamięci: dla s = "TyType 1n" i dla nowego s = "ype 1n". Jednakże dla starego s nie ma już użytku i nie można się do niego dostać, a mimo to blokuje pamięć. Można by użyć GarbageCollector ale jak on działa, to program nie może działać, wiec lepiej znaleźć nowe rozwiązanie

        #endregion

        #region Efficient way to use memory. Slow Span
        //Span to valueType, który ma 3 fields:
        //Span działą tak, że ma pointer (1 field) na punkt w pamięci oraz parametry: Offset (2 field) i Length (3 field)
        //Offset to skąd się zaczyna
        //Length to jak długi

        ReadOnlySpan<char> s2 = "YGG871".AsSpan(); //trzeba string do Spana
        //Tutaj bazowo jest offset 0 i length = string.Length (czyli do konca)
        var first2 = s2.Slice(start: 0, length: 2); //Substring to tutaj Slice
        foreach (var item in first2)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("--------------");
        var second2 = s2.Slice(start: 3);
        foreach (var item in second2)
        {
            Console.WriteLine(item);
        }

        s2 = s2.Slice(3); //tutaj jedyne co się zmienia, to zmienia się offset z 0 na 3. Dlatego nie zaśmieca więcej pamięci.

        int num = int.Parse(s2); //tutaj już nie towrzy sie kolejny element w pamieci, ale pointer do tego samego miejsca w pamięci

        #endregion

        #region Efficient way to allocate memory. Fast Span

        //tutaj Span ma 2 fields: Lenght i Pointer



        #endregion

        Random random = new Random();
        var listOfStringWithSpan = string.Create(length: 10, random, (Span<char> chars, Random r) =>
        {
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)(r.Next(0, 10) + '0'); //to plus zero to zeby zrozumiał, że to jest "string", potem rzutowany na char
            }
        });


        //let us check if this is faster indeed
        string longString = "FDFDfesdzcbrbkrkvksdkcdfsdfRRRVSadseQQQQczxcsEAEDCAEDAEdfdfdDFDSFFFefsedSFDSFSDFDSFDSFDfdsfsefEFEFSEFSDFSDVSVSDFdsfsefbgbhjnyBFGBFGBFTbtdgdvdVRVFDVFVFVfdbdbgBVBVBDFVFDVFDVDFvfdvfdVDFVFDVDFVDF";
        Console.WriteLine(longString.Length);


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

        Console.WriteLine(stopwatch1.Elapsed);
        Console.WriteLine(stopwatch2.Elapsed);
        Console.WriteLine(stopwatch3.Elapsed);
        Console.WriteLine(stopwatch4.Elapsed);
        //Widać ze szybsze
    }
    #region Where to use span when u are a mortal man

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
