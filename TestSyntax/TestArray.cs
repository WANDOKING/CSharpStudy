namespace TestSyntax
{
    [TestClass]
    public class TestArray
    {
        [TestMethod]
        public void ArrayProperties()
        {
            int[] arr1 = { 1, 2, 3, 4, 5 };
            Assert.AreEqual(arr1.Rank, 1);
            Assert.AreEqual(arr1.Length, 5);

            int[,] arr2 = { { 1, 2, 3 }, { 4, 5, 6 } };
            Assert.AreEqual(arr2.Rank, 2);
            Assert.AreEqual(arr2.Length, 6);
        }

        [TestMethod]
        public void IndexOutOfRangeException()
        {
            int[] array = { 1, 2, 3, 4, 5 };

            int zero = 0;

            Assert.ThrowsException<IndexOutOfRangeException>(() => array[zero - 1]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => array[array.Length + 1]);
        }

        [TestMethod]
        public void Copy()
        {
            byte[] randomBytes1 = new byte[100];
            byte[] randomBytes2 = new byte[100];

            new Random().NextBytes(randomBytes1);
            new Random().NextBytes(randomBytes2);
            Assert.IsFalse(randomBytes1.SequenceEqual(randomBytes2));

            Array.Copy(randomBytes1, randomBytes2, 100);
            Assert.IsTrue(randomBytes1.SequenceEqual(randomBytes2));
        }

        [TestMethod]
        public void Sort()
        {
            byte[] randomBytes = new byte[100];

            new Random().NextBytes(randomBytes);

            Array.Sort(randomBytes);

            for (int i = 1; i < randomBytes.Length; ++i)
            {
                Assert.IsTrue(randomBytes[i] >= randomBytes[i - 1]);
            }
        }
    }
}
