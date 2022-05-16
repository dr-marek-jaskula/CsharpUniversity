namespace CsharpAdvanced.Introduction;

public class AnonymousObjects
{
    public static void AnonymousObjectsExample()
    {
        var tupleOne = (amount: 13, wight: 55);
        var tupleTwo = (greeting: "Hello", secret: "MySecret");
        var anonymousObject = new { myAmount = tupleOne.amount, superSecret = tupleTwo.secret };
        Console.WriteLine(anonymousObject);
    }
}