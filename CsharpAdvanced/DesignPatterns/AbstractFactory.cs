using System.Diagnostics;

namespace CsharpAdvanced.DesignPatterns;

//The Abstract Factory design pattern provides an interface for creating families of related or dependent objects without specifying their concrete classes.
//It is highly used

#region Factories

// The Abstract Factory interface declares a set of methods that return different abstract products.
// These products are called a "family" and are related by a high-level theme or concept.
// Products of one family are usually able to collaborate among themselves.
// A family of products may have several variants, but the products of one variant are incompatible with products of another.
public interface IAbstractFactory
{
    IAbstractProductA CreateProductA();

    IAbstractProductB CreateProductB();
}

// Concrete Factories produce a family of products that belong to a single variant.
// The factory guarantees that resulting products are compatible.
// Note that signatures of the Concrete Factory's methods return an abstract product, while inside the method a concrete product is instantiated.
class ConcreteFactory1 : IAbstractFactory
{
    public IAbstractProductA CreateProductA()
    {
        return new ConcreteProductA1();
    }

    public IAbstractProductB CreateProductB()
    {
        return new ConcreteProductB1();
    }
}

// Each Concrete Factory has a corresponding product variant.
class ConcreteFactory2 : IAbstractFactory
{
    public IAbstractProductA CreateProductA()
    {
        return new ConcreteProductA2();
    }

    public IAbstractProductB CreateProductB()
    {
        return new ConcreteProductB2();
    }
}

#endregion Factories

#region Products

// Each distinct product of a product family should have a base interface.
// All variants of the product must implement this interface.
public interface IAbstractProductA
{
    string UsefulFunctionA();
}

// Concrete Products are created by corresponding Concrete Factories.
class ConcreteProductA1 : IAbstractProductA
{
    public string UsefulFunctionA()
    {
        return "The result of the product A1.";
    }
}

class ConcreteProductA2 : IAbstractProductA
{
    public string UsefulFunctionA()
    {
        return "The result of the product A2.";
    }
}

// Here's the base interface of another product.
// All products can interact with each other, but proper interaction is possible only between products of the same concrete variant.
public interface IAbstractProductB
{
    // Product B is able to do its own thing...
    string UsefulFunctionB();

    // ...but it also can collaborate with the ProductA.
    //
    // The Abstract Factory makes sure that all products it creates are of
    // the same variant and thus, compatible.
    string AnotherUsefulFunctionB(IAbstractProductA collaborator);
}

// Concrete Products are created by corresponding Concrete Factories.
class ConcreteProductB1 : IAbstractProductB
{
    public string UsefulFunctionB()
    {
        return "The result of the product B1.";
    }

    // The variant, Product B1, is only able to work correctly with the
    // variant, Product A1. Nevertheless, it accepts any instance of
    // AbstractProductA as an argument.
    public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
    {
        var result = collaborator.UsefulFunctionA();

        return $"The result of the B1 collaborating with the ({result})";
    }
}

class ConcreteProductB2 : IAbstractProductB
{
    public string UsefulFunctionB()
    {
        return "The result of the product B2.";
    }

    // The variant, Product B2, is only able to work correctly with the
    // variant, Product A2. Nevertheless, it accepts any instance of
    // AbstractProductA as an argument.
    public string AnotherUsefulFunctionB(IAbstractProductA collaborator)
    {
        var result = collaborator.UsefulFunctionA();

        return $"The result of the B2 collaborating with the ({result})";
    }
}

#endregion Products


// The client code works with factories and products only through abstract types: AbstractFactory and AbstractProduct.
// This lets you pass any factory or product subclass to the client code without breaking it.
class Client
{
    public void InvokeClientExamples()
    {
        // The client code can work with any concrete factory class.
        Debug.WriteLine("Client: Testing client code with the first factory type...");
        ClientMethod(new ConcreteFactory1());

        Debug.WriteLine("Client: Testing the same client code with the second factory type...");
        ClientMethod(new ConcreteFactory2());
    }

    public void ClientMethod(IAbstractFactory factory)
    {
        var productA = factory.CreateProductA();
        var productB = factory.CreateProductB();

        Debug.WriteLine(productB.UsefulFunctionB());
        Debug.WriteLine(productB.AnotherUsefulFunctionB(productA));
    }
}


#region Concrete Example (Food chain)

/// <summary>
/// MainApp startup class for Real-World
/// Abstract Factory Design Pattern.
/// </summary>
class MainApp
{
    /// <summary>
    /// Entry point into console application.
    /// </summary>
    public static void Main()
    {
        // Create and run the African animal world
        ContinentFactory africa = new AfricaFactory();
        AnimalWorld world = new AnimalWorld(africa);
        world.RunFoodChain();
        // Create and run the American animal world
        ContinentFactory america = new AmericaFactory();
        world = new AnimalWorld(america);
        world.RunFoodChain();
        // Wait for user input
        Console.ReadKey();
    }
}
/// <summary>
/// The 'AbstractFactory' abstract class
/// </summary>
abstract class ContinentFactory
{
    public abstract Herbivore CreateHerbivore();
    public abstract Carnivore CreateCarnivore();
}
/// <summary>
/// The 'ConcreteFactory1' class
/// </summary>
class AfricaFactory : ContinentFactory
{
    public override Herbivore CreateHerbivore()
    {
        return new Wildebeest();
    }
    public override Carnivore CreateCarnivore()
    {
        return new Lion();
    }
}
/// <summary>
/// The 'ConcreteFactory2' class
/// </summary>
class AmericaFactory : ContinentFactory
{
    public override Herbivore CreateHerbivore()
    {
        return new Bison();
    }
    public override Carnivore CreateCarnivore()
    {
        return new Wolf();
    }
}
/// <summary>
/// The 'AbstractProductA' abstract class
/// </summary>
abstract class Herbivore
{
}
/// <summary>
/// The 'AbstractProductB' abstract class
/// </summary>
abstract class Carnivore
{
    public abstract void Eat(Herbivore h);
}
/// <summary>
/// The 'ProductA1' class
/// </summary>
class Wildebeest : Herbivore
{
}
/// <summary>
/// The 'ProductB1' class
/// </summary>
class Lion : Carnivore
{
    public override void Eat(Herbivore h)
    {
        // Eat Wildebeest
        Console.WriteLine(this.GetType().Name +
            " eats " + h.GetType().Name);
    }
}
/// <summary>
/// The 'ProductA2' class
/// </summary>
class Bison : Herbivore
{
}
/// <summary>
/// The 'ProductB2' class
/// </summary>
class Wolf : Carnivore
{
    public override void Eat(Herbivore h)
    {
        // Eat Bison
        Console.WriteLine(this.GetType().Name +
            " eats " + h.GetType().Name);
    }
}
/// <summary>
/// The 'Client' class 
/// </summary>
class AnimalWorld
{
    private Herbivore _herbivore;
    private Carnivore _carnivore;

    // Constructor
    public AnimalWorld(ContinentFactory factory)
    {
        _carnivore = factory.CreateCarnivore();
        _herbivore = factory.CreateHerbivore();
    }
    public void RunFoodChain()
    {
        _carnivore.Eat(_herbivore);
    }
}

#endregion