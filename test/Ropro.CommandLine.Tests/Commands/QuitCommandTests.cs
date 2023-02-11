using Ropro.CommandLine.Commands;

namespace Ropro.CommandLine.Tests.Commands;

public class QuitCommandTests
{

    [Theory]
    [InlineData("quit")]
    [InlineData("q")]
    [InlineData("exit")]
    public void Must_run_on_name_or_alias(string arg)
    {
        var sut = new QuitCommand();
        var actual = sut.MustRun(new[] { arg });
        Assert.True(actual);
    }

    [Fact]
    public void Run_returns_false()
    {
        var sut = new QuitCommand();
        var actual = sut.Run("quit", new string[] { });
        Assert.False(actual);
    }

}
