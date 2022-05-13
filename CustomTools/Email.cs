using System.Text.RegularExpressions;

namespace CustomTools;

//Much better Email system with case sensitivity and implicit conversion!
//Write once, use multiple times

public record class Email
{
    public string Value { get; }

    public Email(string email)
    {
        Regex regex = new(@"^([a-zA-Z])([a-zA-Z0-9]+)\.?([a-zA-Z0-9]+)@([a-z]+)\.[a-z]{2,3}$");

        Value = regex.IsMatch(email) ?
            throw new ArgumentException("Email needs to start from a letter, contain '@' and after that '.'") :
            email;
    }

    public override string ToString()
    {
        return Value;
    }

    public virtual bool Equals(Email? other)
    {
        return Value.Equals(other?.Value, StringComparison.InvariantCulture);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    //This implicit conversion allow us to just write: Email myEmail = "marek.kowalski@gmail.com";
    //Also this allow us to put a normal string to a method with parameter of type Email
    public static implicit operator Email(string name)
    {
        return new Email(name);
    }
}