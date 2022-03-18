using System.Diagnostics;

namespace CsharpAdvanced.AsyncProgramming;

public class ThreadPools
{
    //Creating and deleting threads is expensive.
    //Therefore, rather deleting the thread after its work is done, we can store it and use again
    //This concept leads us to "ThreadPool" class
    //ThreadPool is a pool of threads that are stored

    //The whole user interface (UI) runs on the main thread. 
    //It is hard to modify UI from other threads then main

    public static void InvokeThreadPoolsExamples()
    {
        //an expensive use of thread (create and destroy)
        Enumerable.Range(0, 10).ToList().ForEach(f =>
        {
            new Thread(() =>
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} on");
                Thread.Sleep(1000);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} off");
            }).Start();
        });

        //Now we will use the ThreadPool (more efficient way)
        Enumerable.Range(0, 100).ToList().ForEach(f =>
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} on");
                Thread.Sleep(1000);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} off");
            });
        });

        Console.WriteLine($"Main Thread: {Thread.CurrentThread.ManagedThreadId}");


        Thread threadOneVersionTwo = new(() =>
        {
            Debug.WriteLine("Start downloading...");
            var webClient = new HttpClient();
            var html = webClient.GetStringAsync("https://angelsix.com/");
            Debug.WriteLine("Done");
        });
        threadOneVersionTwo.Start();

        //The Join method block the main thread until the thread is finished
        threadOneVersionTwo.Join();

        Thread.Sleep(3000);
        Console.WriteLine("All done");

        //This will make that a main thread wont end before tasks are finished
        var tsc = new TaskCompletionSource<bool>();
        var b = tsc.Task.Result;
    }
}