using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace BenchmarkStringBuilder;

internal class Runner
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<BenchmarkString>();
    }
}

[MemoryDiagnoser]
public class BenchmarkString
{
    private const int RESULT_STRING_LENGTH = 100;

    [Benchmark]
    public string GetStringByPlus()
    {
        string result = "";

        for (int i = 0; i < RESULT_STRING_LENGTH; ++i)
        {
            result += i;
        }

        return result;
    }

    [Benchmark]
    public string GetStringByStringBuilder()
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < RESULT_STRING_LENGTH; ++i)
        {
            builder.Append(i);
        }

        return builder.ToString();
    }

    [Benchmark]
    public string GetStringByStringBuilderWithCapacity()
    {
        StringBuilder builder = new StringBuilder(RESULT_STRING_LENGTH);

        for (int i = 0; i < RESULT_STRING_LENGTH; ++i)
        {
            builder.Append(i);
        }

        return builder.ToString();
    }
}