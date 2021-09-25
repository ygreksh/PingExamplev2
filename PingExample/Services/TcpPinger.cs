using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using PingExample.Interfaces;

namespace PingExample
{
    public class TcpPinger : IPinger
    {
        private IPAddress Ip;
        private int Port;
        private IPEndPoint RemoteEndPoint;
        private string Host;
        private ILogger _logger;

        public TcpPinger(PingHost pingHost)
        {
            Host = pingHost.Host;
            Port = pingHost.Port;
            if (!IPAddress.TryParse(Host, out Ip))
            {
                Ip = Dns.GetHostAddresses(Host).First();
            }
            RemoteEndPoint = new IPEndPoint(Ip, Port);
        }
        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }        
        public void Start()
        {
            DateTime now = DateTime.Now;

            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Blocking = true;
                socket.Connect(RemoteEndPoint);
                if (socket.Connected)
                {
                    Console.WriteLine(
                        $"{now.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {Host}:{Port} ({Ip}:{Port}) success");
                    _logger.WriteLog($"{now.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {Host}:{Port} ({Ip}:{Port}) success");
                    socket.Close();
                }
                else
                {
                    Console.WriteLine(
                        $"{now.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {Host}:{Port} ({Ip}:{Port}) fail");
                    _logger.WriteLog($"{now.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {Host}:{Port} ({Ip}:{Port}) fail");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {Host} error");
                _logger.WriteLog($"{now.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {Host} error");
            }
        }
    }
}