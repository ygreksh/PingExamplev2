using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace PingExample
{
    public class TcpPinger
    {
        public void Start(string host, string port)
        {
            IPAddress Ip = null;
            IPEndPoint RemoteEndPoint;
            DateTime now = DateTime.Now;

            try
            {
                if (!IPAddress.TryParse(host, out Ip))
                {
                    Ip = Dns.GetHostAddresses(host).First();
                }

                int Port = Int32.Parse(port);

                RemoteEndPoint = new IPEndPoint(Ip, Port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Blocking = true;
                socket.Connect(RemoteEndPoint);
                if (socket.Connected)
                {
                    Console.WriteLine(
                        $"{now.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {host}:{Port} ({Ip}:{Port}) success");
                    socket.Close();
                }
                else
                {
                    Console.WriteLine(
                        $"{now.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {host}:{Port} ({Ip}:{Port}) fail");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {host} fail");
            }
        }
    }
}