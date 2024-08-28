using System.Net;

namespace TestBCL.Network;

[TestClass]
public class TestIPEndPoint
{
    [TestMethod]
    public void IPEndPointCreate()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 9000);
        Assert.AreEqual("127.0.0.1:9000", endPoint.ToString());
    }
}
