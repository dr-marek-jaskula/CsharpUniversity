namespace CustomTools.StringApproxAlgorithms.Eltin_Buchard_Keller_Algorithm;

/// <summary>
/// This class is an implementation of a Burkhard-Keller tree. The BK-Tree is a tree structure used to quickly find close matches to any defined object.
/// </summary>
/// <typeparam name="T"></typeparam>
public class BKTree<T> where T : BKTreeNode
{
    private T _root;
    private readonly Dictionary<T, Int32> _matches;

    public BKTree()
    {
        _root = null;
        _matches = new Dictionary<T, Int32>();
    }

    public void Add(T node)
    {
        if (_root != null)
            _root.Add(node);
        else
            _root = node;
    }

    /// <summary>
    /// This method will find all the close matching Nodes within a certain threshold. For instance, to search for similar strings, threshold set to 1 will return all the strings that are off by 1 edit distance.
    /// </summary>
    /// <param name="searchNode"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public Dictionary<T, Int32> Query(BKTreeNode searchNode, int threshold)
    {
        Dictionary<BKTreeNode, Int32> matches = new();
        _root.Query(searchNode, threshold, matches);
        return CopyMatches(matches);
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
    public T FindBestNode(BKTreeNode node)
    {
        _root.FindBestMatch(node, Int32.MaxValue, out BKTreeNode bestNode);
        return (T)bestNode;
    }

    /// <summary>
    /// Attempts to find the closest match to the search node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns>A match that is within the best edit distance of the search node.</returns>
    public Dictionary<T, Int32> FindBestNodeWithDistance(BKTreeNode node)
    {
        int distance = _root.FindBestMatch(node, Int32.MaxValue, out BKTreeNode bestNode);
        _matches.Clear();
        _matches.Add((T)bestNode, distance);
        return _matches;
    }

    private Dictionary<T, Int32> CopyMatches(Dictionary<BKTreeNode, Int32> source)
    {
        _matches.Clear();

        foreach (KeyValuePair<BKTreeNode, Int32> pair in source)
            _matches.Add((T)pair.Key, pair.Value);

        return _matches;
    }
}
