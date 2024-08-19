namespace TestConcurrency.Lock;

[TestClass]
public class TestSpinLock
{
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
