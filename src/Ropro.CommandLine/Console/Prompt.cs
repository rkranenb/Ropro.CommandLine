namespace Ropro.CommandLine.Console;

public class Prompt
{
    public Prompt(string text = "My repl", ConsoleColor color = ConsoleColor.Gray)
    {
        Text = text;
        Color = color;
    }
    public string Text { get; set; }
    public ConsoleColor Color { get; set; }
}
