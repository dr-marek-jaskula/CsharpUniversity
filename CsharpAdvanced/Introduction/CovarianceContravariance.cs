using CsharpAdvanced.Attributes;
using System.Diagnostics;

namespace CsharpAdvanced.Introduction;

//Covariance and Contravariance can be used only for interfaces and delegates

//For interfaces:

#region Explanation

//Contravariance is when we use "in" keyword before the generic type
//This means that the type can be only a parameter type, not a return type
internal interface IBuildingManager<in T>
{
    void Clean(T item);

    void Add(T item);

    //T Sell(); //would cause an error
}

//Covariance is when we use "out" keyword before the generic type
//This means that the type can be only a return type, not a parameter type
internal interface ICarManager<out T>
{
    T GetByName(string name);

    //void Add(T item); // would cause an error
}

//We can use them both
internal interface IGarageManager<out T, in Z>
{
    T GetGarageContent(string garageName);

    void Add(Z item);
}

//We can use covariance and contravariance is both applied
internal interface IMechanic<G, M>
{
    G GetGarageOfMechanic(M mechanic);

    void HireMechanicInGarage(M mechanic, G garage);
}

#endregion Explanation

public class CovarianceContravariance
{
    public static void InvokeCovarianceContravarianceExamples()
    {
        IContravariant<object> iobj = new Sample<object>();
        IContravariant<string> istr = new Sample<string>();
        iobj.SetCw(new object());
        iobj.GetCw();
        istr.SetCw("Hello");
        istr.GetCw();

        // Ccan assign iobj to istr because the IContravariant interface is contravariant.
        istr = iobj;
        istr.GetCw();
    }
}

// Contravariant interface.
internal interface IContravariant<in A>
{
    void SetCw(A item);

    void GetCw();
}

// Extending contravariant interface.
internal interface IExtContravariant<in A> : IContravariant<A>
{
}

// Implementing contravariant interface.
internal class Sample<A> : IContravariant<A>
{
    public A? superItem;

    public void SetCw(A item)
    {
        superItem = item;
    }

    public void GetCw()
    {
        Debug.WriteLine(superItem);
    }
}