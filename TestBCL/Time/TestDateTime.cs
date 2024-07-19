using System.Globalization;

namespace TestBCL.Time;

[TestClass]
public class TestDateTime
{
    [TestMethod]
    public void Now()
    {
        DateTime now = DateTime.Now;
    }

    [TestMethod]
    public void Create()
    {
        DateTime dateTime = new DateTime(
            year: 2024,
            month: 7,
            day: 14,
            hour: 15,
            minute: 10,
            second: 24);

        Assert.AreEqual(2024, dateTime.Year);
        Assert.AreEqual(7, dateTime.Month);
        Assert.AreEqual(14, dateTime.Day);
        Assert.AreEqual(15, dateTime.Hour);
        Assert.AreEqual(10, dateTime.Minute);
        Assert.AreEqual(24, dateTime.Second);
        Assert.AreEqual("2024-07-14 오후 3:10:24", dateTime.ToString());
    }

    [TestMethod]
    public void MinMax()
    {
        DateTime minDateTime = DateTime.MinValue;

        // 그레고리오력의 00:00:00.0000000 UTC, 0001년 1월 1일
        Assert.AreEqual("0001-01-01 오전 12:00:00", minDateTime.ToString());
        Assert.AreEqual(0, minDateTime.Ticks);

        DateTime maxDateTime = DateTime.MaxValue;

        // 그레고리오력의 23:59:59.9999999 UTC, 9999년 12월 31일
        Assert.AreEqual("9999-12-31 오후 11:59:59", maxDateTime.ToString());
    }

    [TestMethod]
    public void Ticks()
    {
        DateTime a = DateTime.Now;
        DateTime b = a;
        a = a.AddMilliseconds(1);

        const int TICK_NS = 100;
        Assert.AreEqual(TICK_NS * 100, TimeSpan.TicksPerMillisecond);
        Assert.AreEqual(TimeSpan.TicksPerMillisecond, a.Ticks - b.Ticks);
    }

    [TestMethod]
    public void Format()
    {
        DateTime dateTime = DateTime.MaxValue;

        const string HOUR_FORMAT_12 = "yyyy-MM-dd tt hh:mm:ss (ddd)";
        const string HOUR_FORMAT_24 = "yyyy-MM-dd HH:mm:ss (dddd)";

        Assert.AreEqual("9999-12-31 오후 11:59:59 (금)", dateTime.ToString(HOUR_FORMAT_12));
        Assert.AreEqual("9999-12-31 23:59:59 (금요일)", dateTime.ToString(HOUR_FORMAT_24));
    }

    [TestMethod]
    public void CultureInfo()
    {
        DateTime dateTime = DateTime.MaxValue;

        CultureInfo cultureInfoKorea = new CultureInfo("ko-KR");
        CultureInfo cultureInfoUS = new CultureInfo("en-US");

        Assert.AreEqual("9999-12-31 오후 11:59:59", dateTime.ToString(cultureInfoKorea));
        Assert.AreEqual("12/31/9999 11:59:59 PM", dateTime.ToString(cultureInfoUS));
    }

    [TestMethod]
    public void Parse()
    {
        Assert.AreEqual(DateTime.MaxValue, DateTime.Parse("9999-12-31 오후 11:59:59.9999999"));
    }

    [TestMethod]
    public void UTC()
    {
        // 대한민국은 UTC +9
        TimeSpan diff = DateTime.Now - DateTime.UtcNow;
        Assert.AreEqual(9, Math.Ceiling(diff.TotalHours));
    }

    [TestMethod]
    public void Kind()
    {
        DateTime dateTime = new DateTime();
        Assert.AreEqual(DateTimeKind.Unspecified, dateTime.Kind);
        Assert.AreEqual(DateTimeKind.Local, DateTime.Now.Kind);
        Assert.AreEqual(DateTimeKind.Utc, DateTime.UtcNow.Kind);
    }
}