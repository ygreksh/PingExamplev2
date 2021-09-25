namespace PingExample
{
    public interface IConfigManager
    {
        public void Read(string fileName);
        public void Write(string fileName);   
    }
}