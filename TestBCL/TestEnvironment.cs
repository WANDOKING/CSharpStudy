namespace TestBCL;

[TestClass]
public class TestEnvironment
{
    [TestMethod]
    public void ProcessPath()
    {
        StringAssert.Contains(Environment.ProcessPath, "TestBCL");
    }

    [TestMethod]
    public void SystemPageSize()
    {
        Assert.AreEqual(4096, Environment.SystemPageSize);
    }

    [TestMethod]
    public void StackTrace()
    {
        StringAssert.Contains(Environment.StackTrace, "StackTrace");
    }
}
