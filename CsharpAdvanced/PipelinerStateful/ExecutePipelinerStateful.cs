namespace CsharpAdvanced.PipelinerStateful;

public sealed class ExecutePipelinerStateful
{
    public static void Invoke(Action<string> writeLine)
    {
        writeLine("-------------------------");

        var finalResult = StatefulPipeline
            .StartFrom("super")
            .ContinueWith<string, string>(x => x + "hellow")
            .ContinueWith<string, int>(x => x.Length + 2)
            .ContinueWith<int>(x => writeLine(x.ToString()))
            .ContinueWith<int, string>(x => x + "mop")
            .ContinueWith<string>(x => writeLine(x.ToString()))
            .EndWith<string>();

        writeLine(finalResult.ToString());

        writeLine("-------------------------");
    }
}