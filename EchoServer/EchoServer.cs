using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoServer;

internal class EchoServer
{
    static void Main(string[] args)
    {
        using Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        serverSocket.Bind(new IPEndPoint(IPAddress.Any, 13000));

        Console.WriteLine($"Server Start");

        while (true)
        {
            try
            {
                while (true)
                {
                    EndPoint clientEndPoint = new IPEndPoint(IPAddress.None, 0);
                    byte[] receiveBuffer = new byte[1024];
                    int receivedByteCount = serverSocket.ReceiveFrom(receiveBuffer, ref clientEndPoint);
                    serverSocket.SendTo(receiveBuffer, receivedByteCount, SocketFlags.None, clientEndPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }

        }
    }
}
