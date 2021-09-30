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
            
            //mockPingerManager.Setup();
            /*
            PingerManager pingerManager = new PingerManager(mockHost.Object);
            pingerManager.SetLogger(mockLogger.Object);
            pingerManager.StartPing();
            */
            var pinger = mockPingerManager.Object;
            pinger.StartPing();

            mockPingerManager.Verify(pm => pm.StartPing(), Times.Once());
        }
        
        [Fact]
        public void StopPingTest()
        {
            
            var mockHost = new Mock<List<PingHost>>();
            var mockLogger = new Mock<ILogger>();
            var mockPingerManager = new Mock<IPingerManager>();
            
            //mockPingerManager.Setup(pm => pm.StopPing());
            /*
            PingerManager pingerManager = new PingerManager(mockHost.Object);
            pingerManager.SetLogger(mockLogger.Object);
            pingerManager.StopPing();
            */
            var pinger = mockPingerManager.Object;
            pinger.StopPing();
            
            mockPingerManager.Verify(pm => pm.StopPing(),Times.Once);
        }
        
        
    }
}