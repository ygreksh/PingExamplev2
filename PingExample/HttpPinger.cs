using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace PingExample
{
    public class HttpPinger
    {
        public async void Start(PingHost pingHost)
        {
            string Host = pingHost.Host;
            IPAddress Ip;
            HttpResponseMessage responseMessage;
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
                        Host = "http://" + Host;
                    }
                    responseMessage = await client.GetAsync(Host);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} ({Ip}) success");
                    }
                    else
                    {
                        Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} ({Ip}) fail");
                    }
    
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    Console.WriteLine($"{now.ToString("yyyy/MM/dd hh:mm:ss")} HTTP Connect to {Host} fail");
                    //throw;
                }
            }
        }
    }
}