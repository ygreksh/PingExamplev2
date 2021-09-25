namespace PingExample
{
    public interface IConfigManager
    {
        public PingHost[] Hosts { get; set;}
        public void Read(string fileName);
        public void Write(string fileName);   
    }
}