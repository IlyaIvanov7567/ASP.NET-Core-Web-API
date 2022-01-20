using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsManagerTests
{
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController _controller;
        private Mock<ILogger<HddMetricsController>> _loggerMock;

        public HddMetricsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<HddMetricsController>>();
            _controller = new HddMetricsController(_loggerMock.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = _controller.GetMetricsFromAgent(agentId, fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = _controller.GetMetricsFromAllCluster(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}

