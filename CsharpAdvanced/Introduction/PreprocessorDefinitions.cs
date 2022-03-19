//We define the FOO pre-compile (before "usings")
#define FOO

namespace CsharpAdvanced.Introduction;

public class PreprocessorDefinitions
{
    //We define some parameters/arguments before the compilation of rest of the code

    public static void InvokePreprocessorDefinitionsExamples()
    {
        //This if is precompile if: "if FOO was defined this code will compile"
        #if FOO
        Console.WriteLine("Fooing away");
        #endif

        //FOO1 is not defined so it wont be executed 
        #if FOO1
        Console.WriteLine("Fooing away");
        #endif

        //This variable is defined in the debug mode, so it will be executed in debug mode
        #if DEBUG
        Console.WriteLine("Im in debug mode!!!");
        #endif

        //Trace is defined in the debug and release mode (but we can change this)
        #if TRACE
        Console.WriteLine("I'm in Release mode!!!");
        #endif
    }
}

//We can also use the Conditional attribute -> see BuildInAttributes
