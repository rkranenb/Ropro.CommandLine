using Ropro.CommandLine;

public class CliTests
{
    [Fact]
    public void Should_start_repl_when_empty()
    {
        int result = 0;
        var repl = new DummyRepl((a) => result += 1);
        var exec = new DummyExecutor((a) => { result += 2; return true; });
        var sut = new Cli(exec, repl);
        sut.Start(new string[] { });
        Assert.Equal(1, result);
    }

    [Theory]
    [InlineData("-f")]
    [InlineData("-f bar")]
    [InlineData("--foo")]
    public void Should_start_repl_when_flag(params string[] args)
    {
        int result = 0;
        var repl = new DummyRepl((a) => result += 1);
        var exec = new DummyExecutor((a) => { result += 2; return true; });
        var sut = new Cli(exec, repl);
        sut.Start(args);
        Assert.Equal(1, result);
    }

    [Theory]
    [InlineData("foo -f bar")]
    [InlineData("foo --foo")]
    [InlineData("foo bar")]
    public void Should_start_executor_when_not_key(params string[] args)
    {
        int result = 0;
        var repl = new DummyRepl((a) => result += 1);
        var exec = new DummyExecutor((a) => { result += 2; return true; });
        var sut = new Cli(exec, repl);
        sut.Start(args);
        Assert.Equal(2, result);
    }

    class DummyRepl : IRepl
    {
        private readonly Action<string[]> startAction;
        public DummyRepl(Action<string[]> startAction)
        {
            this.startAction = startAction;
        }
        public void Start(string[] args)
        {
            startAction(args);
        }
    }

    class DummyExecutor : IExecutor
    {
        private readonly Func<string[], bool> startAction;
        public DummyExecutor(Func<string[], bool> startAction)
        {
            this.startAction = startAction;
        }
        public bool Start(string[] args)
        {
            return startAction(args);
        }
    }
}