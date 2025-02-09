using CommandLine;

namespace CommandLineParserStudy;

internal sealed class CommandLineOptions
{
    [Value(0)]
    public string Value0 { get; set; } = string.Empty;

    [Value(1)]
    public string Value1 { get; set; } = string.Empty;

    [Option("option", HelpText = "option")]
    public string Option { get; set; } = string.Empty;
}
