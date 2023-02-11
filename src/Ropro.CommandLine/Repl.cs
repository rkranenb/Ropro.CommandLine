using Ropro.CommandLine.Console;

namespace Ropro.CommandLine;

public interface IRepl
{
    void Start(string[] args);
}

public class Repl : IRepl
{
    private readonly IConsole console;
    private readonly IExecutor executor;
    public Repl(IConsole console, IExecutor executor)
    {
        this.console = console;
        this.executor = executor;
    }
    public void Start(string[] args)
    {
        bool loop = true;
        while (loop)
        {
            console.Prompt(console.Prompt.Text);
            var s = console.ReadLine();
            loop = executor.Start(s.SplitSmart());
            console.WriteLine();
        }
    }
}
