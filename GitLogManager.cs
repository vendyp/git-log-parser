using System.Diagnostics;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;

namespace GitLogParser;

public class GitLogManager
{
    private readonly GitLogManagerOption _option;
    private readonly DateTime _now;

    public GitLogManager(GitLogManagerOption option)
    {
        _option = option;
        _now = DateTime.Now.Date;
        _gitProcess = new Process();
    }

    //DEC 1 2014 (MMMM d yyyy)
    private string command = "git log --since \"DEC 1 2014\" --until \"DEC 5 2014\" --pretty=format:\"%h %an %ad\"";
    private readonly Process _gitProcess;

    public void Run(string fileName = "result.txt")
    {
        StreamWriter sw = new StreamWriter(fileName);

        sw.WriteLine($"DATE : {_now:dd-MM-yyyy}");
        const string tableName = "Name";
        const string tab = "    ";
        const string tableQty = "Qty";
        const string tableProjects = "Projects";
        const string tableRow = $"{tableName}{tab}{tab}{tab}{tab}{tableQty}{tab}{tab}{tab}{tab}{tableProjects}";

        sw.WriteLine(tableRow);
        var delimiter = string.Empty;
        for (var i = 0; i < tableRow.Length; i++)
        {
            delimiter += "-";
        }

        sw.WriteLine(delimiter);

        foreach (var item in _option.Authors)
        {
            Dictionary<string, int> dics = new Dictionary<string, int>();

            foreach (var path in item.Paths)
            {
                var repo = new Repository(path);

                dics.Add(
                    repo.Info.WorkingDirectory,
                    repo.Commits
                        .Count(e => e.Committer.Email == item.Name && e.Committer.When.Date >= _now.Date));
            }

            var tt = string.Join(",", dics.Select(e => e.Key).ToList());
            sw.WriteLine(
                $"{item.Name}{tab}{dics.Select(e => e.Value).Sum()}{tab}{tt}");
            dics.Clear();
        }

        var x = new LibGit2Sharp.PullOptions();

        x.FetchOptions = new FetchOptions();
        x.FetchOptions.CredentialsProvider = new CredentialsHandler(
            (url, usernameFromUrl, types) =>
                new SshAgentCredentials()
                {
                    Username = USERNAME,
                    Password = PASSWORD
                });
        sw.Close();
    }

    
    
    private string RunCommand(string args)
    {
        _gitProcess.StartInfo.Arguments = args;
        _gitProcess.Start();
        string output = _gitProcess.StandardOutput.ReadToEnd().Trim();
        _gitProcess.WaitForExit();
        return output;
    }
}