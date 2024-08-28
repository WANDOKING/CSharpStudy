using System.Net;

namespace TestBCL.Network;

[TestClass]
public class TestDns
{
    [TestMethod]
    public void GetMyIpAddress()
    {
        IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ipAddress in hostEntry.AddressList)
        {
            Console.WriteLine($"{ipAddress.AddressFamily}: {ipAddress}");
        }
    }
}
