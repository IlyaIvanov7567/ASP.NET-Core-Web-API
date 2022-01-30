using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsManagerTests
{
    public class NetworkMetricsControllerUnitTests
    {
        private readonly NetworkMetricsController _controller;
        private readonly Mock<ILogger<NetworkMetricsController>> _loggerMock;

        public NetworkMetricsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<NetworkMetricsController>>();
            _controller = new NetworkMetricsController(_loggerMock.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = DateTime.Now.Ticks;
            var toTime = DateTime.Now.Ticks + 100;

            //Act
            var result = _controller.GetMetricsFromAgent(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}