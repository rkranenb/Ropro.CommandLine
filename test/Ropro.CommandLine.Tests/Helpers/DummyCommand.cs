using Ropro.CommandLine;

namespace Ropro.CommandLine.Tests.Helpers;

[Usage("dummy arg1 arg2 -f flag")]
[HelpText("Does nothing.")]
public class DummyCommand : Command {
    public override bool Run(string key, string[] args) => true;
}