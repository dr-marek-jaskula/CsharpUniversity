namespace CsharpAdvanced.NET_7__csharp_11;

public sealed class AutoDefaultStruct
{
    public static void InvokeExample()
    {

    }
}

file struct Point
{
    public int X; 
    public int Y;

    public Point(int x, int y) 
    { 
        X = x;
        //Y = y; //complier add default value if something is not set
    }
}