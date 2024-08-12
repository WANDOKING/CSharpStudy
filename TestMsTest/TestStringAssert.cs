namespace TestMsTest;

using System.Text.RegularExpressions;

[TestClass]
public class TestStringAssert
{
    const string TEST_STRING1 = "Hello World";
    const string TEST_STRING2 = "dowankim";

    [TestMethod]
    public void StartsWithAndEndsWith()
    {
        StringAssert.StartsWith(TEST_STRING1, "Hello");
        StringAssert.EndsWith(TEST_STRING1, "World");
    }

    [TestMethod]
    public void Contains()
    {
        StringAssert.Contains(TEST_STRING1, "Hello");
        StringAssert.Contains(TEST_STRING1, "World");
        StringAssert.Contains(TEST_STRING1, "Hello World");
    }

    [TestMethod]
    public void MatchesAndDoesNotMatch()
    {
        Regex startsWithHello = new Regex("^Hello");

        StringAssert.Matches(TEST_STRING1, startsWithHello);
        StringAssert.DoesNotMatch(TEST_STRING2, startsWithHello);
    }
}