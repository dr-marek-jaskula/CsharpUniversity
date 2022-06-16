using System.Diagnostics;
using System.Net;

namespace LearningApplication2;

public class TasksAscynAwaitBook
{
    #region Private Members

    /// <summary>
    /// The event finished callback for the Thread event example
    /// </summary>
    private static event Action EventFinished = () => { }; 

    /// <summary>
    /// Whether to run the thread example
    /// </summary>
    private static bool RunThreadExamples = false;

    #endregion

    public static void InvokeTasksAsyncAwaitBookExamples()
    {
        Log("Hello World");

        //Asynchronous:
        //is if u start something and don't wait while its happening. It literally means to not occur at the same time.
        //this means not that our code returns early, but rather it doesn't sit there blocking the code while it waits (doesn't block the thread)

        //Issues with Threads
        //threads are asynchronous: as they naturally do something while the calling thread that made it doesn't wait for it. 
        //code example

        if (RunThreadExamples)
        {
            #region Thread are asynchronous

            Log("Before first thread");

            new Thread(() =>
            {
                Thread.Sleep(500);
                Log("Inside first thread");
            }).Start();

            Log("After first thread");
            Thread.Sleep(1000);

            Debug.WriteLine("End of the code example");
            Debug.WriteLine("-----------------------------");

            #endregion

            //What is the issue with Threads?
            //1. Expensive to make
            //2. Not natural to be able to resume after a thread has finished to do something related to the thread that created it.

            //Issue 1 was solver with a ThreadPool (using thread already created rather then creating new ones). However issue 2 is still an issue for thread and is one reason why Tasks were made. In order to resume work after some asynchronous operation has occured we could with a Thread:
            // 1. block your code waiting for it (no better than just doing it on same thread)
            #region Blocking Thread (Wait)

            Log("Before blocking thread");

            Thread blockingThread = new(() =>
            {
                Thread.Sleep(500);
                Log("Inside blocking thread");
            });

            blockingThread.Start();

            //Block and wait
            blockingThread.Join(); //pointless

            Log("After blocking thread");
            Thread.Sleep(1000);
            Debug.WriteLine("End of the blocking code example");
            Debug.WriteLine("-----------------------------");

            #endregion

            // 2. Constantly poll for completion, waiting for a bool flag to say done (inefficient, slow)
            #region polling Thread

            Log("Before polling thread");

            bool pollComplete = false;

            Thread pollingThread = new(() =>
            {
                Log("Inside blocking thread");
                Thread.Sleep(500);
            //set complete
            pollComplete = true;
            });

            pollingThread.Start();
            // Poll for completion

            while (!pollComplete)
            {
                Log("Polling...");
                Thread.Sleep(100);
            }

            Log("After poling thread");
            Thread.Sleep(1000);
            Debug.WriteLine("End of the polling code example");
            Debug.WriteLine("-----------------------------");

            #endregion

            // 3. Event-based callback (lose the calling thread on callback - a UI można zmieniać tylko z main Thread, a to powoduje robienie rzeczy z inne threada wiec średnio). Te twie event to to samo. And causes nesting (zapętlanie threadów i eventów itp)
            #region Event-based Wait

            Log("Before event thread");

            Thread eventThread = new(() =>
            {
                Log("Inside event thread");
                Thread.Sleep(500);
            //Fire completed event
            EventFinished();
            });

            // Hook into callback event
            EventFinished += () =>
            {
            //Called back from thread
            Log("Event thread callback on complete");
            }; 

            eventThread.Start();
            Log("After event thread");
            Thread.Sleep(1000);
            Debug.WriteLine("End of the event code example");
            Debug.WriteLine("-----------------------------");
            #endregion

            #region Event-based Wait Method 

            Log("Before event method thread");

            EventThreadCallbackMethod(() =>
            {
            //Called back from thread
            Log("Event thread callback on complete");
            });

            Log("After event method thread");
            Thread.Sleep(1000);
            Debug.WriteLine("End of the event method code example");
            Debug.WriteLine("-----------------------------");

            #endregion
        }

        // all these above solution can be used 
        // However that makes every time we want to do something asynchronous a lot of code and not easy to follow (and those solution has some problems)

        // WHAT IS THE TASK
        // =================
        // A Task encapsulates the promise of an operation completing in the future
        
        //////////////////////

        // Task, Async and Await
        // =================
        //  
        // Async in c# is mainly 2 words: async and await
        // 
        // The point is to allow easy and clean asynchronous code to be written without complex and messy code

        #region Sync vs Async Method

        Log("Before sync thread");

        // Website to fetch
        var website = "http://www.google.co.uk";

        //download the string
        WebDownloadString(website);

        Log("After sync thread");
        Console.WriteLine("------------------------");
        Log("Before async thread");

        //download the string asychronously 
        Task downloadTask = WebDownloadStringAsync(website);

        Log("After async thread");

        //Wait for task to complete (and wait, just for example)//Niby mozna by bez wait, gdyby tam wcześniej (bez tego downloadTask, tylko prawa strona) po prostu odpalić
        downloadTask.Wait(); //waiting to synchronous operation

        Console.WriteLine("---------- Another async example, slightly different ----------------");

        var task = Task.Run(async () =>
        {
            Log("Before async await thread");

            //download the string asychronously 
            await WebDownloadStringAsync(website);

            Log("After async await thread");

            Console.WriteLine("------------------------");
        });

        //Wait the main task
        task.Wait();


        #endregion

        // async and await are always used together. A method or lambda is tagged with async can then await any Task

        // When you await something, the thread which called the await is free to then return to what it was doing, while in parallel the task inside the await is now running on another thread
        // Once the task is done, it returns either to the original calling thread or carries on another thread to do the work that codes after the await   


        // Async Analogy
        // =============
        // Imagine you go to Starbucks and the entire shop is run by one person.
        // His name is Mr UI Thread. You walk in and ask Mr Thread for a Vanilla Latter.
        // He obliges and starts to make your coffee 
        // He puts the milk into the container and turns on the hot steam and proceeds to stand there and wait for the mild to reach 70 degrees.
        //
        // During this time you remember you wanted a muffin as well, so you shout over to Mr Thread and ask for a muffin... but he ignores you. He is blocked waiting for the mild to boil.
        //
        // Several minutes goes by and 3 more customers have come in and are waiting to be served. Finally the milk is finished and he completes the Latte.
        //Returning to you. You are a little annoyed at being ignored for minutes and decided to leave your muffin.
        //
        // Then he continues to serve one customer at a time, doing one job at a time.
        // Not a good situation.

        // This is what happens with single threaded application.

        // Now in order to improve business Mr Thread employ 2 new members of staff called Mr and Mrs Worker Thread. The pair work well independently and as Mr Thred takes order from the customers, he askes Mr Worker Thread to complete the order, and then without waiting for Mr Worker Thread to finish the drink, proceeds to serve next customer.

        // Once Mr Worker Thread has finished a drink, instead of having to take the drinks to the customers she asks Mrs worker Thread to serve the drinks and then without waiting he proceed to start the next order
        //The business is now a well-oiled, multi-threaded business.


        // The Synchronous part in Tasks
        // =========================== 
        // 
        //

        #region  The Synchronous Part of Tasks

        //Run some work to show the synchronous parts of the call

        Task.Run(async () =>
        {
            Log($"Before DoWork thread");

            //this will return a Task and run the lines of code inside the method
            //up until the point at which the first await is hit
            var doWorkTask = DoWorkAsync("me"); 

            //Await the task
            //This will then spin off to a new thread and come with the result (async mowi to, że robi się nowy thread dopiero przy dojsciu do await)
            await doWorkTask;

            Log($"After DoWork thread");
        }).Wait();

        Console.WriteLine("------------------------");
        #endregion

        // Async return Types
        // ===================
        // 
        // You can only return void, Task or Task<T> for a method marked as async, as the method is not complete when it returns, so no other result is valid

        #region Method 1 Getting Result of Async from Sync

        //Get the task
        var doWorkResultTask = DoWorkAndGetResultAsync("Return this");

        //Wait for it
        doWorkResultTask.Wait();

        //Get the result
        var doWorkResult = doWorkResultTask.Result;
        Console.WriteLine(doWorkResult);
        Console.WriteLine("------------------------");
        #endregion

        #region Method 2 Getting Result of Async from Sync

        Task.Run(async () =>
        {
            string doWorkResult2 = await DoWorkAndGetResultAsync("Return this 2");
            Console.WriteLine(doWorkResult2);
        }).Wait();

        Console.WriteLine("------------------------");
        #endregion

        //
        // Async Keyword
        // =================
        // the async keyword is not actually added to the method declaration signature, the only effect it has is to change the compiled code. 
        // Thats why interfaces cannot declare async, as it isn't a declarative statement its a compilation statement to change the flow of the function 


        // Consuming Ansyc Methods 
        //
        // the best way to consume or call a method that returns a task is to be async yourself in the caller method, to ultimately awaiting it. By that definition async method are naturally contagious
        //

        #region Consuming Wait
        //Store the task
        var doWorkReultTask = DoWorkAndGetResultAsync("Consume Wait");

        //Wait for it
        doWorkResultTask.Wait(); 

        //Get the result
        var workResult = doWorkResultTask.Result;
        Console.WriteLine("------------------------");
        #endregion

        //Better way
        #region Consuming via Task

        //declare the result
        var workResultViaTask = default(string);

        //Store the task
        Task.Run(async () =>
        {
            //Get result 
            workResultViaTask = await DoWorkAndGetResultAsync("Consume via Task");
        }).Wait();
        Console.WriteLine("------------------------");

        #endregion

        //
        // What happens during an Async call
        // ===========================================
        // 
        //  Code inside a function that returns a Task runs its code on the callers thread up until the first line that calls await. At this point in time:
        // 
        // 1. The current thread executing your code is released  (making your code asynchronous). This means from a normal point of view, your function has returned at this point (it has return the Task object)
        //
        // 2. Once the task you have awaited completes, your method should appear to continue from where it left off, as if it never returned from the method, so reusme on the line below the await.
        //
        // To achieve this, c# at the point of reaching the await calls:
        // 1. Stores all local variables in the scope of the method
        // 2. Stores all parameters of your method
        // 3. The "this" variable to store all class-level variables
        // 4. stores all contexts (Execution, Security, Call)

        // and on resuming to the line after the await, restores all of these values as if nothing had changes. All of this information is stored on the garbage collection heap


        // What is happening with threads during an async call
        // =====================================================
        // As you call a method that returns a "Task" and uses "async", inside the method all code, up until the first "await" statement, is run like a normal function on the thread that called it. 
        //
        // Once it hits the "await" the function returns the "Task" to the caller, and does its work thats inside tha "await" call on a new thread (or existing). 
        //
        // Once its done and effectively "after" the "await" line, execution returns to a certain thread.
        //
        // That thread is determined by first checking if the thread has an synchronization context and if it does, it asks that thread to return to. For UI threads this will return work to the UI thread itself. [on UI to the same thread that was before the await]
        //

        //Console application has no synchronization context!!
        var syncContex = SynchronizationContext.Current;

        //
        // For normal threads that have no synchronization context, the code after the "await" typically, but not always, continues on the same thread!!! that the inner work was being done on [before await], but has no requirement to resume on any specific thread
        //
        // Typically if you use "ContinueWith" instead of "await", the code inside "ContinueWith" runs on a different thread that the inner task was running on, and using "await" typically continues on the same thread.
        //

        //Show ContinueWith typically changing thread ID's
        DoWorkAsync("ContinueWith").ContinueWith(t =>
        {
            Log("ContinueWith Complete");
        }).Wait();

        Console.WriteLine("-------------------------------");

        // This also means after ever await the next line is typically on a new thread if there is no synchronization context
        // ***************************************************************
        // An exception is if you use "ConfigureAwait(false)". Then the SynchronizationContex is totally ignored and the resuming thread is treated as if there were no context
        //
        //
        // Resuming on the original thread via the synchronization context is an expensive thing (takes time) and so if you choose to not care about resuming on the same thread and want to save time you can use "ConfigureAwait" to remove the overhead
        //*****************************************************************


        //
        // Exceptions in Async calls
        // ================================
        // Any exceptions thrown that are not caught by the method itself are thrown into the Task objects value "IsFaulted" and the "Exception" property
        // 
        // If you do not await the Task, the exception will not throw on your calling thread
        //


        #region Throw on Calling Thread, Without Awaiting (Task returning function)

        Log("Before ThrowAwait");
        
        var crashedTask = ThrowAwait(true); //to nie robi crasha pomimo wywalenie exception, ponieważ jest async i Task i wtedy zwraca zepsutego Taska
        //jeśliby dało się powyżej przed ThrowBeforeAwait(true) zrobić await, to by normalnie scrashowało. Tutaj w aplikacji konsolowej sie raczej tego nie pokaże ale w UI by dało radę

        //Did it crashs?
        var isFautlted = crashedTask.IsFaulted;
        Console.WriteLine(isFautlted);

        //the exception
        Log(crashedTask.Exception.Message );
        Log(crashedTask.Exception.ToString());

        Log("After ThrowAwait");

        #endregion

        // If you await, the exception will rethrow onto the caller thread that awaited it 
        //
        // The exception to the rule is a method with async void. As it cannot be awaited, any exceptions that occur in an async void method are re-thrown like this:
        // 
        // 1. If there is a synchronization context the exception is Post back to the caller thread.
        // 2. If not, it is thrown on the thread pool
        // 

        #region Throw on Calling Thread, Without Awaiting (void function)

        Log("Before ThrowAwaitVoid");

        //uncomment if want to see crash
        // ThrowAwaitVoid(true); //tutaj crashuje całą aplikacje, zgodnie z tym jak podejrzewaliśmy

        Log("After ThrowAwaitVoid");

        #endregion

        Console.ReadLine(); 
    }

    #region Helper Methods
    /// <summary>
    /// Output a message with the curent thread ID appended
    /// </summary>
    /// <param name="messege"></param>
    private static void Log(string messege)
    {
        //write line
        Console.WriteLine($"{messege} [{Thread.CurrentThread.ManagedThreadId}]");
    }
    #endregion

    #region Thread Methods
    /// <summary>
    /// Shows an event-based thread callback via a method
    /// </summary>
    /// <param name="completed">The callback to call once the work is complete</param>
    private static void EventThreadCallbackMethod(Action completed)
    {
        //starts a new thread
        new Thread(() => 
        {
            //log it
            Log("Inside event thread method");
            Thread.Sleep(500);
            //Fire completed event
            completed();
        }).Start();
    }
    #endregion

    #region Task example Method
    /// <summary>
    /// Downloads a string from a website URL synchronously
    /// </summary>
    /// <param name="url"></param>
    private static void WebDownloadString(string url)
    {
        //synchronous pattern
        WebClient webClient = new();
        string result = webClient.DownloadString(url);
        Log($"Downloaded {url}. {result.Substring(0, 10)}");
    }

    /// <summary>
    /// Downloads a string from a website URL asynchronously
    /// </summary>
    /// <param name="url"></param>
    private static async Task WebDownloadStringAsync(string url)
    {
        //Asynchronous pattern
        WebClient webClient = new();

        //all code up to this moment is going synchronous
        string result = await webClient.DownloadStringTaskAsync(new Uri(url)); 

        Log($"Downloaded {url}. {result.Substring(0, 10)}");
    }

    /// <summary>
    /// Does some work asynchronously for somebody
    /// </summary>
    /// <param name="forWho">Who we are doing the work for</param>
    /// <returns></returns>
    private static async Task DoWorkAsync(string forWho)
    {
        Log($"Doing work for {forWho}");

        Thread.Sleep(2000);

        // Start a new task (so it runs on a different thread
        await Task.Run(async () =>
        {
            Log($"Doing work on inner thread for {forWho}");
            await Task.Delay(500);
            Log($"Done work on inner thread for {forWho}");

        });

        Log($"Done work for {forWho}");

    }

    /// <summary>
    /// Does some work asynchronously for somebody and return a result
    /// </summary>
    /// <param name="forWho">Who we are doing the wrok for</param>
    /// <returns></returns>
    private static async Task<string> DoWorkAndGetResultAsync(string forWho) 
    {
        Log($"Doing work for {forWho}");

        Thread.Sleep(2000);

        // Start a new task (so it runs on a different thread
        await Task.Run(async () =>
        {
            Log($"Doing work on inner thread for {forWho}");
            await Task.Delay(500);
            Log($"Done work on inner thread for {forWho}");

        });

        Log($"Done work for {forWho}");

        //Return what we received
        return forWho;
    }

    /// <summary>
    /// Throws an exception inside a task before the await
    /// </summary>
    /// <param name="before">Throws the exception before an await</param>
    /// <returns></returns>
    private static async Task ThrowAwait(bool before)
    {
        if(before)
            throw new ArgumentException("Ooop");

        await Task.Delay(1);
        throw new ArgumentException("Ooops");
    }

    /// <summary>
    /// Throws an exception inside a void async before await
    /// </summary>
    /// <param name="before">Throws the exception before an await</param>
    /// <returns></returns>
    private static async void ThrowAwaitVoid(bool before)
    {
        if (before)
            throw new ArgumentException("Ooop");

        await Task.Delay(1);
        throw new ArgumentException("Ooops");
    }

    #endregion
}








