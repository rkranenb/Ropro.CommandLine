namespace Ropro.CommandLine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AliasAttribute : Attribute
{
    public AliasAttribute(string alias)
    {
        Alias = alias;
    }
    public string Alias { get; }
}