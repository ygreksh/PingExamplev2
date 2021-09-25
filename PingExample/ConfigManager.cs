using System.IO;
using Newtonsoft.Json;

namespace PingExample
{
    public class ConfigManager : IConfigManager
    {
        public PingHost[] Hosts;

        public void Read(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            using (FileStream fileStream = new FileStream(path,FileMode.Open))
            {
                byte[] array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
                string serializedConfig = System.Text.Encoding.Default.GetString(array);
                Hosts = JsonConvert.DeserializeObject<PingHost[]>(serializedConfig);
            }
        }

        public void Write(string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream fileStream = new FileStream(path,FileMode.CreateNew))
            {
                string serializedConfig = JsonConvert.SerializeObject(Hosts);
                byte[] array = System.Text.Encoding.Default.GetBytes(serializedConfig);
                fileStream.Write(array, 0, array.Length);
            }
        }
    }
}