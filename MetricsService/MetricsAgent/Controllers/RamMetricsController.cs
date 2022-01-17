using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Models;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRepository<RamMetric> _repository;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRepository<RamMetric> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] MetricCreateRequest<RamMetric> request)
        {
            _repository.Create(new RamMetric
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

            var response = new AllMetricsResponse<RamMetric>()
            {
                Metrics = new List<MetricDto<RamMetric>>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new MetricDto<RamMetric> { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
