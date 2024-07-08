namespace TestConcurrency;

[TestClass]
public class TestParallel
{
    [TestMethod]
    public void For()
    {
        int primeNumberCount = 0;

        Parallel.For(2, 10000, (int number) =>
        {
            if (IsPrime(number))
            {
                Interlocked.Increment(ref primeNumberCount);
            }
        });

        Assert.AreEqual(1229, primeNumberCount);

        bool IsPrime(int number)
        {
            int sqrtNum = (int)Math.Sqrt(number);

            for (int i = 2; i <= sqrtNum; ++i)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }

    [TestMethod]
    public void ForEach()
    {
        int primeNumberCount = 0;

        List<int> numbers = new List<int>();

        for (int i = 2; i <= 10000; ++i)
        {
            numbers.Add(i);
        }

        Parallel.ForEach(numbers, (int number) =>
        {
            if (IsPrime(number))
            {
                Interlocked.Increment(ref primeNumberCount);
            }
        });

        Assert.AreEqual(1229, primeNumberCount);

        bool IsPrime(int number)
        {
            int sqrtNum = (int)Math.Sqrt(number);

            for (int i = 2; i <= sqrtNum; ++i)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }

    [TestMethod]
    public void ParallelStop()
    {
        int sum = 0;

        Parallel.For(1, 10, (number, state) =>
        {
            if (number == 5)
            {
                state.Stop();
            }

            Interlocked.Add(ref sum, number);
        });

        Assert.AreNotEqual(55, sum);
    }

    [TestMethod]
    [ExpectedException(typeof(OperationCanceledException))]
    public void ParallelCancel()
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        ParallelOptions parallelOptions = new ParallelOptions()
        {
            CancellationToken = cts.Token
        };

        _ = Task.Run(async () =>
        {
            await Task.Delay(1);
            cts.Cancel();
        });

        int sum = 0;
        Parallel.For(1, int.MaxValue, parallelOptions, number => Interlocked.Add(ref sum, number));
    }

    [TestMethod]
    public void ParallelInvoke()
    {
        int ACTIONS_COUNT = 50;
        int sum = 0;

        List<Action> actions = new List<Action>();
        
        for (int i = 0; i < ACTIONS_COUNT; ++i)
        {
            actions.Add(() => Interlocked.Increment(ref sum));
        }

        Parallel.Invoke(actions.ToArray());

        Assert.AreEqual(ACTIONS_COUNT, sum);
    }
}