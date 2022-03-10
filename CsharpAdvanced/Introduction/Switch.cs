namespace CsharpAdvanced.Introduction;

public class Swtich
{
    //In this file we will cover both switch statement and switch expression
    //Beginning with C# 8.0, you use the switch expression to evaluate a single expression from a list of candidate expressions based on a pattern match with an input expression.

    public static void InvokeSwitchExamples()
    {
        StrangeClass strangeClass = new StrangeClass() { LevelOrMystery = 30 };
        SwitchMe(strangeClass);
    }

    #region Advanced example: SwitchStatement

    //This switch uses the tuple to switch on two values
    public static void SwitchOnInteger(int first, int second) 
    {
        string result;

        switch ((first, second))
        {
            case ( > 0, > 0) when first == second:
                result = $"Both numbers are positive and equal to {first}";
                break;
            case ( >0, > 0):
                result = $"Both number are positive. First eqauls {first} and the second equals {second}";
                break;
            default:
                result = "Values are not valid numbers";
                break;
        }

        Console.WriteLine($"The result is: {result}");
    }

    //We can also look at the example with switch on object
    public void SwitchOnObject(object obj)
    {
        string result;

        switch (obj)
        {
            case IPower power when power.PowerLevel < 10 && power.PowerLevel >= 5:
                result = "Average power";
                break;
            case MysteryClass mystery when mystery.LevelOrMystery == 20 || mystery.LevelOrMystery == 30:
                throw new ArgumentException("Mystery cannot be 20 or 30");
            case RecordClass:
                result = "It is a record";
                break;
            default:
                throw new InvalidOperationException();
        }

        Console.WriteLine($"The result is: {result}");
    }

    #endregion

    #region Advanced example: SwitchExpression

    //This function presents switching suing tuples and using discard "_" character - representing any character
    public static string RockPaperScissors(string first, string second) => (first, second) switch
    {
        ("rock", "paper") => "rock is covered by paper. Paper wins",
        ("rock", "scissors") => "rock breaks scissors. Rock wins.",
        (_, "Hello") => "ERROR",
        (_, _) => "tie"
    };

    //Example with matching patterns
    public static string SwitchMe(object somethingStrange)
    {
        //We could convert it to the arrow function
        return somethingStrange switch
        {
            IPower { PowerLevel: < 10 and >= 5 } => "Average power", //If the object implements the IPower interface and fits the pattern
            MysteryClass { LevelOrMystery: 30 or 20 } => "Mystery of 30 or 20", //If the object inherits from MysteryClass and fits the pattern
            OddClass { LevelOrOddness: 30 or > 40 } => "Odd of 30 or more then 40", //Similar to last case
            RecordClass { FirstName: "Frank", LastName: "Jonas" } => "Frank Jonas here", //If the class inherits from RecordClass (which is a record) and first the pattern
            OddClass oddClass => $"Random oddness: {oddClass.LevelOrOddness}", //Similar to third case
            _ => "It is a secret", //otherwise (default case)
        };
    }

    //Example with "when"

    public readonly struct Point
    {
        public Point(int x, int y) => (X, Y) = (x, y);
        public int X { get; }
        public int Y { get; }
    }

    static Point Transform(Point point) => point switch
    {
        { X: 0, Y: 0 } => new Point(10, 10),
        { X: >10, Y: <100 } => new Point(-10, -100),
        { X: int x, Y: int y } when x < y => new Point(x + y, y), //when should be used when we need to specify the relation between two or more members like "x<y"
        { X: var x, Y: var y } when x > y => new Point(x - y, y),
        { X: var x, Y: var y } => new Point(2 * x, 2 * y), //when should be used when we need to use the member values in the return statement
    };

    #endregion
}

#region Helpers

interface IPower
{
    int PowerLevel { get; set; }
}

class MysteryClass : IPower
{
    public int LevelOrMystery { get; set; }
    public int PowerLevel { get; set; }
}

class OddClass : IPower
{
    public int LevelOrOddness { get; set; }
    public int PowerLevel { get; set; }
}

class StrangeClass : MysteryClass
{

}

record RecordClass(string FirstName, string LastName);

#endregion