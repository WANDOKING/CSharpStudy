using BenchmarkDotNet.Attributes;

namespace BenchmarkCollection;

[MemoryDiagnoser]
[MaxColumn, MinColumn]
public class BenchmarkInsert
{
    private Stack<int> intStack = new();
    private Queue<int> intQueue = new();
    private List<int> intList = new();

    [Params(0, 1000, 10000)]
    public int InitialCapacity { get; set; }

    [Params(100, 1000, 10000)]
    public int ElementCount { get; set; }

    [IterationSetup]
    public void IterationSetup()
    {
        intStack = new Stack<int>(InitialCapacity);
        intQueue = new Queue<int>(InitialCapacity);
        intList = new List<int>(InitialCapacity);
    }

    [Benchmark]
    public void Stack()
    {
        for (int i = 0; i < ElementCount; ++i)
        {
            intStack.Push(i);
        }
    }

    [Benchmark]
    public void Queue()
    {
        for (int i = 0; i < ElementCount; ++i)
        {
            intQueue.Enqueue(i);
        }
    }

    [Benchmark]
    public void List()
    {
        for (int i = 0; i < ElementCount; ++i)
        {
            intList.Add(i);
        }
    }
}