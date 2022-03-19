using System.Diagnostics;
using System.Reflection;

namespace CsharpAdvanced.Attributes;

//We can also use attributes for method parameters: it is commonly use in controller in ASP.Net [examine the AS.Net University project]

//To create a custom attributes that will be used for parameters we just need to:

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class DisplayPropertyAttribute : Attribute
{
    public string DisplayName { get; set; }

    public DisplayPropertyAttribute(string displayName)
    {
        DisplayName = displayName;
    }
}

public class ParameterAttributes
{
    public static void InvokeParameterAttributesExamples()
    {
        Worker worker = new() { FirstName = "Adam", LastName = "Omidur" };
        worker.Work("Working on core project");
    }
}

class Worker
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    /// <summary>
    /// Method with attribute on parameter
    /// </summary>
    /// <param name="value"></param>
    public void Work([DisplayProperty("myValue")] string value)
    {
        Debug.WriteLine("Working...");
        Type type = value.GetType();
        var attribute = type.GetCustomAttribute<DisplayPropertyAttribute>();
        Debug.WriteLine($"My true name is {attribute?.DisplayName} and I will {value}");
    }
}