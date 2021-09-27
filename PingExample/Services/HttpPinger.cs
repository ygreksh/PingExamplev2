using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
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
        private HttpStatusCode previousStatus;
        private HttpStatusCode currentStatus;
        private bool changedStatus = false;
        private bool isStarted = false;
        private int Period;
        private DateTime previousDateTime;
        private DateTime currentDateTime;

        public HttpPinger(PingHost pingHost)
        {
            Host = pingHost.Host;
            _httpStatusCode = pingHost.StatusCode;
            Period = pingHost.Period;
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
                
                 currentDateTime = DateTime.Now;
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

                    
                    //первый пинг
                    responseMessage = await client.GetAsync(FullHost);
                    if (!isStarted)
                    {
                        currentStatus = responseMessage.StatusCode;
                        isStarted = true;
                    }
                    previousStatus = responseMessage.StatusCode;
                    if (previousStatus == currentStatus)
                    {
                        previousDateTime = currentDateTime;
                    }
                    
                    
                    if (responseMessage.StatusCode == _httpStatusCode)
                    {
                        Console.WriteLine($"{previousDateTime.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} ({Ip}) success");
                        Console.WriteLine($"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} ({Ip}) success");
                        _logger.WriteLog($"{previousDateTime.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} ({Ip}) success");
                        _logger.WriteLog($"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} ({Ip}) success");
                    }
                    /*
                    else
                    {
                        Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} ({Ip}) fail");
                        _logger.WriteLog($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} ({Ip}) fail");
                    }
                    */
    
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} error");
                    _logger.WriteLog($"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} error");
                }
            }
        }
    }
}