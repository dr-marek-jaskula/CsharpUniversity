namespace CsharpBasics.Introduction;

public class Properties
{
    public static void InvokePropertiesExamples()
    {
        Person person = new("Mark") { Id = 1, IsHardWorking = true };
        person.Salary = 100;
        Console.WriteLine($"Mark salary is {person.Salary}");

        Person person1 = new("Tom") { Id = 2, IsManagerSon = true };
        person.Salary = 100;
        Console.WriteLine($"Tom salary is {person1.Salary}");
    }
}


#region Helpers

public class Person
{
    //this property has just a getter, so it is same as we would add readonly modifier.
    //Its value can be set only in the constructor
    public string Name { get; }

    //this property can 'init' setter, which means that is value can be set only in two cases:
    //1) in the constructor
    //2) using Object Initializer Syntax, which is like "new Person("Mark") { Id = 4 };"
    public int Id { get; init; }

    //just two simple properties with getters and setters.
    //somewhere in the memory there are created a backing field for them
    //properties are preferred. Try to avoid creating public fields if possible
    public bool IsHardWorking { get; set; }
    public bool IsManagerSon { get; set; }

    //private backing field for Salary property. _underscoreCamelCase is preferred 
    private double _salary;

    public double Salary
    {
        get { if (!IsHardWorking) return _salary; return 1.1 * _salary; } //when the value is requested the getter is executed
        set { if (!IsManagerSon) _salary = value; _salary = 2 * value; } //when the program want to set the value of this property, the setter is executed. 'value' keyword represent the value the is plan to be setted
    }

    public Person(string name)
    {
        Name = name;
    }
}

#endregion
