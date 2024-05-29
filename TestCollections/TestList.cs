namespace TestCollections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TestList
{
    [TestMethod]
    public void TestIntList()
    {
        const int ELEMENT_COUNT = 10;

        var nums = new List<int>();
        Assert.AreEqual(nums.Capacity, 0);
        Assert.AreEqual(nums.Count, 0);

        for (int i = 0; i < ELEMENT_COUNT; ++i)
        {
            nums.Add(i);
        }

        Assert.AreEqual(nums.Count, ELEMENT_COUNT);

        for (int i = 0; i < ELEMENT_COUNT; ++i)
        {
            Assert.IsTrue(nums.Contains(i));
        }
    }
}