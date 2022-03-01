namespace CsharpAdvanced.Introduction;

public class Constraints
{
    // Constraints are used restrict client code to specify certain types while instantiating generic types
    // Without any constraints, the type argument could be any type
    // Constraints are specified by using the where contextual keyword
    // Existing constraints: (can combine multiple constraints)
    /*
     where T : struct               type has to be a value type 
     where T : class                type has to be a reference type
     where T : class?               This constraint applies also to any class, interface, delegate, or array type.
     where T : notnull              The argument can be a non-nullable reference type in C# 8.0 or later, or a non-nullable value type.
     where T : new()                The type argument must have a public parameterless constructor. When used together with other constraints, the new() constraint must be specified last. The new() constraint can't be combined with the struct and unmanaged constraints. 
     where T : default              This constraint resolves the ambiguity when you need to specify an unconstrained type parameter when you override a method or provide an explicit interface implementation. The default constraint implies the base method without either the class or struct constraint. 
     where T : unmanaged            The type argument must be a non-nullable unmanaged type. The unmanaged constraint implies the struct constraint and can't be combined with either the struct or new() constraints.
     where T : <base class name>    The type argument must be or derive from the specified base class. In a nullable context in C# 8.0 and later, T must be a non-nullable reference type derived from the specified base class.
     where T : <base class name>?   The type argument must be or derive from the specified base class. In a nullable context in C# 8.0 and later, T may be either a nullable or non-nullable type derived from the specified base class.
     where T : <interface name>     The type argument must be or implement the specified interface. Multiple interface constraints can be specified. The constraining interface can also be generic. In a nullable context in C# 8.0 and later, T must be a non-nullable type that implements the specified interface.
     where T : <interface name>?    The type argument must be or implement the specified interface. Multiple interface constraints can be specified. The constraining interface can also be generic. In a nullable context in C# 8.0, T may be a nullable reference type, a non-nullable reference type, or a value type. T may not be a nullable value type.
     */

    /* Multiple types
        class Base 
        { }
        class Test<T, U> where U : struct where T : Base, new()
        { }
     */

    public delegate void DataDelegate(int delegateField);

    public static void InvokeConstraintsExamples()
    {
        #region where T : struct

        //Notice that the generic type needs to be a value type:
        DataStructStore<int> dataStore = new();
        DataStructStore<double> dataStore2 = new();
        DataStructStore<DataStructure> dataStore3 = new();
        DataStructStore<DataEnum> dataStore4  = new();
        DataStructStore<Guid> dataStore4b  = new();
        DataStructStore<DateTime> dataStore4c  = new();
        //DataStore<DataClass> dataStore5 = new(); //this is not allowed

        #endregion where T : struct

        #region where T : class

        //Notice that the generic type needs to be a reference type:
        DataClassStore<string> dataStore5 = new(); 
        DataClassStore<object> dataStore6 = new(); 
        DataClassStore<dynamic> dataStore7 = new(); 
        DataClassStore<DataClass> dataStore8 = new(); 
        DataClassStore<IDataClass> dataStore9 = new();
        DataClassStore<DataRecord> dataStore10 = new();
        DataClassStore<DataDelegate> dataStore11 = new();

        #endregion where T : class

        #region where T : notnull, new()

        //here two constraint at one time will be discussed (however they done need to be combined)
        //notnull says that the type cannot hold null value (but it can be specified at the 
        //new() says that the type needs to have parameterless constructor
        DataNotnullStore<object> dataStore12 = new();
        DataNotnullStore<int> dataStore13 = new();
        DataNotnullStore<DateTime> dataStore14 = new();
        DataNotnullStore<Guid> dataStore15 = new();
        DataNotnullStore<DataClass> dataStore16 = new();
        //DataNotnullStore<DataClass?> dataStore17 = new(); //not allowed
        //DataNotnullStore<DataStructure?> dataStore18 = new(); //not allowed
        //DataNotnullStore<DataWithoutParameterlessConstructor> dataStore19 = new(); //not allowed

        #endregion where T : notnull, new()

        #region where T : <base class name>

        DataBaseClassNameStore<Animal> dataStore20 = new(); 
        DataBaseClassNameStore<Dog> dataStore21 = new();
        //DataBaseClassNameStore<Dog?> dataStore22 = new(); //not allowed (but would if we change constraint to "where T : Animal?" or "where T : Dog?"
        //DataBaseClassNameStore<DataClass> dataStore23 = new(); //not allowed

        #endregion where T : <base class name>

        #region where T : <interface name> 

        DataInterfaceNameStore<IInterface> dataStore24 = new();
        DataInterfaceNameStore<DataImplementsInterface> dataStore25 = new();
        //DataInterfaceNameStore<DataClass> dataStore26 = new(); //not allowed

        #endregion where T : <interface name> 
    }
}

#region where T : struct

internal class DataStructStore<T> where T : struct
{
    public T? Data { get; set; }
}

internal struct DataStructure
{
    public int structProperty { get; }

    public DataStructure()
    {
        structProperty = 0;
    }
}

internal enum DataEnum
{
    FirstData, SecondData
}

#endregion where T : class

#region where T: class

internal class DataClassStore<T> where T : class
{
    public T? Data { get; set; }
}

internal class DataClass
{
    public int classPRoperty { get; set; }
}

internal interface IDataClass
{
    public int interfaclePorperty { get; set; }
}

internal record DataRecord //its just a class with some predefined features. They are discussed in: "Introduction" folder, "Record.cs" file
{
    public int recordProperty { get; set; }
}

#endregion where T: class

#region where T : notnull, new()

internal class DataNotnullStore<T> where T : notnull, new()
{
    public T Data { get; set; } = new T();
}

internal class DataWithoutParameterlessConstructor
{
    public int SomeProperty { get; set; }

    public DataWithoutParameterlessConstructor(int someProperty)
    {
        SomeProperty = someProperty;
    }
}

#endregion where T : notnull

#region where T : <base class name>

internal class DataBaseClassNameStore<T> where T : Animal
{
    public T? Data { get; set; }
}

internal class Animal
{
    public void AnimalSound()
    {
        Console.WriteLine("Make sound");
    }
}

internal class Dog : Animal
{
    public new void AnimalSound()
    {
        Console.WriteLine("Dog sound");
    }
}

#endregion where T : <base class name>

#region where T : <interface name> 
internal class DataInterfaceNameStore<T> where T : IInterface
{
    public T? Data { get; set; }
}

internal interface IInterface
{
    public void InterfaceSound();
}

internal class DataImplementsInterface : IInterface
{
    public void InterfaceSound()
    {
        Console.WriteLine("Beep");
    }
}

#endregion where T : <interface name> 
