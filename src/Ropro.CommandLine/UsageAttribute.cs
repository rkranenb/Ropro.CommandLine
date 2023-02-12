namespace Ropro.CommandLine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class UsageAttribute : Attribute
{
    public UsageAttribute(string usage)
    {
        Usage = usage;
    }
    public string Usage { get; }
}