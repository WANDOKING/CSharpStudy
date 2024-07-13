using System.Text;

namespace TestBCL;

[TestClass]
public class TestEncoding
{
    [TestMethod]
    public void SerializeAndDeserializeEnglish()
    {
        string str = "ABCDE";
        byte[] serialized = Encoding.UTF8.GetBytes(str);
        Assert.AreEqual(1 * str.Length, serialized.Length);
        Assert.AreEqual(str, Encoding.UTF8.GetString(serialized));
        Assert.AreEqual("41-42-43-44-45", BitConverter.ToString(serialized));
    }

    [TestMethod]
    public void DeserializePartialEnglish()
    {
        string str = "ABCDE";
        byte[] serialized = Encoding.UTF8.GetBytes(str);
        Assert.AreEqual(1 * str.Length, serialized.Length);
        Assert.AreEqual("ABC", Encoding.UTF8.GetString(serialized, 0, 3));
        Assert.AreEqual("41-42-43", BitConverter.ToString(serialized, 0, 3));
    }

    [TestMethod]
    public void SerializeAndDeserializeKorean()
    {
        string str = "가나다라";
        byte[] serialized = Encoding.UTF8.GetBytes(str);
        Assert.AreEqual(3 * str.Length, serialized.Length);
        Assert.AreEqual(str, Encoding.UTF8.GetString(serialized));
    }

    [TestMethod]
    public void DeserializePartialKorean()
    {
        string str = "가나다라";
        byte[] serialized = Encoding.UTF8.GetBytes(str);
        Assert.AreEqual(3 * str.Length, serialized.Length);
        Assert.AreEqual("가", Encoding.UTF8.GetString(serialized, 0, 3));
    }
}
