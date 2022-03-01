using System.Globalization;
using System.Reflection;

namespace CsharpBasics.Introduction;

//In this file we cover the topic of basic types and casting

public class Types
{
    public static void InvokeTypesExamples()
    {
        #region Value types

        //Value types are allocated in the stack or in the heap
        //It depends on where they were created
        //To examine this topic deeply, to go "CsharpAdvanced" project, "Introduction" folder and "MemoryAllocation.cs" file

        //Value types in c# are:
        // 1) Integral numeric types
        // 2) Floating-point numeric type
        // 3) bool type
        // 4) char type (UTF-16)

        #region Integral types

        //'sbyte' (signed byte) is signed 8-bit integer: from -128 to 127
        sbyte signedByte; //byte by definition is 8 bits

        //'byte' is unsigned 8-bit integer: from 0 to 255
        byte unsignedByte;

        //'short' is signed 16-bit integer: from -32 768 to 32 767
        short signedShort;
        
        //'ushort' is signed 16-bit integer: from 0 to 65 535
        ushort unsignedShort;

        //'int' is signed 32-bit integer: from -2 147 483 648 to 2 147 483 647
        int signedInteger; //mostly popular

        //'uint' is unsigned 32-bit integer: from 0 to 4 294 967 295
        uint unsignedInteger;

        //'long' is signed 64-bit integer: from -9 223 372 036 854 775 808 to 9 223 372 036 854 775 807
        long signedLong;
        
        //'long' is unsigned 64-bit integer: from 0 to 18 446 744 073 709 551 615
        ulong unsignedLong;

        //There are also 'nint" and "nuint" that are 32-bit or 64-bit, depending on a platform

        //We can define a variable also as an interger in the following way
        System.Int32 otherInteger;

        //By default if the type has to be obtained from the right hand side, the type is int if it can be int
        var defaultTypeOrIntegralNumber = 10;

        //If the value is not specified, the defualt value is equal 0
        int defaultValue;
        int defaultValue2 = default;

        #endregion Integral types

        #region Integer literals

        //There are three integer literal in c# (by default)
        //decimal: without any prefix
        //hexadecimal: with the '0x' or '0X' prefix
        //binary: with the '0b' or '0B' prefix

        var decimalLiteral = 42;
        var hexadecimalLiteral = 0x2A;
        var binaryLiteral = 0b00101010;

        #endregion Integer literals
        
        #region Digit separator
        //We can use the digit separator '_' to make a number readable

        var decimalLiteral2 = 4_423_32;
        var hexadecimalLiteral2 = 0x2A_B4_13;
        var binaryLiteral2 = 0b0010_1010_1001;

        #endregion Digit separator

        #region Floating point numbers

        //In c# there exist three types of floating-point number: float, double, decimal

        //'float' is signed 4-bytes number, with precision of approximately 6-9 digits.
        float floatNumber;

        //'double' is signed 8-bytes number, with precision of approximately 15-17 digits
        double doubleNumber; 

        //'decimal' is signed 16-bytes number, with precision of approximately 28-29 digits
        decimal decimalNumber;

        //By default if we assign a floating-point number to a variable it becomes a 'double' variable
        var doubleNumber2 = 12.5;

        //Alternate declaration
        System.Double doubleNumber3;

        //The default value of the floating-point number is 0
        double doubleNumber4 = default;

        //Each floating point MinValue, MaxValue constants. For example
        var doubleMaxValue = double.MaxValue;

        #endregion

        #region Real literals

        //The type of a real literal is determined by its suffix:
        //'d' or 'D' is a suffix for double (without suffix it is consider default case)
        //'f' or 'F' is a suffix for a float
        //'m' or 'M' is a suffix for a decimal

        double doubleValue = 3D;
        double doubleValue2 = 13.4d;
        double doubleValue3 = 13.442_002;

        float floatValue = 3_000.3F;
        float floatValue2 = 3_000.33_331f;

        decimal decimalValue = 3_000.2M;
        decimal decimalValue2 = 333_331_000.213_221m;

        //We can use the exponential notation, using suffix 'e' or 'E'
        double doubleValue4 = 0.4213e2; //its 0.4213 * 10^2
        float floatValue3 = 132.42E-2f; //its 132.42 * 10^-2
        decimal decimalValue3 = 1.4213e5m; //its 1.4213 * 10^5

        #endregion

        #region bool

        //bool represents the boolean value which can be either true or false keywords
        //bool is used for logical operator, for example in Predicates, loops, conditionals

        //there are two ways of declaring a boolean variable
        bool boolValue;
        System.Boolean boolValue2;

        //By default the declared boolean value is false
        bool boolValue3 = default;

        //basic operation are:
        // "&&" and operator
        // "||" or operator
        // "^" xor operator
        // "!" not operator

        //Even though the bool is just true or false it size is 1 byte:
        int sizeOfBool = sizeof(bool);

        #endregion bool

        #region char

        //char represents a Unicode UTF-16 character
        //the size of the char is 16-bits

        char charValue;
        System.Char charValue2;

        //By default the char is '\0' (U+0000)
        char charValue3 = default;

        //char can be specified by a character literal
        char charValue4 = 'p';

        //or Unicode literal by '\u' and the four-symbol hexadecimal representation
        char charValue5 = '\u006A';

        //we can also cast the number to character
        char charValue6 = (char)106;
        
        int charMaxValue = char.MaxValue;

        #endregion char

        #endregion Value types

        #region Strings and Enums

        //Strings are covered in other file "Introduction" -> "String.cs"

        //Enums are covered in other file "Introduction" -> "Enums.cs"

        #endregion Strings and Enums

        #region DateTime, DateOnly, TimeOnly, TimeSpan

        //One of the common used types in c# is the DateTime. It is a referenced type.

        //We create the DateTime instance by calling a constructor 
        DateTime dateTime = new(1991, 6, 18);

        //We can also parse the string representation
        DateTime dateTime1 = DateTime.Parse("6/18/1991 8:30:52 AM", CultureInfo.InvariantCulture); //month is before the day

        //Since c# 10, we can use DateOnly and TimeOnly classes
        DateOnly dateOnly = new(1991, 6, 18);
        TimeOnly timeOnly = new(8, 22, 54);

        //To obtain DateTime we can:
        DateTime dateTime2 = dateOnly.ToDateTime(timeOnly);

        //TimeSpan represents the interval of time

        DateTime dateOfBirth = new(1991, 8, 12);
        TimeSpan timeSpan = DateTime.Now - dateOfBirth;
        Console.WriteLine($"You were born: {timeSpan.TotalDays} days ago");
        //so TimeSpan contains information about the time internalval

        #endregion

        #region TypeOf, Type, SizeOf

        //Represents type declarations: class types, interface types, array types, value types, enumeration types, delegates types, type parameters, generic type definitions and open or closed constructed generic types
        //We can create the Type instance using typeof operator:

        Type stringType = typeof(string);
        Type intType = typeof(int);

        //we can get information about methods from a type:
        MethodInfo? methodInfo = stringType.GetMethod("Substring");

        //we can also get type by using "GetType" method
        Type type;
        object[] values = { "word", true, 120, 123.32, 'f' };
        foreach (var item in values)
        {
            type = item.GetType();
        }

        //we use the sizeof operator to get the size of the specified type
        int sizeOfIntegerVaraible = sizeof(int);


        //TypeOf returs a


        //the sizeOf can be use to the memory size of the specified type



        #endregion TypeOf, Type, SizeOf

        #region Object type

        //The Object Type is the ultimate base class for all data types in C#
        //Object is an alias for System.Object class
        //Object types can be assigned values of any other types, value types, reference types, predefined or user-defined types.
        //However, before assigning values, it needs type conversion.

        //When a value type is converted to object type, it is called boxing
        //On the other hand, when an object type is converted to a value type, it is called unboxing.

        object obj;
        int intNumber = 10;
        obj = intNumber; // this is boxing
        int intNumber2 = (int)obj; //this is unboxing

        //object can be null
        object obj2 = null;

        #endregion

        #region Dynamic type

        //Covered in CsharpAdvanced project -> "Keywords" folder -> "Dynamics.cs" file

        #endregion

        #region Pointer type

        //Covered in Introduction folder -> "Pointer.cs" file

        #endregion

        #region Unmanage type

        //A type is an unmanaged type if it's any of the following types:
        //sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double, decimal, or bool
        //Any enum type
        //Any pointer type
        //Any user-defined struct type that contains fields of unmanaged types only and, in C# 7.3 and earlier, is not a constructed type (a type that includes at least one type argument)

        //Beginning with C# 7.3, you can use the unmanaged constraint to specify that a type parameter is a non-pointer, non-nullable unmanaged type.
        //Beginning with C# 8.0, a constructed struct type that contains fields of unmanaged types only is also unmanaged

        #endregion

        #region Reference types

        //reference types are type that are store in the heap
        //use reference type to store much data

        //Reference types in c#:
        /*
        not build-in:
            class
            interface
            delegate
            record
        build-in:
            dynamic
            object
            string
        */

        #endregion
    }
}
