using ASP.NETCoreWebAPI.StringApproxAlgorithms;

namespace Microsoft.Extensions.DependencyInjection;

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