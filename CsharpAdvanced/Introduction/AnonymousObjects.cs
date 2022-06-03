namespace CsharpAdvanced.Introduction;

public class AnonymousObjects
{
    public static void AnonymousObjectsExample()
    {
        var tupleOne = (amount: 13, wight: 55);
        var tupleTwo = (greeting: "Hello", secret: "MySecret");
        var anonymousObject = new { myAmount = tupleOne.amount, superSecret = tupleTwo.secret };
        Console.WriteLine(anonymousObject);

        //Since C# 10, we can use cloning using "with" keyword, same as for structs:
        var someValue = new { Value = 10, Name = "test" };
        var someValue2 = someValue with { Value = 20 };
    }
}