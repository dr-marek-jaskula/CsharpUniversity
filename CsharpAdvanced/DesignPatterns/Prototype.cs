namespace CsharpAdvanced.DesignPatterns;

public class Person
{
    public int Age;
    public DateTime BirthDate;
    public string Name;
    public IdInfo IdInfo;

    public Person ShallowCopy()
    {
        return (Person)MemberwiseClone();
    }

    public Person DeepCopy()
    {
        Person clone = (Person)MemberwiseClone();
        clone.IdInfo = new IdInfo(IdInfo.IdNumber);
        clone.Name = string.Copy(Name);
        return clone;
    }
}

public class IdInfo
{
    public int IdNumber;

    public IdInfo(int idNumber)
    {
        IdNumber = idNumber;
    }
}

class Program2
{
    static void LearnPrototype(string[] args)
    {
        Person p1 = new();
        p1.Age = 42;
        p1.BirthDate = Convert.ToDateTime("1977-01-01");
        p1.Name = "Jack Daniels";
        p1.IdInfo = new IdInfo(666);

        // Perform a shallow copy of p1 and assign it to p2.
        Person p2 = p1.ShallowCopy();
        // Make a deep copy of p1 and assign it to p3.
        Person p3 = p1.DeepCopy();

        // Display values of p1, p2 and p3.
        Console.WriteLine("Original values of p1, p2, p3:");
        Console.WriteLine("   p1 instance values: ");
        DisplayValues(p1);
        Console.WriteLine("   p2 instance values:");
        DisplayValues(p2);
        Console.WriteLine("   p3 instance values:");
        DisplayValues(p3);

        // Change the value of p1 properties and display the values of p1,
        // p2 and p3.
        p1.Age = 32;
        p1.BirthDate = Convert.ToDateTime("1900-01-01");
        p1.Name = "Frank";
        p1.IdInfo.IdNumber = 7878;
        Console.WriteLine("\nValues of p1, p2 and p3 after changes to p1:");
        Console.WriteLine("   p1 instance values: ");
        DisplayValues(p1);
        Console.WriteLine("   p2 instance values (reference values have changed):");
        DisplayValues(p2);
        Console.WriteLine("   p3 instance values (everything was kept the same):");
        DisplayValues(p3);
    }

    public static void DisplayValues(Person p)
    {
        Console.WriteLine("Name: {0:s}, Age: {1:d}, BirthDate: {2:MM/dd/yy}",
            p.Name, p.Age, p.BirthDate);
        Console.WriteLine("ID#: {0:d}", p.IdInfo.IdNumber);
    }
}

#region Other

public abstract class AbstractCloneable : ICloneable
{
    public object Clone()
    {
        var clone = (AbstractCloneable)this.MemberwiseClone();
        HandleCloned(clone);
        return clone;
    }

    protected virtual void HandleCloned(AbstractCloneable clone)
    {
        //Nothing particular in the base class, but maybe useful for children.
        //Not abstract so children may not implement this if they don't need to.
    }
}


public class ConcreteCloneable : AbstractCloneable
{
    protected override void HandleCloned(AbstractCloneable clone)
    {
        //Get whathever magic a base class could have implemented.
        base.HandleCloned(clone);

        //Clone is of the current type.
        ConcreteCloneable obj = (ConcreteCloneable)clone;

        //Here you have a superficial copy of "this". You can do whathever 
        //specific task you need to do.
        //e.g.:
        //obj.SomeReferencedProperty = this.SomeReferencedProperty.Clone();
    }
}

#endregion
