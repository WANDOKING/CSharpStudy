using System.Text.RegularExpressions;

namespace TestBCL.String;

[TestClass]
public class TestRegex
{
    [TestMethod]
    public void MatchEmail()
    {
        string correctEmail1 = "tester@test.com";
        string correctEmail2 = "dowankim57@gmail.com";
        string correctEmail3 = "dowankim@nexon.co.kr";

        string wrongEmail1 = "16123";
        string wrongEmail2 = "1@@";
        string wrongEmail3 = "tester@@test.com";
        string wrongEmail4 = "abcd@test@com.com";

        Regex emailRegex = new Regex(@"^([0-9a-zA-Z]+)@([0-9a-zA-Z]+)(\.[0-9a-zA-Z]+){1,}$");

        Assert.IsTrue(emailRegex.IsMatch(correctEmail1));
        Assert.IsTrue(emailRegex.IsMatch(correctEmail2));
        Assert.IsTrue(emailRegex.IsMatch(correctEmail3));

        Assert.IsFalse(emailRegex.IsMatch(wrongEmail1));
        Assert.IsFalse(emailRegex.IsMatch(wrongEmail2));
        Assert.IsFalse(emailRegex.IsMatch(wrongEmail3));
        Assert.IsFalse(emailRegex.IsMatch(wrongEmail4));
    }

    [TestMethod]
    public void Replace()
    {
        Regex regex = new Regex("apple");

        string original = "I love anything that tastes like apple, including apple juice.";
        string replaced = regex.Replace(original, "mango");
        Assert.AreEqual("I love anything that tastes like mango, including mango juice.", replaced);
    }
}
