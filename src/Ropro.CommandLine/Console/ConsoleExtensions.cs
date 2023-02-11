namespace Ropro.CommandLine.Console;

public static class ConsoleExtensions
{
    public static void Warn(this IConsole c, string s)
    {
        c.SetColor(ConsoleColor.Yellow);
        c.WriteLine(s);
        c.ResetColor();
    }
    public static void Info(this IConsole c, string s)
    {
        c.SetColor(ConsoleColor.White);
        c.WriteLine(s);
        c.ResetColor();
    }
    public static void Debug(this IConsole c, string s)
    {
        c.SetColor(ConsoleColor.DarkGray);
        c.WriteLine(s);
        c.ResetColor();
    }
    public static void Alert(this IConsole c, string s)
    {
        c.SetColor(ConsoleColor.Red);
        c.WriteLine(s);
        c.ResetColor();
    }
    public static void Prompt(this IConsole c, string? s)
    {
        c.SetColor(c.Prompt.Color);
        c.Write(s);
        c.ResetColor();
        c.Write(" > ");
    }    
}