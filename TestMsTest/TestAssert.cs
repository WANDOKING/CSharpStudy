namespace TestMsTest;

[TestClass]
public class TestAssert
{
    internal class TestClass()
    {
        public int Value1 { get; set; }

        public int Value2 { get; set; }

        public override bool Equals(object? obj) => true;

        public override int GetHashCode() => throw new NotImplementedException();
    }

    /// <summary>
    /// 객체의 실질적 값 비교를 한다.
    /// 내부적으로 Equals()를 호출해서 비교를 한다.
    /// 따라서
    /// </summary>
    [TestMethod]
    public void AreEqualOrNotEqual()
    {
        string string1 = "Hello";
        string string2 = string1;
        string string3 = "Hello";
        string string4 = "OK";

        Assert.AreEqual(string1, string2);
        Assert.AreEqual(string2, string3);

        Assert.AreNotEqual(string1, string4);
        Assert.AreNotEqual(string2, string4);
        Assert.AreNotEqual(string3, string4);

        // Equals를 무조건 true로 반환하게 하면 값이 달라도 패스
        // 즉, Equals() 구현에 의존
        TestClass testObject1 = new TestClass { Value1 = 1, Value2 = 2 };
        TestClass testObject2 = new TestClass { Value1 = 3, Value2 = 4 };
        Assert.AreEqual(testObject1, testObject2);
    }

    /// <summary>
    /// 참조 비교를 한다.
    /// 참고로 AreSame은 ValueType을 전달할 수 없다.
    /// </summary>
    [TestMethod]
    public void AreSame()
    {
        string string1 = new("Hello");
        string string2 = string1;
        string string3 = new("Hello");
        string string4 = "OK";

        Assert.AreSame(string1, string2);
        Assert.AreNotSame(string2, string3);

        Assert.AreNotSame(string1, string4);
        Assert.AreNotSame(string2, string4);
        Assert.AreNotSame(string3, string4);

        // Equals() 구현에 상관 없이 참조만 비교
        TestClass testObject1 = new TestClass { Value1 = 1, Value2 = 2 };
        TestClass testObject2 = new TestClass { Value1 = 3, Value2 = 4 };
        Assert.AreNotSame(testObject1, testObject2);
    }

    [TestMethod]
    public void IsTrueOrFalse()
    {
        Assert.IsTrue(true);
        Assert.IsFalse(false);
    }

    [TestMethod]
    public void IsNullOrNotNull()
    {
        Assert.IsNull(null);
        Assert.IsNotNull("Hello");
    }

    [TestMethod]
    public void IsInstanceOfType()
    {
        // 제네릭
        Assert.IsInstanceOfType<object>(5);

        // Type을 전달
        Assert.IsInstanceOfType(5, typeof(int));

        // Not 검사
        Assert.IsNotInstanceOfType(5, typeof(float));
    }

    [TestMethod]
    public void Inconclusive()
    {
        // 결정적이지 않은 테스트에 대해 AssertInconclusiveException를 던진다.
        Assert.Inconclusive("테스트");
    }

    [TestMethod]
    public void ThrowsException()
    {
        Assert.ThrowsException<ArgumentException>(() => throw new ArgumentException());

        // async 메서드의 경우 ThrowsException을 쓰면 정상적으로 테스트가 안된다.
        // 꼭 await를 해주는 ThrowsExceptionAsync를 사용해야 한다.
        Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
        {
            await Task.CompletedTask;
            throw new ArgumentException();
        });
    }
}