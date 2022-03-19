using System.Diagnostics;

namespace CsharpAdvanced.Attributes;

public class ThreadAttributes
{
    //The [ThreadStatic] creates isolated versions of the same variable in each thread.

    [ThreadStatic] public static int i; // Declaration of the variable i with ThreadStatic Attribute.

    public static void InvokeThreadStaticAttributeExamples()
    {
        new Thread(() =>
        {
            for (int x = 0; x < 10; x++)
            {
                i+=1;
                Debug.WriteLine($"Thread A: {i}"); // Uses one instance of the i variable.
            }
        }).Start();

        new Thread(() =>
        {
            for (int x = 0; x < 10; x++)
            {
                i+=2;
                Debug.WriteLine("Thread B: {i}"); // Uses another instance of the i variable.
            }
        }).Start();
    }
}