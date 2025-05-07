using BenchmarkDotNet.Attributes;
using Microsoft.IO;
using System.Threading.Tasks;

/// <summary>
/// 멀티스레드 환경에서 <see cref="RecyclableMemoryStream"/>의 성능을 측정합니다.
/// </summary>
[MemoryDiagnoser]
public class BenchmarkRecyclableStreamConcurrency
{
    private const int RandomSeed = 42;

    private readonly byte[] _sampleData;
    private readonly RecyclableMemoryStreamManager _manager;
    private const int BufferSize = 1024;

    public BenchmarkRecyclableStreamConcurrency()
    {
        _sampleData = new byte[BufferSize];
        new Random(RandomSeed).NextBytes(_sampleData);

        var options = new RecyclableMemoryStreamManager.Options()
        {
            BlockSize = BufferSize,
            LargeBufferMultiple = 1024 * 1024,
            MaximumBufferSize = 16 * 1024 * 1024,
            GenerateCallStacks = false,
            AggressiveBufferReturn = true,
            MaximumLargePoolFreeBytes = 16 * 1024 * 1024 * 4,
            MaximumSmallPoolFreeBytes = 100 * 1024,
        };

        _manager = new RecyclableMemoryStreamManager();
    }

    [Benchmark(Baseline = true)]
    public void SingleThreaded_Access()
    {
        for (int i = 0; i < 1000; i++)
        {
            using var stream = _manager.GetStream();
            stream.Write(_sampleData, 0, BufferSize);
        }
    }

    [Benchmark]
    public void Parallel_GetAndRead()
    {
        Parallel.For(0, 1000, _ =>
        {
            using var stream = _manager.GetStream();
            stream.Write(_sampleData, 0, BufferSize);
        });
    }

    [Benchmark]
    public void Thread_Complicated()
    {
        int threadCount = Environment.ProcessorCount;
        var threads = new Thread[threadCount];

        for (int t = 0; t < threadCount; t++)
        {
            threads[t] = new Thread(() =>
            {
                var streams = new List<Stream>(10);

                for (int i = 0; i < 1000; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        var stream = _manager.GetStream();
                        stream.Write(_sampleData, 0, BufferSize);
                        streams.Add(stream);
                    }

                    for (int j = 0; j < 10; j++)
                    {
                        streams[j].Dispose();
                    }
                }
            });
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }

    [Benchmark]
    public void Thread_GetAndRead()
    {
        int threadCount = Environment.ProcessorCount;
        var threads = new Thread[threadCount];

        for (int t = 0; t < threadCount; t++)
        {
            threads[t] = new Thread(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    using var stream = _manager.GetStream();
                    stream.Write(_sampleData, 0, BufferSize);
                }
            });
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
    }
}
