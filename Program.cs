using GitLogParser;

var optionBuilder = new GitLogManagerOptionBuilder();

//add each user email, and path of git
optionBuilder.Add("dwivendypratamadev@gmail.com",
    new[] {"D:\\Projects\\console-app\\src\\GitLogParser"});

var gitLogManager = new GitLogManager(optionBuilder.Build());

gitLogManager.Run();

Console.WriteLine("Run successfully");