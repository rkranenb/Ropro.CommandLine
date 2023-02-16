namespace Ropro.CommandLine;

public class CommandArgumentException : Exception
{
    public CommandArgumentException(string message) : base(message) { }
}