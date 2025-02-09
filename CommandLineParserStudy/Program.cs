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

internal class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments<CommandLineOptions>(args)
            .MapResult(
                options => Run(options),
                errors => HandleParseError(errors));
    }

    private static int HandleParseError(IEnumerable<Error> errors)
    {
        Console.WriteLine($"{nameof(HandleParseError)}");

        foreach (var error in errors)
        {
            Console.WriteLine(error);
        }

        return 0;
    }

    private static int Run(CommandLineOptions options)
    {
        Console.WriteLine($"[{nameof(CommandLineOptions)}]");

        foreach (var property in options.GetType().GetProperties())
        {
            Console.WriteLine($"{property.Name}: {property.GetValue(options)}");
        }

        return 0;
    }
}