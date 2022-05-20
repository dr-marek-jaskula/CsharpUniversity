namespace CsharpAdvanced.Records;

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