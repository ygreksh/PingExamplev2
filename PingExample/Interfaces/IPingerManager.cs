using PingExample.Interfaces;

namespace PingExample
{
    public interface IPingerManager
    {
        public void StartPing();
        public void StopPing();
        public void SetLogger(ILogger logger);
    }
}