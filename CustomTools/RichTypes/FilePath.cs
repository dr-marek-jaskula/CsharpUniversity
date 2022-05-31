using System.Text.RegularExpressions;

namespace CustomTools;

//Much better FilePath system with case insensitivity and implicit conversion!
//Write once, use multiple times

//FilePath is for Windows paths
public record class FilePath : AsString
{
    private static readonly char[] _illegalCharacter = new[] { '<', '>', '|', '/', '?' };

    public FilePath(string path)
    {
        Value = string.IsNullOrWhiteSpace(path)
            ? throw new ArgumentException("Path cannot be null or empty")
            : System.IO.Path.GetInvalidPathChars().Union(_illegalCharacter).Intersect(path).Any() || path[2..].Contains(':')
            ? throw new ArgumentException("Path contains illegal characters")
            : System.IO.Path.GetFullPath(path.Trim());
    }

    //This implicit conversion allow us to just write: FilePath myFilePath = "test.txt";
    //Also this allow us to put a normal string to a method with parameter of type FilePath
    public static implicit operator FilePath(string name)
    {
        return new FilePath(name);
    }

    public FileInfo GetInfo()
    {
        return new FileInfo(Value);
    }

    public FilePath Combine(params string[] paths)
    {
        return System.IO.Path.Combine(paths.Prepend(Value).ToArray());
    }
}