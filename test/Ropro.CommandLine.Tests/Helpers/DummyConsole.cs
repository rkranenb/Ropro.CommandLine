using Ropro.CommandLine.Console;

namespace Helpers;

public class DummyConsole : IConsole
{
    public Prompt Prompt => new Prompt();

    public string ReadLine() => string.Empty;

    public void ResetColor() { }

    public void SetColor(ConsoleColor c) { }

    public void Write(string? s) { }

    public void WriteLine(string? s = null) { }
}