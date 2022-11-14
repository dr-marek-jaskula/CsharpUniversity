namespace CsharpAdvanced.NET_7__csharp_11;

public sealed class FileScopedTypes
{
    
}

//It is new access modifier. We can have same two classes if at least one is 'file' scoped
file sealed class MyClass
{

}

file interface IMyInterface
{
    string Name { get; }
}

//we can mix them
public sealed class Extra : IMyInterface
{
    public string Name { get; }
}