using Ropro.CommandLine;

namespace Ropro.CommandLine.Tests.Helpers;

public class MockCommand : Command
{
    private readonly bool mustRun;
    private readonly bool result;
    public MockCommand(bool mustRun = false, bool result = true, int order = 0)
    {
        this.mustRun = mustRun;
        this.result = result;
        Order = order;
    }
    public string[] Args { get; private set; } = new string[] { };
    public string Key { get; private set; } = string.Empty;
    public int RunCount { get; private set; } = 0;
    public override int Order { get; }
    public override bool Run(string key, string[] args)
    {
        Args = args;
        Key = key;
        RunCount++;
        return result;
    }
    public override bool MustRun(string[] args) => mustRun;
}