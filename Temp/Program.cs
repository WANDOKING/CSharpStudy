using System.Net;
using System.Text;

namespace Temp;

internal class Program
{
    private static void Main(string[] args)
    {
        HttpClient client = new HttpClient();
        string text = client.GetStringAsync("http://www.naver.com:80").Result;
        Console.WriteLine(text);

        WebClient wc = new WebClient();
        string 
    }
}