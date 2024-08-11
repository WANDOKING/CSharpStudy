using System.Text;

namespace TestBCL.String;

[TestClass]
public class TestEncoding
{
    [TestMethod]
    public void AsciiToUtf16()
    {
        string original = "abcd";
        byte[] encoded = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, Encoding.UTF8.GetBytes(original));
        Assert.AreEqual(8, encoded.Length);
        Assert.AreEqual("abcd", Encoding.Unicode.GetString(encoded));
    }

    [TestMethod]
    public void AsciiToUtf32()
    {
        string original = "abcd";
        byte[] encoded = Encoding.Convert(Encoding.UTF8, Encoding.UTF32, Encoding.UTF8.GetBytes(original));
        Assert.AreEqual(16, encoded.Length);
        Assert.AreEqual("abcd", Encoding.UTF32.GetString(encoded));
    }
}
