﻿using System;
using System.Collections.Generic;
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
            List<PingHost> Hosts = new List<PingHost>();
            string remoteHost;
            int portNumber;
            HttpStatusCode httpStatusCode = HttpStatusCode.OK;
            remoteHost = "127.0.0.1";
            portNumber = 80;
            PingHost pingHost = new PingHost();
            pingHost.Host = remoteHost;
            pingHost.Port = portNumber;
            pingHost.StatusCode = httpStatusCode;

            PingHost HostICMP = new PingHost() { Host = "127.0.0.1", Period = 1000, PingProtocol = PingProtocol.ICMP }; 
            PingHost HostHTTP = new PingHost() { Host = "127.0.0.1", Period = 1000, PingProtocol = PingProtocol.HTTP, StatusCode = HttpStatusCode.OK}; 
            PingHost HostTCP = new PingHost() { Host = "127.0.0.1", Period = 1000, PingProtocol = PingProtocol.TCP, Port = 80};

            Hosts.Add(HostICMP);
            Hosts.Add(HostHTTP);
            Hosts.Add(HostTCP);

            string fileName = "config.json";
            ConfigManager configManager = new ConfigManager();
            /*
            configManager.Hosts = Hosts.ToArray();
            configManager.Write(fileName);
            */
            configManager.ReadFromFile(fileName);
            Hosts = configManager.Hosts.ToList();            
            
            foreach (var host in Hosts)
            {
                Pinger pinger = new Pinger(host);
                pinger.Start();
            }
            
            Console.ReadKey();
        }
    }
}