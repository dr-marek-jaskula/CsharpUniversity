namespace CsharpAdvanced.Pipeliner;

public sealed class ExecutePipeliner
{
    public static void Invoke(Action<string> writeLine)
    {
        writeLine("-------------------------");

        var finalResult = Pipeline
            .StartFrom("super")
            .ContinueWith(x => x + "hellow")
            .ContinueWith(x => x.Length + 2)
            .ContinueWith(x => writeLine(x.ToString()));

        writeLine(finalResult.ToString());

        writeLine("-------------------------");

        var finalResult2 = Pipeline
            .StartFrom(() => "superInput")
            .ContinueWith(x => writeLine(x.ToString()))
            .ContinueWith(() => 2);

        writeLine(finalResult2.ToString());

        writeLine("-------------------------");

        var finalResult3 = Pipeline
            .StartFrom(() => writeLine("initial"))
            .ContinueWith(() => 2)
            .ContinueWith(x => 2 + x);

        writeLine(finalResult3.ToString());

        writeLine("-------------------------");
    }
}