using System.Text;

namespace TestBCL;

[TestClass]
public class TestEncoding
{
    private const string stringEnglish = "ABCDE";
    private const string stringKorean = "가나다라";
    private const string stringMixed = "A가B나C다D라";

    [TestMethod]
    public void SerializeAndDeserializeEnglish()
    {
        byte[] serialized = Encoding.UTF8.GetBytes(stringEnglish);
        Assert.AreEqual(1 * stringEnglish.Length, serialized.Length);
        Assert.AreEqual(stringEnglish, Encoding.UTF8.GetString(serialized));
        Assert.AreEqual("41-42-43-44-45", BitConverter.ToString(serialized));
    }

    [TestMethod]
    public void DeserializePartialEnglish()
    {
        byte[] serialized = Encoding.UTF8.GetBytes(stringEnglish);
        Assert.AreEqual(1 * stringEnglish.Length, serialized.Length);
        Assert.AreEqual("ABC", Encoding.UTF8.GetString(serialized, 0, 3));
        Assert.AreEqual("41-42-43", BitConverter.ToString(serialized, 0, 3));
    }

    [TestMethod]
    public void SerializeAndDeserializeKorean()
    {
        byte[] serialized = Encoding.UTF8.GetBytes(stringKorean);
        Assert.AreEqual(3 * stringKorean.Length, serialized.Length);
        Assert.AreEqual(stringKorean, Encoding.UTF8.GetString(serialized));
    }

    [TestMethod]
    public void DeserializePartialKorean()
    {
        byte[] serialized = Encoding.UTF8.GetBytes(stringKorean);
        Assert.AreEqual(3 * stringKorean.Length, serialized.Length);
        Assert.AreEqual("가", Encoding.UTF8.GetString(serialized, 0, 3));
    }

    [TestMethod]
    public void SerializeAndDeserializeMixed()
    {
        byte[] serialized = Encoding.UTF8.GetBytes(stringMixed);
        Assert.AreEqual(stringMixed, Encoding.UTF8.GetString(serialized));
    }

    [TestMethod]
    public void UTF8ByteCount()
    {
        Assert.AreEqual(stringEnglish.Length, Encoding.UTF8.GetByteCount(stringEnglish));
        Assert.AreEqual(3 * stringKorean.Length, Encoding.UTF8.GetByteCount(stringKorean));
    }
}
