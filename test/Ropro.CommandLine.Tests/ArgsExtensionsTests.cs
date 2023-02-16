namespace Ropro.CommandLine.Tests;

public class ArgsExtensionsTests
{

    [Theory]
    [InlineData("-f", "foo")]
    [InlineData("-f", "foo", "baz")]
    [InlineData("bar", "-f", "foo")]
    [InlineData("bar", "-f", "foo", "baz")]
    public void GetValue_returns_flag_value_if_provided(params string[] args)
    {
        var actual = args.GetValue("-f");
        Assert.Equal("foo", actual);
    }

    [Fact]
    public void GetValue_returns_null_if_not_found()
    {
        var args = new[] { "-f", "foo" };
        var actual = args.GetValue("--not-found");
        Assert.Null(actual);
    }

    [Theory]
    [InlineData("-d", "2023-02-14")]
    [InlineData("-d", "14-02-2023")]
    [InlineData("-d", "14/02/2023")]
    [InlineData("-d", "14.02.2023")]
    public void GetDate_returns_flag_value_as_datetime_if_provided(params string[] args)
    {
        var actual = args.GetDate("-d");
        Assert.Equal(new DateTime(2023, 2, 14), actual);
    }

    [Theory]
    [InlineData("-d")]
    [InlineData("-d", "not a date")]
    [InlineData()]
    [InlineData("--date", "2023-02-14")]
    public void GetDate_throws_exception_if_no_datetime_is_provided(params string[] args)
    {
        var actual = Assert.Throws<CommandArgumentException>(() => args.GetDate("-d"));
        Assert.Equal("Unexpected value provided for '-d'. Expected a date.", actual.Message);
    }

    [Fact]
    public void GetInt_returns_flag_value_as_datetime_if_provided()
    {
        var args = new[] { "-i", "123" };
        var actual = args.GetInt("-i");
        Assert.Equal(123, actual);
    }

    [Theory]
    [InlineData("-i")]
    [InlineData("-i", "not an int")]
    [InlineData()]
    [InlineData("--int", "123")]
    [InlineData("-i", "123.56")]
    [InlineData("-i", "123,56")]
    public void GetInt_throws_exception_if_no_int_is_provided(params string[] args)
    {
        var actual = Assert.Throws<CommandArgumentException>(() => args.GetInt("-d"));
        Assert.Equal("Unexpected value provided for '-d'. Expected an integer number.", actual.Message);
    }


}