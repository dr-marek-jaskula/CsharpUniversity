//The using keyword has two major uses:
// 1) The using statement defines a scope at the end of which an object will be disposed.
// 2) The using directive creates an alias for a namespace or imports types defined in other namespaces.

#region using directive

// At first we deal with the 'using directive'
// As it was witted, it can import functionalities

global using System.Collections.Generic; //The global modifier has the same effect as adding the same using directive to every source file in your project. This modifier was introduced in C# 10.
//global using must precede all other usings
//It is good practice to specify global in a project file (or file connected to the project file) or in a single file 
using System.Linq; //this import Linq functionalities to the file
using static System.Math; //using static is used to omit the "System.Math" prefix when using System.Math functions
//The static modifier imports the static members and nested types from a single type rather than importing all the types in a namespace. This modifier was introduced in C# 6.0.
//we can also: 'global using static System.Math;'

// Using alias directive for a class.
using AliasToMyClass = CsharpBasics.Introduction.Car;

// using alias directive for a generic class.
// using UsingAlias = NameSpace2.MyClass<int>;

using MyAlias = System.Console;

#endregion using directive

namespace CsharpBasics.Keywords;

public class Using
{
    public static void InvokeUsingExamples()
    {
        List<string> examples = new List<string>() { "First", "Second", "Third" };
        var filtredList = examples.Where(x => x.StartsWith('T')); //we imported Linq library, so we can use it

        double number = 3;
        double power = 4;
        double result = Pow(number, power); //we can just write "Pow" because we have written "using static"

        MyAlias.WriteLine(result); //using alias

        #region using statement

        //Provides a convenient syntax that ensures the correct use of IDisposable objects.
        //Beginning in C# 8.0, the using statement ensures the correct use of IAsyncDisposable objects.
        //A simple example of "using statement" can be found in "CSharpBasics.DataManagment.DataFromFiles.cs"

        //old way, but still sometimes useful:
        using (MyResource myRes = new MyResource())
        {
            myRes.DoSomething();
        }

        //this block of code is equivalent to:
        { // Limits scope of myRes
            MyResource myRes2 = new MyResource();
            try
            {
                myRes2.DoSomething();
            }
            finally
            {
                // Check for a null resource.
                if (myRes2 != null)
                    // Call the object's Dispose method.
                    ((IDisposable)myRes2).Dispose();
            }
        }

        //modern way is to use the following, elegant syntax:
        using var myRes3 = new MyResource(); //here it is important to notice, that using this syntax, from this moment if the current scope would be leaved, the Dispose method will be executed 
        myRes3.DoSomething();

        //So 'using statement' is used in various cases, mostly when we connect to file, db or server

        #endregion using statement
    }
}

public class MyResource : IDisposable
{
    public void DoSomething()
    {
        MyAlias.WriteLine("Doing something...");
    }

    public void Dispose()
    {
        MyAlias.WriteLine("Close the connection");
    }
}