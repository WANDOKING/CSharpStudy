using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoClient;

internal class EchoClient
{
    static void Main(string[] args)
    {
        using Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        EndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 13000);

        while (true)
        {
            string input = Console.ReadLine()!;

            Console.WriteLine($"Send | Length = {input.Length}, {input}");
            
            clientSocket.SendTo(Encoding.UTF8.GetBytes(input), serverEndPoint);

            byte[] receiveBuffer = new byte[1024];

            EndPoint receivedEndPoint = new IPEndPoint(IPAddress.None, 0);
            int receivedByteCount = clientSocket.ReceiveFrom(receiveBuffer, ref receivedEndPoint);

            string receivedString = Encoding.UTF8.GetString(receiveBuffer, 0, receivedByteCount);

            Console.WriteLine($"Receive | Length = {receivedString.Length}, {receivedString}");
        }
    }
}
