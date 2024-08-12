namespace TestMsTest;

[TestClass]
public class TestCollectionAssert
{
    [TestMethod]
    public void IsSubsetOf()
    {
        List<int> list1 = [1, 2, 3, 4, 5];
        List<int> list2 = [1, 2, 3, 4, 5];
        List<int> list3 = [5];
        List<int> list4 = [1, 2];
        List<int> list5 = [1, 6];
        List<int> list6 = [];

        CollectionAssert.IsSubsetOf(subset: list2, superset: list1);
        CollectionAssert.IsSubsetOf(subset: list3, superset: list1);
        CollectionAssert.IsSubsetOf(subset: list4, superset: list1);
        CollectionAssert.IsNotSubsetOf(subset: list5, superset: list1);
        CollectionAssert.IsSubsetOf(subset: list6, superset: list1);
    }
}