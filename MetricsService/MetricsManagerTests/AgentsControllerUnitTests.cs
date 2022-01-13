using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController controller;

        public AgentsControllerUnitTests()
        {
            controller = new AgentsController();
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Arrange
            var AgentInfo = new AgentInfo();

            //Act
            var result = controller.RegisterAgent(AgentInfo);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            //Arrange
            var AgentInfo = new AgentInfo();

            //Act
            var result = controller.EnableAgentById(AgentInfo.AgentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            //Arrange
            var AgentInfo = new AgentInfo();

            //Act
            var result = controller.DisableAgentById(AgentInfo.AgentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

    }
}

