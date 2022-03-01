using CsharpBasics.Keywords;
using System.Collections;
using System.Xml.Linq;

namespace CsharpBasics.OOP;

public class Structures
{
    #region Structures introduction with example

    //structure is a similar concept to classes (can encapsulate data and related functionality).
    //The main difference is that the structure is a value type, not a reference type. (All struct types implicitly inherit from the class System.ValueType)
    //therefore structure is allocated in the stack, not in the heap
    //structure fits for small amounts of data

    //structure-type variable can't be null (unless it's a variable of a nullable value type),

    //As it is a value type:
    //Each variable contains its own copy of the data (except in the case of the ref and out parameter variables)
    //and an operation on one variable does not affect another variable.

    //Other important differences:
    //struct can not inherit, but they can implement interfaces
    //The default value of a struct is the value produced by setting all fields to their default value 

    //Good idea is to store few members in the struct, co it can represent for example a 2D point
    internal struct Point2D
    {
        public int x = 667; //since c# is is possible to have initializers here
        public int y;

        //When using this method, the defensive copy is made, impacting the performace
        public double DistanceFromZero()
        {
            return Math.Round(Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)));
        }

        //Beginning with C# 8.0, you can also use the readonly modifier to declare that an instance member doesn't modify the state of a struct. 
        //If you can't declare the whole structure type as readonly, use the readonly modifier to mark the instance members that don't modify the state of the struct.
        //To avoid the problem of defensive copies we can use (after c# 8.0) 'readonly' modifies, telling compiler not to make a defensive copy
        public readonly int MultiplyCoordinates()
        {
            return x * y;
        }

        //Beginning with C# 10, you can declare a parameterless instance constructor in a structure type, as the following example shows:
        public Point2D()
        {
            //x = 10; //the x initializer is above at variable declaration.
            y = 10;
        }
        //If you don't declare a parameterless constructor explicitly, a structure type provides a parameterless constructor whose behavior is as follows:
        //If a structure type has explicit instance constructors or has no field initializers, an implicit parameterless constructor produces the default value of a structure type, regardless of field initializers
        //If a structure type has no explicit instance constructors and has field initializers, the compiler synthesizes a public parameterless constructor that performs the specified field initializations

        //we override the operator + to achieve the desired result
        public static Point2D operator +(Point2D a, Point2D b)
        {
            return new Point2D(a.x + b.x, a.y + b.y);
        }

        //structs can have empty constructor. Each constructor HAS TO define all members
        public Point2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    #endregion Structures introduction with example

    #region Readonly struct

    //Beginning with C# 7.2, you use the readonly modifier to declare that a structure type is immutable. All data members of a readonly struct must be read-only as follows:
    //Any field declaration must have the readonly modifier
    //Any property, including auto-implemented ones, must be read-only.In C# 9.0 and later, a property may have an init accessor.
    //That guarantees that no member of a readonly struct modifies the state of the struct. In C# 8.0 and later, that means that other instance members except constructors are implicitly readonly.
    internal readonly struct Point3D
    {
        public readonly int x; 
        public readonly int y;
        public readonly int z;

        public readonly double DistanceFromZero()
        {
            return Math.Round(Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) + Math.Pow(z, 2));
        }

        public readonly int MultiplyCoordinates()
        {
            return x * y * z;
            //Within a readonly instance member, you can't assign to structure's instance fields.
            //However, a readonly member can call a non-readonly member. In that case the compiler creates a copy of the structure instance and calls the non-readonly member on that copy. As a result, the original structure instance is not modified.
        }

        public static Point3D operator +(Point3D a, Point3D b)
        {
            return new Point3D(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public Point3D(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    //In a readonly struct, a data member of a mutable reference type still can mutate its own state.
    //For example, you can't replace a List<T> instance, but you can add new elements to it.

    #endregion Readonly struct

    #region ref struct

    //When you pass a structure-type variable to a method as an argument or return a structure-type value from a method,
    //the whole instance of a structure type is copied.
    //That can affect the performance of your code in high-performance scenarios that involve large structure types.
    //You can avoid value copying by passing a structure-type variable by reference

    //Use the ref, out, or in method parameter modifiers to indicate that an argument must be passed by reference.
    //Use ref returns to return a method result by reference

    //Beginning with C# 7.2, you can use the ref modifier in the declaration of a structure type.
    //Instances of a ref struct type are allocated on the stack and can't escape to the managed heap

    /*
    A ref struct can't be the element type of an array.
    A ref struct can't be a declared type of a field of a class or a non-ref struct.
    A ref struct can't implement interfaces.
    A ref struct can't be boxed to System.ValueType or System.Object.
    A ref struct can't be a type argument.
    A ref struct variable can't be captured by a lambda expression or a local function.
    A ref struct variable can't be used in an async method. However, you can use ref struct variables in synchronous methods, for example, in those that return Task or Task<TResult>.
    A ref struct variable can't be used in iterators.
    Beginning with C# 8.0, you can define a disposable ref struct.
    */

    #region Example 1

    public ref struct CustomRef
    {
        public bool IsValid;
        public Span<int> Inputs;
        public Span<int> Outputs;
    }

    //To declare a ref struct as readonly, combine the readonly and ref modifiers in the type declaration 
    public readonly ref struct ConversionRequest
    {
        public ConversionRequest(double rate, ReadOnlySpan<double> values)
        {
            Rate = rate;
            Values = values;
        }

        public double Rate { get; }
        public ReadOnlySpan<double> Values { get; }
    }
    //This C# feature is also known as “interior pointer” or “ref-like types”.
    //The proposal is to allow the compiler to require that certain types such as Span<T> only appear on the stack

    #endregion Example 1

    #region Example 2

    //it tell that this thing can be only allocated on the stack
    //so it cant be a class member because it would be allocated on the heap
    internal ref struct IntHolder
    {
        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    #endregion Example 2

    #region ref return

    //By using ref and return ref, we can return the reference to the object
    //integer is a value type so normally we would loose the reference
    public static ref int GetNumberInPosition(int[] arr, int position)
    {
        return ref arr[position];
    }

    #endregion ref return

    #endregion

    public static void InvokeStructureExamples()
    {
        Point2D point0 = new();

        //this will crate a struct with a default values so point (0,0) (and all reference-type fields to null if there are so)
        Point2D point0a = default(Point2D);

        //The preferred way
        Point2D point1 = new(2,3);

        Point2D point2; //we can also just declare the struct variable, which will result in default values
        point2.x = 5;
        point2.y = 3;

        Point2D point3 = new(1,1);
        point3 = point2; //the value will be copied
        point3.x = 7;

        #region Nondestructive mutation

        //Beginning with C# 10, you can use the with expression to produce a copy of a structure-type instance with the specified properties and fields modified.
        //You use object initializer syntax to specify what members to modify and their new values, as the following example shows:

        var p1 = new Point2D(0, 0);
        Console.WriteLine(p1);  // output: (0, 0)

        var p2 = p1 with { x = 3 }; //make a copy with x set to 3. This way is more efficient because makes only one copy and:
        //p2 = p1; //one copy
        //p2.x = 3; //second copy
        //makes two copies
        Console.WriteLine(p2);  // output: (3, 0)

        var p3 = p1 with { x = 1, y = 4 };
        Console.WriteLine(p3);  // output: (1, 4)

        #endregion Nondestructive mutation

        #region ref struct example 2

        //nested function
        void ChangeValue(IntHolder value)
        {
            value.Value = 3;
        }

        IntHolder intHolder = new();
        intHolder.Value = 60;
        ChangeValue(intHolder);

        #endregion ref struct example 2

        #region return ref

        var number = new[] { 69, 320 };
        //need ref to the method call and on local  
        ref var value = ref GetNumberInPosition(number, 1);
        value = 10; //now the element of the array was modified (debug to examine this)
        Console.WriteLine($"Number is {value}");
        //it is also possible to make ref var readonly variable and make it immutable

        #endregion return ref
    }
}

