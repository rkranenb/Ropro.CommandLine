using System.Text;
using Ropro.CommandLine.Console;

namespace Ropro.CommandLine.Tests.Helpers;

public class SpyConsole : IConsole
{
    private readonly StringBuilder output = new StringBuilder();
    private readonly Prompt prompt = new Prompt();
    public Prompt Prompt => prompt;

    public string ReadLine() => string.Empty;

    public void ResetColor()
    {
        output.Append("#endcolor#");
    }

    public void SetColor(ConsoleColor c)
    {
        output.Append($"#color {c}#");
    }

    public void Write(string? s)
    {
        output.Append(s);
    }

    public void WriteLine(string? s = null)
    {
        output.AppendLine(s);
    }

    public override string ToString() => output.ToString();

}