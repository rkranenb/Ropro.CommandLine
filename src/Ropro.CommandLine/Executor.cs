using Ropro.CommandLine.Commands;
using Ropro.CommandLine.Console;

namespace Ropro.CommandLine;

public interface IExecutor
{
    bool Start(string[] args);
}

public class Executor : IExecutor
{
    private readonly List<ICommand> commands;
    private readonly IConsole console;

    public Executor(IConsole console, IEnumerable<Command> commands, HelpCommand help)
    {
        this.commands = new List<ICommand>(commands);
        this.commands.Add(help);
        this.console = console;
    }
    public bool Start(string[] args)
    {        
        return args.Length == 0 || commands.OrderBy(c => c.Order)
            .First(c => c.MustRun(args))
            .Run(args[0], args.Skip(1).ToArray());
    }
}
