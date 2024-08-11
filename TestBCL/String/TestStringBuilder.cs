using System.Text;

namespace TestBCL.String;

[TestClass]
public class TestStringBuilder
{
    [TestMethod]
    public void BuildString()
    {
        StringBuilder builder = new StringBuilder();

        Assert.AreEqual(0, builder.Length);

        builder.Append("ab");
        builder.Append("c");
        builder.Append("defg");

        Assert.AreEqual(7, builder.Length);
        Assert.AreEqual("abcdefg", builder.ToString());
    }

    [TestMethod]
    public void ExceedMaxCapacity()
    {
        const int CAPACITY = 5;
        const int MAX_CAPACITY = 5;

        StringBuilder builder = new StringBuilder(CAPACITY, MAX_CAPACITY);

        builder.Append("a");
        builder.Append("b");
        builder.Append("c");
        builder.Append("d");
        builder.Append("e");
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => builder.Append("f"));
    }
}
