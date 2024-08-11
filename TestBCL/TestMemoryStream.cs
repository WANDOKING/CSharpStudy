using System.IO;

namespace TestBCL;

[TestClass]
public class TestMemoryStream
{
    [TestMethod]
    public void WriteAndRead()
    {
        using var memoryStream = new MemoryStream();

        memoryStream.Write(BitConverter.GetBytes(1));
        memoryStream.Write(BitConverter.GetBytes('A'));
        memoryStream.Write(BitConverter.GetBytes(10));

        Assert.AreEqual(sizeof(int) + sizeof(char) + sizeof(int), memoryStream.Length);
        Assert.AreEqual(sizeof(int) + sizeof(char) + sizeof(int), memoryStream.Position);

        memoryStream.Position = 0;

        byte[] result = new byte[12];
        memoryStream.Read(result);

        Assert.AreEqual(1, result[0]);
        Assert.AreEqual(0x00, result[1]);
        Assert.AreEqual(0x00, result[2]);
        Assert.AreEqual(0x00, result[3]);

        Assert.AreEqual(65, result[4]);
        Assert.AreEqual(0x00, result[5]);

        Assert.AreEqual(10, result[6]);
        Assert.AreEqual(0x00, result[7]);
        Assert.AreEqual(0x00, result[8]);
        Assert.AreEqual(0x00, result[9]);

        Assert.AreEqual(0x00, result[10]);
        Assert.AreEqual(0x00, result[11]);
    }

    [TestMethod]
    public void InitialState()
    {
        using var memoryStream = new MemoryStream();

        Assert.AreEqual(0, memoryStream.Capacity);
        Assert.AreEqual(0, memoryStream.Length);
        Assert.AreEqual(0, memoryStream.Position);

        Assert.IsTrue(memoryStream.CanRead);
        Assert.IsTrue(memoryStream.CanSeek);
        Assert.IsTrue(memoryStream.CanWrite);
        Assert.IsFalse(memoryStream.CanTimeout);
    }

    [TestMethod]
    [Description("공식 문서에서 보장하는 동작이 아님")]
    public void CapacityGrowth()
    {
        using var memoryStream = new MemoryStream();
        Assert.AreEqual(0, memoryStream.Capacity);

        memoryStream.Write(new byte[1]);
        Assert.AreEqual(256, memoryStream.Capacity);

        memoryStream.Write(new byte[memoryStream.Capacity]);
        Assert.AreEqual(512, memoryStream.Capacity);
    }
}
