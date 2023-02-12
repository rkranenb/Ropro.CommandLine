namespace Ropro.CommandLine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class HelpTextAttribute : Attribute
{
    public HelpTextAttribute(string text)
    {
        Text = text;
    }
    public string Text { get; }
}