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
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _repositoryMock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricsAgent.Requests.MetricCreateRequest<CpuMetric> 
            { 
                Time = TimeSpan.FromSeconds(1), Value = 50 
            });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _repositoryMock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());

        }
    }
}
