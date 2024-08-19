namespace TestConcurrency;

[TestClass]
public class TestTaskScheduler
{
    [TestMethod]
    public void DefaultMaximumConcurrencyLevel()
    {
        Assert.AreEqual(int.MaxValue, TaskScheduler.Default.MaximumConcurrencyLevel);
    }

    [TestMethod]
    public void DefaultId()
    {
        Assert.AreEqual(1, TaskScheduler.Default.Id);
    }

    [TestMethod]
    public async Task UnobservedTaskException()
    {
        int innerExceptionCount = 0;
        Exception? innerException = null;

        TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

        _ = Task.Run(() =>
        {
            throw new ArgumentException();
        });

        await Task.Yield();

        GC.Collect();
        GC.WaitForPendingFinalizers();

        TaskScheduler.UnobservedTaskException -= OnUnobservedTaskException;

        Assert.AreEqual(1, innerExceptionCount);
        Assert.IsTrue(innerException is ArgumentException);

        void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs args)
        {
            innerExceptionCount = args.Exception.InnerExceptions.Count;
            innerException = args.Exception.InnerException;
        }
    }
}
