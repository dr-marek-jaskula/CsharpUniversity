using System.Diagnostics;

namespace CsharpAdvanced.DesignPatterns;

public class StrategyPatternExample
{
    public static void InvokeStrategyPatternExample()
    {
        //This is a simple dependency inversion based strategy

        var strategy = new BikeStrategy();

        var map = new Map(strategy);

        var strat = new Coordinate();
        var end = new Coordinate();

        map.CreateRoute(strat, end);
    }
}

internal interface IRouteStrategy
{
    void CreateRoute(Coordinate start, Coordinate end);
}

internal class Map
{
    private readonly IRouteStrategy _strategy;

    public Map(IRouteStrategy strategy)
    {
        _strategy = strategy;
    }

    public void CreateRoute(Coordinate start, Coordinate end)
    {
        _strategy.CreateRoute(start, end);
    }
}

internal class Coordinate
{
    public double Long { get; set; }
    public double Lat { get; set; }
}

internal class WalkStrategy : IRouteStrategy
{
    public void CreateRoute(Coordinate start, Coordinate end)
    {
        Debug.WriteLine("Walk strategy");
    }
}

internal class CarStrategy : IRouteStrategy
{
    public void CreateRoute(Coordinate start, Coordinate end)
    {
        Console.WriteLine("Car strategy");
    }
}

internal class BikeStrategy : IRouteStrategy
{
    public void CreateRoute(Coordinate start, Coordinate end)
    {
        Console.WriteLine("Bike strategy");
    }
}