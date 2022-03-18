using System.Linq.Expressions;

namespace CsharpAdvanced.DelegatesEvents;

public class DelegateTypes
{
    //A delegate is a type that represents references to methods with a particular parameter list and return type.
    //When you instantiate a delegate, you can associate its instance with any method with a compatible signature and return type.
    //You can invoke (or call) the method through the delegate instance.

    //There are three delegate types that are implemented by default into c#:
    //1. Action
    //2. Predicate
    //3. Func

    //Each of them are introduced to simplify the process of creating the delegates, and in nearly all circumstances they are good enough to deal with tasks.

    #region 1. Action

    //This is a delegate with "void" return type. It can have zero or more parameters. 
    //Therefore, "Action" is just a delegate:
    public delegate void MyCustomAction(); //no input, void return
    public delegate void MyCustomAction2(int someNumber, string someString); //two input variables, void return
    //We can define custom action like that:
    MyCustomAction myAction = () => { Console.WriteLine("This is custom action"); };
    MyCustomAction2 myAction2 = (number, text) => { Console.WriteLine($"This is custom action {number}, {text}"); };

    //However c# provide a default Action, that we can define:
    Action defaultAction = () => { Console.WriteLine("This is default action"); };
    Action<int, string> defaultAction2 = (number, text) => { Console.WriteLine($"This is default action {number}, {text}"); };

    #endregion

    #region 2. Predicate

    //This is a delegate with "bool" return type and one parameter.
    //Therefore, "Predicate" is just a delegate:
    public delegate bool MyCustomPredicate(int someNumber); //single input, bool return
    public delegate bool MyCustomPredicate2(DateOnly dateOnly); //single input, bool return
    //We can define custom predicates like that:
    MyCustomPredicate myPredicate = myNumber => myNumber > 10;
    MyCustomPredicate2 myPredicate2 = myDate => myDate.Day > 5;

    //However c# provide a default Predicate, that we can define:
    Predicate<int> defaultPredicate = myNumber => myNumber > 10;
    Predicate<DateOnly> defaultPredicate2 = myDate => myDate.Day > 5;

    #endregion

    #region 3. Func

    //This is a delegate with given return type and parameters (zero, single or multiple).
    //Therefore, "Func" is just a delegate:
    public delegate string MyCustomFunc(); //no input, string return
    public delegate int MyCustomFunc2(int someNumber, string text); //two inputs, int return
    //We can define custom func like that:
    MyCustomFunc myFunc = () => "Hello";
    MyCustomFunc2 myFunc2 = (myNumber, myText) => myNumber + myText.Length;

    //However c# provide a default Func, that we can define (last generic type is the return type):
    Func<string> defaultFunc = () => "Hello";
    Func<int, string, int> defaultFunc2 = (myNumber, myText) => myNumber + myText.Length;

    #endregion

    public static void InvokeDelegateTypesExamples()
    {
        //Expression Represents a strongly typed lambda expression as a data structure in the form of an expression tree. This class cannot be inherited.

        // Lambda expression as executable code.
        Func<int, bool> deleg = i => i < 5;
        // Invoke the delegate and display the output.
        Console.WriteLine("deleg(4) = {0}", deleg(4));

        // Lambda expression as data in the form of an expression tree.
        Expression<Func<int, bool>> expr = i => i < 5;
        // Compile the expression tree into executable code.
        Func<int, bool> deleg2 = expr.Compile();
        // Invoke the method and print the output.
        Console.WriteLine("deleg2(4) = {0}", deleg2(4));
    }
}






