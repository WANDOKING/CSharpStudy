using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace BenchmarkCollection;

internal class Runner
{
    private static void Main(string[] args)
    {
        BenchmarkSwitcher.FromAssembly(typeof(Runner).Assembly).Run(args);
    }
}