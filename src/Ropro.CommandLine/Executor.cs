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
        try
        {
            if (args.Length == 0) return true;
            var command = commands.OrderBy(c => c.Order)
                .FirstOrDefault(c => c.MustRun(args));
            if (command == null)
            {
                console.Warn($"No such command: '{args[0]}'.");
                return true;
            }
            return command.Run(args[0], args.Skip(1).ToArray());
        }
        catch (CommandArgumentException e)
        {
            console.Warn(e.Message);
            return true;
        }
        catch (CommandException e)
        {
            console.Warn(e.Message);
            console.WriteLine(e.Command.GetUsage());
            return true;
        }
        catch (Exception e)
        {
            console.Alert($"Exiting due to an unexpected error: {e.Message}");
            return false;
        }
    }

}
