using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Models;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IRepository<DotNetMetric> _repository;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IRepository<DotNetMetric> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] MetricCreateRequest<DotNetMetric> request)
        {
            _repository.Create(new DotNetMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            _logger.LogInformation("Time: {0}; Value: {1}", request.Time, request.Value);
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new AllMetricsResponse<DotNetMetric>()
            {
                Metrics = new List<MetricDto<DotNetMetric>>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new MetricDto<DotNetMetric> { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
