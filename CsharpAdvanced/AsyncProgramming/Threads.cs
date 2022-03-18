using System.Diagnostics;

namespace CsharpAdvanced.AsyncProgramming;

public class Threads
{
    //The whole program is being executed in the single thread that is call the "main" thread.
    //However, sometimes we would like to do some tasks simultaneously.
    //For this purpose we need to examine "Threads" in c#

    public static void InvokeThreadsExamples()
    {
        //This command will be executed in the main thread
        Debug.WriteLine($"Hello from main thread {Thread.CurrentThread.Name}, {Thread.CurrentThread.ManagedThreadId}");

        //We create a new thread called "threadOne"
        //We need to provide the ThreadStart delegate. that is just a parameterless Action
        Thread threadOne = new(() => { Thread.Sleep(2000); Debug.WriteLine($"Hello from ThreadOne: {Thread.CurrentThread.Name}, {Thread.CurrentThread.ManagedThreadId}"); });

        //To execute the other thread, we use method "Start"
        threadOne.Start();

        //For example we will download 
        Thread threadOneVersionTwo = new(() =>
        {
            Debug.WriteLine("Start downloading...");
            var webClient = new HttpClient();
            var html = webClient.GetStringAsync("https://angelsix.com/");
            Debug.WriteLine("Done");
        });
        threadOneVersionTwo.Start();

        //This command will be executed in the main thread
        Debug.WriteLine($"This is still main thread: {Thread.CurrentThread.Name}, {Thread.CurrentThread.ManagedThreadId}");

        //We can specify the thread parameter "isBeckground"
        //By default "isBackground" is "false"
        //if "isBackground" is set to "true", the application (and this thread) will continue to work, even if the main thread would end
        Thread ThreadTwo = new(() =>
        {
            Thread.Sleep(500000);
            Console.WriteLine($"Hello from ThreadTwo: {Thread.CurrentThread.Name}, {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(500000);
        })
        { IsBackground = true };

        //Execute the thread
        ThreadTwo.Start();

        Debug.WriteLine($"Hello from main thread {Thread.CurrentThread.Name}, {Thread.CurrentThread.ManagedThreadId}");
    }
}
