namespace Ropro.CommandLine;

public class CommandException : Exception
{
    public CommandException(Command command, string message)
        : base(message)
    {
        Command = command ?? throw new ArgumentNullException(nameof(command));
    }

    public Command Command { get; }
}