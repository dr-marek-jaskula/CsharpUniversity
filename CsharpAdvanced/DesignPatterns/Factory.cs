namespace CsharpAdvanced.DesignPatterns;

public class FactoryPatternExample
{
    public void InvokeFactoryPatternExample()
    {
        var shapeFactory = new ShapeFactory();
        var circle = shapeFactory.CreateShape(ShapeType.Circle);
        circle.Render();

        var triangle = shapeFactory.CreateShape(ShapeType.Triangle);
        triangle.Render();
    }
}

internal enum ShapeType
{
    Circle,
    Rectangle,
    Triangle
}

internal class ShapeFactory
{
    public Shape CreateShape(ShapeType type)
    {
        return type switch
        {
            ShapeType.Circle => new Circle(),
            ShapeType.Rectangle => new Rectangle(),
            ShapeType.Triangle => new Triangle(),
            _ => throw new Exception($"Shape type {type} is not handled"),
        };
    }
}

internal abstract class Shape
{
    public int X { get; set; }
    public int Y { get; set; }

    public abstract void Render();
}

internal class Circle : Shape
{
    public override void Render()
    {
        Console.WriteLine("Render circle");
    }
}

internal class Rectangle : Shape
{
    public override void Render()
    {
        Console.WriteLine("Render rectangle");
    }
}

internal class Triangle : Shape
{
    public override void Render()
    {
        Console.WriteLine("Render triangle");
    }
}