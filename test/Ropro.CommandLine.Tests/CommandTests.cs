using Ropro.CommandLine;
using Helpers;

public class CommandTests
{

    [Theory]
    [InlineData("dummy")]
    [InlineData("Dummy")]
    public void Must_execute_with_correct_name(params string[] args)
    {
        var actual = new DummyCommand().MustRun(args);
        Assert.True(actual);
    }

    [Fact]
    public void Must_not_execute_with_incorrect_name()
    {
        var actual = new DummyCommand().MustRun(new[] { "foo" });
        Assert.False(actual);
    }

    [Fact]
    public void Must_not_execute_with_no_name()
    {
        var actual = new DummyCommand().MustRun(new string[] { });
        Assert.False(actual);
    }

    [Theory]
    [InlineData("alias")]
    [InlineData("a")]
    [InlineData("-a")]
    [InlineData("--alias")]
    public void Must_run_on_name_and_alias(string alias)
    {
        var sut = new AliasCommand();
        var actual = sut.MustRun(new[] { alias });
        Assert.True(actual);
    }

    [Alias("a"), Alias("-a"), Alias("--alias")]    
    private class AliasCommand : Command
    {
        public override bool Run(string key, string[] args) => true;
    }

}