using System.Diagnostics;
using System.Reflection;

namespace CsharpAdvanced.Attributes;

//Basics of Attributes
//you can use attributes to inject additional information (metadata) to the assemblies that can be queried at runtime (if needed - using reflection)
//An attribute is a piece of additional declarative information that is specified for a declaration

//Attributes have the following properties:

//1. Attributes add metadata to your program. Metadata is information about the types defined in a program. All .NET assemblies contain a specified set of metadata that describes the types and type members defined in the assembly. You can add custom attributes to specify any additional information that is required.
//2. You can apply one or more attributes to entire assemblies, modules, or smaller program elements such as classes and properties.
//3. Attributes can accept arguments in the same way as methods and properties.
//4. Your program can examine its own metadata or the metadata in other programs by using reflection. For more information, see Accessing Attributes by Using Reflection(C#).

#region Basics of Attributes
//An attribute is just a class that inherits from the "Attribute" class
//The class name should end with "Attribute" suffix (it is good practice)
public class MyCustomAttribute : Attribute
{
}

//To add our attribute to other class/method/variable we just need to:
[MyCustom]
public class TestingMyCustomAttr
{
}

//We can also use the whole name
[MyCustomAttribute]
public class TestingMyCustomAttrFullName
{
}

//We can use Attributes for classes, variables, methods and much more (for example interfaces):
public class TestingMyCustomAttrForDifferentEntities
{
    [MyCustom]
    string myString1 = string.Empty;

    [MyCustom]
    public int MyProperty { get; set; }

    [MyCustom]
    public void MyMethod() { }
}

//We can create an Attribute just for specified entities. By default it is  set to "all"
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property)]
public class MySecondCustomAttribute : Attribute
{
}

// jesli napiszemy tu [MyExample2] to będzie nam podreślać, chociać inteligence nam i tak to podpowie
public class TestingMySecondCustomAttrDifferentEntities
{
    [MySecondCustom]
    int myField;

    [MySecondCustom]
    public void MyMethod() { }
}

#endregion

#region Attributes members

//Now let us add some members to Attrubite class
[AttributeUsage(AttributeTargets.Class)] 
public class MyThirdCustomAttribute : Attribute
{
    public string Name { get; set; } = string.Empty;
    public int Version { get; set; }
}

//We use the attribute and give it some metadata
[MyThirdCustom(Name = "John", Version = 1)]
public class TestingCustomAttrWithMembers
{
    public int IntValue { get; set; }

    public void Method() { }
}

//AllowMultiple (by default set to false) says do we allow to use multiple times a single attribute for some class
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)] 
public class MyFourthCustomAttribute : Attribute
{
    public string Name { get; set; } = string.Empty;
    public int Version { get; set; }
}

[MyFourthCustom(Name = "Wenus", Version = 2)]
[MyFourthCustom(Name = "Mars", Version = 4)]
public class TestingMultipleCustomAttWithMembers
{
    public int IntValue { get; set; }
    public void Method() { }
}

#endregion

//✔️ Name custom attribute classes with the suffix "Attribute."

//✔️ Apply the AttributeUsageAttribute to custom attributes.

//✔️ Provide settable properties for optional arguments.

//✔️ Provide get-only properties for required arguments.

//✔️ Provide constructor parameters to initialize properties corresponding to required arguments. Each parameter should have the same name (although with different casing) as the corresponding property.

//❌ AVOID providing constructor parameters to initialize properties corresponding to the optional arguments.

//In other words, do not have properties that can be set with both a constructor and a setter. This guideline makes very explicit which arguments are optional and which are required, and avoids having two ways of doing the same thing.

//❌ AVOID overloading custom attribute constructors.

public class AttributesBasics
{
    public static void InvokeAttributesBasicsExamples()
    {
        //Let us get (using linq) the custom attributes we created
        var types = from t in Assembly.GetExecutingAssembly().GetTypes() where t.GetCustomAttributes<MyThirdCustomAttribute>().Count() > 0 select t;

        foreach (var item in types)
        {
            Debug.WriteLine(item.Name);

            foreach (var t in item.GetProperties())
                Debug.WriteLine(t.Name);
        
            foreach (var t in item.GetMethods())
                Debug.WriteLine(t.Name);
        }
    }
}