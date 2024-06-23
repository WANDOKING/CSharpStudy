using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSyntax
{
    public class EvenNumberGenerator
    {
        public event EventHandler EvenNumberGenerated;

        public void Run(int limit)
        {
            for (int i = 1; i<= limit; ++i)
            {
                if (i % 2 == 0)
                {
                    EvenNumberGenerated(this, new EventArgs());
                }
            }
        }
    }

    [TestClass]
    public class TestEvent
    {
        [TestMethod]
        public void EventCallBack()
        {
            EvenNumberGenerator generator = new EvenNumberGenerator();

            int count = 0;

            generator.EvenNumberGenerated += (sender, e) =>
            {
                count++;
            };

            generator.Run(10);

            // 1부터 10까지의 짝수 개수는 5개
            Assert.AreEqual(5, count);
        }
    }
}
