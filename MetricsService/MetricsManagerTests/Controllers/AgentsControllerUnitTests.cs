using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using MetricsManager;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController _controller;
        private Mock<ILogger<AgentsController>> _loggerMock;

        public AgentsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<AgentsController>>();
            _controller = new AgentsController(_loggerMock.Object);
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Arrange
            var AgentInfo = new AgentInfo();

            //Act
            var result = _controller.RegisterAgent(AgentInfo);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            //Arrange
            var AgentInfo = new AgentInfo();

            //Act
            var result = _controller.EnableAgentById(AgentInfo.AgentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            //Arrange
            var AgentInfo = new AgentInfo();

            //Act
            var result = _controller.DisableAgentById(AgentInfo.AgentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

    }
}

