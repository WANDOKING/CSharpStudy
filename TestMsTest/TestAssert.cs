namespace TestMsTest;

[TestClass]
public class TestAssert
{
    [TestMethod]
    public void AreEqualOrNotEqual()
    {
        string string1 = "Hello";
        string string2 = string1;
        string string3 = "Hello";

        Assert.AreEqual(string1, string2);
        Assert.AreEqual(string2, string3);
    }

    [TestMethod]
    public void IsTrueOrFalse()
    {
        Assert.IsTrue(true);
        Assert.IsFalse(false);
    }

    [TestMethod]
    public void IsNullOrNotNull()
    {
        Assert.IsNull(null);
        Assert.IsNotNull("Hello");
    }
}