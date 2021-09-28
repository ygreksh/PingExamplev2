using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PingExample.Interfaces;

namespace PingExample
{
    public class PingerManager : IPingerManager
    {
        private List<PingHost> _hosts;
        private ILogger _logger;
        private CancellationTokenSource cancelTokenSource;
        private CancellationToken token;

        public PingerManager(List<PingHost> hosts)
        {
            _hosts = hosts;
        }
        
        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void StartPing()
        {
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;

            foreach (var pinghost in _hosts)
            {
                
                Task.Run(() =>
                {
                   if (token.IsCancellationRequested)
                   {
                       Console.WriteLine("Canceled");
                       return;
                   }
                   switch (pinghost.PingProtocol)
                   {
                       case PingProtocol.ICMP:
                           IcmpPinger icmpPinger = new IcmpPinger(pinghost);
                           icmpPinger.SetLogger(_logger);
                           icmpPinger.Start();
                           break;
                       case PingProtocol.HTTP:
                           HttpPinger httpPinger = new HttpPinger(pinghost);
                           httpPinger.SetLogger(_logger);
                           httpPinger.Start();
                           break;
                       case PingProtocol.TCP:
                           TcpPinger tcpPinger = new TcpPinger(pinghost);
                           tcpPinger.SetLogger(_logger);
                           tcpPinger.Start();
                           break;
                   }
                });
            }
        }
        public void StopPing()
        {
            cancelTokenSource.Cancel();
        }
    }
}