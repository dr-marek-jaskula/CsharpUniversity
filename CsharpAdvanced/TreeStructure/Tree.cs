namespace CsharpAdvanced.TreeStructure;

public sealed class Tree<T, K>
    where T : notnull
    where K : class
{
    public Root<T, K> Root { get; }
    public Node<T, K> CurrentNode { get; private set; }
    public LinkedList<Node<T, K>> PathFromStartNodeToCurrentNode { get; } = new();
    
    private Tree(Root<T, K> root)
    {
        Root = root;
        CurrentNode = root;
    }

    public static Tree<T, K> Create(T id, Func<K, K> execute, Func<K, T>? determineNextNode, Action<Root<T, K>>? configure = null)
    {
        var root = new Root<T, K>(id, execute, determineNextNode);

        if (configure is not null)
        {
            configure(root);
        }
        
        var newTree = new Tree<T, K>(root);

        ValidateTree(newTree);

        return newTree;
    }

    private static void ValidateTree(Tree<T, K> newTree)
    {
        var allNodes = GetAllNodes(newTree.Root);

        var duplicatedNodeIds = allNodes
            .GroupBy(x => x.Id)
            .Where(x => x.Count() > 1)
            .Select(x => x.First().Id)
            .ToArray();

        if (duplicatedNodeIds.Length > 0)
        {
            throw new ArgumentException($"Duplicated node ids: {string.Join(',', duplicatedNodeIds)}");
        }

        var numberOfRoots = allNodes
            .Where(x => x.PreviousNode is null)
            .Count();

        if (numberOfRoots is not 1)
        {
            throw new ArgumentException($"Invalid number or roots: {numberOfRoots}");
        }

        var idsOfNodesThatAreNotLastAndCannotDetermineSubsequentNode = allNodes
            .Where(x => x.IsLast is false)
            .Where(x => x.CanDetermineSubsequentNode is false)
            .Select(x => x.Id)
            .ToArray();

        if (idsOfNodesThatAreNotLastAndCannotDetermineSubsequentNode.Length is not 0)
        {
            throw new ArgumentException($"DetermineSubsequentNode was not set for: {string.Join(',', idsOfNodesThatAreNotLastAndCannotDetermineSubsequentNode)}");
        }
    }

    public K ExecuteTreeFlow(K input)
    {
        var lastNodeResult = Root.ExecuteSubsequentFlow(input);
        SetPathAndCurrentNode(Root);
        return lastNodeResult;  
    }

    public K ExecuteSubsequentFlow(Node<T, K> startNode, K input)
    {
        var lastNodeResult = startNode.ExecuteSubsequentFlow(input);
        SetPathAndCurrentNode(startNode);
        return lastNodeResult;
    }

    public Node<T, K>? FirstOrDefaultNode(T id)
    {
        return RecursiveNodeSearch(id, Root)
            .FirstOrDefault(x => x is not null);
    }

    private static IEnumerable<Node<T, K>?> RecursiveNodeSearch(T id, Node<T, K> currentNode)
    {
        if (currentNode.Id.Equals(id))
        {
            yield return currentNode;
        }

        foreach (var node in currentNode.SubsequentNodes)
        {
            yield return RecursiveNodeSearch(id, node)
                .FirstOrDefault(x => x is not null);
        }

        yield return null;
    }

    private void SetPathAndCurrentNode(Node<T, K> startNode)
    {
        PathFromStartNodeToCurrentNode.AddFirst(startNode);
        Node<T, K> currentNode = startNode;

        while (currentNode.IsLast is false && currentNode.Executed)
        {
            currentNode = currentNode.MoveNext();
            PathFromStartNodeToCurrentNode.AddLast(currentNode);
        }

        CurrentNode = currentNode;
    }
    
    private static Node<T, K>[] GetAllNodes(Root<T, K> root)
    {
        var nodes = root.SubsequentNodes
            .SelectMany(GetBranchNodes)
            .ToList();
        
        return [root, .. nodes];
    }

    private static IEnumerable<Node<T, K>> GetBranchNodes(Node<T, K> node)
    {
        yield return node;

        foreach (var nodes in node.SubsequentNodes)
        {
            foreach (var subsequentNodes in GetBranchNodes(nodes))
            {
                yield return subsequentNodes;
            }
        }
    }
}
