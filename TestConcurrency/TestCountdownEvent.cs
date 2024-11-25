namespace TestConcurrency;

[TestClass]
public class TestCountdownEvent
{
    [TestMethod]
    public void Count()
    {
        const int INITIAL_COUNT = 2;

        using CountdownEvent countdownEvent = new(INITIAL_COUNT);

        Assert.AreEqual(INITIAL_COUNT, countdownEvent.CurrentCount);
        Assert.AreEqual(INITIAL_COUNT, countdownEvent.InitialCount);
        Assert.IsFalse(countdownEvent.IsSet);

        Assert.IsFalse(countdownEvent.Signal());

        Assert.AreEqual(INITIAL_COUNT - 1, countdownEvent.CurrentCount);
        Assert.AreEqual(INITIAL_COUNT, countdownEvent.InitialCount);
        Assert.IsFalse(countdownEvent.IsSet);

        Assert.IsTrue(countdownEvent.Signal());

        Assert.AreEqual(INITIAL_COUNT - 2, countdownEvent.CurrentCount);
        Assert.AreEqual(INITIAL_COUNT, countdownEvent.InitialCount);
        Assert.IsTrue(countdownEvent.IsSet);
    }

    [TestMethod]
    public void WaitAndSignal()
    {
        using CountdownEvent countdownEvent = new(100);

        Parallel.For(0, 100, _ => countdownEvent.Signal());

        countdownEvent.Wait();

        Assert.AreEqual(0, countdownEvent.CurrentCount);
        Assert.AreEqual(100, countdownEvent.InitialCount);
        Assert.IsTrue(countdownEvent.IsSet);
    }

    [TestMethod]
    public void AddCount()
    {
        using CountdownEvent countdownEvent = new(1);

        countdownEvent.AddCount();
        Assert.AreEqual(2, countdownEvent.CurrentCount);

        countdownEvent.Signal();
        countdownEvent.Signal();
        Assert.ThrowsException<InvalidOperationException>(() => countdownEvent.AddCount());
    }

    [TestMethod]
    public void TryAddCount()
    {
        using CountdownEvent countdownEvent = new(1);

        Assert.IsTrue(countdownEvent.TryAddCount());
        Assert.AreEqual(2, countdownEvent.CurrentCount);

        countdownEvent.Signal();
        countdownEvent.Signal();
        Assert.IsFalse(countdownEvent.TryAddCount());
    }
}