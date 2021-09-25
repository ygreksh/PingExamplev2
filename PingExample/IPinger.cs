using PingExample.Interfaces;

namespace PingExample
{
    public interface IPinger
    {
        public void SetLogger(ILogger logger);
        public void Start();
    }
}