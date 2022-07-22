namespace CsharpAdvanced.Performance;

public class PerformanceTips
{
    //In order to improve the performance of the application we can:
    //1. Seal all classes we can

    //2. Use Spans when possible (and Memory)

    //3. If we want to return an empty array or empty list we should not return a new one but use:
    IEnumerable<int> emptyEnumerable = Enumerable.Empty<int>();
    int[] emptyArray = Array.Empty<int>();
    //Therefore, no memory will be allocated
}