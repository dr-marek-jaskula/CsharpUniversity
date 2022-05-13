namespace CustomTools;

//Much better FilePath system with case insensitivity and implicit conversion!
//Write once, use multiple times

public record class FilePath
{
    public string Path { get; }

    public FilePath(string path)
    {
        Path = string.IsNullOrWhiteSpace(path)
            ? throw new ArgumentException("Path cannot be null or empty")
            : System.IO.Path.GetInvalidPathChars().Intersect(path).Any()
            ? throw new ArgumentException("Path contains illegal characters")
            : System.IO.Path.GetFullPath(path.Trim());
    }

    public override string ToString()
    {
        return Path;
    }

    public virtual bool Equals(FilePath? other)
    {
        return Path.Equals(other?.Path, StringComparison.InvariantCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return Path.ToLowerInvariant().GetHashCode();
    }

    //This implicit conversion allow us to just write: FilePath myFilePath = "test.txt";
    //Also this allow us to put a normal string to a method with parameter of type FilePath
    public static implicit operator FilePath(string name)
    {
        return new FilePath(name);
    }

    public FileInfo GetInfo()
    {
        return new FileInfo(Path);
    }

    public FilePath Combine(params string[] paths)
    {
        return System.IO.Path.Combine(paths.Prepend(Path).ToArray());
    }
}