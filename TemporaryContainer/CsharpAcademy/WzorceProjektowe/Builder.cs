using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzorceProjektowe
{
    class Pizza
    {
        string dough;
        string sauce;
        string topping;
        public string Dough { get { return dough; } set { dough = value; } }
        public string Sauce { get { return sauce; } set { sauce = value; } }
        public string Topping { get { return topping; } set { topping = value; } }

        public override string ToString()
        {
            return string.Format(
                    "Pizza with Dough as {0}, Sauce as {1} and Topping as {2}",
                    Dough,
                    Sauce,
                    Topping);
        }
    }

    //Abstract Builder
    abstract class PizzaBuilder
    {
        protected Pizza pizza;
        public Pizza Pizza { get { return pizza; } }
        public void CreateNewPizza() { pizza = new Pizza(); }

        public abstract void BuildDough();
        public abstract void BuildSauce();
        public abstract void BuildTopping();
    }

    //Concrete Builder
    class HawaiianPizzaBuilder : PizzaBuilder
    {
        public override void BuildDough() { pizza.Dough = "cross"; }
        public override void BuildSauce() { pizza.Sauce = "mild"; }
        public override void BuildTopping() { pizza.Topping = "ham+pineapple"; }
    }

    //Concrete Builder
    class SpicyPizzaBuilder : PizzaBuilder
    {
        public override void BuildDough() { pizza.Dough = "pan baked"; }
        public override void BuildSauce() { pizza.Sauce = "hot"; }
        public override void BuildTopping() { pizza.Topping = "pepparoni+salami"; }
    }

    /** "Director" */
    class Waiter
    {
        private PizzaBuilder pizzaBuilder;

        public PizzaBuilder PizzaBuilder
        {
            get { return pizzaBuilder; }
            set { pizzaBuilder = value; }
        }
        public Pizza Pizza { get { return pizzaBuilder.Pizza; } }

        public void ConstructPizza()
        {
            pizzaBuilder.CreateNewPizza();
            pizzaBuilder.BuildDough();
            pizzaBuilder.BuildSauce();
            pizzaBuilder.BuildTopping();
        }
    }

    public class TestPizza
    {
        private static void BuildAndDisplayPizza(Waiter waiter)
        {
            waiter.ConstructPizza();
            System.Console.WriteLine(waiter.Pizza);
        }

        public static void Builder()
        {
            Waiter waiter = new();

            waiter.PizzaBuilder = new HawaiianPizzaBuilder();
            BuildAndDisplayPizza(waiter);

            waiter.PizzaBuilder = new SpicyPizzaBuilder();
            BuildAndDisplayPizza(waiter);
        }
    }
}
