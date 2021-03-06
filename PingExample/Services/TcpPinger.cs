using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
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
        private int Period;
        private DateTime previousDateTime;
        private DateTime currentDateTime;
        private bool previousStatus = false;
        private bool currentStatus = false;
        private bool isStarted = false;

        public TcpPinger(PingHost pingHost)
        {
            Host = pingHost.Host;
            Port = pingHost.Port;
            Period = pingHost.Period;
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
            while (true)
            {
                currentDateTime = DateTime.Now;

                try
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    socket.Blocking = true;
                    socket.Connect(RemoteEndPoint);
                    currentStatus = socket.Connected;

                    /*
                    if (!isStarted)
                    {
                        previousStatus = currentStatus;
                        previousDateTime = currentDateTime;
                        isStarted = true;
                    }
                    */
                    if (currentStatus)
                    {
                        socket.Close();
                    }

                }
                catch (Exception e)
                {
                    //Console.WriteLine($"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {Host} error");
                    //Console.WriteLine(e);
                    //_logger.WriteLog($"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {Host} error");
                    currentStatus = false;
                }
                finally
                {
                    if (currentStatus != previousStatus)
                    {
                        Console.WriteLine(
                            $"{previousDateTime.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {Host}:{Port} ({Ip}:{Port}) {previousStatus}");
                        Console.WriteLine(
                            $"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {Host}:{Port} ({Ip}:{Port}) {currentStatus}");
                        _logger.WriteLog($"{previousDateTime.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {Host}:{Port} ({Ip}:{Port}) {previousStatus}");
                        _logger.WriteLog($"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {Host}:{Port} ({Ip}:{Port}) {currentStatus}");
                    }
                    
                    previousDateTime = currentDateTime;
                    previousStatus = currentStatus;    
                }                
                
                Thread.Sleep(Period);
            }
        }
    }
}