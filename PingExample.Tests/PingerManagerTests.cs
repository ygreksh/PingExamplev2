using System.Collections.Generic;
using Moq;
using PingExample.Interfaces;
using Xunit;

namespace PingExample.Tests
{
    public class PingerManagerTests
    {
        [Fact]
        public void StartPingTest()
        {
            
            var mockHost = new Mock<List<PingHost>>();
            var mockLogger = new Mock<ILogger>();
            
            PingerManager pingerManager = new PingerManager(mockHost.Object);
            pingerManager.SetLogger(mockLogger.Object);
            pingerManager.StartPing();
            
            mockLogger.Verify();
        }
        
        public void StopPingTest()
        {
            
            var mockHost = new Mock<List<PingHost>>();
            var mockLogger = new Mock<ILogger>();
            
            PingerManager pingerManager = new PingerManager(mockHost.Object);
            pingerManager.SetLogger(mockLogger.Object);
            pingerManager.StopPing();
            
            mockLogger.Verify();
        }
        
        
    }
}