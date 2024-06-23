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

        [TestMethod]
        public void BreakStackTrace()
        {
            try
            {
                Method1();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.StackTrace!.Contains("Method1"));
                Assert.IsFalse(ex.StackTrace!.Contains("Method2"));
                Assert.IsFalse(ex.StackTrace!.Contains("Method3"));
            }

            static void Method1()
            {
                try
                {
                    Method2();
                }
                catch (Exception ex)
                {
#pragma warning disable CA2200 // Rethrow to preserve stack details
                    throw ex; // 스택 정보가 날아간다
#pragma warning restore CA2200 // Rethrow to preserve stack details
                }
            }

            static void Method2()
            {
                Method3();
            }

            static void Method3()
            {
                int num1 = 10;
                int num2 = 0;
                int num3 = num1 / num2;
                Console.WriteLine(num3);
            }
        }

        [TestMethod]
        public void throwCorrectly()
        {
            try
            {
                Method1();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.StackTrace!.Contains("Method1"));
                Assert.IsTrue(ex.StackTrace!.Contains("Method2"));
                Assert.IsTrue(ex.StackTrace!.Contains("Method3"));
            }

            static void Method1()
            {
                try
                {
                    Method2();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            static void Method2()
            {
                Method3();
            }

            static void Method3()
            {
                int num1 = 10;
                int num2 = 0;
                int num3 = num1 / num2;
                Console.WriteLine(num3);
            }
        }
    }
}
