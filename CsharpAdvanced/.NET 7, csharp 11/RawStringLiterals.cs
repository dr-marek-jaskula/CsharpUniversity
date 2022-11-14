namespace CsharpAdvanced.NET_7__csharp_11;

public sealed class RawStringLiterals
{
    public static void InvokeRawStringLiteralsExample()
    {
        //The RawStringLiteral starts with """ and also ends with the same.
        //If we have in the string more doubleuates, then we can make it 4 times
        var xmlPrologue = """<?xml version="1.0" encoding="UTF-8"?>""";
        var xmlPrologue2 = """"<?xml version="1.0" encoding="UTF-8"?>"""";
        //or 5
        var xmlPrologue3 = """""<?xml version="1.0" encoding="UTF-8"?>""""";

        //for json is better
        var json1 = """
        {
            "name" : "Marek"
        }            
        """;

        string marek = "Marek";
        //for string interpolation we need to use that many $$$$$$$ that we want to use for string interpolation:
        var json2 = $$"""
        {
            "name" : {{marek}}
        }            
        """;
    }
}