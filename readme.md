# What's CommandLine
It is a simple framework that helps build console apps with multiple commands.

# How to use
From the CLI, create a new console application:

    dotnet new console -o my-console
    cd my-console
    
   Add the package:
   

    dotnet add package Ropro.CommandLine --version 0.1.0

Open your favorite editor and and add a new class:

    using Ropro.CommandLine;
    public class FooCommand : Command {
	    public override bool Run(string key, string[] args) {
		    System.Console.WriteLine("Foo!");
		    return true;
	    }
    }

Add the following code to the `Program.cs` class:

    using Microsoft.Extensions.DependencyInjection;
    using Ropro.CommandLine;
    new ServiceCollection()
	    .AddCli()
	    .AddCommands(typeof(FooCommand).Assembly)
	    .BuildServiceProvider()
	    .GetRequiredService<Cli>()
	    .Start(args);

You can use your console application either as a REPL by typing:

    dotnet run

Or you can run your commands directly from the CLI:

    dotnet run foo

# Help
To get help you can type `help` either in the REPL or from the CLI.