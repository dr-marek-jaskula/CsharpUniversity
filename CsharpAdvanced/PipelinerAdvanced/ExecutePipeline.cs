using CsharpAdvanced.PipelinerStateful.Interfaces;

namespace CsharpAdvanced.PipelinerStateful;

public sealed class ExecutePipeline
{
    public static async Task Invoke(Action<string> writeLine)
    {
        writeLine("-------------------------");

        var finalResult = await Pipeline<string>
            .StartFrom("super")
            .ContinueWith<string, string>(x => x + "hellow")
                .EndIf<string>(x => x.Length < 100000, "my super fallback")
                //.EndIf(() => 1 < 100000, "my super fallback")
            .ContinueWith<string, int>(x => x.Length + 2)
            .ContinueWith<int>(x => writeLine(x.ToString()))
            .ContinueWith<int, string>(x => x + "mop")
            .ContinueWith<string>(x => writeLine(x + "++++++++++++++"))
            .EndWithAsync();

        writeLine(finalResult.ToString());

        writeLine("-------------------------");

        var pipeline = Pipeline<int>
            .StartFrom("super")
            .ContinueWith<string, string>(x => x + "hellow")
            .ContinueWith<string, int>(x => x.Length + 2)
            .ContinueWith<int, int>(async x =>
            {
                var task = Add4(x);
                return await task;
            });

        await pipeline
            .MoveNextAsync();

        var current = pipeline.GetCurrentMiddleware();

        await pipeline
            .MoveNextAsync();

        var current2 = pipeline.GetCurrentMiddleware();

        await current2.ExecuteAsync();
        var someResult = ((IHasOutput<int>)current2).Output;

        var finalResult2 = await pipeline
            .EndWithAsync();

        writeLine(finalResult2.ToString());

        writeLine("-------------------------");
    }

    public static async Task<int> Add4(int number)
    {
        await Task.Delay(1000);
        return number + 4;
    }

    public static async Task WaitASecond()
    {
        await Task.Delay(1000);
    }
}