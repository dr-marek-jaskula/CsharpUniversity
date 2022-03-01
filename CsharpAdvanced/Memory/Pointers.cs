namespace CsharpBasics.Memory;

public class Pointers
{
    //Pointer are variable with value is a memory address of other variable
    //The C# statements can be executed either as in a safe or in an unsafe context.
    //The statements marked as unsafe by using the keyword unsafe runs outside the control of Garbage Collector.
    //In C# any code involving pointers requires an unsafe context.

    //try to avoid pointers void*, however they are allowed. This means that the pointer point the unkwon type

    //the 'unsefe' keyword is required to define pointers

    //To execute this code it is needed to end to the project:
    //	  <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    public static unsafe void InvokePointersExample()
    {
        // Normal pointer to an object.
        int[] a = new int[5] { 10, 20, 30, 40, 50 };

        // Must be in unsafe code to use interior pointers.
        unsafe
        {
            int number = 10;
            //pointer is define by asterisk '*' after the type
            //references (addresses of the variable) are obtained by placing ampersant '&' before the variable name

            int* pointer = &number; //set pointer's value to the address of the number variable
            //this is an integer value, so it is stored in the stack (so it is fixed)

            Console.WriteLine($"The address of the number variable with value {number} is {(int)pointer}");

            //To obtain the value that is pointered, we need to "dereference" the pointer which is just placing another asterisk '*' before the pointer
            *pointer = 50; //this will change the value of the number.

            Console.WriteLine($"The new value of number is {number} and it is equal to {*pointer}");

            //We must pin object on heap so that it doesn't move while using interior pointers (because defragmentation can occur -> because of Garbage Collector)
            fixed (int* p = &a[0])
            {
                // p is pinned as well as object, so create another pointer to show incrementing it.
                int* p2 = p;
                Console.WriteLine(*p2);
                // Incrementing p2 bumps the pointer by four bytes due to its type ...
                p2 += 1;
                Console.WriteLine(*p2);
                p2 += 1;
                Console.WriteLine(*p2);
                Console.WriteLine("--------");
                Console.WriteLine(*p);
                // Dereferencing p and incrementing changes the value of a[0] ...
                *p += 1;
                Console.WriteLine(*p);
                *p += 1;
                Console.WriteLine(*p);
            }
        }
    }
}

