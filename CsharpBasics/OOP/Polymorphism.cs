//Polymorphism provides the ability to override the definition of the method or a class member marked with "virtual" keyword.
//Let us notice here that abstract members in abstract classes can be also modified in the child classes.

//the using static was used here to avoid writing "Math.Pow". 
using static System.Math;

namespace CsharpBasics.OOP;

public class Shapre
{
    //this property is marked with "virtual" keyword to make is possible to override
    public virtual int SecretProperty { get; set; } = 100;

    //this method is marked with "virtual" keyword to make is possible to override
    public virtual double Area()
    {
        //default implementation. In abstract classes the default implementation can be avoided.
        return 0;
    }
}

public class Circle : Shapre
{
    public override int SecretProperty { get; set; } = 1;

    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double Area()
    {
        return PI * Pow(Radius, 2);
    }
}

public class Square : Shapre
{
    public override int SecretProperty { get; set; } = 2;

    public double Length { get; set; }
    public Square(double length)
    {
        Length = length;
    }

    public override double Area()
    {
        return Pow(Length, 2);
    }
}

public class Rectangle : Shapre
{
    public override int SecretProperty { get; set; } = 3;

    public double Height { get; set; }
    public double Width { get; set; }
    public Rectangle(double height, double width)
    {
        Height = height;
        Width = width;
    }
    public override double Area()
    {
        return Height * Width;
    }
}