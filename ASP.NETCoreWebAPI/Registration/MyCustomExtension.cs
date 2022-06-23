namespace ASP.NETCoreWebAPI.Registration;

//This shows how to add to the extension method the very popular approach of passing in the options in form of the lambda (action delegate)
//Such approach is very common in many libraries
public static class MyCustomExtension
{
    public static void AddCustom(this IServiceCollection services, Action<CustomOptions>? options = null)
    {
        //To configure options that are passed in
        services.Configure(options);

        //Do some work...
    }
}

public class CustomOptions
{
    public string? Prefix { get; set; }
    public int AgeFilter { get; set; } = 50;
}