namespace BenchmarkArrayPool;

using BenchmarkDotNet.Running;

internal class Runner
{
    static void Main(string[] args)
    {
        BenchmarkSwitcher.FromAssembly(typeof(Runner).Assembly).Run(args);
    }
}
