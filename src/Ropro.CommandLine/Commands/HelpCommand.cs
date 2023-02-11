using Ropro.CommandLine.Console;

namespace Ropro.CommandLine.Commands;

public class HelpCommand : ICommand
{
    static string[] keywords = new[] { "-h", "--help", "help", "-?", "?" };
    private readonly IEnumerable<Command> commands;
    private readonly IConsole console;

    public HelpCommand(IEnumerable<Command> commands, IConsole console)
    {
        this.commands = commands;
        this.console = console;
    }

    public int Order => 0;

    public bool MustRun(string[] args)
    {
        return args.Length > 0 && keywords.Contains(args[0].ToLower());
    }

    public bool Run(string key, string[] args)
    {
        foreach (var c in commands)
        {
            if (!c.GetType().GetCustomAttributes(inherit: true)
                .Any(a => a.GetType().Equals(typeof(HideFromHelpAttribute))))
                console.WriteLine(c.GetType().Name);
        }
        return true;
    }
}
