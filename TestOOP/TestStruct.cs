using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOOP
{
    [TestClass]
    public class TestStruct
    {
        public struct Vector2D
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        [TestMethod]
        public void DefaultConstructor()
        {
            Vector2D vector = new Vector2D();

            Assert.AreEqual(0, vector.X);
            Assert.AreEqual(0, vector.Y);
        }
    }
}
