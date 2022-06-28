namespace CsharpAdvanced.Records;

//Record are fast ways to get a class close to readonly and with value comparison

public class RecordsBasic
{
    //
}

//valid record
public record class ShortRecord();

public record class StandardRecord(string FullName, DateOnly DateOfBirth);

public record class StandardRecord2
{
    public string FullName { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; } = default!;
}

#region overriding "ToString" in base class with "sealed" keyword

public record class BaseRecord(string BaseValue)
{
    //"sealed" keyword will result in such behavior that no derived class can override it
    public sealed override string ToString()
    {
        return "Base implementation";
    }
}

public record class DerivedRecord(string Value, string BaseValue) : BaseRecord(BaseValue)
{
    //can not override the "ToString" method
}

#endregion overriding "ToString" in base class with "sealed" keyword

#region record struct since C# 10

public record struct StructRecord(string StringValue, int IntValue);

#endregion record struct since C# 10

