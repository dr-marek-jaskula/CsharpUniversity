namespace CsharpBasics.PreferredWays;

public class NullChecks
{
    //The null checks in c# evolved in time

    public static void InvokeNullChecksExamples()
    {
        //oldest way
        int? name = 0;

        if (name == null)
            throw new ArgumentNullException(nameof(name));

        //old way
        if (name != null) 
        {
        }

        //c# 9.0

        //newer way
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        //old way 
        if (!(name is null)) 
        { 
        }

        //other way
        if (name is object) //if its an object it is not null
        {

        }

        if (name is {}) // same as above but a bit odd
        {

        }

        //newer way
        if (name is not null) 
        { 
        }

        //newer way
        _ = name ?? throw new ArgumentNullException(nameof(name));

        //c# 10.0
        ArgumentNullException.ThrowIfNull(name);
        //There has a second, optional argument, annotated with the [CallerArgumentExpression] attribute.
        //This attribute takes a single argument that is required to identify some other parameter
        //(and the C# compiler will tell you if the name you've used doesn't match any of your parameter names).
        //This works in a similar way to longer-established attributes such as [CallerMemberName] and [CallerLineNumber]:
        //when a method has one of these attributes on an optional parameter, and when some code calls that method without supplying an explicit value for that parameter,
        //the C# compiler generates code that supplies the relevant contextual information. With [CallerMemberName], it supplies the name of the method or property from which the call is being made. 

        //And with this new[CallerArgumentExpression] it provide the text of the expression that was used to supply the argument value. So in this case those first three lines effectively become this:

        //ArgumentNullException.ThrowIfNull(id, "id");
        //ArgumentNullException.ThrowIfNull(name, "name");
        //ArgumentNullException.ThrowIfNull(favouriteColour, "favouriteColour");

        //The [NotNull] on the first argument is not strictly on-topic, but worth discussion because its behavior is slightly non-obvious.
        //Why is the argument declared as nullable (object? rather than object) and yet marked as [NotNull]? This attribute expresses a post-condition:
        //it says that if this method returns, then the compiler can safely deduce that argument was not null
        //More info in ParameterAttributes

        //c# 11 introduces changes to this topic
    }
}

