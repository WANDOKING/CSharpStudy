using System.Net;
using System.Net.Sockets;

namespace TestBCL.Network;

[TestClass]
public class TestIPAddress
{
    [TestMethod]
    public void IPAddressCreate()
    {
        IPAddress ip = new IPAddress(new byte[] { 127, 0, 0, 1 });
        Assert.IsTrue(IPAddress.IsLoopback(ip));
    }

    [TestMethod]
    public void IPAddressParse()
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        Assert.IsTrue(IPAddress.IsLoopback(ip));
    }

    [TestMethod]
    public void IPAddressIPv6Any()
    {
        IPAddress ip = IPAddress.IPv6Any;
        Assert.AreEqual("::", ip.ToString());
        // TODO: IPV6 주소인지 어떻게 암?
    }

    [TestMethod]
    public void IPAddressIPv6Loopback()
    {
        IPAddress ip = IPAddress.IPv6Loopback;
        Assert.AreEqual("::1", ip.ToString());
    }

    [TestMethod]
    public void IPAddressAny()
    {
        IPAddress any = IPAddress.Any;
        Assert.AreEqual(AddressFamily.InterNetwork, any.AddressFamily);
        Assert.AreEqual("0.0.0.0", any.ToString());
    }

    [TestMethod]
    public void IPAddressLoopback()
    {
        IPAddress ip = IPAddress.Loopback;
        Assert.AreEqual(AddressFamily.InterNetwork, ip.AddressFamily);
        Assert.AreEqual("127.0.0.1", ip.ToString());
    }
}
