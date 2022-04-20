namespace CustomTools.StringApproxAlgorithms.Eltin_Buchard_Keller_Algorithm;
using System.Diagnostics;

//In ASP.NETCoreWebAPI in "Helpers" folder there is applicable BK-Tree algorithm for LevenshteinNodeRecord approach

/*  
    To use BKTree:
    1. Create a class dervied from BKTreeNode
    2. Add a member variable of your data to be sorted / retrieved
    3. Override the calculateDistance method to calculate the distance metric between two nodes for the data to be sorted / retrieved.
    4. Instantiate a BKTree with the type name of the class created in (1).
*/

//Example of use can be found in "Wild Rift" project [part of it is below]

public class EltinBuchardKellerAlgorithEntrypoint
{
    public static void DemonstrateTheAlgorith()
    {
        Debug.WriteLine(DistanceMetric.CalculateLevenshteinDistance("kitten", "sitten"));
        Debug.WriteLine(DistanceMetric.CalculateLevenshteinDistance("sittin", "sitten"));
        Debug.WriteLine(DistanceMetric.CalculateLevenshteinDistance("sittin", "sitting"));
        Debug.WriteLine(DistanceMetric.CalculateLevenshteinDistance("thme", "them"));
        Debug.WriteLine(DistanceMetric.CalculateLevenshteinDistance("arithmatic", "arithmetic"));
        Debug.WriteLine(DistanceMetric.CalculateLevenshteinDistance("helo", "hello"));
        Debug.WriteLine(DistanceMetric.CalculateLevenshteinDistance("tommorrow", "tomorrow"));
        Debug.WriteLine(DistanceMetric.CalculateLevenshteinDistance("Ahri", "Ari"));
        Debug.WriteLine(DistanceMetric.CalculateLevenshteinDistance("Ahri", "Arhi"));
        Debug.WriteLine(DistanceMetric.CalculateLevenshteinDistance("Ahri", "Arh"));
        //for string above the appropriate threshold should be equal to 2

        //Now let us demonstrate the whole algorithm

        // Create BKTree with derived node class from top of file
        BKTree<LevenshteinNodeRecord> tree = new();

        // Add some nodes
        tree.Add(new LevenshteinNodeRecord(1, "demorogor"));
        tree.Add(new LevenshteinNodeRecord(2, "bomoromoro"));
        tree.Add(new LevenshteinNodeRecord(3, "sitting"));
        tree.Add(new LevenshteinNodeRecord(4, "genzo"));
        tree.Add(new LevenshteinNodeRecord(5, "wizugo"));

        // Get best node from our tree with best distance (can return same string)
        Dictionary<LevenshteinNodeRecord, Int32> results = tree.FindBestNodeWithDistance(new LevenshteinNodeRecord("kitten"));

        // Get best nodes below threshold
        results = tree.Query(new LevenshteinNodeRecord("kitten"), 3); // arbitrary threshold 3

        Debug.WriteLine("End of the application");
    }

    #region Example of use
    /*
    public interface IName
    {
        string Name { get; set; }
    }

    /// <summary>
    /// Approximate name of entity, based on the Levenshtein metric.
    /// </summary>
    /// <typeparam name="T">The db set type that implements the IName interface</typeparam>
    /// <param name="name">String to be approximated</param>
    /// <param name="dbSet">The collection where the approximation is done</param>
    /// <returns></returns>
    public static string ApproximateName<T>(string name, DbSet<T> dbSet) where T : class, IName
    {
        var itemNames = dbSet.Select(item => item.Name).ToList();
        List<string> itemNamesTree = new();

        foreach (var element in itemNames)
            itemNamesTree.AddRange(element.Split(new char[] { ' ', '\'' }));

        itemNamesTree.AddRange(itemNames);

        BKTree tree = new(new(name));
        tree.AddMultiple(itemNamesTree);
        return tree.FindBestNodeWithDistance(name);
    }
    */
    #endregion
}

