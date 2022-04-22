namespace ASP.NETCoreWebAPI.Exceptions;

public class UnavailableException : Exception
{
    public UnavailableException(string message) : base(message)
    {
    }
}