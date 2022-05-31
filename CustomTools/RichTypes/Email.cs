using System.Text.RegularExpressions;

namespace CustomTools;

//Much better Email system with case sensitivity and implicit conversion!
//Write once, use multiple times

public record class Email : AsString
{
    private static readonly Regex _regex = new(@"^([a-zA-Z])([a-zA-Z0-9]+)\.?([a-zA-Z0-9]+)@([a-z]+)\.[a-z]{2,3}$");

    public Email(string email)
    {
        Value = _regex.IsMatch(email) ?
            email :
            throw new ArgumentException("Email needs to start from a letter, contain '@' and after that '.'");
    }

    public static implicit operator Email(string name)
    {
        return new Email(name);
    }
}