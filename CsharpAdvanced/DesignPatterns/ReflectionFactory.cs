using System.Diagnostics;

namespace CsharpAdvanced.DesignPatterns;

public class ReflectionFactoryCustomPatternExample
{
    public static void InvokeReflectionFactoryCustomPatternExample()
    {
        //Supported type
        var circle = ReflectionShapeFactory.CreateShape<ReflectionCircle>();
        circle.Render();

        //Supported type, created after the factory was finished
        var trapez = ReflectionShapeFactory.CreateShape<ReflectionTrapezoid>();
        trapez.Render();
    }
}

internal class ReflectionShapeFactory
{
    public static ReflectionShape CreateShape<T>()
        where T : ReflectionShape
    {
        return (ReflectionShape)Activator.CreateInstance(typeof(T))!;
    }
}

internal abstract class ReflectionShape
{
    public int X { get; set; }
    public int Y { get; set; }

    public abstract void Render();
}

internal class ReflectionCircle : ReflectionShape
{
    public override void Render()
    {
        Debug.WriteLine("Render circle");
    }
}

internal class ReflectionRectangle : ReflectionShape
{
    public override void Render()
    {
        Debug.WriteLine("Render rectangle");
    }
}

internal class ReflectionTriangle : ReflectionShape
{
    public override void Render()
    {
        Debug.WriteLine("Render triangle");
    }
}

//Added after the factory was finished
internal class ReflectionTrapezoid : ReflectionShape
{
    public override void Render()
    {
        Debug.WriteLine("Render trapezoid");
    }
}