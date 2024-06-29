using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConcurrency;

[TestClass]
public class TestInterlocked
{
    [TestMethod]
    public void Increment()
    {
        int num = 0;
        int result = Interlocked.Increment(ref num);
        Assert.AreEqual(1, num);
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void Decrement()
    {
        int num = 0;
        int result = Interlocked.Decrement(ref num);
        Assert.AreEqual(-1, num);
        Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void Exchange()
    {
        int num = 0;
        int result = Interlocked.Exchange(ref num, 3);
        Assert.AreEqual(3, num);
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public async Task IncrementSynchronization()
    {
        const int TASK_COUNT = 100;
        const int INCREMENT_AT_TASK_COUNT = 100;

        int num = 0;

        List<Task> tasks = new List<Task>();

        for (int i = 0; i < TASK_COUNT; ++i)
        {
            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < INCREMENT_AT_TASK_COUNT; ++i)
                {
                    Interlocked.Increment(ref num);
                }
            }));
        }

        await Task.WhenAll(tasks);

        Assert.AreEqual(TASK_COUNT * INCREMENT_AT_TASK_COUNT, num);
    }
}
