using Microsoft.IO;

namespace TestExtendedLibrary;

[TestClass]
public class TestRecyclableMemoryStream
{
    [TestMethod]
    public void WriteAndRead()
    {
        var options = new RecyclableMemoryStreamManager.Options()
        {
            BlockSize = 1024,
            LargeBufferMultiple = 1024 * 1024,
            MaximumBufferSize = 16 * 1024 * 1024,
            GenerateCallStacks = true,
            AggressiveBufferReturn = true,
            MaximumLargePoolFreeBytes = 16 * 1024 * 1024 * 4,
            MaximumSmallPoolFreeBytes = 100 * 1024,
        };

        var manager = new RecyclableMemoryStreamManager(options);

        byte[] toWrite = { 0x01, 0x02, 0x03, 0x04 };

        using (var stream = new RecyclableMemoryStream(manager))
        {
            stream.Write(toWrite);

            byte[] buffer = stream.GetBuffer();

            Assert.IsTrue(buffer.Length >= toWrite.Length);
            Assert.AreEqual(toWrite[0], buffer[0]);
            Assert.AreEqual(toWrite[1], buffer[1]);
            Assert.AreEqual(toWrite[2], buffer[2]);
            Assert.AreEqual(toWrite[3], buffer[3]);
        }
    }
}
