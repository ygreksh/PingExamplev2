using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PingExample
{
    public class Pinger
    {
        private IPAddress Ip;
        private int Port;
        public void Init(PingHost pingHost)
        {
            Ip = IPAddress.Parse(pingHost.Host);
            Port = pingHost.Port;
            
        }

        public void Start(PingHost pingHost)
        {
            //IcmpPinger icmpPinger = new IcmpPinger();
            //icmpPinger.Start();
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