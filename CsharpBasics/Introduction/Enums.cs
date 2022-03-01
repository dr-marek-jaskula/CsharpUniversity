namespace CsharpBasics.Introduction;

public class Enums
{
    public static void InvokeEnumExamples()
    {
        Seasons currentSeason = Seasons.Winter;
        Console.WriteLine(currentSeason);

        //convert to number (int here)
        Console.WriteLine((int)UniqueEnum.Odd);

        //convert to enum from number
        UniqueEnum uniqueEnum = (UniqueEnum)15;
        Console.WriteLine(uniqueEnum);

        //to string
        string seasonName = Seasons.Autumn.ToString();
        Console.WriteLine(seasonName);

        string seasonName2 = "Spring";
        bool pasing = Enum.TryParse(seasonName2, out Seasons secretSeason);
        Console.WriteLine($"It is {pasing} that {seasonName2} is a Season! However {secretSeason}");
    }

    //basic enum. Spring = 0, Summor = 1, Autumn = 2, Winter = 3
    public enum Seasons
    {
        Spring, Summer, Autumn, Winter 
    }

    //here January is 1, and then February is 2 and so on
    public enum Month
    {
        January = 1, February, March, April, May, June, July, August, September, October, November, December
    }

    //The default type of enum is int
    //we can change the type of enum for example to byte (0-255)
    public enum UniqueEnum : byte
    {
        Secret = 4, Mystery = 15, Odd = 1
    }
}