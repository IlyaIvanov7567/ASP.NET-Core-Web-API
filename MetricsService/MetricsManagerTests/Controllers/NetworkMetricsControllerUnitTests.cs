﻿using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using AutoMapper;
using Core.DAL.Models;
using Core.Interfaces;
using MetricsManager.Clients;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsManagerTests
{
    public class NetworkMetricsControllerUnitTests
    {
        private readonly NetworkMetricsController _controller;
        private readonly Mock<ILogger<NetworkMetricsController>> _loggerMock;
        private readonly Mock<IHttpClientFactory> _clientFactoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepository<NetworkMetric>> _repositoryMock;
        private readonly Mock<IMetricsAgentClient> _agenClientMock;

        public NetworkMetricsControllerUnitTests()
        {
            _agenClientMock = new Mock<IMetricsAgentClient>();
            _repositoryMock = new Mock<IRepository<NetworkMetric>>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<NetworkMetricsController>>();
            _controller = new NetworkMetricsController(
                _loggerMock.Object, 
                _mapperMock.Object, 
                _repositoryMock.Object,
                _agenClientMock.Object);
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