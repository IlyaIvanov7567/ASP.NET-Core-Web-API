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
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController _controller;
        private Mock<ILogger<HddMetricsController>> _loggerMock;
        private Mock<IRepository<HddMetric>> _repositoryMock;

        public HddMetricsControllerUnitTests()
        {
            _loggerMock = new Mock<ILogger<HddMetricsController>>();
            _repositoryMock = new Mock<IRepository<HddMetric>>();
            _controller = new HddMetricsController(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит HddMetric объект
            _repositoryMock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricsAgent.Requests.MetricCreateRequest<HddMetric>
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _repositoryMock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());

        }
    }
}
