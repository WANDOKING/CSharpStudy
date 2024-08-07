using System.IO.Compression;

namespace TestBCL.Compression;

[TestClass]
public class TestZLibStream
{
    [TestMethod]
    public void CompressAndDecompress()
    {
        byte[] orignal = CompressionTestUtils.GetBytesFilledWithRandomValue(1024);
        byte[] compressed;
        byte[] decompressed;

        using (var compressedStream = new MemoryStream())
        {
            using (var zLibStream = new ZLibStream(compressedStream, CompressionLevel.Optimal))
            {
                zLibStream.Write(orignal, 0, orignal.Length);
            }

            compressed = compressedStream.ToArray();
        }

        using (var compressedStream = new MemoryStream(compressed))
        {
            using var zLibStream = new ZLibStream(compressedStream, CompressionMode.Decompress);
            using var decompressedStream = new MemoryStream();

            zLibStream.CopyTo(decompressedStream);
            decompressed = decompressedStream.ToArray();
        }

        Assert.IsTrue(orignal.SequenceEqual(decompressed));
    }

    [TestMethod]
    public void CompressedSize()
    {
        byte[] orignal = CompressionTestUtils.GetBytesFilledWithFF(1024);
        byte[] compressed;

        using (var compressedStream = new MemoryStream())
        {
            using (var zLibStream = new ZLibStream(compressedStream, CompressionLevel.Optimal))
            {
                zLibStream.Write(orignal, 0, orignal.Length);
            }

            compressed = compressedStream.ToArray();
        }

        Assert.IsTrue(orignal.Length >= compressed.Length);
    }

    [TestMethod]
    public void DecompressInvalidValue()
    {
        byte[] orignal = CompressionTestUtils.GetBytesFilledWithRandomValue(1024);
        byte[] compressed;

        using (var compressedStream = new MemoryStream())
        {
            using (var zLibStream = new ZLibStream(compressedStream, CompressionLevel.Optimal))
            {
                zLibStream.Write(orignal, 0, orignal.Length);
            }

            compressed = compressedStream.ToArray();
        }

        // 압축된 데이터 손상시키기
        compressed[20] = 0xFF;
        compressed[45] = 0xFF;

        using (var compressedStream = new MemoryStream(compressed))
        {
            using var zLibStream = new ZLibStream(compressedStream, CompressionMode.Decompress);
            using var decompressedStream = new MemoryStream();

            Assert.ThrowsException<InvalidDataException>(() => zLibStream.CopyTo(decompressedStream));
        }
    }
}
