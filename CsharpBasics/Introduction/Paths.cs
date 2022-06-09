namespace CsharpBasics.Introduction;

public class Paths
{
    public static void InvokePathsExample()
    {
        var pathToProjectDirectory = AppDomain.CurrentDomain.BaseDirectory.Split(@"bin\", StringSplitOptions.None)[0];
    }
}