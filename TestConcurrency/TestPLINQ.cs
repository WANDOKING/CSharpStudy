namespace TestConcurrency;

[TestClass]
public class TestPLINQ
{
    [TestMethod]
    public void Sum()
    {
        const int NUMBERS_COUNT = 1000;

        List<int> numbers = new List<int>();
        Random random = new Random();

        int sum = 0;
        for (int i = 0; i < NUMBERS_COUNT; ++i)
        {
            int randomNumber = random.Next(100);
            sum += randomNumber;
            numbers.Add(randomNumber);
        }

        Assert.AreEqual(sum, numbers.AsParallel().Sum());
    }

    [TestMethod]
    public void AsOrderedSelect()
    {
        const int NUMBERS_COUNT = 100;

        List<int> numbers = new List<int>();

        for (int i = 0; i < NUMBERS_COUNT; ++i)
        {
            numbers.Add(i);
        }

        var result = numbers.AsParallel().AsOrdered().Select(value => value * 2);

        for (int i = 0; i < NUMBERS_COUNT; ++i)
        {
            Assert.AreEqual(numbers[i] * 2, result.ElementAt(i));
        }
    }
}
