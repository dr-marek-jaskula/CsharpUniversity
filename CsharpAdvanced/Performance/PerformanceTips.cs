namespace CsharpAdvanced.Performance;

public class PerformanceTips
{
    //In order to improve the performance of the application we can:
    //1. Seal all classes we can
    /*
    Sealing classes will result in a small performance boost and will keep our system more secured.

    Therefore, especially for records we use for Data Transfer Object, use the **sealed** keyword.

    The proper way is to seal a class in advance (change class snippet and class template) and then remove the **sealed** keyword if the class is supposed to be a father.

    To seal the **Program** class in a Top Level Statement we can add 

    ```csharp
    sealed partial class Program { }
    ```

    at the end of a file.
     */

    //2. Use Spans when possible (and Memory)

    //3. If we want to return an empty array or empty list we should not return a new one but use:
    IEnumerable<int> emptyEnumerable = Enumerable.Empty<int>();
    int[] emptyArray = Array.Empty<int>();
    //Therefore, no memory will be allocated
}