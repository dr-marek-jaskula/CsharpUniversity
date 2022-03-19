namespace CsharpAdvanced.DesignPatterns;

public abstract class Pizza2
{
    public abstract decimal GetPrice();

    public enum PizzaType
    {
        HamMushroom, Deluxe, Hawaiian
    }
    public static Pizza2 PizzaFactory(PizzaType pizzaType)
    {
        switch (pizzaType)
        {
            case PizzaType.HamMushroom:
                return new HamAndMushroomPizza();

            case PizzaType.Deluxe:
                return new DeluxePizza();

            case PizzaType.Hawaiian:
                return new HawaiianPizza();

            default:
                break;
        }

        throw new System.NotSupportedException("The pizza type " + pizzaType.ToString() + " is not recognized.");
    }

    public static void FactoryMethod()
    {
        Console.WriteLine(Pizza2.PizzaFactory(Pizza2.PizzaType.Hawaiian).GetPrice().ToString("C2")); // $11.50
        Console.WriteLine(Pizza2.PizzaFactory(Pizza2.PizzaType.Hawaiian).GetPrice().ToString("C2") ); // $11.50
    }

}

#region Pizzas
public class HamAndMushroomPizza : Pizza2
{
    private readonly decimal price = 8.5M;
    public override decimal GetPrice() { return price; }
}

public class DeluxePizza : Pizza2
{
    private readonly decimal price = 10.5M;
    public override decimal GetPrice() { return price; }
}

public class HawaiianPizza : Pizza2
{
    private readonly decimal price = 11.5M;
    public override decimal GetPrice() { return price; }
}
#endregion  


