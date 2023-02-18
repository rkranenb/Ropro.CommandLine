namespace Ropro.CommandLine;

public static class FileExtensions
{

#pragma warning disable CS8625
    public static IEnumerable<string?> ReadAll(this string path, Func<string?, bool> filter = null)
    {
        using var reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            var s = reader.ReadLine();
            if (filter == null || !filter(s))
            {
                yield return s;
            }
        }
    }

    public static void SaveAs(this IEnumerable<string?> lines, string path)
    {
        using var writer = new StreamWriter(path);
        lines.Each(line => writer.WriteLine(line));
    }
}