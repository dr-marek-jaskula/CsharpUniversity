namespace CsharpAdvanced.DesignPatterns;

internal class GenericStrategyPatternExample
{
    public IGenericRouteStrategy<GenericWalkStrategy> _genericRouteStrategy { get; set; }

    public GenericStrategyPatternExample(IGenericRouteStrategy<GenericWalkStrategy> genericRouteStrategy)
    {
        _genericRouteStrategy = genericRouteStrategy;
    }

    public void InvokeStrategyPatternExample()
    {
        var strat = new Coordinate();
        var end = new Coordinate();

        _genericRouteStrategy.CreateRoute(strat, end);
    }
}

internal interface IGenericRouteStrategy
{
    void CreateRoute(Coordinate start, Coordinate end);
}

internal interface IGenericRouteStrategy<TType>  : IGenericRouteStrategy
{
}

internal class GenericCoordinate
{
    public double Long { get; set; }
    public double Lat { get; set; }
}

internal class GenericWalkStrategy : IGenericRouteStrategy<GenericWalkStrategy>
{
    public void CreateRoute(Coordinate start, Coordinate end)
    {
        Console.WriteLine("Walk strategy");
    }
}

internal class GenericCarStrategy : IGenericRouteStrategy<GenericCarStrategy>
{
    public void CreateRoute(Coordinate start, Coordinate end)
    {
        Console.WriteLine("Car strategy");
    }
}

internal class GenericBikeStrategy : IGenericRouteStrategy<GenericBikeStrategy>
{
    public void CreateRoute(Coordinate start, Coordinate end)
    {
        Console.WriteLine("Bike strategy");
    }
}