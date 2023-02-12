using System.Reflection;
using System.Text.RegularExpressions;

using Microsoft.Extensions.DependencyInjection;
using Ropro.CommandLine.Commands;
using Ropro.CommandLine.Console;

namespace Ropro.CommandLine;

public static class Extensions
{

    public static IServiceCollection AddCommands(this IServiceCollection services, Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        return AddAllTypes<Command>(services, assembly, lifetime);
    }

    public static IServiceCollection AddCli(this IServiceCollection services, string prompt = "")
    {
        return services.AddTransient<Cli>()
            .AddTransient<IRepl, Repl>()
            .AddTransient<IExecutor, Executor>()
            .AddTransient<IConsole, CliConsole>()
            .AddSingleton<Prompt>(s => new Prompt(prompt))
            .AddTransient<HelpCommand>()
            .AddAllTypes<Command>();
    }

    public static void Each<T>(this IEnumerable<T> items, Action<T> action)
    {
        foreach (T item in items) action(item);
    }

    public static IServiceCollection AddAllTypes<T>(this IServiceCollection services, Assembly? assembly = null, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        var type = typeof(T);
        assembly = assembly ?? type.Assembly;
        assembly.DefinedTypes
            .Where(t => type == t.BaseType || (!t.IsAbstract && t.GetInterfaces().Contains(type)))
            .Each(t =>
            {
                services.Add(new ServiceDescriptor(type, t, lifetime));
            });
        return services;
    }

    public static IServiceCollection List(this IServiceCollection services)
    {
        foreach (var s in services)
        {
            System.Console.WriteLine($"{s.ImplementationType} - {s.ServiceType}");
        }
        return services;
    }

    public static string[] SplitSmart(this string s)
    {
        var re = new Regex("(?<=\")[^\"]*(?=\")|[^\" ]+");
        return re.Matches(s).Select(m => m.Value).ToArray();
    }

}