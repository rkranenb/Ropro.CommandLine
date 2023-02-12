using Helpers;

namespace Ropro.CommandLine.Tests;

public class ExtensionsTests
{
    [Theory]
    [InlineData("dummy", new[] { "dummy" })]
    [InlineData("dummy data", new[] { "dummy", "data" })]
    [InlineData("dummy \"super duper\" data", new[] { "dummy", "super duper", "data" })]
    [InlineData("dummy -d data", new[] { "dummy", "-d", "data" })]
    [InlineData("dummy -d \"my data\"", new[] { "dummy", "-d", "my data" })]
    [InlineData("dummy -d \"my data", new[] { "dummy", "-d", "my", "data" })]
    public void SplitSmart_splits_corectly(string s, string[] expected)
    {
        var actual = s.SplitSmart();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetUsage_returns_default_usage_if_not_provided()
    {
        var sut = new MockCommand();
        var actual = sut.GetUsage();
        Assert.Equal("No usage information is provided.", actual);
    }

    [Fact]
    public void GetUsage_returns_usage_if_provided()
    {
        var sut = new DummyCommand();
        var actual = sut.GetUsage();
        Assert.Equal("usage: [dotnet run] dummy arg1 arg2 -f flag", actual);
    }

    [Usage("dummy arg1 arg2 -f flag")]
    private class DummyCommand : Command
    {
        public override bool Run(string key, string[] args) => true;
    }
}