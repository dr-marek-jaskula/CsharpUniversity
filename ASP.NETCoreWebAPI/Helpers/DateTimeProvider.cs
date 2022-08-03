namespace ASP.NETCoreWebAPI.Helpers;

//This is a very important interface to make classes testable

public interface IDateTimeProvider
{
    public DateTime Now { get; }
    public DateTime Midnight { get; }
}

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;

    public DateTime Midnight => DateTime.MinValue;
}