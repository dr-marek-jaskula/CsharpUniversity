namespace Eltin_Buchard_Keller_Algorithm;

/// <summary>
/// This class is an implementation of a Burkhard-Keller tree. The BK-Tree is a tree structure used to quickly find close matches to any defined object.
/// </summary>
public class BKTree
{
    private readonly BKTreeNode _root;
    private readonly Dictionary<BKTreeNode, Int32> _matches;

    public BKTree(BKTreeNode initialNode)
    {
        _root = initialNode;
        _matches = new Dictionary<BKTreeNode, Int32>();
    }

    public void Add(BKTreeNode node)
    {
        _root.Add(node);
    }

    public void AddMultiple(List<string> list)
    {
        if (_root != null)
            _root.AddMultiple(list);
    }

    /// <summary>
    /// This method will find all the close matching Nodes within a certain threshold. For instance, to search for similar strings, threshold set to 1 will return all the strings that are off by 1 edit distance.
    /// </summary>
    /// <param name="searchNode"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public List<string> Query(BKTreeNode searchNode, int threshold)
    {
        Dictionary<BKTreeNode, Int32> matches = new();
        _root.Query(searchNode, threshold, matches);
        var dictionary = CopyMatches(matches);
        return dictionary.Keys.Select(x => x.Data).ToList();
    }

    /// <summary>
    /// Attempts to find the closest match to the search node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns>The edit distance of the best match</returns>
    public int FindBestDistance(BKTreeNode node)
    {
        return _root.FindBestMatch(node, Int32.MaxValue, out _);
    }

    /// <summary>
    /// Attempts to find the closest match to the search node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns>A match that is within the best edit distance of the search node.</returns>
    public BKTreeNode FindBestNode(BKTreeNode node)
    {
        _root.FindBestMatch(node, Int32.MaxValue, out BKTreeNode bestNode);
        return bestNode;
    }

    /// <summary>
    /// Attempts to find the closest match to the search node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns>A match that is within the best edit distance of the search node.</returns>
    public string FindBestNodeWithDistance(string name)
    {
        int distance = _root.FindBestMatch(_root, Int32.MaxValue, out BKTreeNode bestNode);
        _matches.Clear();
        _matches.Add(bestNode, distance);
        var listOfPossibleResults = _matches.Keys.First()._children.Select(x => x.Value).ToList();

        foreach (var item in listOfPossibleResults)
            item.Distance = DistanceMetric.CalculateLevenshteinDistance(item.Data, name);

        if (listOfPossibleResults.OrderBy(x => x.Distance).First().Distance > 10)
            return "";
        else
            return listOfPossibleResults.OrderBy(x => x.Distance).First().Data;
    }

    /// <summary>
    /// Make source input same as _matches private dictionary
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    private Dictionary<BKTreeNode, Int32> CopyMatches(Dictionary<BKTreeNode, Int32> source)
    {
        _matches.Clear();

        foreach (KeyValuePair<BKTreeNode, Int32> pair in source)
            _matches.Add(pair.Key, pair.Value);

        return _matches;
    }
}
