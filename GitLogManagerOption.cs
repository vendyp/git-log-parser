namespace GitLogParser;

public class GitLogManagerOption
{
    public GitLogManagerOption()
    {
        Authors = new List<Author>();
    }

    public readonly List<Author> Authors;

    public void Add(Author author)
    {
        if (Authors.Any(e => e.Name == author.Name))
        {
            var currentAuthor = Authors.First(e => e.Name == author.Name);
            foreach (var item in author.Paths)
                currentAuthor.AddPath(item);
        }
        else
        {
            Authors.Add(author);
        }
    }
}