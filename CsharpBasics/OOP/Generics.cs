namespace CsharpBasics.OOP;

public class Generics
{
}

//default value for generic type (only way)
public class MyTemplate<T1, T2>
{
    public T1? Prop1 { get; set; }
    public T2? Prop2 { get; set; }
}

public class MyTemplate<T1> : MyTemplate<T1, string> { }

