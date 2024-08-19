namespace TestConcurrency.Lock;

[TestClass]
public class TestReaderWriterLock
{
    [TestMethod]
    public void ReadLock()
    {
        ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

        Assert.AreEqual(0, rwLock.CurrentReadCount);
        Assert.IsFalse(rwLock.IsReadLockHeld);
        Assert.IsFalse(rwLock.IsWriteLockHeld);

        rwLock.EnterReadLock();
        Assert.AreEqual(1, rwLock.CurrentReadCount);
        Assert.IsTrue(rwLock.IsReadLockHeld);

        rwLock.ExitReadLock();
        Assert.AreEqual(0, rwLock.CurrentReadCount);
        Assert.IsFalse(rwLock.IsReadLockHeld);
        Assert.IsFalse(rwLock.IsWriteLockHeld);
    }


    [TestMethod]
    public void WriteLock()
    {
        ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

        Assert.IsFalse(rwLock.IsReadLockHeld);
        Assert.IsFalse(rwLock.IsWriteLockHeld);

        rwLock.EnterWriteLock();
        Assert.IsFalse(rwLock.IsReadLockHeld);
        Assert.IsTrue(rwLock.IsWriteLockHeld);

        rwLock.ExitWriteLock();
        Assert.IsFalse(rwLock.IsReadLockHeld);
        Assert.IsFalse(rwLock.IsWriteLockHeld);
    }
}
