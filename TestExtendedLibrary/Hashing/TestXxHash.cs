namespace TestExtendedLibrary.Hashing;

using System.IO.Hashing;

[TestClass]
public class TestXxHash
{
    [TestMethod]
    public void Hash32()
    {
        var xxHash = new XxHash32();
        byte[] data = System.Text.Encoding.UTF8.GetBytes("Hello World");
        xxHash.Append(data);
        byte[] hashValue = xxHash.GetHashAndReset();

        Assert.AreEqual(4, hashValue.Length);
        Assert.AreEqual("B1-FD-16-EE", BitConverter.ToString(hashValue));
    }

    [TestMethod]
    public void Hash64()
    {
        var xxHash = new XxHash64();
        byte[] data = System.Text.Encoding.UTF8.GetBytes("Hello World");
        xxHash.Append(data);
        byte[] hashValue = xxHash.GetHashAndReset();

        Assert.AreEqual(8, hashValue.Length);
        Assert.AreEqual("63-34-D2-07-19-24-5B-C2", BitConverter.ToString(hashValue));
    }

    [TestMethod]
    public void GetCurrentHashTwice()
    {
        var xxHash = new XxHash64();
        byte[] data = System.Text.Encoding.UTF8.GetBytes("Hello World");
        xxHash.Append(data);
        byte[] hashValue1 = xxHash.GetCurrentHash();
        byte[] hashValue2 = xxHash.GetCurrentHash();

        Assert.IsTrue(hashValue1.SequenceEqual(hashValue2));
    }

    [TestMethod]
    public void GetCurrentHashAfterGetHashAndReset()
    {
        var xxHash = new XxHash64();
        byte[] data = System.Text.Encoding.UTF8.GetBytes("Hello World");
        xxHash.Append(data);
        byte[] hashValue1 = xxHash.GetHashAndReset();
        byte[] hashValue2 = xxHash.GetCurrentHash();

        Assert.IsFalse(hashValue1.SequenceEqual(hashValue2));
    }
}