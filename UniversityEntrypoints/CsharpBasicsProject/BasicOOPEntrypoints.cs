using Xunit;
using CsharpBasics.OOP;

namespace UniversityEntrypoints.CsharpBasicsProject;

public class BasicOOPEntrypoints
{
    [Fact]
    public void ClassesConstructorsEntrypoint()
    {
        Constructors.InvokeConstructors();
    }

    [Fact]
    public void MethodOverloadingEntrypoint()
    {
        MethodOverloading.BuildGame();
        MethodOverloading.BuildGame(2);
        MethodOverloading.BuildGame("Much fun");
        MethodOverloading.BuildGame(true);
        MethodOverloading.BuildGame(4, "Best game", "So much fun", "Great", "Boring");
        MethodOverloading.BuildGame(10, new string[] {"Best game", "So much fun", "Great", "Boring"});
    }

    [Fact]
    public void PolymorphismEntrypoint()
    {
        Circle circle = new(4);
        System.Console.WriteLine(circle.SecretProperty);
        System.Console.WriteLine(circle.Area());

        Rectangle rectangle = new(4.3, 2.4);
        System.Console.WriteLine(rectangle.SecretProperty);
        System.Console.WriteLine(rectangle.Area());

        Square square = new(40);
        System.Console.WriteLine(square.SecretProperty);
        System.Console.WriteLine(square.Area());
    }

    [Fact]
    public void InheritanceEntrypoint()
    {
        Inheritance.InvokeInheritanceExamples();
    }

    [Fact]
    public void StructuresEntrypoint()
    {
        Structures.InvokeStructureExamples();
    }
}