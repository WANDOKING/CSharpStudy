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
    /// ��ü�� ������ �� �񱳸� �Ѵ�.
    /// ���������� Equals()�� ȣ���ؼ� �񱳸� �Ѵ�.
    /// ����
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

        // Equals�� ������ true�� ��ȯ�ϰ� �ϸ� ���� �޶� �н�
        // ��, Equals() ������ ����
        TestClass testObject1 = new TestClass { Value1 = 1, Value2 = 2 };
        TestClass testObject2 = new TestClass { Value1 = 3, Value2 = 4 };
        Assert.AreEqual(testObject1, testObject2);
    }

    /// <summary>
    /// ���� �񱳸� �Ѵ�.
    /// ����� AreSame�� ValueType�� ������ �� ����.
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

        // Equals() ������ ��� ���� ������ ��
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
        // ���׸�
        Assert.IsInstanceOfType<object>(5);

        // Type�� ����
        Assert.IsInstanceOfType(5, typeof(int));

        // Not �˻�
        Assert.IsNotInstanceOfType(5, typeof(float));
    }

    [TestMethod]
    public void Inconclusive()
    {
        // ���������� ���� �׽�Ʈ�� ���� AssertInconclusiveException�� ������.
        Assert.Inconclusive("�׽�Ʈ");
    }

    [TestMethod]
    public void ThrowsException()
    {
        Assert.ThrowsException<ArgumentException>(() => throw new ArgumentException());

        // async �޼����� ��� ThrowsException�� ���� ���������� �׽�Ʈ�� �ȵȴ�.
        // �� await�� ���ִ� ThrowsExceptionAsync�� ����ؾ� �Ѵ�.
        Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
        {
            await Task.CompletedTask;
            throw new ArgumentException();
        });
    }
}