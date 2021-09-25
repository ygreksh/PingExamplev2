using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PingExample.Interfaces;

namespace PingExample
{
    public class Pinger : IPinger
    {
        private List<PingHost> _hosts;
        private ILogger _logger;
        private CancellationTokenSource cancelTokenSource;
        private CancellationToken token;

        public Pinger(List<PingHost> hosts)
        {
            _hosts = hosts;
        }


        
        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Start()
        {
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;

            foreach (var pinghost in _hosts)
            {
                
                Task.Run(() =>
                {
                    while (true)
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
                        Thread.Sleep(pinghost.Period);
                    }
                });
                
                
            }
        }
        public void Stop()
        {
            cancelTokenSource.Cancel();
        }

    }
}