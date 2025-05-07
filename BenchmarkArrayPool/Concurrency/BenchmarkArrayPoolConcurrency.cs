using System.Buffers;
using BenchmarkDotNet.Attributes;

/// <summary>
/// 멀티스레드 환경에서 <see cref="ArrayPool{T}.Shared"/>의 성능을 측정합니다.
/// </summary>
[MemoryDiagnoser]
public class BenchmarkArrayPoolConcurrency
{
    private const int RandomSeed = 42;

    private readonly byte[] _sampleData;
    private const int BufferSize = 1024;

    public BenchmarkArrayPoolConcurrency()
    {
        _sampleData = new byte[BufferSize];
        new Random(RandomSeed).NextBytes(_sampleData);
    }

    [Benchmark(Baseline = true)]
    public void SingleThreaded_Access()
    {
        for (int i = 0; i < 1000; i++)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(BufferSize);

            try
            {
                Buffer.BlockCopy(_sampleData, 0, buffer, 0, BufferSize);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }

    [Benchmark]
    public void Parallel_GetAndRead()
    {
        Parallel.For(0, 1000, _ =>
        {
            var buffer = ArrayPool<byte>.Shared.Rent(BufferSize);

            try
            {
                Buffer.BlockCopy(_sampleData, 0, buffer, 0, BufferSize);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
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
                var buffers = new List<byte[]>(10);

                for (int i = 0; i < 1000; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        var buffer = ArrayPool<byte>.Shared.Rent(BufferSize);
                        Buffer.BlockCopy(_sampleData, 0, buffer, 0, BufferSize);
                        buffers.Add(buffer);
                    }

                    for (int j = 0; j < 10; j++)
                    {
                        ArrayPool<byte>.Shared.Return(buffers[j]);
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
                    var buffer = ArrayPool<byte>.Shared.Rent(BufferSize);

                    try
                    {
                        Buffer.BlockCopy(_sampleData, 0, buffer, 0, BufferSize);
                    }
                    finally
                    {
                        ArrayPool<byte>.Shared.Return(buffer);
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
}
