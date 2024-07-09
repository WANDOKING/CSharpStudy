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

    [TestMethod]
    public void SpinLock()
    {
        SpinLock spinLock = new SpinLock();

        Assert.IsFalse(spinLock.IsHeld);
        Assert.IsFalse(spinLock.IsHeldByCurrentThread);

        const int LOOP_COUNT = 100;
        int number = 0;

        Parallel.For(0, LOOP_COUNT, (i) =>
        {
            bool lockTaken = false;
            spinLock.Enter(ref lockTaken);
            {
                number++;
                Assert.IsTrue(lockTaken);
                Assert.IsTrue(spinLock.IsHeld);
                Assert.IsTrue(spinLock.IsHeldByCurrentThread);
            }
            spinLock.Exit();
            Assert.IsFalse(spinLock.IsHeldByCurrentThread);
        });

        Assert.AreEqual(LOOP_COUNT, number);
    }

    [TestMethod]
    public void SpinLockWithTrueArguement()
    {
        SpinLock spinLock = new SpinLock();
        bool lockTaken = true;
        Assert.ThrowsException<ArgumentException>(() => spinLock.Enter(ref lockTaken));
    }

    [TestMethod]
    public void SpinLockExitNotHeld()
    {
        SpinLock spinLock = new SpinLock();
        Assert.ThrowsException<SynchronizationLockException>(() => spinLock.Exit());
    }

    [TestMethod]
    public void SpinLockEnterTwice()
    {
        SpinLock spinLock = new SpinLock();
        bool lockTaken = false;
        spinLock.Enter(ref lockTaken);
        lockTaken = false;
        Assert.ThrowsException<LockRecursionException>(() => spinLock.Enter(ref lockTaken));
    }

    [TestMethod]
    public void SpinLockExitTwice()
    {
        SpinLock spinLock = new SpinLock();
        bool lockTaken = false;
        spinLock.Enter(ref lockTaken);
        spinLock.Exit();
        Assert.ThrowsException<SynchronizationLockException>(() => spinLock.Exit());
    }
}