namespace CsharpAdvanced.NET_7__csharp_11;

//We can now add static abstract members to interfaces.
//So when giving the constraint on the type, we are able to use the static member 
public sealed class StaticAbstractOperatorVirtualInterfaceMembers
{
    void DisplayShape<TShape>(TShape shape) 
        where TShape : IShape
    {
        Console.WriteLine(TShape.Color);
        Console.WriteLine(shape.CalculateArea());
    }

    IShape GetGreaterShape<TShape, TShape2>(TShape shape, TShape2 shape2) 
        where TShape : IShape
        where TShape2 : IShape
    {
        return shape > shape2 ? shape : shape2;
    }
}

public interface IShape
{
    double CalculateArea();
    static abstract string? Color { get; set; }
    static virtual bool operator >(IShape left, IShape right)
    {
        return left.CalculateArea() > right.CalculateArea();
    }
    static virtual bool operator <(IShape left, IShape right)
    {
        return left.CalculateArea() < right.CalculateArea();
    }
}

public sealed class Rectangle : IShape
{
    private readonly double _width;
    private readonly double _height;

    public Rectangle(double width, double height)
    {
        _width = width;
        _height = height;
    }

    public static string? Color { get; set; } = "red";

    public double CalculateArea()
    {
        return _width * _height;
    }
}

public sealed class Circle : IShape
{
    private readonly double _radius;

    public Circle(double radius)
    {
        _radius = radius;
    }

    public static string? Color { get; set; } = "green";

    public double CalculateArea()
    {
        return _radius * _radius * Math.PI;
    }
}
