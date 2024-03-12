using CsharpAdvanced.PipelinerStateful.Interfaces;

namespace CsharpAdvanced.PipelinerStateful;

public sealed class ExecutePipeline
{
    public static async Task Invoke(Action<string> writeLine)
    {
        writeLine("------------ Default Use --------------");

        var finalResultWithDefaultUse = await Pipeline<string>
            .StartFrom("super")
            .ContinueWith<string, string>(PerformExampleOperation)
                .EndIf<string>(x => x.Length < 100000, "my working fallback")
            .ContinueWith<string, int>(x => x.Length + 2)
                .EndIf(() => 1 < 100000, "my not working fallback -> too late")
            .ContinueWith<int>(x => writeLine(x.ToString()))
            .ContinueWith<int, string>(x => x + "op")
            .ContinueWith<string>(x => writeLine(x + "++++++++++++++"))
            .EndWithAsync();

        writeLine(finalResultWithDefaultUse.ToString());

        writeLine("------------ Step By Step --------------");

        var pipeline = Pipeline<int>
            .StartFrom("super")
            .ContinueWith<string, string>(PerformExampleOperation)
            .ContinueWith<string, int>(x => x.Length + 2)
                //.EndIf<int>(x =>  x < 20, 200) //Uncomment to examine the behavior
            .ContinueWith<int, int>(async x => await AddNumber(x, 4));

        await pipeline
            .MoveNextAsync();

        var current = pipeline.GetCurrentMiddleware();

        await pipeline
            .MoveNextAsync();

        current = pipeline.GetCurrentMiddleware();

        await current.ExecuteAsync();

        var currentResult = ((IHasOutput<int>)current).Output;

        var finalResultWithStepByStepUse = await pipeline
            .EndWithAsync();

        writeLine(finalResultWithStepByStepUse.ToString());

        writeLine("-------------------------");
    }

    public static async Task<int> AddNumber(int number, int toAdd)
    {
        await Task.Delay(1000);
        return number + toAdd;
    }

    public static string PerformExampleOperation(string input)
    {
        return input + " hello";
    }
}