using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using System.IO.Compression;

namespace BenchmarkCompression;

internal class Runner
{
    private static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<BenchmarkCompressAndDepress>();
    }
}

[MemoryDiagnoser]
public class BenchmarkCompressAndDepress
{
    private const int ORIGINAL_BYTES_LENGTH = 1024;

    private Random random = new Random();

    private byte[]? Original { get; set; }

    [Params(256)]
    public int PatternLength { get; set; }

    [Params(CompressionLevel.Optimal, CompressionLevel.SmallestSize, CompressionLevel.Fastest)]
    public CompressionLevel CompressionLevel { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        Original = GetTestBytes(ORIGINAL_BYTES_LENGTH, PatternLength);
    }

    [Benchmark]
    public int DeflateStream()
    {
        byte[] compressed;
        byte[] decompressed;

        using (var compressedStream = new MemoryStream())
        {
            using (var deflateStream = new DeflateStream(compressedStream, CompressionLevel))
            {
                deflateStream.Write(Original!, 0, Original!.Length);
            }

            compressed = compressedStream.ToArray();
        }

        using (var compressedStream = new MemoryStream(compressed))
        {
            using var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress);
            using var decompressedStream = new MemoryStream();

            deflateStream.CopyTo(decompressedStream);
            decompressed = decompressedStream.ToArray();
        }

        return compressed.Length;
    }

    [Benchmark]
    public int ZlibStream()
    {
        byte[] compressed;
        byte[] decompressed;

        using (var compressedStream = new MemoryStream())
        {
            using (var zlibStream = new ZLibStream(compressedStream, CompressionLevel))
            {
                zlibStream.Write(Original!, 0, Original!.Length);
            }

            compressed = compressedStream.ToArray();
        }

        using (var compressedStream = new MemoryStream(compressed))
        {
            using var zlibStream = new ZLibStream(compressedStream, CompressionMode.Decompress);
            using var decompressedStream = new MemoryStream();

            zlibStream.CopyTo(decompressedStream);
            decompressed = decompressedStream.ToArray();
        }

        return compressed.Length;
    }

    private byte[] GetTestBytes(int byteLength, int patternLength)
    {
        byte[] result = new byte[byteLength];

        byte[] pattern = new byte[patternLength];

        random.NextBytes(pattern);

        int currentPosition = 0;
        while (currentPosition + patternLength <= byteLength)
        {
            Array.Copy(pattern, 0, result, currentPosition, patternLength);
            currentPosition += patternLength;
        }

        return result;
    }
}

public class CompressionConfig : ManualConfig
{
    public CompressionConfig()
    {
        AddColumn(new CompressedSizeColumn("DeflateStream"));
        AddColumn(new CompressedSizeColumn("ZlibStream"));
    }
}

public class CompressedSizeColumn : IColumn
{
    private readonly string _benchmarkMethodName;

    public CompressedSizeColumn(string benchmarkMethodName)
    {
        _benchmarkMethodName = benchmarkMethodName;
    }

    public string Id => _benchmarkMethodName + nameof(CompressedSizeColumn);
    public string ColumnName => $"{_benchmarkMethodName} Compressed Size";
    public bool AlwaysShow => true;
    public ColumnCategory Category => ColumnCategory.Custom;
    public int PriorityInCategory => 0;
    public bool IsNumeric => true;
    public UnitType UnitType => UnitType.Size;
    public string Legend => "Size of the compressed output in bytes.";

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase)
    {
        var report = summary[benchmarkCase];
        if (report == null)
            return "-";

        var compressMethod = benchmarkCase.Descriptor.WorkloadMethod;
        if (compressMethod.Name != _benchmarkMethodName)
            return "-";

        var instance = Activator.CreateInstance(benchmarkCase.Descriptor.Type);
        var result = (int)compressMethod.Invoke(instance, null);
        return result.ToString();
    }

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
    {
        return GetValue(summary, benchmarkCase);
    }

    public bool IsAvailable(Summary summary)
    {
        return true;
    }

    public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase)
    {
        return false;
    }
}