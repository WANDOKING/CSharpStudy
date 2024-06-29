using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestConcurrency;

[TestClass]
public class TestLock
{
    [TestMethod]
    public async Task Lock()
    {
        const int TASK_COUNT = 100;
        const int INCREMENT_AT_TASK_COUNT = 100;

        object lockObject = new object();
        int num = 0;

        List<Task> tasks = new List<Task>();

        for (int i = 0; i < TASK_COUNT; ++i)
        {
            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < INCREMENT_AT_TASK_COUNT; ++i)
                {
                    lock (lockObject)
                    {
                        num++;
                    }
                }
            }));
        }

        await Task.WhenAll(tasks);

        Assert.AreEqual(TASK_COUNT * INCREMENT_AT_TASK_COUNT, num);
    }
}
