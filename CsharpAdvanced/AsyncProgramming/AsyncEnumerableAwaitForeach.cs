using System.Diagnostics;

namespace CsharpAdvanced.AsyncProgramming;

//Since C# 8.0 we can use the "IAsyncEnumerable" (and it generics) that can be use for streaming and much more.
//"IAsyncEnumerable" was introduced to deal with problem of "yield return" for async methods:
//The return type of method containing "yield return" needs to be IEnumerable, so for async methods the "IAsyncEnumerable" was made
public class AsyncEnumerableAwaitForeach
{
    public static async void InvokeAsyncEnumerableAwaitForeachExample()
    {
        var path = @"C:\Users\Marek\source\repos\EltinCreator\CsharpUniversity\CsharpAdvanced\AsyncProgramming\file.txt";

        //Now we cant await the GetLine methods
        var lines = GetLines(path);

        //But we need to await the foreach loop to get lines one by one (streaming)
        await foreach (var line in lines)
            Debug.WriteLine(line);
    }

    public static async IAsyncEnumerable<string> GetLines(string filePath)
    {
        string? line;
        using StreamReader streamReader = new(filePath);

        if (streamReader is null)
            throw new ArgumentException("FilePath is incorrect");

        while ((line = await streamReader.ReadLineAsync()) is not null)
        {
            await Task.Delay(1000);
            //By returning a line into the IAsyncEnumerable we will be able to get data one by one, when they are needed.
            yield return line;
        }
    }
}