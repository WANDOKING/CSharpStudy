using System.IO.Compression;

namespace TestBCL.Compression;

[TestClass]
public class TestDeflateStream
{
    [TestMethod]
    public void CompressAndDecompress()
    {
        byte[] orignal = CompressionTestUtils.GetBytesFilledWithRandomValue(1024);
        byte[] compressed;
        byte[] decompressed;

        using (var compressedStream = new MemoryStream())
        {
            using (var deflateStream = new DeflateStream(compressedStream, CompressionLevel.Optimal))
            {
                deflateStream.Write(orignal, 0, orignal.Length);
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

        Assert.IsTrue(orignal.SequenceEqual(decompressed));
    }

    [TestMethod]
    public void CompressedSize()
    {
        byte[] orignal = CompressionTestUtils.GetBytesFilledWithFF(1024);
        byte[] compressed;

        using (var compressedStream = new MemoryStream())
        {
            using (var deflateStream = new DeflateStream(compressedStream, CompressionLevel.Optimal))
            {
                deflateStream.Write(orignal, 0, orignal.Length);
            }

            compressed = compressedStream.ToArray();
        }

        Assert.IsTrue(orignal.Length >= compressed.Length);
    }
}
