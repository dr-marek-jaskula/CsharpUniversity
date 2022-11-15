using System.Diagnostics;

namespace CsharpAdvanced.DelegatesEvents;

//Delegates are class members that are "delegated" to ensure communication between different classes.
//For example in some long running process, we can delegate a member to send information about the progress of this process

//Delegates can serve also just a method pointers, and many developers treats them just like that
//We can create w custom delegate using "delegate" keyword or use one of three type in c#: Func, Action, Predicate

//In this file we will consider only the custom delegates and we will present the class communication during long running process
//Also we will show how to point the method

public class DelegatesBasics
{
    //Mostly the delegate should be marked as public
    //We also need to specify the type of the type of the method connected with the delegate
    //At first we will just use the delegate as a simple method pointer
    public delegate void MyMethodPointer();

    //For showing that multiple methods can be assign to the delegate
    public delegate void Display(string text);

    public static void InvokeDelegatesExamples()
    {
        #region Delegate as method pointer

        //We instantiate a method pointer that is a method pointer. This object points at MyMethod
        MyMethodPointer methodPointer = new(MyMethod);

        //now we can invoke (execute) the method that is being pointed (MyMethod is parameterless)
        methodPointer.Invoke();

        //Of course we can do it just like that:
        MyMethod();

        //So now we can use "methodPointer" as an parameter of a different method
        void UseOtherMethod(MyMethodPointer pointer)
        {
            pointer.Invoke();
            pointer();
        }

        UseOtherMethod(methodPointer);

        #endregion Delegate as method pointer

        #region Delegate as communication channel

        //Basic example
        DelegateAsCommunicationChannel communcator = new();
        communcator.LongRunningProcess(Callback);

        //Other example
        //We program could be designed to change cloths when the temperature changes
        Clothes cloths = new();
        Heat heat = new();

        MonthRunning monthRunning = new();
        monthRunning.MonthPassing(MonthCallback);

        #endregion Delegate as communication channel

        #region Multiple methods inside one delegate

        //Set the delegate to cw
        Display Display = Console.WriteLine;
        //Add another cw to the delegate
        Display += Console.WriteLine;
        //Display the test two times
        Display("Simple text");
        //Remove one of the methods
        Display -= Console.WriteLine;
        //Display the test one time
        Display("Simple text");

        #endregion Multiple methods inside one delegate
    }

    //Method for delegate as pointer example
    private static void MyMethod()
    {
        Debug.WriteLine("Delegate as pointer");
    }

    //This method will we used to provide communication with DelegateAsCommunicationChannel. The return type and parameters of this method fits the delegate defined in DelegateAsCommunicationChannel
    private static void Callback(int i)
    {
        Debug.WriteLine($"{i / 100}% has finished");
    }

    public static int MonthCallback(int i)
    {
        Debug.WriteLine(i);
        return i;
    }
}

internal class DelegateAsCommunicationChannel
{
    //We declare a delegate that will provide communication during the long running process
    public delegate void MyCallBack(int i);

    //The parameter of this method has to be a delegate, because by it we will provide communication
    public void LongRunningProcess(MyCallBack method)
    {
        for (int i = 0; i < 10000; i++)
        {
            //does something and then send some data (for example message based on the iteration step) to the outer world
            method(i); //this process could run in background (other thread)
        }
    }
}

#region Other example

internal class Heat
{
    public int Temperature { get; set; }
}

internal class Clothes
{
    public string MyClothes { get; set; } = "TShape-shirt";
}

internal class MonthRunning
{
    public delegate int MonthPassingHolder(int i);

    public void MonthPassing(MonthPassingHolder holder)
    {
        for (int i = 1; i < 13; i++)
        {
            Debug.WriteLine((Month)i);
            holder(i);
            Thread.Sleep(3000);
        }
    }
}

internal enum Month
{
    January = 1,
    February,
    March,
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    December,
}

#endregion Other example