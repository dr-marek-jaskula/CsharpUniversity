namespace CsharpAdvanced.TreeStructure;

public sealed class Root<T, K> : Node<T, K>
    where T : notnull
    where K : class
{
    public Root(T id, Func<K, K> execute, Func<K, T>? determineNextNode = null)
        : base(id, execute, determineNextNode)
    {
        PreviousNode = null;
    }
}