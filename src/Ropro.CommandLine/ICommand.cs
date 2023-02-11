namespace Ropro.CommandLine;

public interface ICommand
{
    int Order { get; }
    bool MustRun(string[] args);
    bool Run(string key,string[] args);
}
