using LibGit2Sharp;

namespace GitLogParser;

public class Author
{
    public Author(string name, string[] paths)
    {
        if (paths == null) throw new ArgumentNullException(nameof(paths));
        if (paths.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(paths));

        Name = name ?? throw new ArgumentNullException(nameof(name));
        Paths = paths;

        ValidatePath();
    }

    public Author(string name, List<string> paths)
    {
        if (paths == null) throw new ArgumentNullException(nameof(paths));
        if (paths.Count == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(paths));

        Name = name;
        Paths = paths.ToArray();

        ValidatePath();
    }

    public string Name { get; }
    public string[] Paths { get; private set; }

    private void ValidatePath()
    {
        foreach (var path in Paths)
        {
            if (!Repository.IsValid(path))
                throw new InvalidOperationException($"Path : {path} is not a valid git repository");
        }
    }

    public void AddPath(string path)
    {
        if (!Repository.IsValid(path))
            throw new InvalidOperationException($"Path : {path} is not a valid git repository");

        if (Paths.Any(e => e == path))
            throw new InvalidOperationException($"Path : {path} already added in author {Name}");

        var currentList = Paths.ToList();
        currentList.Add(path);
        Paths = currentList.ToArray();
    }
}