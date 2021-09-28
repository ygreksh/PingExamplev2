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
        //private IPAddress Ip { get; set; }
        private string Host;
        private HttpStatusCode _httpStatusCode;
        private ILogger _logger;
        private IPinger _pingerImplementation;
        private HttpStatusCode previousStatus = HttpStatusCode.BadRequest;
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
            HttpResponseMessage responseMessage;
            string FullHost;
            while (true)
            {
                using (HttpClient client = new HttpClient())
                {
                
                    currentDateTime = DateTime.Now;
                    try
                    {
                        /*
                        if (!IPAddress.TryParse(Host, out Ip))
                        {
                            Ip = Dns.GetHostAddresses(Host).First();
                        }
                        */

                        if (!Regex.IsMatch(Host, "^(http|https)://"))
                        {
                            FullHost = "http://" + Host;
                        }
                        else
                        {
                            FullHost = Host;
                        }
                        responseMessage = await client.GetAsync(FullHost);
                        currentStatus = responseMessage.StatusCode;
                        if (!isStarted)
                        {
                            previousDateTime = currentDateTime;
                            isStarted = true;
                        }

                        currentStatus = responseMessage.StatusCode;

                    }
                    catch (Exception e)
                    {
                        /*
                        Console.WriteLine(
                            $"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} error");
                        _logger.WriteLog(
                            $"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} error");
                        */
                        currentStatus = HttpStatusCode.BadRequest;
                    }
                    finally
                    {
                        if (previousStatus != currentStatus &&
                            (previousStatus == _httpStatusCode || currentStatus == _httpStatusCode))
                        {
                            Console.WriteLine(
                                $"{previousDateTime.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} {previousStatus}");
                            Console.WriteLine(
                                $"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} {currentStatus}");
                            
                            _logger.WriteLog(
                                $"{previousDateTime.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} {previousStatus}");
                            _logger.WriteLog(
                                $"{currentDateTime.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} {currentStatus}");
                            
                        } 
                        previousStatus = currentStatus;
                        previousDateTime = currentDateTime;
                    }
                }
                Thread.Sleep(Period);
            }
        }
    }
}