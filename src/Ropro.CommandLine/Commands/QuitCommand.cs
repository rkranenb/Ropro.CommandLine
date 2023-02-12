namespace Ropro.CommandLine;

[Alias("exit"), Alias("q")]
[HideFromHelp]
public class QuitCommand : Command
{
    public override bool Run(string key, string[] args) => false;
}