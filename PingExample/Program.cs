using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PingExample.Interfaces;

namespace PingExample
{
    class Program
    {
        // args[0] can be an IPaddress or host name.
        public static async Task Main (string[] args)
        {
            List<PingHost> Hosts = new List<PingHost>();
            ILogger logger = new Logger("logs.txt");
            
            PingHost HostICMP = new PingHost() { Host = "8.8.8.8", Period = 2000, PingProtocol = PingProtocol.ICMP }; 
            PingHost HostHTTP = new PingHost() { Host = "mail.ru", Period = 2000, PingProtocol = PingProtocol.HTTP, StatusCode = HttpStatusCode.OK}; 
            PingHost HostTCP = new PingHost() { Host = "192.168.1.1", Period = 1000, PingProtocol = PingProtocol.TCP, Port = 21};

            Hosts.Add(HostICMP);
            Hosts.Add(HostHTTP);
            Hosts.Add(HostTCP);

            string fileName = "config.json";
            IConfigManager configManager = new ConfigManager();
            configManager.Hosts = Hosts.ToArray();
            configManager.Write(fileName);
            
            configManager.Read(fileName);
            Hosts = configManager.Hosts.ToList();            
            
            IPingerManager pingerManager = new PingerManager(Hosts);
            pingerManager.SetLogger(logger);
            pingerManager.StartPing();
            
            Console.ReadKey();
            
            pingerManager.StopPing();
            
        }
    }
}