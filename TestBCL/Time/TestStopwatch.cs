using System.Diagnostics;

namespace TestBCL.Time;

[TestClass]
public class TestStopwatch
{
    [TestMethod]
    public void Frequency()
    {
        Assert.AreEqual(10_000_000, Stopwatch.Frequency);
    }

    [TestMethod]
    public void HighResoultion()
    {
        Assert.IsTrue(Stopwatch.IsHighResolution);
    }

    [TestMethod]
    public void Ticks()
    {
        const int SLEEP_TIME_MS = 1;

        Stopwatch watch = Stopwatch.StartNew();
        Thread.Sleep(SLEEP_TIME_MS);
        watch.Stop();

        Assert.IsTrue(watch.ElapsedMilliseconds > SLEEP_TIME_MS, $"watch.ElapsedMilliseconds = {watch.ElapsedMilliseconds}");
    }
}
