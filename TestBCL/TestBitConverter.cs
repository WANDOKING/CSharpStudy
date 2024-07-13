namespace TestBCL;

[TestClass]
public class TestBitConverter
{
    [TestMethod]
    public void ByteOrder()
    {
        // 일반적인 시스템이라면 이 값은 true
        Assert.AreEqual(true, BitConverter.IsLittleEndian);
    }

    [TestMethod]
    public void SerializeAndDeserializeBool()
    {
        bool valueToSerialize = true;
        byte[] serialized = BitConverter.GetBytes(valueToSerialize);
        Assert.AreEqual(sizeof(bool), serialized.Length);
        Assert.AreEqual(valueToSerialize, BitConverter.ToBoolean(serialized));
        Assert.AreEqual("01", BitConverter.ToString(serialized));
    }

    [TestMethod]
    public void SerializeAndDeserializeInt()
    {
        int valueToSerialize = 10;
        byte[] serialized = BitConverter.GetBytes(valueToSerialize);
        Assert.AreEqual(sizeof(int), serialized.Length);
        Assert.AreEqual(valueToSerialize, BitConverter.ToInt32(serialized));
        Assert.AreEqual("0A-00-00-00", BitConverter.ToString(serialized));
    }

    [TestMethod]
    public void DeserializeFail()
    {
        byte[] bytes = new byte[2];
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => BitConverter.ToInt32(bytes));
    }
}
