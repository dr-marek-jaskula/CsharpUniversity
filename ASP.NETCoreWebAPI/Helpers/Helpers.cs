using CustomTools.StringApproxAlgorithms.SymSpellAlgorithm;
using Eltin_Buchard_Keller_Algorithm;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreWebAPI.Helpers;

public interface IName
{
    string Name { get; set; }
}

public static class Helpers
{
    //Approx algorithms can be stored in the external project, but for simplicity we adapt it do the current project

    #region BK-Tree

    //Maximum distances and other algorithm parameters can be modified directly in algorithm classes

    /// <summary>
    /// Approximate name of entity, based on the Levenshtein metric using BK-Tree algorithm.
    /// </summary>
    /// <typeparam name="T">The db set type that implements the IName interface</typeparam>
    /// <param name="name">String to be approximated</param>
    /// <param name="dbSet">The collection where the approximation is done</param>
    /// <param name="separators">Split input strings on given separators like "Jason Mandela" to "Jason" and "Mandela"</param>
    /// <returns></returns>
    public static string ApproximateNameByBKTree<T>(string name, DbSet<T> dbSet, char[]? separators = null) where T : class, IName
    {
        var itemNames = dbSet.Select(item => item.Name).ToList();

        if (separators is not null)
        {
            List<string> itemNamesTree = new();

            foreach (var element in itemNames)
                itemNamesTree.AddRange(element.Split(separators));

            itemNames.AddRange(itemNamesTree);
        }

        BKTree tree = new(new(name));
        tree.AddMultiple(itemNames);
        return tree.FindBestNodeWithDistance(name);
    }

    /// <summary>
    /// Approximate name of entity, based on the Levenshtein metric using BK-Tree algorithm.
    /// </summary>
    /// <param name="name">String to be approximated</param>
    /// <param name="approximateTo">The list of strings where the approximation is done</param>
    /// <returns></returns>
    public static string ApproximateNameByBKTree(string name, List<string> approximateTo)
    {
        BKTree tree = new(new(name));
        tree.AddMultiple(approximateTo);
        return tree.FindBestNodeWithDistance(name);
    }

    #endregion BK-Tree

    #region SymSpell

    //Maximum distances and other algorithm parameters can be modified directly in algorithm classes

    /// <summary>
    /// Approximate name of entity, based on the fuzzy search by SymSpell algorithm.
    /// </summary>
    /// <param name="name">String to be approximated</param>
    /// <param name="approximateTo">The list of strings where the approximation is done</param>
    /// <returns></returns>
    public static string ApproximateNameBySymSpell(string name, List<string> approximateTo)
    {
        SymSpellAlgorithm symSpellAlgorithm = new(4, 2);


        return suggestions.OrderBy(s => s.distance).First().term;
    }

    #endregion

    /// <summary>
    /// Capitalizes the given string input. First letter will be capitalized and the following ones will be lowered.
    /// </summary>
    /// <param name="input">The input to be capitalized</param>
    public static void Capitalize(ref string input)
    {
        input = $"{input[0].ToString().ToUpper()}{input[1..].ToLower()}";
    }
}
