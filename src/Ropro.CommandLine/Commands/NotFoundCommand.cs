using Ropro.CommandLine.Console;

namespace Ropro.CommandLine;

[HideFromHelp]
public class NotFoundCommand : Command
{
    private readonly IConsole console;

    public NotFoundCommand(IConsole console)
    {
        this.console = console;
    }

    public override int Order => int.MaxValue;

    public override bool Run(string key,string[] args) {
        console.WriteLine($"Unknown command '{key}'. See '--help'.");
        return true;
    }

    public override bool MustRun(string[] args) => true;
}
