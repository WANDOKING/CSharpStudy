namespace TestBCL;

[TestClass]
public class TestConvert
{
    [TestMethod]
    public void ConvertToBoolean()
    {
        Assert.IsFalse(Convert.ToBoolean(false));
        Assert.IsFalse(Convert.ToBoolean(0));
        Assert.IsFalse(Convert.ToBoolean(0.0f));
        Assert.IsFalse(Convert.ToBoolean(null));
        Assert.IsFalse(Convert.ToBoolean("False"));
        Assert.IsFalse(Convert.ToBoolean("false"));

        Assert.IsTrue(Convert.ToBoolean(true));
        Assert.IsTrue(Convert.ToBoolean(1));
        Assert.IsTrue(Convert.ToBoolean(10.0f));
        Assert.IsTrue(Convert.ToBoolean("True"));
        Assert.IsTrue(Convert.ToBoolean("true"));

        Assert.ThrowsException<FormatException>(() => Convert.ToBoolean(""));
    }
}