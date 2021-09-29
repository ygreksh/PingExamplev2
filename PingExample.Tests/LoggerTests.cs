using Moq;
using PingExample.Interfaces;
using Xunit;

namespace PingExample.Tests
{
    public class LoggerTests
    {
        [Fact]
        public void WriteLogTest()
        {
            var mock = new Mock<ILogger>();
            mock.Setup(l => l.WriteLog(It.IsAny<string>()));
            
            mock.Verify();
        } 
    }
}