namespace Ropro.CommandLine;

public static class ArgsExtensions
{

    public static string? GetValue(this string[] args, string flag)
    {
        bool contains = false;
        for (int i = 0; i < args.Length; i++)
        {
            if (contains) return args[i];
            contains = args[i] == flag;
        }
        return null;
    }

    public static DateTime GetDate(this string[] args, string flag)
    {
        var value = args.GetValue(flag);
        if (value != null && DateTime.TryParse(value, out DateTime result))
        {
            return result;
        }
        throw new CommandArgumentException($"Unexpected value provided for '{flag}'. Expected a date.");
    }

    public static int GetInt(this string[] args, string flag)
    {
        var value = args.GetValue(flag);
        if (value != null && int.TryParse(value, out int result))
        {
            return result;
        }
        throw new CommandArgumentException($"Unexpected value provided for '{flag}'. Expected an integer number.");
    }

}