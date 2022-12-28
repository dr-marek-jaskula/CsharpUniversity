using System.Reflection;

namespace CustomTools.Utilities;

public static class AssemblyUtility
{
    public static IList<Type> GetAssignableFrom<TType>()
    {
        var assembly = Assembly<TType>();
        return assembly
            .GetTypes()
            .Where(type => type.IsAssignableFrom(typeof(TType)))
            .ToList();
    }

    public static Assembly Assembly<TType>()
    {
        var type = typeof(TType);
        return type.Assembly;
    }
}