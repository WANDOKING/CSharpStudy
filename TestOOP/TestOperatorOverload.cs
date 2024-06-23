using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestOOP
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point operator+(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
    }

    public class Currency
    {
        public decimal Money { get; set; }

        public Currency(decimal money)
        {
            Money = money;
        }
    }

    public class Won : Currency
    {
        public Won(decimal money)
            : base(money)
        {
        }

        public override string ToString() => Money + "Won";

        static public implicit operator Dollar(Won won) => new Dollar(won.Money / 1400M);
    }

    public class Dollar : Currency
    {
        public Dollar(decimal money)
            : base(money)
        {
        }

        public override string ToString() => Money + "Dollar";

        static public implicit operator Won(Dollar dollar) => new Won(dollar.Money * 1400M);
    }

    [TestClass]
    public class TestOperatorOverload
    {
        [TestMethod]
        public void PlusOperatorOverload()
        {
            Point point1 = new Point(1, 3);
            Point point2 = new Point(2, 5);

            Point point3 = point1 + point2;

            Assert.AreEqual(1, point1.X);
            Assert.AreEqual(3, point1.Y);
            Assert.AreEqual(2, point2.X);
            Assert.AreEqual(5, point2.Y);
            Assert.AreEqual(point1.X + point2.X, point3.X);
            Assert.AreEqual(point1.Y + point2.Y, point3.Y);
        }

        [TestMethod]
        public void TypeCastOperatorOverload()
        {
            Won won1 = new Won(1400);
            Dollar dollar1 = won1;
            Assert.AreEqual(1M, dollar1.Money);

            Dollar dollar2 = new Dollar(1);
            Won won2 = dollar2;
            Assert.AreEqual(1400M, won2.Money);
        }
    }
}
