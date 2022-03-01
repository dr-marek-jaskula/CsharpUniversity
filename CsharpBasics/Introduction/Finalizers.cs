using System.Diagnostics;
using System.Text;

namespace CsharpBasics.Introduction;

public class Finalizers
{
    //Finalizes (historically referred to as destructors)
    //are used to perform any necessary final clean-up when a class instance is being collected by the garbage collector
    //In most cases, you can avoid writing a finalizer by using the System.Runtime.InteropServices.SafeHandle or derived classes to wrap any unmanaged handle.

    /*
    Finalizers cannot be defined in structs. They are only used with classes. (strucs are value types)
    A class can only have one finalizer.
    Finalizers cannot be inherited or overloaded.
    Finalizers cannot be called. They are invoked automatically.
    A finalizer does not take modifiers or have parameters.
     */

    static public void InvokeFinalizersExample()
    {
        Car car = new Car();
        Destroyer destroyer = new Destroyer();
    }
}

internal class Car
{
    ~Car() // finalizer
    {
        Console.WriteLine("Cleaning...");
    }
}

public class Destroyer
{
    public override string ToString() => GetType().Name;

    //finalizer defined using expression body definition
    ~Destroyer() => Console.WriteLine($"The {ToString()} finalizer is executing.");
}