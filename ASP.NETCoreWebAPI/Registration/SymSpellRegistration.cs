using ASP.NETCoreWebAPI.StringApproxAlgorithms;

namespace ASP.NETCoreWebAPI.Registration;

public static class SymSpellRegistration
{
    public static void RegisterSymSpell(this IServiceCollection services)
    {
        SymSpells symSpells = new();
        SymSpell symSpellEnDictionary = SymSpellFactory.CreateSymSpell();
        symSpells.SymSpellsDictionary.Add("en", symSpellEnDictionary);
        services.AddSingleton(symSpells);
    }
}