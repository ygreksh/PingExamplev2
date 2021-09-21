using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PingExample
{
    public class Pinger
    {
        private PingHost _pingHost;
        private PingLogger _logger;

        public Pinger(PingHost pingHost)
        {
            _pingHost = pingHost;
        }

        public void SetLogger(PingLogger pingLogger)
        {
            _logger = pingLogger;
        }
        
        public void Start()
        {
            switch (_pingHost.PingProtocol)
            {
                case PingProtocol.ICMP:
                    IcmpPinger icmpPinger = new IcmpPinger(_pingHost);
                    icmpPinger.SetLogger(_logger);
                    icmpPinger.Start();
                    break;
                case PingProtocol.HTTP:
                    HttpPinger httpPinger = new HttpPinger(_pingHost);
                    httpPinger.SetLogger(_logger);
                    httpPinger.Start();
                    break;
                case PingProtocol.TCP:
                    TcpPinger tcpPinger = new TcpPinger(_pingHost);
                    tcpPinger.SetLogger(_logger);
                    tcpPinger.Start();
                    break;
            }
        }

    }
}