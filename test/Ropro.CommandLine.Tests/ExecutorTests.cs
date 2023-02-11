using Ropro.CommandLine;
using Ropro.CommandLine.Commands;
using Helpers;

public class ExecutorTests
{

    [Fact]
    public void Name_and_arguments_are_passed()
    {
        // arrange        
        var command = new MockCommand(mustRun: true);
        var sut = CreateExecutor(command);
        var args = new[] { "foo", "bar", "baz" };
        // act
        sut.Start(args);
        // assert
        Assert.Equal("foo", command.Key);
        Assert.Equal(2, command.Args.Length);
        Assert.Equal("bar", command.Args[0]);
        Assert.Equal("baz", command.Args[1]);
    }

    [Fact]
    public void No_args_provided()
    {
        var command = new MockCommand(mustRun: true);
        var sut = CreateExecutor(command);
        var actual = sut.Start(new string[] { });
        Assert.True(actual);
        Assert.Equal(0, command.RunCount);
    }

    [Fact]
    public void Commands_are_processed_in_order()
    {
        var command1 = new MockCommand(mustRun: true, order: 2);
        var command2 = new MockCommand(mustRun: true, order: 3);
        var command3 = new MockCommand(mustRun: true, order: 1);
        var sut = CreateExecutor(command1, command2, command3);
        var actual = sut.Start(new[] { "not-relevant" });
        Assert.True(actual);
        Assert.Equal(0, command1.RunCount);
        Assert.Equal(0, command2.RunCount);
        Assert.Equal(1, command3.RunCount);
    }

    private IExecutor CreateExecutor(params Command[] c)
    {
        var commands = c ?? new[] { new MockCommand() };
        var help = new HelpCommand(commands, new DummyConsole());
        return new Executor(new DummyConsole(), commands, help);
    }

}