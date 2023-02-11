using Ropro.CommandLine;

namespace Helpers;

public class DummyCommand : Command {
    public override bool Run(string key, string[] args) => true;
}