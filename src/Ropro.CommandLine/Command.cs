namespace Ropro.CommandLine;

public abstract class Command : ICommand
{
    public virtual int Order => 0;
    public virtual bool MustRun(string[] args)
    {
        return args.Any() && MatchesNameOrAlias(args[0]);
    }
    public abstract bool Run(string key, string[] args);
    private bool MatchesNameOrAlias(string arg)
    {
        var aliases = this.GetType().GetCustomAttributes(inherit: false)
            .Where(x => x.GetType() == typeof(AliasAttribute))
            #pragma warning disable CS8602
            .Select(x => (x as AliasAttribute).Alias);
        return $"{arg}Command".Equals(this.GetType().Name, StringComparison.OrdinalIgnoreCase)
            || aliases.Any(x => x.Equals(arg, StringComparison.OrdinalIgnoreCase));

    }
}


