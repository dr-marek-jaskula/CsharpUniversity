using System.Text.RegularExpressions;

namespace CustomTools;

//Much better Password system with case sensitivity and implicit conversion!
//Write once, use multiple times

public record class Password : AsString
{
    private static readonly Regex _regex = new(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{0,}$",
    //This option boots the performance of the regex
    RegexOptions.Compiled);

    public Password(string password)
    {
        Value = _regex.IsMatch(password) ?
            password :
            throw new ArgumentException("Password needs to contain at least one digit, one small letter and one capital letter.");
    }

    //This implicit conversion allow us to just write: Password myPassword = "mySuper123Password";
    //Also this allow us to put a normal string to a method with parameter of type Password
    public static implicit operator Password(string name)
    {
        return new Password(name);
    }
}