namespace CsharpBasics.Introduction;

//In this file we cover the topic of basic types and converting: implicit, explicit and at run time

public class Casting
{
    public static void InvokeCastingExamples()
    {
        #region Implicit conversion

        //Implicit conversion is the conversion that is made by the compiler if it is necessary and possible

        //Because the long has a wider range (use more memory) we can cast int to long
        int integerNumber = 2331112;
        long longNumber = integerNumber;

        //For the reference type, the implicit conversion exists: from a class (child) to any one of its direct or indirect base classes (parents) or interfaces
        Son child = new() { Age = 13, Name = "Tom" };
        Parent parent = child;
        //Now parent variable is a reference to the child, but with access only to the members that are common for both classes (so just for ParentClass)
        parent.Name = "John";
        //now the child object name has changed

        //We cam also use the "as" keyword but it is redundant
        Parent parent2 = child as Parent;

        #endregion Implicit conversions

        #region Explicit conversions

        //explicit conversion is called also "casting". 
        //in this approach we explicitly inform the compiler that we intend to make the conversion and we are aware of data loss that may occur or the cast may fail at run time

        //Value types
        //we cast double to int, knowing that we will loose some data
        double doubleNumber = 123.512;
        //to cast we use the following syntax
        int intNumber = (int)doubleNumber;

        //Reference types
        //Casting the parent class to the child class will result in exception.


        //se of ToString();

        #endregion Explicit conversions

        #region Convert class

        //We can use the Convert static class to execute conversion 
        try
        {
            string stringNumber = "124";
            byte byteConvertionResult = Convert.ToByte(stringNumber);

            string stringBoolean = "true";
            bool boolConvertionResult = Convert.ToBoolean(stringBoolean);

            string stringNumber2 = "1";
            int intConvertionResult = Convert.ToInt32(stringNumber2);
        }
        catch (OverflowException exception)
        {
            Console.WriteLine($"{exception.Message}");
        }

        #endregion
    }
}

#region Helper classes

public class Parent
{
    public string Name { get; set; } = string.Empty;
}

public class Son : Parent
{
    public int Age { get; set; } = 0;
}

public class Daughter : Parent  
{
    public int LuckyNumber { get; set; } = 0;
}

#endregion