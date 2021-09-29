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
            var mockPingerManager = new Mock<IPingerManager>();
            
            mockPingerManager.Setup(pm => pm.StartPing());
            /*
            PingerManager pingerManager = new PingerManager(mockHost.Object);
            pingerManager.SetLogger(mockLogger.Object);
            pingerManager.StartPing();
            */

            mockPingerManager.Verify();
        }
        
        [Fact]
        public void StopPingTest()
        {
            
            var mockHost = new Mock<List<PingHost>>();
            var mockLogger = new Mock<ILogger>();
            var mockPingerManager = new Mock<IPingerManager>();
            
            mockPingerManager.Setup(pm => pm.StopPing());
            /*
            PingerManager pingerManager = new PingerManager(mockHost.Object);
            pingerManager.SetLogger(mockLogger.Object);
            pingerManager.StopPing();
            */
            
            mockPingerManager.Verify();
        }
        
        
    }
}