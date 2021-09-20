using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PingExample
{
    class Program
    {
        // args[0] can be an IPaddress or host name.
        public static async Task Main (string[] args)
        {
            
            string remoteHost;
            int portNumber;
            
            //remoteIpAddress = args[0];
            remoteHost = "ya.ru";
            portNumber = 80;
            PingHost pingHost = new PingHost();
            pingHost.Host = remoteHost;
            pingHost.Port = portNumber;
            
            //  классы
            var icmpPinger = new IcmpPinger(pingHost);
            icmpPinger.Start();
            var httpPinger = new HttpPinger();
            httpPinger.Start(pingHost);
            //var tcpPinger = new TcpPinger();
            //tcpPinger.Start(remoteHost,portNumber);
            
            

            //  методы
            //IcmpPinger(remoteHost);
            //await HttpPinger(remoteHost);
            //TcpPinger(remoteHost, portNumber);
            
            //Task.WaitAll();
            Console.ReadKey();
        }

        /*
        public static void IcmpPinger(string _remoteHost)
        {
            DateTime now = DateTime.Now;
            try
            {
                Ping pingSender = new Ping ();
                PingOptions options = new PingOptions ();
                options.DontFragment = true;
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes (data);
                int timeout = 120;
                PingReply reply = pingSender.Send (_remoteHost, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    //Console.WriteLine ($"{now.ToString("yyyy/MM/dd hh:mm:ss")} {reply.Address.ToString ()} time={reply.RoundtripTime} TTL={reply.Options.Ttl}");
                    Console.WriteLine ($"{now.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {_remoteHost} ({reply.Address.ToString ()}) success");
                }
                else
                {
                    Console.WriteLine ($"{now.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {_remoteHost} ({reply.Address.ToString ()}) fail");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine ($"{now.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {_remoteHost} fail");
            }
        }

        public static void TcpPinger(string _remoteHost, string _portNumber)
        {
            IPAddress Ip = null;
            IPEndPoint RemoteEndPoint;
            DateTime now = DateTime.Now;

            try
            {
                if (!IPAddress.TryParse(_remoteHost, out Ip))
                {
                    Ip = Dns.GetHostAddresses(_remoteHost).First();
                }

                int Port = Int32.Parse(_portNumber);

                RemoteEndPoint = new IPEndPoint(Ip, Port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Blocking = true;
                socket.Connect(RemoteEndPoint);
                if (socket.Connected)
                {
                    Console.WriteLine(
                        $"{now.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {_remoteHost}:{Port} ({Ip}:{Port}) success");
                    socket.Close();
                }
                else
                {
                    Console.WriteLine(
                        $"{now.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {_remoteHost}:{Port} ({Ip}:{Port}) fail");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} TCP Connect to {_remoteHost} fail");
            }
            
        }

        public static async Task HttpPinger(string _remoteHost)
        {
            IPAddress Ip = null;
            HttpResponseMessage responseMessage;
            //TeamResponse teamResponse;
            using (HttpClient client = new HttpClient())
            {
                DateTime now = DateTime.Now;
                try
                {
                    if (!IPAddress.TryParse(_remoteHost, out Ip))
                    {
                        Ip = Dns.GetHostAddresses(_remoteHost).First();
                    }
                    responseMessage = await client.GetAsync("http://" + _remoteHost);
                    //responseMessage.EnsureSuccessStatusCode();
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {_remoteHost} ({Ip}) success");
                    }
                    else
                    {
                        Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {_remoteHost} ({Ip}) fail");
                    }
    
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {_remoteHost} fail");
                    //throw;
                }
            }
        }
        */
    }
}