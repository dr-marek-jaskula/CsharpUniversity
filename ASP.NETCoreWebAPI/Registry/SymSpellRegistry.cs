using ASP.NETCoreWebAPI.StringApproxAlgorithms;

namespace ASP.NETCoreWebAPI.Registry;

public static class SymSpellRegistry
{
    public static void RegisterSymSpell(this IServiceCollection services)
    {
        SymSpells symSpells = new();
        SymSpell symSpellEnDictionary = SymSpellFactory.CreateSymSpell();
        symSpells.SymSpellsDictionary.Add("en", symSpellEnDictionary);
        services.AddSingleton(symSpells);
    }
}