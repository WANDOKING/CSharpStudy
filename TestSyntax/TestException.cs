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

        [TestMethod]
        public void CheckedOverflow()
        {
            int num1 = int.MaxValue;
            checked
            {
                Assert.ThrowsException<OverflowException>(() => num1++);
            }

            int num2 = int.MaxValue;
            unchecked
            {
                num2++;
            }
        }
    }
}
