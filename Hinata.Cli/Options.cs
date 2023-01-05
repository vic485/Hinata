using CommandLine;

namespace Hinata.Cli;

internal class Options
{
    [Value(0, MetaName = "input file", HelpText = "Input file to be processed.", Required = true)]
    public string FilePath { get; set; }

    [Option('c', "codepage", Required = false, HelpText = "Encoding code page for archive file names")]
    public int? CodePage { get; set; }

    [Option('o', "output", Required = false, HelpText = "Path to extract output to")]
    public string? OutputPath { get; set; }

    [Option('p', "password", Required = false, HelpText = "Archive password")]
    public string? Password { get; set; }

    [Option('v', "verbose", Required = false, HelpText = "Display verbose output")]
    public bool Verbose { get; set; }
}
