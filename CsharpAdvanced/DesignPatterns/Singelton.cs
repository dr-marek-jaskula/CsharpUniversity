using System.Diagnostics;

namespace CsharpAdvanced.DesignPatterns;

 
//Use Singleton if in application there should be just one, global object of certain type. 
//Good example is to use Singleton for database connection (EF Core), or in ASP.Net

//Singleton can be created only using the specific method.

// The Singleton class defines the `GetInstance` method that serves as an
// alternative to constructor and lets clients access the same instance of
// this class over and over.
class Singleton
{
    // The Singleton's constructor should always be private to prevent
    // direct construction calls with the `new` operator.
    private Singleton() { }

    // The Singleton's instance is stored in a static field. There there are
    // multiple ways to initialize this field, all of them have various pros
    // and cons. In this example we'll show the simplest of these ways,
    // which, however, doesn't work really well in multithreaded program.
    private static Singleton _instance;

    // This is the static method that controls the access to the singleton
    // instance. On the first run, it creates a singleton object and places
    // it into the static field. On subsequent runs, it returns the client
    // existing object stored in the static field.
    public static Singleton GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Singleton();
        }
        return _instance;
    }

    // Finally, any singleton should define some business logic, which can
    // be executed on its instance.
    public static void someBusinessLogic()
    {
        // ...
    }
}

class SingletonPattern
{
    public static void InvokeSingletonExamples()
    {
        // The client code.
        Singleton s1 = Singleton.GetInstance();
        Singleton s2 = Singleton.GetInstance();

        if (s1 == s2)
            Debug.WriteLine("Singleton works, both variables contain the same instance.");
        else
            Debug.WriteLine("Singleton failed, variables contain different instances.");
    }
}
