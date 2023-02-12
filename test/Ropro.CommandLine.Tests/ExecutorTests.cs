using Ropro.CommandLine;
using Ropro.CommandLine.Commands;
using Helpers;
using Ropro.CommandLine.Console;
using Ropro.CommandLine.Tests.Helpers;

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

    private class NonBreakingExceptionCommand : Command
    {
        public override bool Run(string key, string[] args) =>
            throw new CommandException(this, "Message goes here.");
    }

    [Fact]
    public void CommandException_is_written_to_console()
    {
        var console = new SpyConsole();
        var sut = CreateExecutor(console, new NonBreakingExceptionCommand());
        sut.Start(new[] { "NonBreakingException" });
        var actual = console.ToString();
        Assert.Equal($"#color Yellow#Message goes here.{Environment.NewLine}#endcolor#No usage information is provided.{Environment.NewLine}", actual);
    }

    [Fact]
    public void CommandException_does_not_break_repl()
    {
        var console = new SpyConsole();
        var sut = CreateExecutor(console, new NonBreakingExceptionCommand());
        var actual = sut.Start(new[] { "NonBreakingException" });
        Assert.True(actual);
    }

    private class BreakingExceptionCommand : Command
    {
        public override bool Run(string key, string[] args) =>
            throw new Exception("Message goes here.");
    }

    [Fact]
    public void Unhandled_exception_is_written_to_console()
    {
        var console = new SpyConsole();
        var sut = CreateExecutor(console, new BreakingExceptionCommand());
        sut.Start(new[] { "BreakingException" });
        var actual = console.ToString();
        Assert.Equal($"#color Red#Exiting due to an unexpected error: Message goes here.{Environment.NewLine}#endcolor#", actual);
    }

    [Fact]
    public void Unhandled_exception_does_break_repl()
    {
        var console = new SpyConsole();
        var sut = CreateExecutor(console, new BreakingExceptionCommand());
        var actual = sut.Start(new[] { "BreakingException" });
        Assert.False(actual);
    }

    private IExecutor CreateExecutor(params Command[] c)
    {
        return CreateExecutor(new DummyConsole(), c);
    }

    private IExecutor CreateExecutor(IConsole console, params Command[] c)
    {
        var commands = c ?? new[] { new MockCommand() };
        var help = new HelpCommand(commands, console);
        return new Executor(console, commands, help);
    }

}