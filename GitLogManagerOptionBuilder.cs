namespace GitLogParser;

public class GitLogManagerOptionBuilder
{
    public GitLogManagerOptionBuilder()
    {
        _option = new GitLogManagerOption();
    }

    private readonly GitLogManagerOption _option;

    public GitLogManagerOptionBuilder Add(string authorName, string[] paths)
    {
        _option.Add(new Author(authorName, paths));
        return this;
    }

    public GitLogManagerOptionBuilder Add(string authorName, List<string> paths)
    {
        _option.Add(new Author(authorName, paths));
        return this;
    }

    public GitLogManagerOption Build()
    {
        return _option;
    }
}