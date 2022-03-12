using Sigil; //for Emit
using System.Reflection;

namespace CsharpAdvanced.Reflections;

//For fair comparison, the someClass instance should be cached before
//Here we will compare the reflections (benchmarks are done in benchmarchs project)

public static class ReflectionPerformance
{
    /// <summary>
    /// Compares the way of getting private properties by reflection, to show how to optimize it
    /// </summary>
    public static void ReflectionPerformanceFunction()
    {
        //Console.WriteLine(TraditionalReflection());
        //Console.WriteLine(OptimizedTraditionalReflection());
    }

    /// <summary>
    /// Fast because it just gets the public value
    /// </summary>
    /// <returns></returns>
    public static string SimpleGet()
    {
        var someClass = new VeryPublicClass();
        return someClass.VeryPublicProperty;
    }

    /// <summary>
    /// Slow because getting property info is expensive process (and then getting value)
    /// </summary>
    /// <returns></returns>
    public static string TraditionalReflection()
    {
        var someClass = new VeryPublicClass();
        //VeryPrivateProperty is private so we need to use reflection to get it

        //At first we get the property info
        PropertyInfo propertyInfo = someClass.GetType().GetProperty("VeryPrivateProperty", BindingFlags.Instance | BindingFlags.NonPublic)!;

        //then we get the value of the private property or certain class instance
        var value = propertyInfo.GetValue(someClass);

        return value?.ToString()!;
    }

    //At first we cache the property info
    private static readonly PropertyInfo CachedProperty = typeof(VeryPublicClass).GetProperty("VeryPrivateProperty", BindingFlags.Instance | BindingFlags.NonPublic)!;

    /// <summary>
    /// Store the property info in the backing field, to optimize process
    /// </summary>
    /// <returns></returns>
    public static string OptimizedTraditionalReflection()
    {
        var someClass = new VeryPublicClass();
        //VeryPrivateProperty is private so we need to use reflection to get it

        //then we get the value of the private property or certain class instance
        var value = CachedProperty.GetValue(someClass);
        return value?.ToString()!;
    }

    private static readonly Func<VeryPublicClass, string> GetPropertyDelegate = (Func<VeryPublicClass, string>)Delegate.CreateDelegate(typeof(Func<VeryPublicClass, string>), CachedProperty.GetGetMethod(true)!);

    /// <summary>
    /// Store the delegate in cache, delegate stores the instance of the class and the value of the property.
    /// The Problem with this approach is that we have to know the class type by compile time. 
    /// </summary>
    /// <returns></returns>
    public static string CompiledDelegateOptimizedTraditionalReflection()
    {
        var someClass = new VeryPublicClass();
        return GetPropertyDelegate(someClass);
    }

    //the way to get the internal class type (assume that this internal class is in other file)
    private static readonly Type VeryInternalClassType = Type.GetType("CsharpAdvanced.Reflections.VeryInternalClass, CsharpAdvanced")!; //TypeFullName.ClassName, assemblyName

    private static readonly PropertyInfo CachedInternalProperty = VeryInternalClassType.GetProperty("VeryPrivateProperty", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly Emit<Func<object, string>> GetPropertyEmitter = Emit<Func<object, string>>
        .NewDynamicMethod("GetInternalPropertyValue")
        .LoadArgument(0)
        .CastClass(VeryInternalClassType)
        .Call(CachedInternalProperty.GetGetMethod(true)!)
        .Return(); //this will emitt the code

    private static readonly Func<object, string> GetPropertyEmttedDelegate =
        GetPropertyEmitter.CreateDelegate();

    /// <summary>
    /// Store the delegate in cache, delegate stores the instance of the class and the value of the property.
    /// This approach use the "sigil" nugget package. Run time type is available now
    /// However it is more dangerous?
    /// </summary>
    /// <returns></returns>
    public static string EmittedOptimizedTraditionalReflection()
    {
        var internalClass = Activator.CreateInstance(VeryInternalClassType);
        return GetPropertyEmttedDelegate(internalClass!);
    }
}

#region Helpers

public class VeryPublicClass
{
    public string VeryPublicProperty { get; set; } = "This is VeryPublicProperty";
    private string VeryPrivateProperty { get; set; } = "This is VeryPrivateProperty";
}

internal class VeryInternalClass
{
    public string VeryPublicProperty { get; set; } = "This is VeryPublicProperty";
    private string VeryPrivateProperty { get; set; } = "This is VeryPrivateProperty";
}

#endregion