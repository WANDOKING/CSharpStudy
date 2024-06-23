using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSyntax
{
    [TestClass]
    public class TestEnum
    {
        enum EDirection
        {
            North,
            South,
            East,
            West,
        }

        [TestMethod]
        public void EnumToString()
        {
            Assert.AreEqual("South", EDirection.South.ToString());
        }

        enum EFruit
        {
            Apple = 1 << 0,
            Orange = 1 << 1,
            Kiwi = 1 << 2,
            Mango = 1 << 3
        }

        [Flags]
        enum EFruitFlags
        {
            Apple = 1 << 0,
            Orange = 1 << 1,
            Kiwi = 1 << 2,
            Mango = 1 << 3
        }

        [TestMethod]
        public void EnumFlagsToString()
        {
            Assert.AreEqual("Orange", ((EFruit)(2)).ToString());
            Assert.AreEqual("5", ((EFruit)(1 | 4)).ToString());

            Assert.AreEqual("Orange", ((EFruitFlags)(2)).ToString());
            Assert.AreEqual("Apple, Kiwi", ((EFruitFlags)(1 | 4)).ToString());
        }

        [TestMethod]
        public void EnumHasFlag()
        {
            EFruit fruits = EFruit.Apple | EFruit.Mango;

            Assert.IsTrue(fruits.HasFlag(EFruit.Apple));
            Assert.IsTrue(fruits.HasFlag(EFruit.Mango));
        }

        [TestMethod]
        public void EnumFlagsHasFlag()
        {
            EFruitFlags fruits = EFruitFlags.Apple | EFruitFlags.Mango;

            Assert.IsTrue(fruits.HasFlag(EFruitFlags.Apple));
            Assert.IsTrue(fruits.HasFlag(EFruitFlags.Mango));
        }
    }
}
