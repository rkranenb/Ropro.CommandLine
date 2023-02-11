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
}