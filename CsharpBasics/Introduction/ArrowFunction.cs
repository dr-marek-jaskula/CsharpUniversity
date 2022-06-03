namespace LearningApplication2;

//Arrow function are just function with other, fast definition.
//They are mostly used as predicated or order delegates - arguments of other functions
public class ArrowFunction
{
    public static void InvokeArrowFunctionExamples()
    {
        Greet2(" Mike");

        int result = ArrowDelegate(4);

        //other example of arrow function
        Action hello = () => Console.WriteLine("Hello");
        hello();

        var result2 = ArrowDelegate2(2, 3);
        var result3 = $"{ArrowDelegate3(2, 3)[0]} and {ArrowDelegate3(2, 3)[1]}";

        MyNumber(3);
        AngryNumber(4);

        Console.WriteLine(Activator(ArrowDelegate, 10));
        Console.WriteLine(Activator(y => y * y * y, 3));

        IGoldRipper goldRipper = new GoldRipper();
        goldRipper.RippingIndex();
        //Console.WriteLine(goldRipper.goldIndex);
        Console.WriteLine(goldRipper.goldLevel);

        //Location location = new("Lodz");
        //Console.WriteLine(location.Name);
        //location.ChangeSuffix(" ;)");
        //Console.WriteLine(location.Name);

        //use anonymous arrow function as predicate
        List<int> list = new() { 1, 4, 9, 23, 41 };
        var filtredList = list.Where(x => x > 10).ToList();

        #region C# 10 improvements: arguments types, return types, attributes for lambdas

        //Since C# 10 we can use:
        var parse = (string s) => int.Parse(s);
        //Instead of:
        Func<string, int> parse2 = s => int.Parse(s);

        //Since C# 10 we can explicitly show the return type (important if the return type is not straightforward)
        var resultWithTypeObject = object (bool b) => b ? 1 : "0";

        //Since C# 10 we can set attributes for whole lambda expressions
        var someLambda = [Obsolete] (string s) => parse(s);

        #endregion C# 10 improvements: arguments types, return types, attributes for lambdas
    }

    //Simple example of arrow function
    public static void Greet() => Console.WriteLine("Hello");

    //Examples of delegates that are defined by the arrow function (Func)
    public static Func<int, int> ArrowDelegate = x => x * x;

    public static Func<int, int, bool> ArrowDelegate2 = (x, y) => x == y;

    public static Func<int, int, List<int>> ArrowDelegate3 = (x, y) =>
    {
        List<int> list = new List<int>();
        list.Add(x);
        list.Add(y);
        return list;
    };

    //Examples of delegates that are defined by arrow function (Action)
    private static Action<string> Greet2 = name =>
    {
        string greeting = $"Goodbye {name}";
        Console.WriteLine(greeting);
    };

    private static Action<int> MyNumber = number => Console.WriteLine($"My number is {number}");
    private static Action<int> AngryNumber = number => { Console.Write("My "); Console.WriteLine($"angry number is {number + 3}"); };

    //some function that executed the function stored by the delegate, with given argument
    public static int Activator(Func<int, int> selectedFunction, int selectedArgument)
    {
        return selectedFunction(selectedArgument);
    }
}

#region Arrow defined properties

//Example 1
internal class Location
{
    private string _name;
    public string suffix = string.Empty;

    public string Name => _name + suffix; //this property value is set when it is called

    public Location(string name)
    {
        _name = name;
    }

    public void ChangeSuffix(string suffix)
    {
        this.suffix = suffix;
    }
}

//Example 2
internal class GoldRipper : IGoldRipper
{
    public int goldLevel { get; private set; } = 1;
    public int goldIndex = 6;

    //set the value of goldLevel to goldIndex + 10, but only if the interface 'goldLevel' is used
    int IGoldRipper.goldLevel => goldIndex + 10;

    public void RippingGold()
    {
        Console.WriteLine("Your gold is mine");
    }

    public void RippingIndex()
    {
        goldIndex++;
    }
}

internal interface IGoldRipper
{
    void RippingGold();

    void RippingIndex();

    int goldLevel { get; }
}

#endregion Arrow defined properties

//private dynamic defaultReminder => reminder.TimeSpanText[TimeSpan.FromMinutes(15)];
//Be careful using this technique, since using => doesn 't set the actual value, but will execute the code each time defaultReminder is accessed. This may not be intended, and have a negative performance impact, or generate unwanted pressure for GC