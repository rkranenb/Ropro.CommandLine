using Ropro.CommandLine.Commands;
using Ropro.CommandLine.Tests.Helpers;

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

    [Fact]
    public void Help_without_arguments_shows_all_commands()
    {
        var mock = new MockCommand();
        var dummy = new DummyCommand();
        var console = new SpyConsole();
        var sut = new HelpCommand(new Command[] { mock, dummy }, console);
        sut.Run("help", new string[] { });
        var expected = ""
            + $"  dummy             Does nothing.{Environment.NewLine}"
            + $"  mock{Environment.NewLine}";
        Assert.Equal(expected, console.ToString());
    }

}
