namespace TestSyntax
{
    [TestClass]
    public class TestObject
    {
        [TestMethod]
        public void TestNumberGetHashCode()
        {
            int randomNumber = new Random().Next();

            Assert.AreEqual(randomNumber, randomNumber.GetHashCode());
        }
    }
}