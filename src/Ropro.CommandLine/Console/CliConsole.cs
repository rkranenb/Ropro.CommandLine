namespace Ropro.CommandLine.Console;

public class CliConsole : IConsole
{
    public Prompt Prompt => new Prompt(color: ConsoleColor.Green);
    public string ReadLine()
    {
        return System.Console.ReadLine() ?? string.Empty;
    }
    public void ResetColor()
    {
        System.Console.ResetColor();
    }
    public void SetColor(ConsoleColor c)
    {
        System.Console.ForegroundColor = c;
    }
    public void Write(string? s = null)
    {
        System.Console.Write(s);
    }
    public void WriteLine(string? s = null)
    {
        System.Console.WriteLine(s);
    }
}
