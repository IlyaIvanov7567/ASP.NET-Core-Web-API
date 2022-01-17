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
    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsController _controller;
        private Mock<ILogger<NetworkMetricsController>> _loggerMock;
        private Mock<IRepository<NetworkMetric>> _repositoryMock;

        public NetworkMetricsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<NetworkMetricsController>>();
            _repositoryMock = new Mock<IRepository<NetworkMetric>>();
            _controller = new NetworkMetricsController(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит NetworkMetric объект
            _repositoryMock.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricsAgent.Requests.MetricCreateRequest<NetworkMetric>
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _repositoryMock.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());

        }
    }
}
