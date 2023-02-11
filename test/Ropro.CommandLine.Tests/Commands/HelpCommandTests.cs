using Helpers;
using Ropro.CommandLine.Commands;

namespace Ropro.CommandLine.Tests.Commands;

public class HelpCommandTests
{

    [Theory]
    [InlineData("help")]
    [InlineData("-h")]
    [InlineData("--help")]
    [InlineData("?")]
    [InlineData("-?")]
    public void Must_run_on_name_or_alias(string arg)
    {
        var sut = new HelpCommand(new Command[] { }, new DummyConsole());
        var actual = sut.MustRun(new[] { arg });
        Assert.True(actual);
    }

}
