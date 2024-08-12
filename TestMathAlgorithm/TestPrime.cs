namespace TestMathAlgorithm;

/// <summary>
/// IsPrime �˰����� �׽�Ʈ�ϴ� ���� �׽�Ʈ�Դϴ�.
/// �� Ŭ������ ���� �Ҽ� ã�⸦ �׽�Ʈ �Ѵٱ� ���ٴ� MS Test�� �����ϴ� �Ϳ� ������ �ΰ� �ֽ��ϴ�.
/// </summary>
[TestClass]
public class TestPrime
{
    public static int GlobalNumber { get; set; }

    public static bool IsPrime(int naturalNum)
    {
        if (naturalNum < 1)
        {
            throw new ArgumentException($"{naturalNum} is not natural number.");
        }

        if (naturalNum == 1)
        {
            return false;
        }

        int sqrtNum = (int)Math.Sqrt(naturalNum);

        for (int i = 2; i <= sqrtNum; ++i)
        {
            if (naturalNum % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    [TestInitialize]
    public void TestInitialize()
    {
        GlobalNumber = 1;
    }

    [TestCleanup]
    public void TestCleanup()
    {
        GlobalNumber = -1;
    }

    [TestMethod]
    [DataRow(2, 3, 5, 7)]
    public void PrimeNumbersLessThen10(params int[] nums)
    {
        foreach (int num in nums)
        {
            Assert.IsTrue(IsPrime(num));
        }
    }

    [TestMethod]
    [DataRow(1, 4, 6, 8, 9)]
    [Description("10���� ���� �Ҽ��� ���ؼ� �׽�Ʈ�մϴ�.")]
    public void CompositeNumbersLessThen10(params int[] nums)
    {
        CollectionAssert.AllItemsAreUnique(nums);

        foreach (int num in nums)
        {
            Assert.IsFalse(IsPrime(num));
        }
    }

    [TestMethod]
    [DataRow(-9, -8, -7, -6, -5, -4, -3, -2, -1)]
    public void NegativeNumbersBiggerThanMinus10(params int[] nums)
    {
        CollectionAssert.AllItemsAreUnique(nums);

        foreach (int num in nums)
        {
            Assert.ThrowsException<ArgumentException>(() => IsPrime(num));
        }
    }

    [TestMethod]
    public void OneToThousand()
    {
        for (int testNum = 2; testNum <= 1_000; ++testNum)
        {
            bool bIsPrime = true;

            for (int divideNum = 2; divideNum < testNum; ++divideNum)
            {
                if (testNum % divideNum == 0)
                {
                    bIsPrime = false;
                    break;
                }
            }

            Assert.AreEqual(bIsPrime, IsPrime(testNum));
        }
    }

    [TestMethod]
    public void AssemblyInitialize()
    {
        Assert.AreEqual(AssemblyInitializer.GlobalNumber, 1);
        AssemblyInitializer.GlobalNumber = 2;
    }

    [TestMethod]
    public void InitializeTest1()
    {
        Assert.AreEqual(GlobalNumber, 1);
        GlobalNumber = 100;
    }

    [TestMethod]
    public void InitializeTest2()
    {
        Assert.AreEqual(GlobalNumber, 1);
        GlobalNumber = 200;
    }
}