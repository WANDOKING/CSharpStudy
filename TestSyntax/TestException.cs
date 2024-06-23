namespace TestSyntax
{
    [TestClass]
    public class TestException
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TryCatch()
        {
            throw new Exception();
        }
    }
}
