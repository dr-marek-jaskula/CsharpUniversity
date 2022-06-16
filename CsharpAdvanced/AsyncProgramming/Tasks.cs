using System.Diagnostics;

namespace CsharpAdvanced.AsyncProgramming;

public class Tasks
{
    //Task provides better methodology to use ThreadPool in the most convenient way

    public static void InvokeTasksExamples()
    {
        Debug.WriteLine($"Running Thread is {Thread.CurrentThread.ManagedThreadId}");

        //At first we create an Action, just for demo purpose
        Action<object?> action = (object? obj) =>
        {
            Debug.WriteLine($"Task={Task.CurrentId}, obj={obj}, Thread={Thread.CurrentThread.ManagedThreadId}");
        };

        // Create a task but do not start it.
        Task taskOne = new(action, "alpha");

        // Construct a started task
        Task taskTwo = Task.Factory.StartNew(action, "beta");

        // Block the main thread to demonstrate that taskTwo is executing
        taskTwo.Wait();

        // Launch taskOne 
        taskOne.Start();
        Debug.WriteLine($"TaskOne has been launched. (Main Thread={Thread.CurrentThread.ManagedThreadId})");
        // Wait for the task to finish.
        taskOne.Wait();

        // Construct a started task using Task.Run.
        string taskData = "delta";

        Task taskThree = Task.Run(() => 
        {
            Debug.WriteLine($"Task={Task.CurrentId}, obj={taskData}, Thread={Thread.CurrentThread.ManagedThreadId}");
        });

        // Wait for the task to finish.
        taskThree.Wait();

        // Construct a task
        Task taskFour = new(action, "gamma");

        // Run it synchronously
        taskFour.RunSynchronously();

        // Although the task was run synchronously, it is a good practice to wait for it in the event exceptions were thrown by the task.
        taskFour.Wait();

        //Task instances may be created in a variety of ways.
        //The most common approach, which is available starting with the .NET is to call the static Run method.
        //The Run method provides a simple way to start a task using default values and without requiring additional parameters.

        Task.Run(() =>
        {
            Debug.WriteLine($"Up you go1 + {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            Debug.WriteLine("Let us fight like a gentelment1");
        });

        Task.Run(async () =>
        {
            Console.WriteLine($"Up you go2 + {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(3000); //this task is done async
            Console.WriteLine("Let us fight like a gentelment2");
        });

        TaskingSpree3();
        TaskingSpree4();

        //Just not to finish the program
        var tsc = new TaskCompletionSource<bool>(); 
        var b = tsc.Task.Result;
    }

    //For demo purpose, mostly we do not use "void" but "Task"
    public static async void TaskingSpree3()
    {
        //The function execution will wait until the task is done, due to the await keyword (awaiting tasks is a common)
        await Task.Run(() => 
        {
            Debug.WriteLine($"Execute: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Debug.WriteLine("After execute");
            Thread.Sleep(1000);
        });

        //executed after task is done
        Debug.WriteLine($"End of the this method. On Thread {Thread.CurrentThread.ManagedThreadId}");
    }

    public static async void TaskingSpree4() 
    {
        await Task.Run(async () => 
        {
            Debug.WriteLine($"Execute: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(1000);
            Debug.WriteLine("After execute");
        });

        Debug.WriteLine($"End of the this method. On Thread {Thread.CurrentThread.ManagedThreadId}");
    }
}