using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController _controller;
        private Mock<ILogger<CpuMetricsController>> _loggerMock;
        private Mock<IRepository<CpuMetric>> _repositoryMock;

        public CpuMetricsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<CpuMetricsController>>();
            _repositoryMock = new Mock<IRepository<CpuMetric>>();
            _controller = new CpuMetricsController(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            _repositoryMock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // ��������� �������� �� �����������
            var result = _controller.Create(new MetricsAgent.Requests.MetricCreateRequest<CpuMetric> 
            { 
                Time = TimeSpan.FromSeconds(1), Value = 50 
            });

            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            _repositoryMock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());

        }
    }
}
