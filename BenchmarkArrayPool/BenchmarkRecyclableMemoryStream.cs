using System;
using System.Buffers;
using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.IO;

[MemoryDiagnoser]
public class MemoryStreamBenchmark
{
    private readonly byte[] sampleData;
    private readonly RecyclableMemoryStreamManager recyclableManager;

    public MemoryStreamBenchmark()
    {
        sampleData = new byte[1024 * 64]; // 64KB
        new Random(42).NextBytes(sampleData);
        recyclableManager = new RecyclableMemoryStreamManager();
    }

    [Benchmark(Baseline = true)]
    public void ArrayPool_MultiThread()
    {
        Parallel.For(0, 100, _ =>
        {
            var buffer = ArrayPool<byte>.Shared.Rent(sampleData.Length);
            try
            {
                Buffer.BlockCopy(sampleData, 0, buffer, 0, sampleData.Length);
                using var ms = new MemoryStream(buffer, 0, sampleData.Length, writable: false);
                Span<byte> tmp = stackalloc byte[1];
                ms.Read(tmp);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        });
    }

    [Benchmark]
    public void RecyclableMemoryStream_MultiThread()
    {
        Parallel.For(0, 100, _ =>
        {
            using var stream = recyclableManager.GetStream();
            stream.Write(sampleData, 0, sampleData.Length);
            stream.Position = 0;
            Span<byte> tmp = stackalloc byte[1];
            stream.Read(tmp);
        });
    }
}