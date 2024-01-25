namespace CsharpAdvanced.TreeStructure;

public class Node<T, K>
    where T : notnull
    where K : class
{
    private Func<K, K> _execute { get; }
    private Func<K, T>? _determineSubsequentNode { get; }

    public Node(T id, Func<K, K> execute, Func<K, T>? determineSubsequentNode = null)
    {
        Id = id;
        _execute = execute;
        _determineSubsequentNode = determineSubsequentNode;
        Output = null;
    }

    public T Id { get; }
    public bool Executed => Output is not null;
    public bool IsLast => SubsequentNodes.Count is 0;
    public bool CanDetermineSubsequentNode => SubsequentNodes.Count is 1 || _determineSubsequentNode is not null;
    public K? Output { get; private set; }
    public Node<T, K>? PreviousNode { get; protected set; }
    public readonly List<Node<T, K>> SubsequentNodes = [];
    public T? SubsequentNodeId = default;

    /// <summary>
    /// Adds a subsequent node to the current node and return the newly added subsequent node
    /// </summary>
    /// <returns>Newly added subsequent node for deep chain</returns>
    public Node<T, K> AddSubsequentNode(T id, Func<K, K> execute, Func<K, T>? determineSubsequentNode = null, Action<Node<T, K>>? configure = null)
    {
        var subsequentNode = new Node<T, K>(id, execute, determineSubsequentNode)
        {
            PreviousNode = this
        };

        SubsequentNodes.Add(subsequentNode);

        if (configure is not null)
        {
            configure(subsequentNode);
        }

        return subsequentNode;
    }

    public K ExecuteSubsequentFlow(K input)
    {
        Output = ExecuteCurrentNode(input);

        if (IsLast)
        {
            return Output;
        }

        return MoveNext()
            .ExecuteSubsequentFlow(Output);
    }

    public K ExecuteCurrentNode(K input)
    {
        if (Executed)
        {
            throw new InvalidOperationException("Cannot execute the already executed Node.");
        }

        Output = _execute(input);
        return Output;
    }

    public Node<T, K> MoveNext()
    {
        if (Output is null)
        {
            throw new InvalidOperationException("Current Node was not executed.");
        }

        if (IsLast)
        {
            throw new InvalidOperationException("Current Node is the last one in this branch.");
        }

        if (SubsequentNodes.Count is 1)
        {
            var subsequentNode = SubsequentNodes.First();
            SubsequentNodeId = subsequentNode.Id;
            return subsequentNode;
        }

        if (_determineSubsequentNode is null)
        {
            throw new ArgumentException("DetetmineSubsequentNode must not be null");
        }

        SubsequentNodeId = _determineSubsequentNode(Output);
        return SubsequentNodes.Single(x => x.Id.Equals(SubsequentNodeId));
    }

    public override string ToString()
    {
        return $"{Id}";
    }
}
