using System.Reflection;

namespace CsharpAdvanced.Reflections;

public class FieldInfo_SetValue
{
    public static void GoSetValues()
    {
        //We instantiate we class in order to demonstrate setting private value
        ClassForSetExample myObject = new ClassForSetExample();
        Type myType = typeof(ClassForSetExample);

        //At first we get the field: BindingFlags are needed for qualifying the member we want to get
        FieldInfo myFieldInfo = myType.GetField("_myString", BindingFlags.NonPublic | BindingFlags.Instance)!;

        //Display the string before applying SetValue to the field.
        Console.WriteLine($"\nThe field value of _myString is \"{myFieldInfo.GetValue(myObject)}\".");

        //Display the SetValue signature used to set the value of a field.
        Console.WriteLine("Applying SetValue(Object, Object).");

        //Change the field value using the SetValue method.
        myFieldInfo.SetValue(myObject, "New value");

        //Display the string after applying SetValue to the field.
        Console.WriteLine($"The field value of _myString is \"{myFieldInfo.GetValue(myObject)}\".");
    }
}

//This code example produces the following output:
//The field value of myString is "Old value".
//Applying SetValue(Object, Object).
//The field value of mystring is "New value".
 

public class ClassForSetExample
{
    private string _myString;

    public ClassForSetExample()
    {
        _myString = "Old value";
    }

    public string MyStringProperty
    {
        get
        {
            return _myString;
        }
    }
}