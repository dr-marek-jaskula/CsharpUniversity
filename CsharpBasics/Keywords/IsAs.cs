using Microsoft.VisualBasic;

namespace CsharpBasics.Keywords;

public class Is
{
    //The is operator is used to check if the run-time type of an object is compatible with the given type or not.
    //It returns true if the given object is of the same type otherwise, return false. It also returns false for null objects.

    public static void InvokeIsExamples()
    {
        Animal animal = new();
        Cat cat = new();
        int number = 10;
        Animal? nullAnimal = null;

        //examine compatibility with a given type
        bool result = animal is object; //true
        bool result2 = animal is string; //false
        bool result3 = animal is Cat; //false
        bool result4 = cat is Animal; //true

        if (animal is Animal a && cat is Cat c) 
        {
            //now a becomes a variable in this scope (also scope outside the if body)
            a.AnimalSound();
            c.AnimalSound();
            c.Run();
        }

        //we can use 'is' as follows
        if (cat is Animal catAnimal)
        {
            //catAnimal can not use Run method, but cat can
            cat.Run();
            catAnimal.AnimalSound(); //catAnimal is of type Animal
            catAnimal.Age = -10; //both cat and catAnimal has -10 now
        }

        if (number is int num)
            Console.WriteLine(num);

        //is can be used for null checks (also for the negation pattern - since c# 9.0) 
        if (nullAnimal is null)
            Console.WriteLine("nullAnimal is null");
        if (animal is not null)
            Console.WriteLine("animal is not null");

        //Beginning with c# 7.0, you can also use the is operator to match an expression against a pattern
        DateTime dateTime = DateTime.Now;
        bool result5 = dateTime is { Month: 10, Day: <= 7, DayOfWeek: DayOfWeek.Friday };

        //Beginning with c$ 10.0, we can use is keyword to compare variables to constants (only constants)
        bool result6 = number is 10;
        bool result7 = number is 11;
        bool result8 = number is 11 or 12; 
        bool result9 = number is 11 and <14; 
        bool result10 = number is <11 and >5 or 33;

        DayOfWeek dayOfWeek = DayOfWeek.Monday;
        bool result11 = dayOfWeek is DayOfWeek.Monday or DayOfWeek.Friday;
    }
}

public class As
{
    //as keyword explicitly converts an expression to a given type if its run-time type is compatible with that type
    //If the conversion isn't possible, the as operator returns null.
    //Unlike a cast expression, the as operator never throws an exception.

    public static void InvokeAsExamples()
    {
        Animal animal = new Animal("Emor", "White dog", 7);
        Cat cat = new("Alik", "Strange cat", 9, "brown", 16);

        var convertedAnimal = cat as Animal;
        //It is the same as
        Animal convertedAnimal2 = cat;

        //the use of 'as' keyword presented above is equivalent to the following statement 
        var convertedAnimal3 = cat is Animal ? cat : null;

        //The as operator considers only reference, nullable, boxing, and unboxing conversions.
        //You can't use the as operator to perform a user-defined conversion. To do that, use a cast expression.

        //we can use 'as' keyword as follows
        IEnumerable<int> numbers = new[] { 10, 20, 30 };
        IList<int> indexable = numbers as IList<int>;
        if (indexable is not null)
            Console.WriteLine(indexable[0] + indexable[indexable.Count - 1]);  // output: 40
    }
}

#region Helper classes

internal class Animal
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Age { get; set; }

    public Animal()
    {

    }

    public Animal(string name, string type, int age)
    {
        Name = name;
        Description = type;
        Age = age;
    }

    public virtual void AnimalSound()
    {
        Console.WriteLine("Default sound");
    }
}

internal class Cat : Animal
{
    public string Color { get; set; } = string.Empty;
    public int Cuteness { get; set; } 

    public void Run()
    {
        Console.WriteLine("Cat runs");
    }

    public override void AnimalSound()
    {
        Console.WriteLine("Meow");
    }

    public Cat()
    {

    }

    public Cat(string name, string description, int age, string color, int cuteness) : base(name, description, age)
    {
        Color = color;
        Cuteness = cuteness;
    }
}

#endregion Helper classes
