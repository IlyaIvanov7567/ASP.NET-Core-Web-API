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
    public class RamMetricsControllerUnitTests
    {
        private readonly RamMetricsController _controller;
        private readonly Mock<ILogger<RamMetricsController>> _loggerMock;
        private readonly Mock<IHttpClientFactory> _clientFactoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepository<RamMetric>> _repositoryMock;
        private readonly Mock<IMetricsAgentClient> _agenClientMock;

        public RamMetricsControllerUnitTests()
        {
            _agenClientMock = new Mock<IMetricsAgentClient>();
            _repositoryMock = new Mock<IRepository<RamMetric>>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<RamMetricsController>>();
            _controller = new RamMetricsController(
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