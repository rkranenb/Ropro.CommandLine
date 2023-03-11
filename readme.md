# What's CommandLine
It is a simple framework that helps build console apps with multiple commands. It is a work in progress (hence the 0.x version). On my wish list are helpers for arguments and to return a Task, rather than a bool, to make async operations easier to implement. Stay tuned.
# Quick start
From the CLI, create a new console application:

    dotnet new console -o foo && cd foo
    
   Add the package:

    dotnet add package Ropro.CommandLine --version 0.3.0

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

# Convention
Executing commands is based on the convention that command names are prefixed with "Command". In the example above, the FooCommand is run by typing `foo`.

Not following the convention may work in many cases, but may also cause unexpected behavior.
# Providing help
## Help text
To get help a user can type `help` either in the REPL or from the CLI. This will show a list of available commands:

    My repl > help
    foo

As this may not be very helpful, you may provide additional information my using the `HelpText`attribute:

    [HelpText("Writes 'foo' to the console.")]
    public class FooCommand : Command { ... }

This provides the user with a bit more help:

    My repl > help
      foo               Writes 'foo' to the console.
## Usage
Another form of help is usage information. This will be shown when command-specific help is requested (not implemented yet) and when a [command exception](#Command%20exceptions) is thrown.

    [Usage("foo greeting")]
    public class FooCommand : Command { 
	    if (args.Length == 0)
		    throw new CommandException(this, "No arguments!");
    }
When a command exception is thrown, not only the message, but also the usage information is shown:

    My repl > foo
    No arguments!
    usage: [dotnet run] foo greeting

# Aliases
It is possible to add one or more aliases to a command. 

    [Alias("bar")]
    public class FooCommand : Command { ... }
This provides the user with a bit more help:

    My repl > foo
    Foo!
    
    My repl > bar
    Foo!

# Command exceptions
A special kind of exception can be thrown to alert the user. This exception can be thrown when invalid arguments are passed.
