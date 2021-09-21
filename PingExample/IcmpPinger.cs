using System;
using System.Net.NetworkInformation;
using System.Text;

namespace PingExample
{
    public class IcmpPinger
    {
        private string Host;
        private PingLogger _logger;
        public IcmpPinger(PingHost pingHost)
        {
            Host = pingHost.Host;
        }

        public void Start()
        {
            DateTime now = DateTime.Now;
            _logger = new PingLogger();
            try
            {
                Ping pingSender = new Ping ();
                PingOptions options = new PingOptions ();
                options.DontFragment = true;
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes (data);
                int timeout = 120;
                PingReply reply = pingSender.Send (Host, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    //Console.WriteLine ($"{now.ToString("yyyy/MM/dd hh:mm:ss")} {reply.Address.ToString ()} time={reply.RoundtripTime} TTL={reply.Options.Ttl}");
                    Console.WriteLine ($"{now.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} ({reply.Address.ToString ()}) success");
                    _logger.WriteLog($"{now.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} ({reply.Address.ToString ()}) success");
                }
                else
                {
                    Console.WriteLine ($"{now.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} ({reply.Address.ToString ()}) fail");
                    _logger.WriteLog($"{now.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} ({reply.Address.ToString ()}) fail");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine ($"{now.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} error");
                _logger.WriteLog($"{now.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} error");
            }
        }
    }
}