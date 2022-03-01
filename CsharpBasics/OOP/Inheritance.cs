namespace CsharpBasics.OOP;

//In this file the inheritance topic is presented
//Moreover, method overriding and method hiding is demonstrated

public class Inheritance
{
    //in c# child can have only one parent, i.e. a class can inherit only for single class
    //however one parent can have multiple children, i.e. many different classes can inherit from one class

    public static void InvokeInheritanceExamples()
    {
        Person johnKindy = new("John", "Kindy", 94654);
        johnKindy.GoToWork();
        johnKindy.Relax();

        Manager elizabethMoon = new("Elizabeth", "Moon", 93544, 3600);
        elizabethMoon.GoToWork();
        elizabethMoon.Relax();

    }
}

#region Basic inheritance

//parent class
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int PostalCode { get; set; }

    public Person(string name, string lastName, int postalCode)
    {
        FirstName = name;
        LastName = lastName;
        PostalCode = postalCode;
    }

    //a simple method
    public void GoToWork()
    {
        Console.WriteLine($"{FirstName} {LastName} goes to work...");
    }

    //a virtual method. Virtual keyword allows to use the default implementation
    public virtual void Relax() 
    {
        Console.WriteLine($"{FirstName} {LastName} drinks coffee");
    }
}

public class Manager : Person
{
    public int Salary { get; set; }

    public Manager(string FirstName, string LastName, int PostalCode, int salary) : base(FirstName, LastName, PostalCode)
    {
        Salary = salary;
    }

    //the new keyword is used for "method hiding" - if the method from the parent class is not marked as virtual
    //this keyword "new" tell the compiler that method hiding was intended
    //hiding is rarely used
    public new void GoToWork() 
    {
        Console.WriteLine("Person \"GoToWork\":\n");
        base.GoToWork();
        Console.WriteLine("Manager part of \"GoToWork\":\n");
        Console.WriteLine($"Manager {FirstName} {LastName} goes to work...");
    }

    //due to the fact that Relax method in the parent class was virtual, we override it using "override" keyword
    public override void Relax() 
    {
        Console.WriteLine("Person \"Relax\":\n");
        base.Relax();
        Console.WriteLine("Manager part of \"Relax\":\n");
        Console.WriteLine($"Manager {FirstName} {LastName} drink green tea...");
    }

    //The "new" and the "virtual" keyword can be used both. This would cause to make a new default method, ready to be override.
}

#endregion

#region Abstract class inheritance

//An abstract class with most generic data and methods
public abstract class CelestialBody
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    //abstract method can have default implementations, but it is not necessary
    public abstract void Shine();

    public CelestialBody(string name)
    {
        Name = name;
    }
}

//the Planet class inherits form the CelestialBody class, however is 
public class Planet : CelestialBody 
{
    public double Radius { get; set; }

    public Planet(string name, double radius) : base(name)
    {
        Radius = radius;
    }

    //overrides the abstract method without the implementation
    public override void Shine()
    {
        Console.WriteLine("Planets also reflect light");
    }
}

#endregion

#region Constructor inheritance

//go to: "Csharp.Basics.OOP.ClassesConstructors.cs"

#endregion


