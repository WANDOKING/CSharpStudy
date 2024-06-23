using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSyntax
{
    public class EvenNumberGenerator
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public event EventHandler EvenNumberGenerated;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public void Run(int limit)
        {
            for (int i = 1; i<= limit; ++i)
            {
                if (i % 2 == 0)
                {
                    EvenNumberGenerated(this, EventArgs.Empty);
                }
            }
        }
    }

    public class OddNumberGenerator
    {
        public event EventHandler<OddNumberEventArgs> OddNumberGenerated = delegate { };

        public void Run(int limit)
        {
            for (int i = 1; i <= limit; ++i)
            {
                if (i % 2 != 0)
                {
                    OddNumberGenerated(this, new OddNumberEventArgs(i));
                }
            }
        }
    }

    public class OddNumberEventArgs : EventArgs
    {
        public int Number { get; }

        public OddNumberEventArgs(int number)
        {
            Number = number;
        }
    }

    [TestClass]
    public class TestEvent
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void EmptyEvent()
        {
            EvenNumberGenerator generator = new EvenNumberGenerator();
            generator.Run(10);
        }

        [TestMethod]
        public void EventCallBack()
        {
            EvenNumberGenerator generator = new EvenNumberGenerator();

            int count = 0;

            generator.EvenNumberGenerated += (sender, e) =>
            {
                Assert.AreEqual(EventArgs.Empty, e);
                count++;
            };

            generator.Run(10);

            // 1부터 10까지의 짝수 개수는 5개
            Assert.AreEqual(5, count);
        }

        [TestMethod]
        public void EventCallBackWithArgs()
        {
            OddNumberGenerator generator = new OddNumberGenerator();

            int count = 0;

            int[] expectedValues = [1, 3, 5, 7, 9];

            generator.OddNumberGenerated += (sender, e) =>
            {
                Assert.AreEqual(expectedValues[count], e.Number);
                count++;
            };

            generator.Run(10);

            // 1부터 10까지의 홀수 개수는 5개
            Assert.AreEqual(5, count);
        }
    }
}
