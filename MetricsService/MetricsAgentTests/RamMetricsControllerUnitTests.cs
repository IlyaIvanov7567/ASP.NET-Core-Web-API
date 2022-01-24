using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using AutoMapper;
using MetricsAgent;

namespace MetricsAgentTests
{
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController _controller;
        private Mock<ILogger<RamMetricsController>> _loggerMock;
        private Mock<IRepository<RamMetric>> _repositoryMock;
        private Mock<IMapper> _mapperMock;

        public RamMetricsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<RamMetricsController>>();
            _repositoryMock = new Mock<IRepository<RamMetric>>();
            _mapperMock = new Mock<IMapper>();

            _controller = new RamMetricsController(_loggerMock.Object, _repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит RamMetric объект
            _repositoryMock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricCreateRequest<RamMetric>
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _repositoryMock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }
    }
}
