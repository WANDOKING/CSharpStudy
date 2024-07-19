using System;

namespace TestBCL.Time;

[TestClass]
public class TestTimeSpan
{
    [TestMethod]
    public void CreateFrom()
    {
        Assert.AreEqual(1, TimeSpan.FromTicks(1).Ticks);
        Assert.AreEqual(10, TimeSpan.FromMicroseconds(1).Ticks);
        Assert.AreEqual(10_000, TimeSpan.FromMilliseconds(1).Ticks);
        Assert.AreEqual(10_000_000, TimeSpan.FromSeconds(1).Ticks);
        Assert.AreEqual(600_000_000, TimeSpan.FromMinutes(1).Ticks);
    }

    [TestMethod]
    public void Add()
    {
        TimeSpan a = TimeSpan.FromSeconds(1);
        TimeSpan b = TimeSpan.FromMilliseconds(700);
        Assert.AreEqual(TimeSpan.FromMilliseconds(1700), a + b);
    }

    [TestMethod]
    public void Subtract()
    {
        TimeSpan a = TimeSpan.FromSeconds(1);
        TimeSpan b = TimeSpan.FromMilliseconds(700);
        Assert.AreEqual(TimeSpan.FromMilliseconds(300), a - b);
    }

    [TestMethod]
    public void Multiply()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(1);
        Assert.AreEqual(TimeSpan.FromMinutes(1), timeSpan * 60);
    }

    [TestMethod]
    public void Divide()
    {
        TimeSpan timeSpan = TimeSpan.FromMinutes(1);
        Assert.AreEqual(TimeSpan.FromSeconds(1), timeSpan / 60);
    }

    [TestMethod]
    public void Resolution()
    {
        TimeSpan a = TimeSpan.FromSeconds(1);

        Assert.AreEqual(10, TimeSpan.TicksPerMicrosecond);
        Assert.AreEqual(10_000, TimeSpan.TicksPerMillisecond);
        Assert.AreEqual(10_000_000, TimeSpan.TicksPerSecond);
    }
}
