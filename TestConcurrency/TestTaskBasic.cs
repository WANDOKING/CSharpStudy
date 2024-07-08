using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
    public async Task Delay()
    {
        int DELAY_MILLISECONDS = 50;
        TimeSpan delayTime = TimeSpan.FromMilliseconds(DELAY_MILLISECONDS);

        var stopWatch = new Stopwatch();
        using (var timeCheck = new TimeCheckHelper(stopWatch))
        {
            await Task.Delay(delayTime);
        }

        Assert.IsTrue(stopWatch.Elapsed >= TimeSpan.FromMilliseconds(DELAY_MILLISECONDS - 1));
    }

    [TestMethod]
    public async Task CompletedTask()
    {
        await Task.CompletedTask;
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public async Task FromException()
    {
        await Task.FromException<NotImplementedException>(new NotImplementedException());
    }

    [TestMethod]
    public async Task FromResult()
    {
        bool shouldBeTrue = await Task.FromResult(true);

        Assert.IsTrue(shouldBeTrue);
    }

    [TestMethod]
    public async Task WhenAll()
    {
        const int TASK_COUNT = 50;
        const int MIN_DELAY_MS = 1;
        const int MAX_DELAY_MS = 100;

        Random random = new Random();
        var tasks = new List<Task>();

        var stopWatch = new Stopwatch();
        using (var timeCheck = new TimeCheckHelper(stopWatch))
        {
            for (int i = 0; i < TASK_COUNT - 1; ++i)
            {
                tasks.Add(Task.Run(() => Task.Delay(random.Next(MIN_DELAY_MS, MAX_DELAY_MS))));
            }

            tasks.Add(Task.Run(() => Task.Delay(MAX_DELAY_MS)));

            await Task.WhenAll(tasks);
        }

        foreach (var task in tasks)
        {
            Assert.IsTrue(task.IsCompleted);
        }

        Assert.IsTrue(stopWatch.Elapsed >= TimeSpan.FromMilliseconds(MAX_DELAY_MS - 1), $"stopWatch.Elapsed = {stopWatch.Elapsed}");
    }

    [TestMethod]
    public async Task WhenAll_CatchAllExceptions()
    {
        var task1 = Task.FromException<InvalidOperationException>(new InvalidOperationException());
        var task2 = Task.FromException<NotImplementedException>(new NotImplementedException());

        Task allTasks = Task.WhenAll(task1, task2);

        try
        {
            await allTasks;
        }
        catch
        {
            Assert.IsNotNull(allTasks.Exception);

            foreach (var exception in allTasks.Exception.InnerExceptions)
            {
                Assert.IsTrue(exception is InvalidOperationException || exception is NotImplementedException);
            }
        }
    }

    [TestMethod]
    public async Task WhenAny()
    {
        Task<int> taskA = DelayAndReturnAsync(200);
        Task<int> taskB = DelayAndReturnAsync(300);
        Task<int> taskC = DelayAndReturnAsync(100);

        Task<int> result = await Task.WhenAny(taskA, taskB, taskC);

        Assert.IsTrue(result.IsCompleted);
        Assert.AreEqual(100, result.Result);

        async Task<int> DelayAndReturnAsync(int value)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(value));
            return value;
        }
    }

    [TestMethod]
    public async Task ProcessWhenTaskCompleted()
    {
        Task<int> taskA = DelayAndReturnAsync(2);
        Task<int> taskB = DelayAndReturnAsync(3);
        Task<int> taskC = DelayAndReturnAsync(1);
        Task<int>[] tasks = new[] { taskA, taskB, taskC };

        var results = new ConcurrentQueue<int>();

        // 람다는 동기적으로 실행되지만, await 이후 비동기 실행
        Task[] processingTasks = tasks.Select(async task =>
        {
            var result = await task;
            results.Enqueue(result);
        }).ToArray();

        // 모든 작업을 기다린다.
        await Task.WhenAll(processingTasks);

        results.TryDequeue(out var result1);
        results.TryDequeue(out var result2);
        results.TryDequeue(out var result3);

        Assert.AreEqual(1, result1);
        Assert.AreEqual(2, result2);
        Assert.AreEqual(3, result3);

        async Task<int> DelayAndReturnAsync(int value)
        {
            await Task.Delay(TimeSpan.FromSeconds(value));
            return value;
        }
    }

    [TestMethod]
    public async Task Exception()
    {
        Task task = ThrowExceptionAsync();

        await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await task);

        async Task ThrowExceptionAsync()
        {
            await Task.Delay(10);
            throw new InvalidOperationException("Test");
        }
    }
}
