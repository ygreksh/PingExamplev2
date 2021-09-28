using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using PingExample.Interfaces;

namespace PingExample
{
    public class IcmpPinger : IPinger
    {
        private string Host;
        private ILogger _logger;
        private PingReply previousReply;
        private PingReply currentReply;
        private bool changedStatus = false;
        private bool started = false;
        private DateTime previousDateTime;
        private DateTime currentDateTime;
        private int Period;
        public IcmpPinger(PingHost pingHost)
        {
            Host = pingHost.Host;
            Period = pingHost.Period;
        }
        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }
        public void Start()
        {
            while(true){
                currentDateTime = DateTime.Now;
                try
                {
                    Ping pingSender = new Ping();
                    PingOptions options = new PingOptions();
                    options.DontFragment = true;
                    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    int timeout = 120;
                    currentReply = pingSender.Send(Host, timeout, buffer, options);
                    if (!started)
                    {
                        previousDateTime = currentDateTime;
                        previousReply = currentReply;
                    }

                    if (previousReply.Status != currentReply.Status)
                    {
                        changedStatus = true;
                    }
                    else
                    {
                        changedStatus = false;
                    }

                    if (changedStatus)
                    {
                        //Console.WriteLine ($"{now.ToString("yyyy/MM/dd hh:mm:ss")} {reply.Address.ToString ()} time={reply.RoundtripTime} TTL={reply.Options.Ttl}");
                        Console.WriteLine(
                            $"{previousDateTime.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} ({previousReply.Address.ToString()}) success");
                        Console.WriteLine(
                            $"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} ({currentReply.Address.ToString()}) success");
                        _logger.WriteLog(
                            $"{previousDateTime.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} ({previousReply.Address.ToString()}) success");
                        _logger.WriteLog(
                            $"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} ({currentReply.Address.ToString()}) success");
                    }

                    /*
                    else
                    {
                        Console.WriteLine ($"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} ({currentReply.Address.ToString ()}) fail");
                        _logger.WriteLog($"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} ({currentReply.Address.ToString ()}) fail");
                    }
                    */
                    previousReply = currentReply;
                    previousDateTime = currentDateTime;
                }
                catch (Exception e)
                {
                    Console.WriteLine ($"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} error");
                    _logger.WriteLog($"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} ICMP connect to {Host} error");
                }
                Thread.Sleep(Period);
            }
            
        }
    }
}