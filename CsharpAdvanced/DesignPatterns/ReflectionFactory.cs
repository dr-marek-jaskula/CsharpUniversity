using System.Diagnostics;
using System.Reflection;

namespace CsharpAdvanced.DesignPatterns;

public class ReflectionFactoryCustomPatternExample
{
    public static void InvokeReflectionFactoryCustomPatternExample()
    {
        ReflectionShapeFactory reflectionShapeFactory = new();

        //Supported type
        var circle = reflectionShapeFactory.CreateShape<ReflectionCircle>();
        circle.Render();

        //Supported type, created after the factory was finished
        var trapez = reflectionShapeFactory.CreateShape<ReflectionTrapezoid>();
        trapez.Render();

        //Unsupported type
        var ufo = reflectionShapeFactory.CreateShape<Object>();
        ufo.Render();
    }
}

internal class ReflectionShapeFactory
{
    private readonly List<Type> _types = new();

    public ReflectionShapeFactory()
    {
        //Get assembly
        Assembly assembly = Assembly.GetExecutingAssembly();
        //Get all direct children of ReflectionShape
        _types.AddRange(assembly.GetTypes().Where(type => type.BaseType == typeof(ReflectionShape)));
    }

    public ReflectionShape CreateShape<T>()
    {
        if (_types.Contains(typeof(T)))
        {
            return (ReflectionShape)Activator.CreateInstance(typeof(T))!;
        }

        //If the there is for example an additional constructor with 1 parameter:
        //var parameterlessConstructor = type.GetConstructors().SingleOrDefault(c => c.GetParameters().Length == 0);
        //return parameterlessConstructor is not null ? Activator.CreateInstance(type) : Activator.CreateInstance(type, param1);

        throw new ArgumentException($"Invalid input of {MethodBase.GetCurrentMethod()!.Name}. \"{typeof(T).Name}\" type is not supported. Please choose one of supported types: {string.Join(", ", _types.Select(t => t.Name))}.");
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