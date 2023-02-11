namespace Ropro.CommandLine;

public class Cli
{
    private readonly IExecutor executor;
    private readonly IRepl repl;

    public Cli(IExecutor executor, IRepl repl)
    {
        this.executor = executor;
        this.repl = repl;
    }

    public void Start(string[] args)
    {
        if (args.Length == 0 || args[0].StartsWith("-"))
        {
            repl.Start(args);
        }
        else
        {
            executor.Start(args);
        }
    }
}
