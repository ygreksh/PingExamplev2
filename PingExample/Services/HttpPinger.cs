using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using PingExample.Interfaces;

namespace PingExample
{
    public class HttpPinger : IPinger
    {
        private IPAddress Ip { get; set; }
        private string Host;
        private HttpStatusCode _httpStatusCode;
        private ILogger _logger;
        private IPinger _pingerImplementation;

        public HttpPinger(PingHost pingHost)
        {
            Host = pingHost.Host;
            _httpStatusCode = pingHost.StatusCode;
        }
        
        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

       public async void Start()
        {
            IPAddress Ip;
            HttpResponseMessage responseMessage;
            string FullHost;
            using (HttpClient client = new HttpClient())
            {
                DateTime now = DateTime.Now;
                try
                {
                    if (!IPAddress.TryParse(Host, out Ip))
                    {
                        Ip = Dns.GetHostAddresses(Host).First();
                    }

                    if (!Regex.IsMatch(Host,"^(http|https)://"))
                    {
                        FullHost = "http://" + Host;
                    }
                    else
                    {
                        FullHost = Host;
                    }
                    responseMessage = await client.GetAsync(FullHost);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} ({Ip}) success");
                        _logger.WriteLog($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} ({Ip}) success");
                    }
                    else
                    {
                        Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} ({Ip}) fail");
                        _logger.WriteLog($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} ({Ip}) fail");
                    }
    
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} error");
                    _logger.WriteLog($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} error");
                }
            }
        }
    }
}