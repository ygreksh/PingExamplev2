using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Xunit;

namespace PingExample.Tests
{
    public class ConfigManagerTests
    {
        [Fact]
        public void Read_Config_Test()
        {
            //Arrange
            string testFileName = "configreadtest.json";
            string path = Path.Combine(Directory.GetCurrentDirectory(), testFileName);
            PingHost[] expectedHosts = new []{new PingHost(){Host = "127.0.0.1",Period = 2000, PingProtocol = PingProtocol.ICMP}};
            PingHost[] actualHosts;
            //Act
            using (FileStream fileStream = new FileStream(path,FileMode.Open))
            {
                byte[] array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
                string serializedConfig = System.Text.Encoding.Default.GetString(array);
                actualHosts = JsonConvert.DeserializeObject<PingHost[]>(serializedConfig);
            }
            //Assert
            Assert.Equal(expectedHosts,actualHosts);
        }
        [Fact]
        public void Write_Config_Test()
        {
            //Arrange
            string testFileName = "configwritetest.json";
            string path = Path.Combine(Directory.GetCurrentDirectory(), testFileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            PingHost[] expectedHosts =
            {
                new PingHost(){Host = "192.168.1.1",Period = 5000, PingProtocol = PingProtocol.HTTP, StatusCode = HttpStatusCode.OK},
                new PingHost(){Host = "8.8.8.8",Period = 5000, PingProtocol = PingProtocol.TCP, Port = 53}
            };
            PingHost[] actualHosts = {
                new PingHost(){Host = "192.168.1.1",Period = 5000, PingProtocol = PingProtocol.HTTP, StatusCode = HttpStatusCode.OK},
                new PingHost(){Host = "8.8.8.8",Period = 5000, PingProtocol = PingProtocol.TCP, Port = 53}
            };
            //Act
            using (FileStream fileStream = new FileStream(path,FileMode.CreateNew))
            {
                string serializedConfig = JsonConvert.SerializeObject(actualHosts);
                byte[] array = System.Text.Encoding.Default.GetBytes(serializedConfig);
                fileStream.Write(array, 0, array.Length);
            }

            actualHosts = null;
            using (FileStream fileStream = new FileStream(path,FileMode.Open))
            {
                byte[] array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
                string serializedConfig = System.Text.Encoding.Default.GetString(array);
                actualHosts = JsonConvert.DeserializeObject<PingHost[]>(serializedConfig);
            }
            //Assert
            Assert.Equal(expectedHosts,actualHosts);
        }
    }
}