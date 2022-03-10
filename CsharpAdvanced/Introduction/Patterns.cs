﻿namespace CsharpAdvanced.Introduction;

public class Patterns
{
    //In c# we have many patters that make out life easier
    //We can distinguish:
    //1. declaration and type patterns
    //2. constant pattern
    //3. relational patterns
    //4. property pattern (Matching nested patterns)
    //5. Positional pattern

    public static void InvokePatternsExamples()
    {
        #region Declaration and type patterns:
        
        //simple example
        object greeting = "Hello, World!";
        if (greeting is string message)
            Console.WriteLine(message.ToLower());  // output: hello, world!

        //switch expression example
        static int GetSourceLabel<T>(IEnumerable<T> source) => source switch
        {
            Array array => array.Length,
            ICollection<T> collection => 10,
            _ => 3,
        };

        var numbers = new int[] { 10, 20, 30 };
        Console.WriteLine(GetSourceLabel(numbers));  // output: 1

        var letters = new List<char> { 'a', 'b', 'c', 'd' };
        Console.WriteLine(GetSourceLabel(letters));  // output: 2

        //other example
        int? xNullable = 7;
        int y = 23;
        object yBoxed = y; //boxed means that stored in object
        if (xNullable is int a && yBoxed is int b)
            Console.WriteLine(a + b);  // output: 30

        //example to just check the type
        static decimal ChooseTheVehicle(Vehicle vehicle) => vehicle switch
        {
            Car => 2.00m,
            Truck => 7.50m,
            null => throw new ArgumentNullException(nameof(vehicle)),
            _ => throw new ArgumentException("Unknown type of a vehicle", nameof(vehicle))
        };

        ChooseTheVehicle(new Car() { Name = "Mambo" });

        #endregion

        #region Constant pattern

        //Use for null checks
        int? input = 3;
        if (input is null)
            return;
        //or after c# 9.0
        if (input is not null)
        {
            // ...
        }

        #endregion

        #region Relational patterns

        //This is the type of pattern that allow us to use inequalities and "or", "and" keywords

        static string Classify(double measurement) => measurement switch
        {
            < -4.0 and >= -8 => "Mine",
            > 10.0 or < -10 => "Yours",
            double.NaN => "Unknown",
            _ => "Acceptable",
        };

        Classify(4.3);
        Classify(14.3);
        Classify(-4.3);
        Classify(double.NaN);

        #endregion

        #region Matching nested patterns

        //after c# 8.0
        static bool IsConferenceDay(DateTime date) => date is { Year: 2020, Month: 5, Day: 19 or 20 or 21 };

        //using switch expression 
        static string TakeFive(object input) => input switch
        {
            string { Length: >= 5 } s => s[..5],
            string s => s,
            ICollection<char> { Count: >= 5 } symbols => new string(symbols.Take(5).ToArray()),
            ICollection<char> symbols => new string(symbols.ToArray()),
            null => throw new ArgumentNullException(nameof(input)),
            _ => throw new ArgumentException("Not supported input type."),
        };

        //Example with nested (we can use '.' to get deeper into object)
        static bool IsAnyEndOnXAxis(Segment segment) => segment is { Start.Y: 0 } or { End.Y: 0 };

        #endregion

        #region Positional pattern

        //we use this to deconstruction by tuples
        static decimal GetGroupTicketPriceDiscount(int groupSize, DateTime visitDate)
        => (groupSize, visitDate.DayOfWeek) switch
        {
            ( <= 0, _) => throw new ArgumentException("Group size must be positive."),
            (_, DayOfWeek.Saturday or DayOfWeek.Sunday) => 0.0m,
            ( >= 5 and < 10, DayOfWeek.Monday) => 20.0m,
            ( >= 10, DayOfWeek.Monday) => 30.0m,
            ( >= 5 and < 10, _) => 12.0m,
            ( >= 10, _) => 15.0m,
            _ => 0.0m,
        };

        //deconstruct the record, tuple deconstructs the object by its constructor
        static string PrintIfAllCoordinatesArePositive(object point) => point switch
        {
            Point2D (> 0, > 0) p => p.ToString(),
            Point3D (> 0, > 0, > 0) p => p.ToString(),
            _ => string.Empty,
        };

        #endregion

        //combine two type of patterns: property and positional
        static bool IsInDomain(WeightedPoint point) => point is ( >= 0, >= 0) { Weight: >= 0.0 };
    }

    //record with two and three properties
    public record Point2D(int X, int Y);
    public record Point3D(int X, int Y, int Z);

    //this record contains three properties: X,T and Weight. Two of them are in the 
    public record WeightedPoint(int X, int Y)
    {
        public double Weight { get; set; }
    }
}

#region Helpers

public abstract class Vehicle
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
 
public class Car : Vehicle
{
    public int Speed { get; set; }
}

public class Truck : Vehicle
{
    public int Mass { get; set; }
}

#endregion