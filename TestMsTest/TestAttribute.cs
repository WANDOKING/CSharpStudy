namespace TestMsTest;

[TestClass]
public class TestAttribute
{
    [TestMethod]
    [Ignore("IgnoreAttribute를 테스트하기 위한 용도입니다. 경고로 감지되면 정상입니다.")]
    public void Ignore()
    {
        // 실행되면 실패합니다.
        Assert.Fail();
    }
}
