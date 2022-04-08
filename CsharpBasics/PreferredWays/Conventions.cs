//Namespaces should match the folder structure
namespace CsharpBasics.PreferredWays;

//Class should be named PascalCase
public class Conventions
{
    //In this file we will cover the naming conventions and conventions connected to the structuring the solution

    //For static readonly and for const we use PascalCase -> these fields should be on the top of the class
    private static readonly string HeaderName = "X-API_KEY";
    const string HeaderDescription = "Best api key";

    //For private field we use underscored camelCase -> these field should be after static readonly and const field
    private string _userName = "Mark";

    //Then constructors. For parameters and local variables the pascalCase is used
    public Conventions(string userName)
    {
        _userName = userName;
    }

    //Finalizer

    ~Conventions()
    {

    }

    //Then delegates -> PascalCase
    public delegate string SomeDelegate(string value);

    //Events -> PascalCase
    public event EventHandler SomeEvent;

    //Next properties -> PascalCase

    public string SomeProperty { get; set; }

    //Then methods -> PascalCase. Order of methods: public -> internal -> protected -> private
    public void SomePublicMethod()
    {
    }
    
    internal void SomeInternalMethod()
    {
    }

    protected virtual void OnSomeEvent()
    {
        SomeEvent?.Invoke(this, EventArgs.Empty);
    }

    private void SomePrivateMethod()
    {
    }
}

