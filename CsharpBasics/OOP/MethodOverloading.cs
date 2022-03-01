//The idea of method overloading is to use the same method name for different parameter scenarios 
//for the purpose of demonstrating the idea, the internal static class will be created

//As we can see, the return types, number of parameters, types of parameters can differ
//Therefore we can design a specific outcomes for a specific inputs using matching methods names
//However the good practice is to try to make generic solution, so avoid to many overload if it is possible

namespace CsharpBasics.OOP;

public static class MethodOverloading
{
    public static void BuildGame()
    {
        Console.WriteLine("Build simple game...");
    }

    public static void BuildGame(string citeria)
    {
        Console.WriteLine($"Build simple game with criteria: {citeria}");
    }

    public static void BuildGame(int level)
    {
        Console.WriteLine($"Build simple game with level: {level}");
    }

    public static int BuildGame(bool isHard)
    {
        Console.WriteLine(isHard ? "Build simple game that is hard" : "Build simple game that is not hard");
        return Convert.ToInt32(isHard);
    }

    public static List<string> BuildGame(int numberOfPlayers, params string[] reviews)
    {
        Console.WriteLine($"Build simple game for {numberOfPlayers} players \n");
        List<string> list = new();

        foreach (var review in reviews)
        {
            Console.WriteLine($"Review: {review}");
            list.Add(review);
        }
        
        return list;
    }
}