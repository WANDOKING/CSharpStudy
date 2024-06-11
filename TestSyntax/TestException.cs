namespace TestSyntax
{
    [TestClass]
    public class TestException
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestTryCatch()
        {
            throw new Exception();
        }
    }
}
