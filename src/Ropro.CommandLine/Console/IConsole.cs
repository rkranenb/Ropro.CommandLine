namespace Ropro.CommandLine.Console;

public interface IConsole
{
    string ReadLine();
    void Write(string? s);
    void WriteLine(string? s = null);
    void SetColor(ConsoleColor c);
    void ResetColor();
    Prompt Prompt { get; }
}
