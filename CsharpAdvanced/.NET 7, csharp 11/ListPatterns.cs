namespace CsharpAdvanced.NET_7__csharp_11;

public sealed class ListPatterns
{
    public static void InvokeListPatternsExample()
    {
        //It applies to any countable collection (has count property)
        int[] numbers = { 1, 2, 3, 4 };

        bool thisIsTrue = numbers is [ 1, 2, 3, 4 ];
        bool thisIsFalse = numbers is [ 1, 2, 3, 5 ];
        bool thisIsFalse2 = numbers is [ 1, 2, 3, 4, 6 ];


        bool thisIsTrueAndInteresting = numbers is [ 0 or 1, <= 2, >=3 ];

        //The great use is to:
        if (numbers is [var first, _, _])
        {
            Console.WriteLine(first);
        }

        //Other great use is to:
        if (numbers is [var first2, .. var rest])
        {
            Console.WriteLine(first2);
            Console.WriteLine(rest); //this is a slice of an array
        }

        var emptyName = Array.Empty<string>();
        var myName = new[] { "Marek Jaskula" };
        var myNameBrokenDown = new[] { "Marek", "Jaskula" };
        var myNameBrokenDown2 = new[] { "Marek", "Jaskula", "Example" };

        var text = emptyName switch
        {
            [] => "Name was empty",
            [var fullName] => $"My full name is: {fullName}",
            [var firstName, var lastName] => $"My full name is: {firstName} {lastName}",
            _ => "null"
        };
    }
}