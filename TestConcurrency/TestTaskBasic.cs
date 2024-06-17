using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConcurrency;

public class TimeCheckHelper : IDisposable
{
    private Stopwatch mStopwatch;

    public TimeCheckHelper(Stopwatch stopwatch)
    {
        mStopwatch = stopwatch;
        mStopwatch.Start();
    }

    public void Dispose()
    {
        mStopwatch.Stop();
    }
}

[TestClass]
public class TestTaskBasic
{
    [TestMethod]
    public async Task TestDelay()
    {
        TimeSpan delayTime = TimeSpan.FromSeconds(1);

        var stopWatch = new Stopwatch();
        using (var timeCheck = new TimeCheckHelper(stopWatch))
        {
            await Task.Delay(delayTime);
        }

        Assert.IsTrue(stopWatch.Elapsed > delayTime);
    }
}
