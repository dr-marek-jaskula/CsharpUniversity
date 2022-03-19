#define LOG_INFO
using System.Diagnostics;
using System.Reflection;

//We can define some metadata also on a assembly level. To do this we need to use attribute above the namespace
[assembly: AssemblyDescription("My Assembly Description")] //we can change the app behavior this way (for example Controller attr in old fashion asp.net)
namespace CsharpAdvanced.Attributes;

public class AssemblyAttributes
{
    public static void TechingIsGold()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        AssemblyName assemblyName = assembly.GetName();
        Version? version = assemblyName.Version;

        object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

        var assemblyDescriptionAttribute = attributes[0] as AssemblyDescriptionAttribute;

        Debug.WriteLine($"Assembly Name: {assemblyName}");
        Debug.WriteLine($"Assembly Version: {version}");

        if (assemblyDescriptionAttribute is not null)
            Debug.WriteLine($"Assembly Description: {assemblyDescriptionAttribute.Description}");
    }
}
