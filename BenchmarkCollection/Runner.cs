using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace BenchmarkCollection;

internal class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<BenchmarkCollection>();
    }
}

[MemoryDiagnoser]
public class BenchmarkCollection
{
    private const int loopCount = 1000;

    private Stack<int> intStack = new(loopCount);
    private Queue<int> intQueue = new(loopCount);

    [IterationSetup]
    public void IterationSetup()
    {
        intStack.Clear();
        intQueue.Clear();
    }

    [Benchmark]
    public void StackPush()
    {
        for (int i = 0; i < loopCount; ++i)
        {
            intStack.Push(i);
        }
    }

    [Benchmark]
    public void QueueEnqueue()
    {
        for (int i = 0; i < loopCount; ++i)
        {
            intQueue.Enqueue(i);
        }
    }
}