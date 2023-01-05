using CommandLine;
using Hinata.Cli;
using Hinata.Core;

Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
{
    var outputDir = o.OutputPath ?? Directory.GetCurrentDirectory();

    Extractor.Extract(o.FilePath, outputDir, o.CodePage, o.Password, o.Verbose);
});
