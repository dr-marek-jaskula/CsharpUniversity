namespace CsharpAdvanced.NET_7__csharp_11;

public sealed class RequiredMembers
{
    public static void InvokeExmpale()
    {
        var x = new SuperUser
        {
            Name = "Test"
        };
    }
}

public sealed class SuperUser
{
    //we need to initialize the property
    public required string Name { get; init; }
}