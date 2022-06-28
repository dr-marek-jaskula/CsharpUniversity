using System.Collections.Immutable;

namespace CsharpAdvanced.Records;

public class InheritanceAndMembers
{
    #region Records that implement interfaces
    private interface IAmHero
    {
        string Name { get; }
        string Universe { get; }
    }

    // Record implementing an interface
    private record Hero(string Name = "", string Universe = "", bool CanFly = false) : IAmHero;

    public void Work_With_Hero_Interface()
    {
        IAmHero h = new Hero("Groot", "Marvel", false);
        Console.WriteLine(Equals("Groot", h.Name));
    }
    #endregion

    #region Inheritance and records
    // Derived record with additional properties and methods
    private record RealLifeHero(string Name, string Universe, bool CanFly,
        string RealLifeName) : Hero(Name, Universe, CanFly)
    {
        public string? CharacteristicProperty { get; init; }
        public string FullName => $"{Name} ({RealLifeName})";

        public override string ToString() => FullName;
    }

    public void Work_With_Real_Life_Hero()
    {
        var h1 = new RealLifeHero("Superman", "DC", true, "Clark Kent")
        {
            CharacteristicProperty = "Glasses"
        };
        Console.WriteLine(Equals("Superman (Clark Kent)", h1.FullName));
        Console.WriteLine(Equals("Glasses", h1.CharacteristicProperty));

        // Note that value-based equality takes additional init-only properties into account
        var h2 = h1 with { };
        Console.WriteLine(Equals(h1, h2));
        h2 = h1 with { CharacteristicProperty = "FooBar" };
        Console.WriteLine(Equals(h1, h2));
    }
    #endregion

    #region Combining immutable records with immutable collections
    public void Immutable_Hero_Collections()
    {
        // Use immutable records with immutable collections

        Hero homelander;
        var immutableArray = ImmutableArray<Hero>.Empty;
        immutableArray = immutableArray.Add(homelander = new("Homelander", "DC", true));
        immutableArray = immutableArray.Add(homelander with { Name = "Stormfont" });
        immutableArray = immutableArray.SetItem(1,
            immutableArray[1] with { Name = "The Deep", CanFly = false });

        Console.WriteLine(Equals(2, immutableArray.Length));
        Console.WriteLine(Equals(new[] { "Homelander", "The Deep" },
            immutableArray.Select(item => item.Name)));
    }
    #endregion
}