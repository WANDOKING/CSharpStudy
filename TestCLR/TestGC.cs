using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using static TestCLR.TestGC;

namespace TestCLR
{
    [TestClass]
    public class TestGC
    {
        public class MyClass
        {
            public static bool FinalizeCalled { get; set; }

            ~MyClass()
            {
                FinalizeCalled = true;
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            MyClass.FinalizeCalled = false;
        }

        [TestMethod]
        public void CollectionCount()
        {
            int gcCount = GC.CollectionCount(0);

            for (int i = 0; i < 10; ++i)
            {
                GC.Collect();
                gcCount++;
                Assert.AreEqual(gcCount, GC.CollectionCount(0));
            }
        }

        [TestMethod]
        public void GetGeneration()
        {
            int gcCount = GC.CollectionCount(0);
            object obj = new object();
            Assert.AreEqual(0, GC.GetGeneration(obj));
            

            GC.Collect();
            Assert.AreEqual(1, GC.GetGeneration(obj));

            GC.Collect();
            Assert.AreEqual(2, GC.GetGeneration(obj));

            GC.Collect();
            Assert.AreEqual(2, GC.GetGeneration(obj)); // 2세대 유지
        }

        [TestMethod]
        public void Finalizer()
        {
            DoSomething();
            GC.Collect();

            // Finalize 스레드를 잠깐 대기
            GC.WaitForPendingFinalizers();

            Assert.IsTrue(MyClass.FinalizeCalled);

            void DoSomething()
            {
                MyClass myClass = new MyClass();
            }
        }

        [TestMethod]
        public void SuppressFinalize()
        {
            DoSomething();
            GC.Collect();

            // Finalize 스레드를 잠깐 대기
            GC.WaitForPendingFinalizers();

            Assert.IsFalse(MyClass.FinalizeCalled);

            void DoSomething()
            {
                MyClass myClass = new MyClass();
                GC.SuppressFinalize(myClass);
            }
        }
    }
}