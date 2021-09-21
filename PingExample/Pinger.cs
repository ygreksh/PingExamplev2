using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PingExample
{
    public class Pinger
    {
        private PingHost _pingHost;

        public Pinger(PingHost pingHost)
        {
            _pingHost = pingHost;
        }

        
        public void Start()
        {
            //IcmpPinger icmpPinger = new IcmpPinger();
            //icmpPinger.Start();
            switch (_pingHost.PingProtocol)
            {
                case PingProtocol.ICMP:
                    IcmpPinger icmpPinger = new IcmpPinger(_pingHost);
                    icmpPinger.Start();
                    break;
                case PingProtocol.HTTP:
                    HttpPinger httpPinger = new HttpPinger(_pingHost);
                    httpPinger.Start();
                    break;
                case PingProtocol.TCP:
                    TcpPinger tcpPinger = new TcpPinger(_pingHost);
                    tcpPinger.Start();
                    break;
            }
        }

        public Task StartAsinc(CancellationToken cancellationToken)
        {
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}