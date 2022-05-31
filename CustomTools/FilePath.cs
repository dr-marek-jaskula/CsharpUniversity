using System.Collections;

namespace CustomTools;

//Much better FilePath system with case insensitivity and implicit conversion!
//Write once, use multiple times

public record class FilePath : IEnumerable<char>
{
    public string Value { get; }

    public FilePath(string path)
    {
        Value = string.IsNullOrWhiteSpace(path)
            ? throw new ArgumentException("Path cannot be null or empty")
            : System.IO.Path.GetInvalidPathChars().Intersect(path).Any()
            ? throw new ArgumentException("Path contains illegal characters")
            : System.IO.Path.GetFullPath(path.Trim());
    }

    public override string ToString()
    {
        return Value;
    }

    public virtual bool Equals(FilePath? other)
    {
        return Value.Equals(other?.Value, StringComparison.InvariantCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return Value.ToLowerInvariant().GetHashCode();
    }

    //This implicit conversion allow us to just write: FilePath myFilePath = "test.txt";
    //Also this allow us to put a normal string to a method with parameter of type FilePath
    public static implicit operator FilePath(string name)
    {
        return new FilePath(name);
    }

    public IEnumerator<char> GetEnumerator()
    {
        return Value.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Value.GetEnumerator();
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