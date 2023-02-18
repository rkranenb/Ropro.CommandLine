namespace Ropro.CommandLine.Tests;

public class FileExtensionsTests
{

    [Fact]
    public void ReadAll_reads_all_lines()
    {
        var path = "FileExtensionsTests.txt";
        var actual = path.ReadAll().ToArray();
        Assert.NotNull(actual);
        Assert.Equal(6, actual.Length);
        Assert.Equal("Line one", actual[0]);
        Assert.Equal("", actual[2]);
        Assert.Equal("Line four", actual[5]);
    }

    [Fact]
    public void ReadAll_skips_empty_lines()
    {
        var path = "FileExtensionsTests.txt";
        var actual = path.ReadAll(filter: (s) => string.IsNullOrEmpty(s))
            .ToArray();
        Assert.NotNull(actual);
        Assert.Equal(5, actual.Length);
        Assert.Equal("Line one", actual[0]);
        Assert.Equal("Line four", actual[4]);
    }

    [Fact]
    public void ReadAll_skips_empty_and_whitespace_lines()
    {
        var path = "FileExtensionsTests.txt";
        var actual = path.ReadAll(filter: (s) => string.IsNullOrWhiteSpace(s))
            .ToArray();
        Assert.NotNull(actual);
        Assert.Equal(4, actual.Length);
        Assert.Equal("Line one", actual[0]);
        Assert.Equal("Line four", actual[3]);
    }

    [Fact]
    public void SaveAs_writes_lines_to_file()
    {
        var path = Path.Combine(Path.GetTempPath(), "FileExtensionsTests_SaveAs.txt");
        var content = new string[] { "Line one", "", "Line two" };
        content.SaveAs(path);
        Assert.True(File.Exists(path));
    }
}